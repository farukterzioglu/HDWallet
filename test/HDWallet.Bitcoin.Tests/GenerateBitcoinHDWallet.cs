using System;
using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using HDWallet.Bitcoin;
using NUnit.Framework;

namespace HDWallet.Cardano.Tests
{
    public class GenerateBitcoinHDWallet
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";
        
        [Test]
        public void ShouldGenerateFromMnemonic()
        {
            IHDWallet<BitcoinWallet> hdWallet = new BitcoinHDWallet(mnemonic,"");
            var seed = ((HdWalletBase)hdWallet).BIP39Seed;
            Assert.AreEqual("ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2", seed);

            var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
            // var address = wallet.Address;
            var publicKey = wallet.PublicKey;
            var privateKey = wallet.PrivateKey;

            // TODO: Assert the address
            // Assert.AreEqual(expected: "5G6tXDeie2KaUgGrwkBfzApKSCm9sE5QvBm1bi1vmBZ4gcCN", actual: address);
            Assert.AreEqual(expected: "0354b36b66431bbcf41607901d1a55e083cedbc3446b9849e06bcff81d0a3b517d", actual: publicKey.ToHex());
            Assert.AreEqual(expected: "cdcbe9ad36e694b5686ec4ad937a4fdf7eba7ced829e2452f66aeff0363e5114", actual: privateKey.ToHex());
            
            // Console.WriteLine($"\nAddress: {address}");
            Console.WriteLine($"Public key: {publicKey.ToHex()}");
            Console.WriteLine($"Private key: {privateKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateFromSeed()
        {
            var seed = "ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2";
            IHDWallet<BitcoinWallet> hdWallet = new BitcoinHDWallet(seed);

            var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
            // var address = wallet.Address;
            var publicKey = wallet.PublicKey;
            var privateKey = wallet.PrivateKey;

            // TODO: Assert the address
            // Assert.AreEqual(expected: "5G6tXDeie2KaUgGrwkBfzApKSCm9sE5QvBm1bi1vmBZ4gcCN", actual: address);
            Assert.AreEqual(expected: "0354b36b66431bbcf41607901d1a55e083cedbc3446b9849e06bcff81d0a3b517d", actual: publicKey.ToHex());
            Assert.AreEqual(expected: "cdcbe9ad36e694b5686ec4ad937a4fdf7eba7ced829e2452f66aeff0363e5114", actual: privateKey.ToHex());
        }
    }
}