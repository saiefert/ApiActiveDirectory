using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace APIAD.Model
{
    public class AuthenticatedUserModel
    {
        public string adUser { get; set; }
        public string password { get; set; }
        [DefaultValue(false)]
        public bool generatedToken { get; set; }
    }
}