﻿using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Models
{
    public class Context : DbContext
    {
        public Context()
        { }

        public Context(DbContextOptions<Context> options): base(options)
        { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
