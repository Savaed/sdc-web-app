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

            builder.Entity<OpeningHours>().HasData(
             new OpeningHours
             {
                 Id = Guid.NewGuid().ToString(),
                 OpeningHour = new TimeSpan(10, 0, 0),
                 ClosingHour = new TimeSpan(18, 0, 0),
                 DayOfWeek = DayOfWeek.Monday
             });

            builder.Entity<OpeningHours>().HasData(
            new OpeningHours
            {
                Id = Guid.NewGuid().ToString(),
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Tuesday
            });

            builder.Entity<OpeningHours>().HasData(
            new OpeningHours
            {
                Id = Guid.NewGuid().ToString(),
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Wednesday
            });

            builder.Entity<OpeningHours>().HasData(
            new OpeningHours
            {
                Id = Guid.NewGuid().ToString(),
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Thursday
            });

            builder.Entity<OpeningHours>().HasData(
            new OpeningHours
            {
                Id = Guid.NewGuid().ToString(),
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(18, 0, 0),
                DayOfWeek = DayOfWeek.Friday
            });

            builder.Entity<OpeningHours>().HasData(
            new OpeningHours
            {
                Id = Guid.NewGuid().ToString(),
                OpeningHour = new TimeSpan(10, 0, 0),
                ClosingHour = new TimeSpan(16, 0, 0),
                DayOfWeek = DayOfWeek.Saturday
            });

            builder.Entity<OpeningHours>().HasData(
          new OpeningHours
          {
              Id = Guid.NewGuid().ToString(),
              OpeningHour = new TimeSpan(10, 0, 0),
              ClosingHour = new TimeSpan(16, 0, 0),
              DayOfWeek = DayOfWeek.Sunday
          });

            builder.Entity<ActivityLog>().HasData(
                new ActivityLog
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = ActivityLog.ActivityType.LogIn,
                    User = "jacks",
                    Description = "Attempt to steal the Dead Man's Chest"
                },
                new ActivityLog
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = ActivityLog.ActivityType.LogOut,
                    User = "hektorb",
                    Description = "Revolting on a black pearl"
                });


            builder.Entity<Article>().HasData(
                new Article
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = "Jack Sparrow",
                    Title = "The bast pirate i'v every seen",
                    Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl"
                },
                new Article
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = "Hektor Barbossa",
                    Title = "The captain of Black Pearl",
                    Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl"
                });

            builder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = Guid.NewGuid().ToString(),
                    TicketUniqueId = Guid.NewGuid().ToString(),
                    OrderStamp = Guid.NewGuid().ToString()
                },
                new Ticket
                {
                    Id = Guid.NewGuid().ToString(),
                    TicketUniqueId = Guid.NewGuid().ToString(),
                    OrderStamp = Guid.NewGuid().ToString()
                });

            builder.Entity<Discount>().HasData(
                 new Discount
                 {
                     Id = Guid.NewGuid().ToString(),
                     Type = Discount.DiscountType.ForGroup,
                     Description = "Discount for groups",
                     GroupSizeForDiscount = 20,
                     DiscountValueInPercentage = 15
                 },
                 new Discount
                 {
                     Id = Guid.NewGuid().ToString(),
                     Type = Discount.DiscountType.ForFamily,
                     Description = "Discount for people with Family Card",
                     DiscountValueInPercentage = 15
                 },
                 new Discount
                 {
                     Id = Guid.NewGuid().ToString(),
                     Type = Discount.DiscountType.ForDisabled,
                     Description = "Discount for disabled people.",
                     DiscountValueInPercentage = 50
                 },
                 new Discount
                 {
                     Id = Guid.NewGuid().ToString(),
                     Type = Discount.DiscountType.ForChild,
                     Description = "Discount only for kids under specific age.",
                     DiscountValueInPercentage = 100
                 });

            builder.Entity<TicketTariff>().HasData(
                new TicketTariff
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                    DefaultPrice = 28
                },
                new TicketTariff
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Centrum Dziedzictwa Szkła.",
                    DefaultPrice = 22
                },
                new TicketTariff
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Piwnice Przedprożne.",
                    DefaultPrice = 10
                });

            builder.Entity<VisitTariff>(entity =>
            {
                entity.HasMany(s => s.TicketTariffs)
                      .WithOne(t => t.SightseeingTariff)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(new VisitTariff { Id = Guid.NewGuid().ToString(), Name = "BasicTickets" });
            });

            builder.Entity<SightseeingGroup>().HasData(
                new SightseeingGroup
                {
                    Id = Guid.NewGuid().ToString(),
                    SightseeingDate = DateTime.Now.AddDays(7).Date.AddHours(12),
                    MaxGroupSize = 30
                },
                new SightseeingGroup
                {
                    Id = Guid.NewGuid().ToString(),
                    SightseeingDate = DateTime.Now.AddDays(2).Date.AddHours(16),
                    MaxGroupSize = 25
                });

            builder.Entity<VisitInfo>().HasData(new VisitInfo
            {
                Id = Guid.NewGuid().ToString(),
                MaxChildAge = 5,
                MaxAllowedGroupSize = 35,
                MaxTicketOrderInterval = 4,
                Description = "TL;DR",
                SightseeingDuration = 2
            });

            builder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-23),
                    EmailAddress = "example@mail.com",
                    HasFamilyCard = false
                },
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-4),
                    HasFamilyCard = true,
                    EmailAddress = " example2@mail.uk"
                });

            //builder.Entity<OpeningDate>().HasData(
            //    new OpeningDate
            //    {
            //        Id = "1",
            //        OpeningDays = new CustomDayOfWeek[]
            //        {
            //            new CustomDayOfWeek { Id = "1", DayOfWeek = DayOfWeek.Monday },
            //            new CustomDayOfWeek { Id = "2", DayOfWeek = DayOfWeek.Tuesday },
            //            new CustomDayOfWeek { Id = "3", DayOfWeek = DayOfWeek.Wednesday},
            //            new CustomDayOfWeek { Id = "4", DayOfWeek = DayOfWeek.Thursday },
            //            new CustomDayOfWeek { Id = "5", DayOfWeek = DayOfWeek.Friday },
            //            new CustomDayOfWeek { Id = "6", DayOfWeek = DayOfWeek.Saturday },
            //            new CustomDayOfWeek { Id = "7", DayOfWeek = DayOfWeek.Sunday}
            //        },
            //        OpeningHour = new TimeSpan(10, 0, 0),
            //        ClosingHour = new TimeSpan(18, 0, 0)
            //    });
        }
    }
}
