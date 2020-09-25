using NUnit.Framework;
using NBitcoin;
using System;
using HDWallet.Sample;

namespace HDWallet.Tests
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
            
            IHDWallet<BitcoinWallet> wallet = new BitcoinHDWallet(words);
            var account = wallet.GetMasterDepositWallet();
            // var account = wallet.GetAccount(0).GetExternalWallet(0);

            var privateKeyHex = account.PrivateKey.ToHex();
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            Assert.AreEqual("17454e5aed7c41c1a16bd79f0fd0ae50c309f94278830cff96bc75a5dcb74778", privateKeyHex);
        }
    }
}