using System;
using APIAD.Model;
using Newtonsoft.Json;

namespace APIAD
{
    public static class Settings
    {
        public static string Secret = SecretEnv();
        public static string AdminUser = UsuarioAdmin().Username;
        public static string AdminPass = UsuarioAdmin().Password;
        public static string ReadUser = UsuarioLeitura().Username;
        public static string ReadPass = UsuarioLeitura().Password;
        public static string Domain = "yourdomain.com";

        #region Get Environment Variable Methods
        private static string SecretEnv() => Environment.GetEnvironmentVariable("APIAD_SECRET");
        private static UserBearerAdModel UsuarioAdmin()
        {
            var userAdminModel = new { admin = new { user = "", password = "" } };
            var jsonAdmin = JsonConvert.DeserializeAnonymousType(Environment.GetEnvironmentVariable("APIAD_USER"), userAdminModel);
            var userAdmin = new UserBearerAdModel(jsonAdmin.admin.user, jsonAdmin.admin.password);
            return userAdmin;
        }

        private static UserBearerAdModel UsuarioLeitura()
        {
            var userLeituraModel = new { leitura = new { user = "", password = "" } };
            var jsonLeitura = JsonConvert.DeserializeAnonymousType(Environment.GetEnvironmentVariable("APIAD_USER"), userLeituraModel);
            var userLeitura = new UserBearerAdModel(jsonLeitura.leitura.user, jsonLeitura.leitura.password);
            return userLeitura;
        }

        #endregion
    }
}