using NUnit.Framework;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;

namespace HDWallet.Ed25519.Tests
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
            
            IHDWallet<CardanoWallet> wallet = new CardanoHDWallet(words);
            var account = wallet.GetMasterDepositWallet();

            // var privateKeyHex = account.PrivateKey.ToHex();
            // var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            // Assert.AreEqual("17454e5aed7c41c1a16bd79f0fd0ae50c309f94278830cff96bc75a5dcb74778", privateKeyHex);
        }
    }
}