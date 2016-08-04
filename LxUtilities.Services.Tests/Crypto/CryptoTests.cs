using System;
using LxUtilities.Services.Crypto;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Crypto
{
    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public void Hashed_String_Matches_Plain_Text_String()
        {
            var cryptoService = new CryptoService();
            var source = Guid.NewGuid().ToString();

            var hashedCode = cryptoService.CreateHash(source); //Hashes of source string is created
            var isTrue = cryptoService.Validate(source, hashedCode);
            //validiate that source string and hashes of source string match

            Assert.IsTrue(isTrue); //Assert the result of validation of source string and hashes  return true
        }

    }
}