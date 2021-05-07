using AuthenticationJWT.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationJWT.Api.Repositories
{
    public static class UserRepositories
    {
        public static User Get(string userName, string passWord)
        {
            var users = new List<User>();
            users.Add(new User {Id = 1, UserName = "user1", Password= "pass1", Role = "manager" });
            users.Add(new User {Id = 2, UserName = "user2", Password= "pass2", Role = "employee" });

            return users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower() && u.Password == passWord);
        }

    }
}
