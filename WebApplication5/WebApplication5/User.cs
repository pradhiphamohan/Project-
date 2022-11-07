using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5
{
    public class User
    { 
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DOB { get; set; }
        public string MobileID { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }

    
}