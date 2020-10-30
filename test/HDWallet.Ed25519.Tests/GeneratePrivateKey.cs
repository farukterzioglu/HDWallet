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
        // TODO
        // [SetUp]
        // public void Setup()
        // {
        // }

        // [Test]
        // public void ShouldGenerateMasterPrivateKeys()
        // {
        //     string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
        //     IHDWallet<CardanoWallet> wallet = new _CardanoHDWallet(words);
        //     var account = wallet.GetMasterWallet();

        //     var privateKeyBytes = account.PrivateKey;
        //     var privateKeyHex = account.PrivateKey.ToHexString();
            
        //     var message = Encoding.UTF8.GetBytes("159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145");
        //     var messageHash = new Sha3Keccack().CalculateHash(message);
        //     var signature = account.Sign(messageHash);

        //     var signatureBytes = Helper.Concat(signature.R, signature.S);
        //     var validationResult = Signer.Validate(signatureBytes, messageHash, account.PublicKey);

        //     Assert.IsTrue(validationResult);
        // }

        // [Test]
        // public void ShouldSignMessageHash()
        // {
        //     string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
        //     IHDWallet<CardanoWallet> wallet = new _CardanoHDWallet(words);
        //     var account = wallet.GetMasterWallet();
            
        //     var message = Encoding.UTF8.GetBytes("159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145");
        //     var messageHash = new Sha3Keccack().CalculateHash(message);
        //     var signature = account.Sign(messageHash);

        //     var signatureBytes = Helper.Concat(signature.R, signature.S);
        //     var validationResult = Signer.Validate(signatureBytes, messageHash, account.PublicKey);

        //     Assert.IsTrue(validationResult);
        // }

        // [Test]
        // public void ShouldSignMessage()
        // {
        //     var message = Encoding.UTF8.GetBytes("This is a test message.");

        //     string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
        //     IHDWallet<CardanoWallet> wallet = new _CardanoHDWallet(words);
        //     CardanoWallet account = wallet.GetMasterWallet();

        //     var signature = account.Sign(message);
        //     var signatureBytes = Helper.Concat(signature.R, signature.S);
        //     Assert.That(signatureBytes.Length, Is.EqualTo(64));

        //     var validationResult = Signer.Validate(signatureBytes, message, account.PublicKey);
        //     Assert.IsTrue(validationResult);
        // }
    }
}