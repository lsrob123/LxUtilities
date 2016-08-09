using System;
using System.ComponentModel.DataAnnotations;
using Identity.Domain.ValueObjects;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace Identity.Domain.Entities
{
    public class User : EntityBase
    {
        public User()
        {
        }

        public User(Guid userKey, string username, string hashedPassword, string email, string mobile,
            AccountStatus accountStatus) : base(userKey)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Email = email;
            Mobile = mobile;
            AccountStatus = accountStatus;
        }

        [Required]
        [MaxLength(50)]
        public string Username { get; protected set; }

        [MaxLength(100)]
        public string HashedPassword { get; protected set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; protected set; }

        [MaxLength(50)]
        public string Mobile { get; protected set; }

        public AccountStatus AccountStatus { get; protected set; }

        public void SetAccountStatus(AccountStatus accountStatus)
        {
            AccountStatus = accountStatus;
        }
    }
}