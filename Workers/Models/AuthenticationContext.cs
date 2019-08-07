﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace Workers.Models
{
    public class AuthenticationContext:IdentityDbContext<UserAuthentication>
    {
        public DbSet<Vacation> Vacations { get; set; }

        public DbSet<Person> People { get; set; }
        public DbSet<Weekend> HolyDays { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<User> Users { get; set; }
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
