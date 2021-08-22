using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace APIAD.Model
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(string userName, string mail, string fullName)
        {
            FullName = fullName;
            UserName = userName;
            Email = mail;
        }

        public string FullName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Empresa { get; set; }
        public string CorporateNumber { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Photo { get; set; }  
        public List<string> SecurityGroups { get; set; }
        public string Token { get; internal set; }
    }
}