using System;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GeneratePolkadotHDWallet
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";
        

        [Test]
        public void ShouldGenerateMasterWalletFromPurposeAndPath()
        {
            TestHDWalletEd25519 hdWallet = new TestHDWalletEd25519(mnemonic, "");
            var coinTypeWallet = hdWallet.GetWalletFromPath<SampleWallet>("44'/355'/0'/0'/1'");

            Console.WriteLine($"Public key: {coinTypeWallet.PublicKey.ToHexString()}");
            Console.WriteLine($"Private key: {coinTypeWallet.PrivateKey.ToHexString()}");
        }
    }
}