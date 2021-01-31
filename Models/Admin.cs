using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Admin
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string email { set; get; }

        public string password { set; get; }

        public bool Active { set; get; }
        public string RoleName { set; get; }

    }
}