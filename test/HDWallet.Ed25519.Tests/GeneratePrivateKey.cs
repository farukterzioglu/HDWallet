using NUnit.Framework;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Text;
using Ed25519;
using Nethereum.Util;

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
            var account = wallet.GetMasterWallet();

            var privateKeyBytes = account.PrivateKey;
            var privateKeyHex = account.PrivateKey.ToHexString();
            
            var message = Encoding.UTF8.GetBytes("159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145");
            var messageHash = new Sha3Keccack().CalculateHash(message);
            var signature = account.Sign(messageHash);
            var validationResult = Signer.Validate(signature.SignatureBytes, messageHash, account.PublicKey);

            Assert.That(validationResult, Is.True);
        }
    }
}