using System;

namespace APIAD.Model
{
    public class UserBearerAdModel
    {
        public UserBearerAdModel()
        {
        }

        public UserBearerAdModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public int Id { get; internal set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; internal set; }
    }
}