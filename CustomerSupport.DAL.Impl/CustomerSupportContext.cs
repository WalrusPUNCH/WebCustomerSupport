using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CustomerSupport.DAL.Entities;

namespace CustomerSupport.DAL.Impl
{
    public class CustomerSupportContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

        public CustomerSupportContext(DbContextOptions<CustomerSupportContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialist>(SpecialistConfigure);
            modelBuilder.Entity<Request>(RequestConfigure);
            modelBuilder.Entity<Message>(MessageConfigure);
        }

        public void SpecialistConfigure(EntityTypeBuilder<Specialist> builder)
        {
            builder.HasKey(spec => spec.Id);
            builder.HasMany(c => c.ActiveRequests)
                    .WithOne(e => e.Specialist)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.Property(spec => spec.Name).IsRequired();
            builder.Property(spec => spec.Surname).IsRequired();
        }

        public void RequestConfigure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(req => req.Id);

            builder.Property(req => req.ApplicationDate).IsRequired();
            builder.Property(req => req.Subject).IsRequired();
        }

        public void MessageConfigure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(mess => mess.Id);
            builder.Property(mess => mess.Text).IsRequired();
        }
    }
}
