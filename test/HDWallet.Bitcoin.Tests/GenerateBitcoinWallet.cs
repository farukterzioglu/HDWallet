using System;
using HDWallet.Core;
using HDWallet.Bitcoin;
using NUnit.Framework;
using HDWallet.Secp256k1;

namespace HDWallet.Cardano.Tests
{
    public abstract class GenerateSecp256k1Wallet
    {
        public void ShouldGenerateFromPrivateKey<TWallet>(string privateKey, string expectedPublicKeyHex) where TWallet : Wallet
        {
            Wallet wallet = (Wallet)Activator.CreateInstance(typeof(TWallet), new object[] { privateKey });
            Assert.AreEqual(expectedPublicKeyHex, wallet.PublicKey.ToBytes().ToHexString());
        }
    }

    public class GenerateBitcoinWallet : GenerateSecp256k1Wallet
    {
        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5";
            var publicKey = "278117fc144c72340f67d0f2316e8386ceffbf2b2428c9c51fef7c597f1d426e";
            base.ShouldGenerateFromPrivateKey<BitcoinWallet>(privateKey, publicKey);
        }
    }
}