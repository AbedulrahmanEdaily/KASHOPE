using KASHOPE.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "Abd",
                    Email = "abd@gmail.com",
                    FullName = "Abdulrahman Edaily",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser
                {
                    UserName = "tariq",
                    Email = "tariq@gmail.com",
                    FullName = "Tariq Shreem",
                    EmailConfirmed = true
                };
                var user3 = new ApplicationUser
                {
                    UserName = "Khaled",
                    Email = "khaled@gmail.com",
                    FullName = "Khaled Ahmed",
                    EmailConfirmed = true
                };
            
                await _userManager.CreateAsync(user1, "Abd@12345");
                await _userManager.CreateAsync(user2, "Tariq@12345");
                await _userManager.CreateAsync(user3, "Khaled@12345");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "User");
            }
        }
    }
}
