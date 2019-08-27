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
        // All DbSet<TEntity> properties are marked as virtual for enabling unit testing and using Mock.Setup / Verifiable functionality
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<SightseeingTariff> SightseeingTariffs { get; set; }
        public virtual DbSet<TicketTariff> TicketTariffs { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<SightseeingGroup> Groups { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<GeneralSightseeingInfo> GeneralSightseeingInfo { get; set; }


        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SightseeingGroup>(entity =>
            {
                entity.Property(s => s.CurrentGroupSize).HasField("_currentGroupSize").UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
                entity.Property(c => c.IsAvailablePlace).HasField("_isAvailablePlace").UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            });

            builder.Entity<Ticket>(entity =>
            {              
                entity.Property(t => t.Price).HasField("_price").UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            });                            

            builder.Entity<ActivityLog>().Property(a => a.Type).HasConversion<string>();
            builder.Entity<Discount>().Property(d => d.Type).HasConversion<string>();

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
                    ValidFor = DateTime.Now.AddDays(7)
                },
                new Ticket
                {
                    Id = Guid.NewGuid().ToString(),
                    TicketUniqueId = Guid.NewGuid().ToString(),
                    ValidFor = DateTime.Now.AddDays(21)
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

            builder.Entity<SightseeingTariff>(entity =>
            {
                entity.HasMany(s => s.TicketTariffs)
                      .WithOne(t => t.SightseeingTariff)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(new SightseeingTariff { Id = Guid.NewGuid().ToString(), Name = "BasicTickets" });
            });                   

            builder.Entity<SightseeingGroup>().HasData(
                new SightseeingGroup
                {
                    Id = Guid.NewGuid().ToString(),
                    SightseeingDate = DateTime.Now.AddDays(7),
                    MaxGroupSize = 30
                },
                new SightseeingGroup
                {
                    Id = Guid.NewGuid().ToString(),
                    SightseeingDate = DateTime.Now.AddDays(2),
                    MaxGroupSize = 25
                });

            builder.Entity<GeneralSightseeingInfo>().HasData(new GeneralSightseeingInfo
            {
                Id = Guid.NewGuid().ToString(),
                MaxChildAge = 5,
                OpeningHour = 9,
                ClosingHour = 17,
                MaxAllowedGroupSize = 35,
                Description = "TL;DR"
            });

            builder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-23),
                    EmailAddres = "example@mail.com",
                    HasFamilyCard = false
                },
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-4),
                    HasFamilyCard = true,
                    EmailAddres = " example2@mail.uk"
                });
        }
    }
}
