using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Workers.Models
{
    public class AuthenticationContext:IdentityDbContext<UserAuthentication>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
