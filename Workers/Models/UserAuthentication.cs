using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
namespace Workers.Models
{
    public class UserAuthentication:IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set;}
        public Guid? personId { get; set; }
        public Person person { get; set; }
    }
}
