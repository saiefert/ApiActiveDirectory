using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using APIAD.Controllers;
using Model;
using Newtonsoft.Json;

namespace APIAD.Repository
{
#pragma warning disable CA1416
    public class GroupRepository : ActiveDirectoryRepository, IGroupRepository
    {
        List<GroupModel> groupsList;
        public void ManageGroup(string user, List<GroupModel> groups)
        {
            foreach (var group in groups)
            {
                if (group.OnGroup) AddUser(user, group.GroupName);

                if (!group.OnGroup) RemoveUser(user, group.GroupName);
            }
        }
        public List<GroupModel> Groups()
        {

            DirectorySearcher search = new DirectorySearcher(createDirectoryEntry(Settings.AdminUser, Settings.AdminPass));
            search.Filter = $"(&(objectClass=group))";
            groupsList = new List<GroupModel>();

            foreach (SearchResult result in search.FindAll())
            {
                groupsList.Add(new GroupModel
                {
                    GroupName = AdPropertyValue(result, "name"),
                    Description = AdPropertyValue(result, "description")
                });
            }

            return groupsList;
        }

        private void AddUser(string userAd, string groupName)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Settings.Domain, Settings.AdminUser, Settings.AdminPass))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                    group.Members.Add(pc, IdentityType.SamAccountName, userAd);
                    group.Save();
                }
            }

            catch (PrincipalExistsException)
            {

            }

            catch (DirectoryServicesCOMException)
            {

            }
        }

        private void RemoveUser(string adUser, string groupName)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Settings.Domain, Settings.AdminUser, Settings.AdminPass))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                    group.Members.Remove(pc, IdentityType.SamAccountName, adUser);
                    group.Save();
                }
            }

            catch (DirectoryServicesCOMException)
            {

            }
        }

    }
}