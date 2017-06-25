using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class AspNetUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual bool AccessFailedCount { get; set; }

        public virtual string UserName { get; set; }
    }

    public static class UserExtentions
    {
        public static void Update(this AspNetUser user, AspNetUser up)
        {
            user.Email = up.Email;
            user.EmailConfirmed = up.EmailConfirmed;
            user.PasswordHash = up.PasswordHash;
            user.SecurityStamp = up.SecurityStamp;
            user.PhoneNumber = up.PhoneNumber;
            user.PhoneNumberConfirmed = up.PhoneNumberConfirmed;
            user.TwoFactorEnabled = up.TwoFactorEnabled;
            user.LockoutEnabled = up.LockoutEnabled;
            user.LockoutEndDateUtc = up.LockoutEndDateUtc;
            user.AccessFailedCount = up.AccessFailedCount;
            user.UserName = up.UserName;
        }
    }

}
