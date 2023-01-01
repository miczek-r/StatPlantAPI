﻿using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class IdentityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        override public DbSet<User> Users => Set<User>();

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Trigger>().HasMany(trigger=> trigger.Conditions).WithOne(condition => condition.Trigger).On

            base.OnModelCreating(builder);
        }


    }
}
