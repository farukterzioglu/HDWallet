using HDWallet.Ed25519;
using NUnit.Framework;
using dotnetstandard_bip32;

namespace NEd25519.Tests
{
    public class ExtKeyTests
    {
        private const string Vector1Seed = "000102030405060708090a0b0c0d0e0f";
        private const string Vector1KeyHexExpected = "2b4be7f19ee27bbf30c667b642d5f4aa69fd169872f8fc3059c08ebae2eb19e7";
        private const string Vector1ChainCodeExpected = "90046a93de5380a72b5e45010748567d5ea02bbf6522f979e05c0d8d8ca9fffb";

        [Test]
        public void ShouldGenerate()
        {
            NEd25519.ExtKey testMasterKeyFromSeed = new NEd25519.ExtKey(Vector1Seed);

            Assert.AreEqual(Vector1KeyHexExpected, testMasterKeyFromSeed.PrivateKey.ToBytes().ToStringHex());
            Assert.AreEqual(Vector1ChainCodeExpected, testMasterKeyFromSeed.ChainCode.ToStringHex());
        }
    }
}