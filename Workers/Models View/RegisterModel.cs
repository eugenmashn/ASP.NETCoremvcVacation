using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Workers.Models_View
{
    public class RegisterModel
    {
        public string Firstname{ get; set; }
        public string LastName { get;set; }
        [Required(ErrorMessage = "Write Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Write  password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
