using System;
using System.Collections.Generic;
using System.Linq;
using APIAD.Model;

namespace APIAD.Repository
{
    public class UserBearerRepository
    {
        public static UserBearerAdModel Get(string username, string password)
        {
            var users = new List<UserBearerAdModel>();
            users.Add(new UserBearerAdModel { Id = 1, Username = Settings.AdminUser, Password = Settings.AdminPass, Role = "admin" });
            users.Add(new UserBearerAdModel { Id = 2, Username = Settings.ReadUser, Password = Settings.ReadPass, Role = "read" });
            
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).SingleOrDefault();
        }
    }
}