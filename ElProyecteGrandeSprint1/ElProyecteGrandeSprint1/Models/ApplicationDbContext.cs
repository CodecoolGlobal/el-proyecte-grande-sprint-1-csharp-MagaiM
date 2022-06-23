using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ElProyecteGrandeSprint1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(x => x.Password).HasConversion(t => t.UserPassword, s => new Password(s));
        }

        public async Task<User> GetUserDataFromDataBase(int id) => Users.ToListAsync().Result.First(x => x.ID == id);


        public async Task<List<User>> GetAllUserDataFromDataBase() => await Users.ToListAsync();


        public async Task<string> RegisterUser(RegisterUser user)
        {
            if (!ValidateUsername(user.UserName))
            {
                return JsonSerializer.Serialize("This Username is already taken");
            }
            if (ValidatePassword(user) == "accepted")
            {
                User registerUser = new User()
                {
                    UserName = user.UserName,
                    Password = new Password() { UserPassword = user.Password },
                    Rank = Rank.Noob,
                    Reputation = 0
                };
                Users.Add(registerUser);
                await SaveChangesAsync();
                return JsonSerializer.Serialize("Registered Successfully");
            }
            return JsonSerializer.Serialize(ValidatePassword(user));
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

        public async Task<string> ChangeUserProfile(int id, RegisterUser user)
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
            return Users.ToListAsync().Result.First(x => x.UserName == Username);
        }
        public async Task<string> ValidateLogin(RegisterUser user)
        {
            try
            {
                User validateUser = await GetUserByName(user.UserName);
                return JsonSerializer.Serialize(validateUser.Password.ValidatePassword(user.Password).ToString());
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize("false");
            }

        }
    }
}
