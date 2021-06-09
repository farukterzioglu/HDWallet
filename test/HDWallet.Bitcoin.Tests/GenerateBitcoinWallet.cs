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
            var privateKey = "8e99d556c33b20039a17051a75cd86bb26bf907799730385d691ffb965ed03da";
            var publicKey = "03b01d779db39718cc37baf21d596720785c008c6ad49fc404b5396ece75161426";
            base.ShouldGenerateFromPrivateKey<BitcoinWallet>(privateKey, publicKey);
        }
    }
}