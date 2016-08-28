using System.ComponentModel.DataAnnotations.Schema;
using LxUtilities.Definitions.Enumeration;

namespace Identity.Domain.ValueObjects
{
    [ComplexType]
    public class AccountStatus : StringEnumeration
    {
        public static readonly AccountStatus Unknown = new AccountStatus("Unknown");
        public static readonly AccountStatus PendingAcceptance = new AccountStatus("PendingAcceptance");
        public static readonly AccountStatus Active = new AccountStatus("Active");
        public static readonly AccountStatus Suspended = new AccountStatus("Suspended");
        public static readonly AccountStatus Closed = new AccountStatus("Closed");

        private AccountStatus() : this("Unknown")
        {
        }

        private AccountStatus(string value) : base(value)
        {
        }
    }
}