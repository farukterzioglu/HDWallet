using NUnit.Framework;
using NBitcoin;
using System;
using HDWallet.Core;

namespace HDWallet.Tron.Tests
{
    public class GeneratePrivateKey
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldGenerateMasterPrivateKeys()
        {
            string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
            IHDWallet<TronWallet> wallet = new TronHDWallet(words);
            var account = wallet.GetMasterWallet();

            var privateKeyHex = account.PrivateKey.ToHex();
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            Assert.AreEqual("945ee333591e6a709ed574a7ceba0bc09f650a7822ba0c2b7f5c8a5ead295374", privateKeyHex);
            Assert.AreEqual("043be47cf1c281f4843cb6cc991c91d9122dde87ac42ee8bc393ee0033988fc6f15372cb24f8885d5d29dba94a9f761c44a8bb0a5dd6f6856d925921a2c3386e6f", publicKeyHex);
            Assert.AreEqual("TNzDgpvjv48DGMGrendez5LsCn4nwjgLHx", account.Address);
        }

        [TestCase("push wrong tribe amazing again cousin hill belt silent found sketch monitor", "TWroNNekzseGNC6x1BHGd5H7f9b9u6mdHE")]
        [TestCase("treat nation math panel calm spy much obey moral hazard they sorry", "TEFccUNfgWyjuiiUo9LfNSb56jLhBo7pCV")]
        [TestCase("million caught suspect silk lady pond tribe regret vacuum pigeon annual ordinary", "TMxPPqB7y7rhoLrUWp4JoMMsgBaeckJG66")]
        public void ShouldGenerateFromSeed(string words, string address)
        {
            IHDWallet<TronWallet> wallet = new TronHDWallet(words);
            var wallet0 = wallet.GetAccount(0).GetExternalWallet(0);
            
            Assert.AreEqual(address, wallet0.Address);
        }

        [Test]
        public void ShouldGeneratePrivateKeys()
        {
            string words = "treat nation math panel calm spy much obey moral hazard they sorry";
            IHDWallet<TronWallet> tronWallet = new TronHDWallet(words);

            var wallet = tronWallet.GetAccount(0).GetExternalWallet(0);
            var privateKey = wallet.PrivateKey.ToHex();
            var address = wallet.Address;

            Assert.AreEqual("f017915411a0e7827e8f1f357c4ed2ccdcb1b1295cdb0fb0a5c13cbbd5da3734", privateKey);
            Assert.AreEqual("TEFccUNfgWyjuiiUo9LfNSb56jLhBo7pCV", address);
        }

        [Test]
        public void ShouldGeneratePrivateKeysFromAccounts()
        {
            string words = "treat nation math panel calm spy much obey moral hazard they sorry";
            IHDWallet<TronWallet> tronWallet = new TronHDWallet(words);

            var account0 = tronWallet.GetAccount(0);
            var depositWallet00 = account0.GetExternalWallet(0);

            var privateKey = depositWallet00.PrivateKey.ToHex();
            var address00 = depositWallet00.Address;

            Assert.AreEqual("f017915411a0e7827e8f1f357c4ed2ccdcb1b1295cdb0fb0a5c13cbbd5da3734", privateKey);
            Assert.AreEqual("TEFccUNfgWyjuiiUo9LfNSb56jLhBo7pCV", address00);

            var account1 = tronWallet.GetAccount(1);
            var depositWallet10 = account1.GetExternalWallet(0);

            var privateKey10 = depositWallet10.PrivateKey.ToHex();
            var address10 = depositWallet10.Address;

            Assert.AreEqual("TQYqPobCzY5sWpWF8wFYL9zmPsfzEESQ6Z", address10);
        }
    }
}