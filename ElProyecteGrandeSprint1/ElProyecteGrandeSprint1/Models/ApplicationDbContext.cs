﻿using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using ElProyecteGrandeSprint1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ElProyecteGrandeSprint1.Models
{
    public class ApplicationDbContext : DbContext
    {
        private byte[] secret = Encoding.ASCII.GetBytes("MY_SECRET_KEY_dasmd.-dDUNJUOFAOD");
        private readonly EmailSender _emailSender = new EmailSender();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<UserToken> JWTTokens { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<EmailGuid> EmailGuid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(x => x.Password).HasConversion(t => t.UserPassword, s => new Password(s));
        }

        public async Task<User> GetUserDataFromDataBase(long id) => Users.ToListAsync().Result.First(x => x.ID == id);


        public async Task<List<User>> GetAllUserDataFromDataBase() => await Users.ToListAsync();


        public async Task<string> RegisterUser(RegisterUser user)
        {
            if (!ValidateUsername(user.UserName))
            {
                return JsonSerializer.Serialize("This Username is already taken");
            }
            if(!ValidateEmail(user.Email)) return JsonSerializer.Serialize("This Email is already in use!");
            if (ValidatePassword(user) == "accepted")
            {
                User registerUser = new User()
                {
                    UserName = user.UserName,
                    Password = new Password() { UserPassword = user.Password },
                    Email = user.Email,
                    Roles = Roles.Where(r => r.Name == "User").ToHashSet(),
                    Reputation = 0
                };
                Users.Add(registerUser);
                await SaveChangesAsync();

                _emailSender.SendConfirmationEmail(user.UserName, user.Email, "registration", Guid.Empty);
                return JsonSerializer.Serialize("Registered Successfully");
            }
            return JsonSerializer.Serialize(ValidatePassword(user));
        }

        private bool ValidateEmail(string userEmail)
        {
            return Enumerable.All(Users, dbUser => dbUser.Email != userEmail);
        }

        public async Task<string> DeleteUser(int id)
        {
            try
            {
                var deletableUser = GetUserDataFromDataBase(id);
                Users.Remove(deletableUser.Result);
                await SaveChangesAsync();
                return JsonSerializer.Serialize("Your profile was deleted successfully");
            }
            catch (Exception e)
            {

                return JsonSerializer.Serialize(e.Message);
            }
        }

        private bool ValidateUsername(string userName)
        {
            foreach (var dbUser in Users)
            {
                if (dbUser.UserName == userName)
                {
                    return false;
                }
            }
            return true;
        }

        private string ValidatePassword(RegisterUser user)
        {
            if (user.Password.Length < 5)
            {
                return "The password must be longer than 5 characters";
            }
            else if (user.Password.ToLower().Equals(user.Password))
            {
                return "The password must contain minimum 1 upper letter";
            }
            else
            {
                return "accepted";
            }
        }

        public async Task<string> ChangeUserProfile(long id, RegisterUser user, Guid guid)
        {
            var saveOutUser = GetUserDataFromDataBase(id);
            try
            {
                if (user.UserName.Length > 0 && ValidateUsername(user.UserName))
                {
                    saveOutUser.Result.UserName = user.UserName;
                }
                if (ValidatePassword(user) == "accepted")
                {
                    saveOutUser.Result.Password = new Password() { UserPassword = user.Password };

                }else
                {
                    return JsonSerializer.Serialize("Your profile was not Changed successfully");
                }
                await SaveChangesAsync();
                return JsonSerializer.Serialize("Your profile was Changed successfully");
            }
            catch (Exception e)
            {

                return JsonSerializer.Serialize(e.Message);
            }
        }


        public async Task<User> GetUserByName(string Username)
        {
            return Users.Include(u => u.Roles).ToListAsync().Result.First(x => x.UserName == Username);
        }


        public async Task<User> GetUserByEmail(string email)
        {
            return Users.Include(u => u.Roles).ToListAsync().Result.First(x => x.Email == email);
        }
        public async Task<string> Login(LoginUser user)
        {
            try
            {
                if(await ValidateLogin(user))
                {
                    var searchedUser = await GetUserByName(user.UserName);
                    var rolesList = searchedUser.Roles.Select(role => role.Name).ToList();
                    return JsonSerializer.Serialize(new ValidatedUser(){
                        UserName = searchedUser.UserName,
                        Email = searchedUser.Email,
                        Roles = rolesList,
                        Reputation = searchedUser.Reputation,
                        AccessToken = await JWTTokenGenerator(searchedUser.Email, searchedUser.UserName, searchedUser.ID)
                    });
                }
                return JsonSerializer.Serialize("false");
            }
            catch (Exception)
            {
                return JsonSerializer.Serialize("false");
            }
        }

        public async Task<bool> ValidateEmailForPassword(string email)
        {
            try
            {
                foreach (var dbUser in Users)
                {
                    if (dbUser.Email == email)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ValidateLogin(LoginUser user)
        {
            try
            {
                User validateUser = await GetUserByName(user.UserName);
                if (!validateUser.Password.ValidatePassword(user.Password)) return false;
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // private string makeJWTToken()
        // {
        //     JWTHeader header = new JWTHeader();
        //     JWTPayload payload = new JWTPayload();
        //     string encodedHeader = Base64UrlTextEncoder.Encode(SerializeObject(header));
        //     string encodedPayload = Base64UrlTextEncoder.Encode(SerializeObject(payload));
        //     string data = encodedHeader + '.' + encodedPayload;
        //     string hashedData = Hash(data, secret);
        //     string signature = Base64UrlTextEncoder.Encode(hashedData);
        //     string JWTToken = encodedHeader + '.' + encodedPayload + "." + signature;
        //     return JWTToken;
        // }

        // private byte[] ToByteArray(object source)
        // {
        //     var formatter = new BinaryFormatter();
        //     using (var stream = new MemoryStream())
        //     {
        //         formatter.Serialize(stream, source);                
        //         return stream.ToArray();
        //     }
        // }
        // private byte[] SerializeObject(object value) => Encoding.UTF8.GetBytes(JsonSerializer.Serialize((value)));


        private async Task<string> JWTTokenGenerator(string email, string userName, long userId){
            var now = DateTime.UtcNow;
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", "customer") }),
                Issuer = "Who issued the token",
                Claims = new Dictionary<string, object>
                {
                    ["userName"] = userName,
                    ["userId"] = userId,
                    ["email"] = email, 
                },
                IssuedAt = now,
                NotBefore = now,
                Expires = now + TimeSpan.FromDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);

            await SaveTokenToDatabase(serializedToken);
            return serializedToken;
        }

        private async Task SaveTokenToDatabase(string serializedToken)
        {
            JWTTokens.Add(
                new UserToken
                {
                    Token = serializedToken,
                    Date = DateTime.Now
                }
            );
            await SaveChangesAsync();
        }


        //public Task<String> GetArticles()
        //{
        //    return Task.FromResult(JsonSerializer.Serialize(Articles));
        //}

        public async Task<List<Article>> GetArticles() => await Articles.ToListAsync();
        
        
        public void SendForgotPasswordEmail(string email)
        public async void SendForgotPasswordEmail(string email, Guid guid)
        {
            User user = GetUserByEmail(email).Result;
            EmailGuid.Add(
                new EmailGuid 
                    {
                        Email = email, 
                        Guid = guid

                    }
                ); 
            SaveChangesAsync();
            _emailSender.SendConfirmationEmail(user.UserName, email, "forgor", guid);
        }

        public  EmailGuid getEmailFromGuid(Guid emailId)
        {
            return  EmailGuid.ToList().First(x => x.Guid == emailId);
        }

        public void SendSuccesfulPasswordChangeEmail(Guid guid)
        {
            var email =getEmailFromGuid(guid).Email;
            var deleteEmailGuid = getEmailFromGuid(guid);
            EmailGuid.Remove(deleteEmailGuid);
            SaveChanges();
            var user = GetUserByEmail(email).Result;
            _emailSender.SendConfirmationEmail(user.UserName, email, "success", guid);
        }


    }
}
