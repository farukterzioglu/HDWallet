using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GenerateCardanoHDWallet
    {
        private const string ReferenceSeed2 = "fffcf9f6f3f0edeae7e4e1dedbd8d5d2cfccc9c6c3c0bdbab7b4b1aeaba8a5a29f9c999693908d8a8784817e7b7875726f6c696663605d5a5754514e4b484542";
        
        [TestCase("5a5ed38ce39cafd9318077488311463964708602559fdbc020deec7a823e29c3", "00d04fe854a02045b3cb289e045b78b4e78b613f2769f4824c802fd8efbc2d82d0")]
        public void ShouldGenerateCardanoFromSeed(string privateKey, string publicKey)
        {
            CardanoHDWalletEd25519 hdWallet = new CardanoHDWalletEd25519(ReferenceSeed2);
            CardanoWallet wallet = hdWallet.GetCoinTypeWallet();

            Assert.AreEqual(privateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(publicKey, $"00{wallet.PublicKey.ToHexString()}");
        }

        private const string ReferenceSeed = "ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2";

        [TestCase("m/1852'/1852'", "9c3192c9992b5b99b4209501522153e39a486fe3304686692fcbc33bf5399524", "0018cf6275146a3144da79c37c559025d6b74ff987f93a33eb21d7601feb6cf52b")]
        public void ShouldGenerateMasterWalletFromPurposeAndPath(string path, string expectedPrivateKey, string expectedPublicKey)
        {
            TestHDWalletEd25519 hdWallet = new TestHDWalletEd25519(ReferenceSeed);
            CardanoWallet wallet = hdWallet.GetWalletFromPath(path);

            Assert.AreEqual(expectedPrivateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(expectedPublicKey, $"00{wallet.PublicKey.ToHexString()}");
        }

        [TestCase(
            "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window",
            "ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2",
            "9c3192c9992b5b99b4209501522153e39a486fe3304686692fcbc33bf5399524", 
            "0018cf6275146a3144da79c37c559025d6b74ff987f93a33eb21d7601feb6cf52b")]
        public void ShouldGenerateFromMnemonic(string mnemonic, string expectedSeed, string expectedPrivateKey, string expectedPublicKey)
        {
            CardanoHDWalletEd25519 hdWallet = new CardanoHDWalletEd25519(mnemonic, string.Empty);
            Assert.AreEqual(expectedSeed, hdWallet.BIP39Seed);

            CardanoWallet wallet = hdWallet.GetCoinTypeWallet();

            Assert.AreEqual(expectedPrivateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(expectedPublicKey, $"00{wallet.PublicKey.ToHexString()}");
        }

        [TestCase(
            "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window",
            "ea7a0eda22c9c5fa67e033fd25162720543464aa410ddeb4f4ad9bde322566a6", 
            "009559f806b276aac52026ea59746930373f7902055f5ecdf6dc18fd0108a45adb")]
        public void ShouldGenerateAccount0FromMnemonic(string mnemonic, string expectedPrivateKey, string expectedPublicKey)
        {
            IHDWallet<CardanoWallet> hdWallet = new CardanoHDWalletEd25519(mnemonic, string.Empty);
            CardanoWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            Assert.AreEqual(expectedPrivateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(expectedPublicKey, $"00{wallet.PublicKey.ToHexString()}");
        }
    }
}