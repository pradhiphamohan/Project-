using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5
{
    public class UserRepository
    {
        public List<User> GetUser() 
        {
            UDBContext uDBContext = new UDBContext();
            return uDBContext.User.Include("Role").ToList();
        }
    }
}