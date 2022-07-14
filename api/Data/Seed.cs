using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using api.Context;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public static class Seed
    {
        public static async Task SeedUsersASync(ApplicationContext context)
        {
            var usersRes = await context.AppUser.AnyAsync();
            if (!usersRes)
            {
                var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
                var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData);

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.Username = user.Username.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1234"));
                    user.PasswordSalt = hmac.Key;

                    context.AppUser.Add(user);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}