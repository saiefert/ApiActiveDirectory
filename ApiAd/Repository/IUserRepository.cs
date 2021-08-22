using System.Collections.Generic;
using APIAD.Model;

namespace APIAD.Repository
{
    public interface IUserRepository
    {
        UserModel AdAuthenticate(string user, string password);
        UserModel AdAuthenticate(string user, string password, bool generateToken);
        UserModel GetAdUser(string user);
        string GetUserProperty(string user, string property);
        List<SimpleUserModel> GetAdUsers(bool loadManager, bool loadPhoto, string OUFilter);
    }
}