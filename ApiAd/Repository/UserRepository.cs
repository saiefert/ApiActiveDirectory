using APIAD.Model;
using APIAD.Services;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;

namespace APIAD.Repository
{
#pragma warning disable CA1416
    public partial class UserRepository : ActiveDirectoryRepository, IUserRepository
    {
        UserModel userActiveDirectory;

        public UserModel AdAuthenticate(string user, string password)
        {
            DirectorySearcher search = new DirectorySearcher(createDirectoryEntry(user, password));
            search.Filter = $"(&(objectClass=user)(sAMAccountName={user}))";
            SearchResult result = search.FindOne();

            if (result != null)
            {
                return GetAdUser(user);
            }

            return null;
        }

        public UserModel AdAuthenticate(string user, string password, bool genToken)
        {
            DirectorySearcher search = new DirectorySearcher(createDirectoryEntry(user, password));
            search.Filter = $"(&(objectClass=user)(sAMAccountName={user}))";
            SearchResult result = search.FindOne();

            if (result != null)
            {
                if (genToken)
                {
                    var usr = GetAdUser(user);
                    var token = TokenService.GenereteTokenAdUser(usr);
                    usr.Token = token;
                    return usr;
                }

                return GetAdUser(user);
            }

            return null;
        }

        public string GetUserProperty(string user, string property = "sn")
        {
            string adProperty = "";

            try
            {
                DirectorySearcher search = new DirectorySearcher(createDirectoryEntry(Settings.AdminUser, Settings.AdminPass));
                search.Filter = $"(&(objectClass=user)(sAMAccountName={user}))";
                SearchResult result = search.FindOne();

                if (result != null)
                    return AdPropertyValue(result, property);
            }

            catch (Exception ex)
            {
                return "Usuario não encontrado";
            }

            return adProperty;
        }


        public UserModel GetAdUser(string user)
        {
            try
            {
                DirectorySearcher search = new DirectorySearcher(createDirectoryEntry(Settings.AdminUser, Settings.AdminPass));
                search.Filter = $"(&(objectClass=user)(sAMAccountName={user}))";
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    var nomeCompleto = AdPropertyValue(result, "displayName");
                    var name = AdPropertyValue(result, "name");
                    var sam = AdPropertyValue(result, "samaccountname");
                    var email = AdPropertyValue(result, "mail");
                    var city = AdPropertyValue(result, "l");
                    var empresa = AdPropertyValue(result, "company");
                    var title = AdPropertyValue(result, "title");
                    var department = AdPropertyValue(result, "department");
                    var state = AdPropertyValue(result, "st");
                    var CorporateNumber = AdPropertyValue(result, "ipphone");
                    var phone = AdPropertyValue(result, "telephonenumber");
                    var foto = ImageToBase64(result, "thumbnailphoto");
                    var membroDe = result.Properties["memberof"];
                    var securityGroups = new List<string>();
                    string patternGruposApp = @"(APP [\D]*?,)";
                    string patternGruposSeguranca = @"(G [\w]*\.[\w]*)";


                    foreach (var item in membroDe)
                    {
                        Match matchApp = Regex.Match(item.ToString(), patternGruposApp, RegexOptions.IgnoreCase);
                        Match matchSecurity = Regex.Match(item.ToString(), patternGruposSeguranca, RegexOptions.IgnoreCase);

                        if (matchSecurity.Success)
                            securityGroups.Add(matchSecurity.Value);
                    }

                    userActiveDirectory = new UserModel(sam, email, nomeCompleto)
                    {
                        Name = name,
                        Title = title,
                        City = city,
                        Department = department,
                        State = state,
                        Phone = phone,
                        CorporateNumber = CorporateNumber,
                        Empresa = empresa,
                        Photo = foto,
                        SecurityGroups = securityGroups
                    };

                    return userActiveDirectory;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<SimpleUserModel> GetAdUsers(bool loadManager, bool loadPhoto, string OUFilter)
        {
            var listaUsuarios = new List<SimpleUserModel>();
            var filtro = OUFilter;

            if (OUFilter.Equals("all")) filtro = "";
            if (!OUFilter.Equals("all")) filtro = $"OU={OUFilter},";

            try
            {
                DirectorySearcher search = new DirectorySearcher(createDirectoryEntryTV(Settings.AdminUser, Settings.AdminPass, filtro.ToLower()));
                search.Filter = "(&(objectClass=user)(objectCategory=person))";

                var resultCollection = search.FindAll();

                foreach (SearchResult result in resultCollection)
                {
                    if (resultCollection != null)
                    {
                        var user = new SimpleUserModel();

                        user.User = AdPropertyValue(result, "samaccountname");
                        user.Name = AdPropertyValue(result, "displayName");
                        user.Title = AdPropertyValue(result, "title");
                        user.Mail = AdPropertyValue(result, "mail");
                        user.Department = AdPropertyValue(result, "department");

                        if (loadManager) user.Manager = AdPropertyValue(result, "manager");
                        if (loadPhoto) user.Photo = ImageToBase64(result, "thumbnailphoto");
                        user.DistinguishedName = AdPropertyValue(result, "distinguishedName");

                        listaUsuarios.Add(user);
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                listaUsuarios.Add(null);
            }

            return listaUsuarios;
        }

        static string ImageToBase64(SearchResult result, string photoProperty)
        {
            try
            {
                return Convert.ToBase64String((byte[])result.Properties[photoProperty][0]);
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}