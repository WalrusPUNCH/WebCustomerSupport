using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

using CustomerSupport.DAL.Entities;

namespace CustomerSupport.DAL.Impl
{
    public class CustomerSupportContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public CustomerSupportContext(DbContextOptions<CustomerSupportContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLoggerFactory(MyLoggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialist>(SpecialistConfigure);
            modelBuilder.Entity<Request>(RequestConfigure);
            modelBuilder.Entity<Message>(MessageConfigure);
        }

        public void SpecialistConfigure(EntityTypeBuilder<Specialist> builder)
        {
            builder.HasKey(spec => spec.Id);
            builder.HasMany(c => c.ActiveRequests);

            builder.Property(spec => spec.Name).IsRequired();
            builder.Property(spec => spec.Surname).IsRequired();
        }

        public void RequestConfigure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(req => req.Id);

            builder.Property(req => req.ApplicationDate).IsRequired();
            builder.Property(req => req.Subject).IsRequired();
            builder.HasOne(req => req.Specialist).WithMany(spec => spec.ActiveRequests)
                                                 .OnDelete(DeleteBehavior.SetNull)
                                                 .HasForeignKey(req => req.SpecialistId);
        }

        public void MessageConfigure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(mess => mess.Id);
            builder.Property(mess => mess.Text).IsRequired();
        }
    }
}
