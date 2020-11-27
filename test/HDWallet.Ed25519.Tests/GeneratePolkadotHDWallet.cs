using System;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GeneratePolkadotHDWallet
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";
        

        [TestCase("m/0'/1'")]
        public void ShouldGenerateMasterWalletFromPurposeAndPath(string path)
        {
            TestHDWalletEd25519 hdWallet = new TestHDWalletEd25519(mnemonic, "", path);
            var coinTypeWallet = hdWallet.GetCoinTypeWallet();

            Console.WriteLine($"Public key: {coinTypeWallet.PublicKey.ToHexString()}");
            Console.WriteLine($"Private key: {coinTypeWallet.PrivateKey.ToHexString()}");
        }
    }
}