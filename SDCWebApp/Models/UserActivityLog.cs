using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class ActivityLog : BasicEntity
    {
        public enum ActivityType
        {
            // Account activities.
            LogIn,
            LogOut,
            PasswordChange,
            CreateAccount,  // Restricted for admins.
            DeleteAccount,  // Restricted for admins.

            // Data operations e.g. change, create...
            ArticleEdit,
            ArticleCreate,
            ArticleDelete,
            TicketsTarifffEdit,
            TicketsTariffCreate,
            TicketsTariffDelete,
            SightseeingGroupEdit,
            SightseeingGroupCreate,
            SightseeingGroupDelete,
            DiscountEdit,
            DiscountCreate,
            DiscountDelete,

            // Statistics activities.
            StatisticCreate,
            StatisticDownload
        }

        [Column(TypeName = "datetime2(0)")]
        public DateTime Date { get; private set; } = DateTime.Now;
        public string User { get; set; }
        public ActivityType Type { get; set; }
        public string Description { get; set; }
    }
}
