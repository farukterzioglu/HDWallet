using System;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using HDWallet.Polkadot;
using NUnit.Framework;

namespace HDWallet.Cardano.Tests
{
    public class GeneratePolkadotHDWallet
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";
        
        [Test]
        public void ShouldGenerateFromMnemonic()
        {
            IHDWallet<PolkadotWallet> hdWallet = new PolkadotHDWallet(mnemonic,"");
            var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
            var address = wallet.Address;
            var publicKey = wallet.PublicKey;
            var privateKey = wallet.PrivateKey;
            var expandedPrivateKey = wallet.ExpandedPrivateKey;

            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Public key: {publicKey.ToHexString()}");
            Console.WriteLine($"Private key: {privateKey.ToHexString()}");
            Console.WriteLine($"Expanded private key: {expandedPrivateKey.ToHexString()}");
        }
    }
}