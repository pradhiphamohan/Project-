using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter user mail.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Mail")]
        [StringLength(30)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(10)]
        public string Password { get; set; }
    }
}