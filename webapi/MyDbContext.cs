using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapi
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderPicked> OrderPicked { get; set; } // Dodane OrderPicked

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.Role });

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "kubakepka503@gmail.com" },
                new User { Id = 2, Email = "dkakol01@gmail.com" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, Email = "kubakepka503@gmail.com", Role = "normal" },
                new UserRole { UserId = 1, Email = "kubakepka503@gmail.com", Role = "worker" },
                new UserRole { UserId = 2, Email = "dkakol01@gmail.com", Role = "normal" },
                new UserRole { UserId = 2, Email = "dkakol01@gmail.com", Role = "worker" }
            );

            // Dodane konfiguracje dla OrderPicked
            modelBuilder.Entity<OrderPicked>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderPicked>()
                .Property(op => op.ValidTo)
                .HasColumnType("datetime"); // Ustawić odpowiedni typ danych dla ValidTo

            modelBuilder.Entity<OrderPicked>()
                .HasOne(op => op.Order)
                .WithMany()
                .HasForeignKey(op => op.OrderKey);
        }
    }

    public class OrderPicked
    {
        [Key]
        public int Id { get; set; }
        public string OrderRequestId { get; set; }
        public string OrderId { get; set; }
        public int OrderKey { get; set; }
        public DateTime ValidTo { get; set; }

        public Order Order { get; set; }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public User User { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
        public string ApiType { get; set; }
        public string? GetEndpoint { get; set; } = null;
        public string? PostEndpoint { get; set; } = null;

        // Dodane pole dla relacji z OrderPicked
        public ICollection<OrderPicked> OrderPicked { get; set; } = new List<OrderPicked>();
    }
}
