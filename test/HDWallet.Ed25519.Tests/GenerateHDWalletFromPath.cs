using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GenerateHDWalletFromPath
    {
        private const string ReferenceSeed = "000102030405060708090a0b0c0d0e0f";

        [TestCase("m/0'/1'", "b1d0bad404bf35da785a64ca1ac54b2617211d2777696fbffaf208f746ae84f2", "001932a5270f335bed617d5b935c80aedb1a35bd9fc1e31acafd5372c30f5c1187")]
        public void ShouldGenerateMasterWalletFromPurposeAndPath(string expectedPath, string privateKey, string publicKey)
        {
            IHDWallet<CardanoWallet> hdWallet = new PathTestHDWalletEd25519(ReferenceSeed);
            var masterWallet = hdWallet.GetMasterWallet();

            Assert.AreEqual(expected: expectedPath, actual: masterWallet.Path);

            Assert.AreEqual(privateKey, masterWallet.PrivateKey.ToHexString());
            Assert.AreEqual(publicKey, $"00{masterWallet.PublicKey.ToHexString()}");
        }

        [TestCase("m/0'/1'/2'", "92a5b23c0b8a99e37d07df3fb9966917f5d06e02ddbd909c7e184371463e9fc9", "00ae98736566d30ed0e9d2f4486a64bc95740d89c7db33f52121f8ea8f76ff0fc1")]
        public void ShouldGenerateAccountFromPurposeAndPath(string expectedPath, string privateKey, string publicKey)
        {
            IHDWallet<CardanoWallet> hdWallet = new PathTestHDWalletEd25519(ReferenceSeed);
            var accountWallet = hdWallet.GetAccountWallet(2);

            Assert.AreEqual(expected: expectedPath, actual: accountWallet.Path);

            Assert.AreEqual(privateKey, accountWallet.PrivateKey.ToHexString());
            Assert.AreEqual(publicKey, $"00{accountWallet.PublicKey.ToHexString()}");
        }
    }
}