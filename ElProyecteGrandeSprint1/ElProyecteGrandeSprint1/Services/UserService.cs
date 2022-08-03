using System.Text.Json;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace ElProyecteGrandeSprint1.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private UserServiceHelper _contextHelper;
        private EmailSender _emailSender;

        public UserService(ApplicationDbContext context, UserServiceHelper userServiceHelper, EmailSender emailSender)
        {
            _context = context;
            _contextHelper = userServiceHelper;
            _emailSender = emailSender;
        }


        public Task<User> GetUserDataFromDataBase(long id) => Task.FromResult(_context.Users.Include(u => u.Roles).ToListAsync().Result.First(x => x.ID == id));

        public async Task<List<User>> GetAllUserDataFromDataBase() => await _context.Users.ToListAsync();

        public async Task<string> RegisterUser(RegisterUser user)
        {
            if (!_contextHelper.ValidateUsername(user.UserName, _context.Users))
            {
                return JsonSerializer.Serialize("This Username is already taken");
            }
            if (ValidateEmailForPassword(user.Email).Result) return JsonSerializer.Serialize("This Email is already in use!");
            if (_contextHelper.ValidatePassword(user) == "accepted")
            {
                User registerUser = new User()
                {
                    UserName = user.UserName,
                    Password = new Password() { UserPassword = user.Password },
                    Email = user.Email,
                    Roles = _context.Roles.Where(r => r.Name == "User").ToHashSet(),
                    Reputation = 0
                };
                _context.Users.Add(registerUser);
                await _context.SaveChangesAsync();

                _emailSender.SendConfirmationEmail(user.UserName, user.Email, "registration", Guid.Empty);
                return JsonSerializer.Serialize("Registered Successfully");
            }
            return JsonSerializer.Serialize(_contextHelper.ValidatePassword(user));
        }

        public async Task<string> DeleteUser(int id)
        {
            try
            {
                var deletableUser = GetUserDataFromDataBase(id);
                _context.Users.Remove(deletableUser.Result);
                await _context.SaveChangesAsync();
                return JsonSerializer.Serialize("Your profile was deleted successfully");
            }
            catch (Exception e)
            {

                return JsonSerializer.Serialize(e.Message);
            }
        }

        public async Task<string> ChangeUserProfile(long id, RegisterUser user, Guid guid)
        {
            try
            {
                var saveOutUser = GetUserDataFromDataBase(id);
                if (user.UserName.Length > 0 && _contextHelper.ValidateUsername(user.UserName, _context.Users))
                {
                    saveOutUser.Result.UserName = user.UserName;
                }
                if (_contextHelper.ValidatePassword(user) == "accepted")
                {
                    saveOutUser.Result.Password = new Password() { UserPassword = user.Password };

                }
                else
                {
                    return JsonSerializer.Serialize("Your profile was not Changed successfully");
                }
                await _context.SaveChangesAsync();
                return JsonSerializer.Serialize("Your profile was Changed successfully");
            }
            catch (Exception e)
            {

                return JsonSerializer.Serialize(e.Message);
            }
        }

        public Task<User> GetUserByName(string Username)
        {
            return Task.FromResult(_context.Users.Include(u => u.Roles).ToListAsync().Result.First(x => x.UserName == Username));
        }


        public Task<User> GetUserByEmail(string email)
        {
            return Task.FromResult(_context.Users.Include(u => u.Roles).ToListAsync().Result.First(x => x.Email == email));
        }
        public async Task<string> Login(LoginUser user)
        {
            if (!await ValidateLogin(user)) return JsonSerializer.Serialize("false");
                var searchedUser = await GetUserByName(user.UserName);
                var rolesList = searchedUser.Roles.Select(role => role.Name).ToList();
                var JWT = _contextHelper.JWTGenerator(searchedUser.Email, searchedUser.UserName,
                    searchedUser.ID);
                await SaveTokenToDatabase(JWT);
                return JsonSerializer.Serialize(new ValidatedUser()
                {
                    Id = searchedUser.ID,
                    UserName = searchedUser.UserName,
                    Email = searchedUser.Email,
                    Roles = rolesList,
                    Reputation = searchedUser.Reputation,
                    AccessToken = JWT
                });
            }

        private async Task SaveTokenToDatabase(string serializedToken)
        {
            _context.JWTTokens.Add(
                new UserToken
                {
                    Token = serializedToken,
                    Date = DateTime.Now
                }
            );
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateLogin(LoginUser user)
        {
            try
            {
                var validateUser = await GetUserByName(user.UserName);
                return validateUser.Password.ValidatePassword(user.Password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SendForgotPasswordEmail(string email, Guid guid)
        {
            User user = GetUserByEmail(email).Result;
            if (_context.EmailGuid.FirstOrDefault(e => e.Email == email) is { } emailGuid)
            {
                if (email == emailGuid.Email)
                {
                    _context.EmailGuid.Remove(emailGuid);
                }
            }
            _context.EmailGuid.Add(
                new EmailGuid
                {
                    Email = email,
                    Guid = guid

                }
                );
            _context.SaveChanges();
            _emailSender.SendConfirmationEmail(user.UserName, email, "forgor", guid);
        }

        public Task<bool> ValidateEmailForPassword(string email)
        {
            return Task.FromResult(Enumerable.Any(_context.Users, dbUser => dbUser.Email == email));
        }

        public EmailGuid GetEmailFromGuid(Guid emailId)
        {
            {
                return _context.EmailGuid.ToList().FirstOrDefault(x => x.Guid == emailId);
            }
        }

        public void SendSuccessfulPasswordChangeEmail(Guid guid)
        {
            var email = GetEmailFromGuid(guid).Email;
            var deleteEmailGuid = GetEmailFromGuid(guid);
            _context.EmailGuid.Remove(deleteEmailGuid);
            _context.SaveChanges();
            var user = GetUserByEmail(email).Result;
            _emailSender.SendConfirmationEmail(user.UserName, email, "success", guid);
        }
    }
}

