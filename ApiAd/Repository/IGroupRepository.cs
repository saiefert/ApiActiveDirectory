using System.Collections.Generic;
using Model;

namespace APIAD.Repository
{
    public interface IGroupRepository
    {
        void ManageGroup(string user, List<GroupModel> group);
        List<GroupModel> Groups();
    }
}