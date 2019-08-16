using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Models
{
    public class WorkerContext:DbContext
    {
        public DbSet<Vacation> Vacations { get; set; }
       
        public DbSet<Person> People { get; set; }
        public DbSet<Weekend> HolyDays { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoryAddingDays> HistoryAddDays { get; set; }
        public WorkerContext(DbContextOptions<WorkerContext> options)
            : base(options)
        {
           
        }
         
        public WorkerContext()
            :base()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Person>()
                .HasOne(s => s.Team)
                .WithMany(g => g.Workers)
                 .OnDelete(DeleteBehavior.SetNull);
        //    modelBuilder.Entity<Team>()
        //        .HasMany(c => c.Workers)
        //        .WithOne(e => e.Team);
        //    // .WillCascadeOnDelete(false);
        //    //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


    }
}
