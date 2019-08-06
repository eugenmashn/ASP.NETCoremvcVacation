using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Workers.Models_View
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Write Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Write password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; internal set; }
    }
}
