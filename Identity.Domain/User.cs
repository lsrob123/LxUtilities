using System.ComponentModel.DataAnnotations;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace Identity.Domain
{
    public class User : EntityBase
    {
        public User()
        {
        }

        public User(string username, string hashedPassword, string email, string mobile) : this()
        {
            Username = username;
            HashedPassword = hashedPassword;
            Email = email;
            Mobile = mobile;
        }

        [MaxLength(50)]
        public string Username { get; protected set; }

        [MaxLength(100)]
        public string HashedPassword { get; protected set; }

        [MaxLength(50)]
        public string Email { get; protected set; }

        [MaxLength(50)]
        public string Mobile { get; protected set; }
    }
}