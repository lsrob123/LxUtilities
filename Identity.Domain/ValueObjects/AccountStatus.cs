using LxUtilities.Definitions.Core.ValueObject;

namespace Identity.Domain.ValueObjects
{
    public enum AccountStatusOption
    {
        Unknown = 0,
        PendingAcceptance = 100,
        Active = 200,
        Suspended = 300,
        Closed = 400
    }

    public class AccountStatus : EnumBackedValueObject<AccountStatusOption>
    {
        public AccountStatus() : base(30)
        {
        }

        public AccountStatus(string value) : base(value, 30)
        {
        }

        public AccountStatus(AccountStatusOption value) : base(value, 30)
        {
        }
    }
}