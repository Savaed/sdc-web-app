using System;
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
            CreateResource,
            EditResource,
            GetResource,
            DeleteResource,

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
