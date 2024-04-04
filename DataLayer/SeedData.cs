using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer
{
    public static class SeedData
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.Migrate();

            if (context.UserTypes.Any() || context.Users.Any())
            {
                return; 
            }

            var userTypes = new[]
            {
                new UserType("Admin"),
                new UserType("Regular")
            };
            context.UserTypes.AddRange(userTypes);
            context.SaveChanges();

            var users = new[]
            {
                new User("John Doe", "john@example.com", "password", userTypes.First(t => t.Name == "Admin").Id),
                new User("Jane Doe", "jane@example.com", "password", userTypes.First(t => t.Name == "Regular").Id)
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
