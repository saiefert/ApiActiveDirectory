using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace APIAD.Model
{
    public class SimpleUserModel
    {
        public string User { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Mail { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string DistinguishedName { get; set; }
        public string Photo { get; set; }
    }
}
