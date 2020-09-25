using NUnit.Framework;
using NBitcoin;
using System;

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
            var account = wallet.GetMasterDepositWallet();
            // var account = wallet.GetAccount(0).GetExternalWallet(0);

            var privateKeyHex = account.PrivateKey.ToHex();
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            Assert.AreEqual("4065a841ab4fed510897ef5b47a7851b96428ac39081d3f88b9b3207a22b5383", privateKeyHex);
            Assert.AreEqual("0485dd9d7cff0d74b6d7a96d7b266b29f1518253c186ae8e8cdca9d7bd9c84095fbd40aca6346577a2eb494de76f9e7cb3648110333ffc46ee17bef23974010462", publicKeyHex);
            Assert.AreEqual("TQJCCht7HXJNDaU1ReVGTK5Fx3wWL8StZ2", account.Address);
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