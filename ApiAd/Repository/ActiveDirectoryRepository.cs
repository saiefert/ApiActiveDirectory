using System;
using System.DirectoryServices;

namespace APIAD.Repository
{
    public abstract class ActiveDirectoryRepository
    {
        protected DirectoryEntry createDirectoryEntry(string user, string password)
        {
            DirectoryEntry ldapConnection = new ($"LDAP://{Settings.Domain}");
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            ldapConnection.Username = user;
            ldapConnection.Password = password;
            return ldapConnection;
        }

        protected DirectoryEntry createDirectoryEntryTV(string user, string password, string OUFilter = "")
        {
            DirectoryEntry ldapConnection = new ("yourConnection.com");
            ldapConnection.Path = $"LDAP://{OUFilter}OU=yourOU,DC=yourDC,DC=yourDC";
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            ldapConnection.Username = user;
            ldapConnection.Password = password;
            return ldapConnection;
        }

        protected string AdPropertyValue(SearchResult result, string property)
        {
            if (property == null) return "";

            try
            {
                return result.Properties[property][0].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}