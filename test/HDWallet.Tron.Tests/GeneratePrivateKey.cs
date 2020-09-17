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
        public void Test1()
        {
            string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
            IHdWallet wallet = new TronHDWallet(words);
            var account = wallet.GetSeedAccount();

            var privateKeyHex = account.PrivateKey.ToHex();
            Assert.AreEqual("945ee333591e6a709ed574a7ceba0bc09f650a7822ba0c2b7f5c8a5ead295374", privateKeyHex);
            
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            Assert.AreEqual("043be47cf1c281f4843cb6cc991c91d9122dde87ac42ee8bc393ee0033988fc6f15372cb24f8885d5d29dba94a9f761c44a8bb0a5dd6f6856d925921a2c3386e6f", publicKeyHex);

            Assert.AreEqual("TNzDgpvjv48DGMGrendez5LsCn4nwjgLHx", account.Address);

            account = wallet.GetAccount(0);
            privateKeyHex = account.PrivateKey.ToHex();
            // TODO Assert

            publicKeyHex = account.PublicKey.ToHex();
            // TODO Assert pubkey
            // TODO Assert address
        }
    }
}