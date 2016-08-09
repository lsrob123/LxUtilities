using Identity.Domain.ValueObjects;
using NUnit.Framework;

namespace Identity.Domain.Tests.ValueObjects
{
    [TestFixture]
    public class AccountStatusTests
    {
        [Test]
        public void AccountStatusTest()
        {
            var accountStatus = new AccountStatus("active");

            Assert.AreEqual(accountStatus.Value, AccountStatusOption.Active.ToString());
            Assert.IsTrue(accountStatus.Equals(AccountStatusOption.Active));
        }
    }
}