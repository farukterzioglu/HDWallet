using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Cardano.Tests
{
    /// <summary>
    /// Copy of HDWallet.Ed25519.Tests/GenerateCardanoHDWallet.cs
    /// </summary>
    public class GenerateCardanoHDWallet
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";
        private const string ReferenceSeed = "ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2";
        private const string ReferencePrivateKey = "879c41fc84768094f57418dfc96910e81c010f7f522b2ac7bc5abe8360e9e650";
        private const string ReferencePubKey = "000d16c4f4d82c55a54855c53b39c6d9cc7b13f724b9175fbeb11555544c67b72e";

        [Test]
        public void ShouldGenerateFromMnemonic()
        {
            CardanoHDWallet hdWallet = new CardanoHDWallet(mnemonic, string.Empty);
            Assert.AreEqual(ReferenceSeed, hdWallet.BIP39Seed);

            CardanoWallet wallet = hdWallet.GetCoinTypeWallet();

            Assert.AreEqual(ReferencePrivateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(ReferencePubKey, $"00{wallet.PublicKey.ToHexString()}");
        }


        [TestCase(0, 0, "dfa51ad409b49ea112dd6521276f4eea76427a56d1afc9fcbd1c7e2f7ee6e9a8", "004040f4b7cd1e6f6224f961a7e2aaf688b71c7ffde54500e556834f91e3000c64")]
        [TestCase(0, 1, "8b8996de84ff6a79829c53118a423a2ba9e64aa3c7c9faacf7f9e2eeda230566", "009421bf321885174aac224edc33efbaee3a55f5ff542d7f0d5f5c748d546baa29")]
        [TestCase(1, 0, "aa218a977d36e394c2898a9d3cbb65cf79a6b0f4d938492add1fa5a358605261", "00dc14afe561aa7aee54dd2c6c0b1bc2248a8879b2e66f8d911acf11798b550bc2")]
        [TestCase(1, 1, "369b169900ca50db4af1d5677cdd6ead164b732fd77ccac4c810d1459b625a26", "00be5ff2fdb41b12d08cffe965efb9dfc9bb1ca63e9e86c5ae8a34f312617f0105")]
        public void ShouldGenerateExternalFromMnemonic(int accountIndex, int walletIndex, string expectedPrivateKey, string expectedPublicKey)
        {
            IHDWallet<CardanoWallet> hdWallet = new CardanoHDWallet(mnemonic, string.Empty);
            CardanoWallet wallet = hdWallet.GetAccount((uint)accountIndex).GetExternalWallet((uint)walletIndex);

            string pubKey = $"00{wallet.PublicKey.ToHexString()}";
            string privKey = wallet.PrivateKey.ToHexString();

            Assert.AreEqual(expectedPrivateKey, privKey);
            Assert.AreEqual(expectedPublicKey, pubKey);
        }

        [TestCase(0, 0, "b0bc08374eb66b563347bc2e8e2555c0368c96f31c63d1aefe25022c422d6376", "00cb4bc10e4aefcbf3abfa89e3d7d05af4b2c4bb5b72d21900d72235f9ac47a237")]
        [TestCase(0, 1, "288add5c12072a3e47d0d4d612856fc909cdb2cac58eb5d592a6586ba53f0553", "00ff872570c4d7235e53759a9e9938617fb0462456fa3585a1baf715598ac648fa")]
        [TestCase(1, 0, "d5e68df5ca582fdf58c230646107f451c3d77f6323df20770cbd84d025b30ffd", "0001851ece4d8177877001d210fbd477ccfe61f4f5340c372977894e708180b515")]
        [TestCase(1, 1, "487dc9609545a8d35bb55d99cd6cd915aaa6b52c059a6a71ae0acc0322ae7d79", "003e3e493e4376ce65ccbecac8f18f4c051d2484cee8c751ff71bcece3a913c7c3")]
        public void ShouldGenerateInternalFromMnemonic(int accountIndex, int walletIndex, string expectedPrivateKey, string expectedPublicKey)
        {
            IHDWallet<CardanoWallet> hdWallet = new CardanoHDWallet(mnemonic, string.Empty);
            CardanoWallet wallet = hdWallet.GetAccount((uint)accountIndex).GetInternalWallet((uint)walletIndex);

            string pubKey = $"00{wallet.PublicKey.ToHexString()}";
            string privKey = wallet.PrivateKey.ToHexString();

            Assert.AreEqual(expectedPrivateKey, privKey);
            Assert.AreEqual(expectedPublicKey, pubKey);
        }
    }
}