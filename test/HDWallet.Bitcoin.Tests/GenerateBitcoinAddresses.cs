using HDWallet.Core;
using NUnit.Framework;
using System;

namespace HDWallet.Bitcoin.Tests
{
    public class GenerateBitcoinAddresses
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";

        [Test]
        public void ShouldGenerateDepositAddress()
        {
            IHDWallet<BitcoinWallet> hdWallet = new BitcoinHDWallet(mnemonic, "");
            var seed = ((HdWalletBase)hdWallet).BIP39Seed;
            Assert.AreEqual("ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2", seed);

            var walletDeposit = hdWallet.GetAccount(0).GetExternalWallet(0);
            var publicKey = walletDeposit.PublicKey;

            var depositAddress = walletDeposit.Address;

            Assert.AreEqual(expected: "bc1qpznrn52ajq3uqf3ckmfsqdp2wspaggpl47ul3d", actual: depositAddress);

            Console.WriteLine($"Public key: {publicKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateExtendedPrivateKey()
        {
            var privateKey = "xprv9yMdbZB9vP8EPKaUZveRTJUuipCJEu4nJG5Dqih2Gi8LrunoNAGXfMeHBginxcpCtD3M6NJb64rfncoB76YeVhWshoZSb1gBdCrMRWLzxFs";
            var hdWallet = BitcoinHDWallet.GetAccountFromMasterKey(privateKey, 0);
            var walletDeposit = hdWallet.GetExternalWallet(0);

            var publicKey = walletDeposit.PublicKey;
            var depositAddress = walletDeposit.Address;
            Assert.AreEqual(expected: "bc1qpznrn52ajq3uqf3ckmfsqdp2wspaggpl47ul3d", actual: depositAddress);

            Console.WriteLine($"Public key: {publicKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "cdcbe9ad36e694b5686ec4ad937a4fdf7eba7ced829e2452f66aeff0363e5114";
            var bitcoinWallet = new BitcoinWallet(privateKey);

            var pubKey = bitcoinWallet.PublicKey;
            Assert.AreEqual(expected: "0354b36b66431bbcf41607901d1a55e083cedbc3446b9849e06bcff81d0a3b517d", actual: pubKey.ToString());

            var walletAddress = bitcoinWallet.Address;
            Assert.AreEqual(expected: "bc1qpznrn52ajq3uqf3ckmfsqdp2wspaggpl47ul3d", actual: walletAddress);
        }

        [Test]
        public void ShouldGenerateChangeAddress()
        {
            var privateKey = "xprv9yMdbZB9vP8EPKaUZveRTJUuipCJEu4nJG5Dqih2Gi8LrunoNAGXfMeHBginxcpCtD3M6NJb64rfncoB76YeVhWshoZSb1gBdCrMRWLzxFs";
            var hdWallet = BitcoinHDWallet.GetAccountFromMasterKey(privateKey, 0);
            var walletDeposit = hdWallet.GetInternalWallet(0);

            var publicKey = walletDeposit.PublicKey;
            var depositAddress = walletDeposit.Address;
            Assert.AreEqual(expected: "bc1qdynrthkttgqkazjjgpd2qhh37868nvgytvgq50", actual: depositAddress);

            Console.WriteLine($"Public key: {publicKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateTestnetAddress()
        {
            var privateKey = "xprv9yMdbZB9vP8EPKaUZveRTJUuipCJEu4nJG5Dqih2Gi8LrunoNAGXfMeHBginxcpCtD3M6NJb64rfncoB76YeVhWshoZSb1gBdCrMRWLzxFs";
            var hdWallet = BitcoinHDWallet.GetAccountFromMasterKey(privateKey, 0);
            var walletDeposit = hdWallet.GetInternalWallet(0);

            var publicKey = walletDeposit.PublicKey;
            var depositAddress = walletDeposit.GetAddress(NetworkType.Testnet);
            Assert.AreEqual(expected: "tb1qdynrthkttgqkazjjgpd2qhh37868nvgyp2nn0u", actual: depositAddress);

            Console.WriteLine($"Public key: {publicKey.ToHex()}");
        }
    }
}