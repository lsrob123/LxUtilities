using LxUtilities.Services.Constants;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Constants
{
    public class TestConstants
    {
        public const string Active = "Active", Closed = "Closed";
    }


    [TestFixture]
    public class StringConstantsTests
    {
        [Test]
        public void Given_MatchedStringInputWithoutDefault_When_GetValueCalled_Then_ValidValueReturned()
        {
            const string input = "active";
            var value = StringConstantHelper.GetValue<TestConstants>(input);
            Assert.AreEqual(TestConstants.Active, value);
        }

        [Test]
        public void Given_UnmatchedStringInputWithoutDefault_When_GetValueCalled_Then_NullReturned()
        {
            const string input = "active1";
            var value = StringConstantHelper.GetValue<TestConstants>(input);
            Assert.IsNull(value);
        }

        [Test]
        public void Given_UnmatchedStringInputWithDefault_When_GetValueCalled_Then_DefaultValueReturned()
        {
            const string input = "active1";
            var value = StringConstantHelper.GetValue<TestConstants>(input, TestConstants.Closed);
            Assert.AreEqual(TestConstants.Closed, value);
        }
    }
}