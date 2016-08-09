using System.ComponentModel.DataAnnotations;
using LxUtilities.Definitions.Core.ValueObject;
using LxUtilities.Services.Constants;

namespace Identity.Domain.ValueObjects
{
    public class AccountStatus : IValueObject<string>
    {
        public AccountStatus(string value = Constants.Unknown)
        {
            SetValue(value);
        }

        [MaxLength(30)]
        public string Value { get; protected set; }

        public void SetValue(string value)
        {
            Value = StringConstantHelper.GetValue<Constants>(value, Constants.Unknown);
        }

        public class Constants
        {
            public const string Unknown = "Unknown", PendingAcceptance = "PendingAcceptance", Active = "Active",
                Suspended = "Suspended", Closed = "Closed";
        }
    }
}