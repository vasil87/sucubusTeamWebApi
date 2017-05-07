using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Users
{
    public class LoginUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}