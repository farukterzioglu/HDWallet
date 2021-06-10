using NUnit.Framework;
using HDWallet.Core;
using HDWallet.Secp256k1.Sample;

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
            
            IHDWallet<Secp256k1Wallet> wallet = new Secp256k1HDWallet(words, seedPassword: string.Empty);
            var account = wallet.GetMasterWallet();

            var privateKeyHex = account.PrivateKey.ToHex();
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            Assert.AreEqual("945ee333591e6a709ed574a7ceba0bc09f650a7822ba0c2b7f5c8a5ead295374", privateKeyHex);
        }
    }
}