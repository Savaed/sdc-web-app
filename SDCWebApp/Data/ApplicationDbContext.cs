using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Models;
using System;

namespace SDCWebApp.Data
{
    /// <summary>
    /// Main database context for entire application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        // All DbSet<TEntity> properties are marked as virtual for enabling unit testing and using Mock.Setup / Verifiable functionality.
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<VisitTariff> VisitTariffs { get; set; }
        public virtual DbSet<TicketTariff> TicketTariffs { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<SightseeingGroup> Groups { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<VisitInfo> Info { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<OpeningHours> OpeningDates { get; set; }


        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var sightseeingDate = DateTime.Now.AddDays(7).Date.AddHours(12);

            base.OnModelCreating(builder);

            builder.Entity<ActivityLog>().Property(a => a.Type).HasConversion<string>();
            builder.Entity<Discount>().Property(d => d.Type).HasConversion<string>();
            builder.Entity<OpeningHours>().Property(d => d.DayOfWeek).HasConversion<string>();

            // Allow deleting OpeningHours if deleting SightseeingInfo.
            builder.Entity<VisitInfo>()
                .HasMany(x => x.OpeningHours)
                .WithOne(o => o.Info)
                .OnDelete(DeleteBehavior.Cascade);

            // Allow deleting Tickets if deleting SightseeingGroup.
            builder.Entity<SightseeingGroup>()
                .HasMany(x => x.Tickets)
                .WithOne(x => x.Group)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed VisitInfo.
            builder.Entity<VisitInfo>().HasData(new
            {
                Id = "1",
                MaxChildAge = 5,
                MaxAllowedGroupSize = 35,
                MaxTicketOrderInterval = 4,
                Description = "TL;DR",
                SightseeingDuration = 2.0f,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow,
            });

            // Seed OpeningHours
            builder.Entity<OpeningHours>().HasData(
            new 
            {
                Id = "1",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Monday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "2",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Tuesday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "3",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Wednesday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "4",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Thursday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "5",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Friday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "6",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(16, 0, 0),
                DayOfWeek = DayOfWeek.Saturday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            },
            new
            {
                Id = "7",
                InfoId = "1",
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(16, 0, 0),
                DayOfWeek = DayOfWeek.Sunday,
                UpdatedAt = DateTime.MinValue,
                CreatedAt = DateTime.UtcNow
            });

            // Seed ActivityLog.
            builder.Entity<ActivityLog>().HasData(
                new 
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = ActivityLog.ActivityType.LogIn,
                    User = "jacks",
                    Description = "Attempt to steal the Dead Man's Chest",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue,
                    Date = DateTime.UtcNow
                },
                new 
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = ActivityLog.ActivityType.LogOut,
                    User = "hektorb",
                    Description = "Revolting on a black pearl",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue,
                    Date = DateTime.UtcNow
                });

            // Seed Article.
            builder.Entity<Article>().HasData(
                new 
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = "Jack Sparrow",
                    Title = "The bast pirate i'v every seen",
                    Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                },
                new 
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = "Hektor Barbossa",
                    Title = "The captain of Black Pearl",
                    Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                });         

            // Seed VisitTariff.
            builder.Entity<VisitTariff>(entity =>
            {
                entity.HasMany(s => s.TicketTariffs)
                      .WithOne(t => t.VisitTariff)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(
                    new 
                    {
                        Id = "1",
                        Name = "BasicTickets",
                        UpdatedAt = DateTime.MinValue,
                        CreatedAt = DateTime.UtcNow
                    });
            });

            // Seed TicketTariff.
            builder.Entity<TicketTariff>().HasData(
                new
                {
                    Id = "1",
                    VisitTariffId = "1",
                    Description = "Ticket tariff 1",
                    DefaultPrice = 20.0f,
                    CreatedAt = DateTime.UtcNow,
                    IsPerHour = false,
                    UpdatedAt = DateTime.MinValue,
                    IsPerPerson = true
                },
                new 
                {
                    Id = "2",
                    VisitTariffId = "1",
                    Description = "Ticket tariff 2",
                    DefaultPrice = 20.0f,
                    CreatedAt = DateTime.UtcNow,
                    IsPerHour = false,
                    UpdatedAt = DateTime.MinValue,
                    IsPerPerson = true
                });

            // Seed Discount.
            builder.Entity<Discount>().HasData(
                 new 
                 {
                     Id = "1",
                     Type = Discount.DiscountType.ForGroup,
                     Description = "Discount for groups",
                     GroupSizeForDiscount = 20,
                     DiscountValueInPercentage = 50,
                     UpdatedAt = DateTime.MinValue,
                     CreatedAt = DateTime.UtcNow
                 },
                 new 
                 {
                     Id = "2",
                     Type = Discount.DiscountType.ForFamily,
                     Description = "Discount for people with Family Card",
                     DiscountValueInPercentage = 50,
                     UpdatedAt = DateTime.MinValue,
                     CreatedAt = DateTime.UtcNow
                 });

            // Seed SightseeingGroup.
            builder.Entity<SightseeingGroup>().HasData(
                new 
                {
                    Id = "1",
                    SightseeingDate = sightseeingDate,
                    MaxGroupSize = 30,
                    CurrentGroupSize = 29,
                    IsAvailablePlace = true,
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                },
                new 
                {
                    Id = "2",
                    SightseeingDate = sightseeingDate,
                    MaxGroupSize = 30,
                    CurrentGroupSize = 29,
                    IsAvailablePlace = true,
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                });

            // Seed Customer
            builder.Entity<Customer>().HasData(
                new 
                {
                    Id = "1",
                    DateOfBirth = DateTime.Now.AddYears(-23),
                    EmailAddress = "example@mail.com",
                    HasFamilyCard = false,
                    IsChild = false,
                    IsDisabled = false,
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                },
                new 
                {
                    Id = "2",
                    DateOfBirth = DateTime.Now.AddYears(-4),
                    HasFamilyCard = true,
                    EmailAddress = " example2@mail.uk",
                    IsChild = true,
                    IsDisabled = false,
                    UpdatedAt = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow
                });

            // Seed Ticket.
            builder.Entity<Ticket>().HasData(
                new
                {
                    Id = "1",
                    OrderStamp = "order-stamp-nr-1",
                    Price = 10.0f,
                    PurchaseDate = DateTime.Now.AddDays(-7),
                    TicketUniqueId = "ticket-unique-id",
                    CustomerId = "1",
                    DiscountId = "1",
                    GroupId = "1",
                    TariffId = "1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue,
                    ValidFor = sightseeingDate
                },
                new
                {
                    Id = "2",
                    OrderStamp = "order-stamp-nr-2",
                    Price = 10.0f,
                    PurchaseDate = DateTime.Now.AddDays(-7),
                    TicketUniqueId = "ticket-unique-id-nr-2",
                    CustomerId = "2",
                    DiscountId = "2",
                    GroupId = "1",
                    TariffId = "1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue,
                    ValidFor = sightseeingDate
                });
        }
    }
}
