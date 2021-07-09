using HDWallet.Core;
using NUnit.Framework;
using System;

namespace HDWallet.Ethereum.Tests
{
    public class GenerateEthereumAddresses
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";

        [Test]
        public void ShouldGenerateDepositAddress()
        {
            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic, "");
            var seed = ((HdWalletBase)hdWallet).BIP39Seed;
            Assert.AreEqual("ba78b733ffe929e400f844751a48dded5ebc7c62635a1590e97b066e3b9e8b890741602a69279c45ed5d17dfd6e8703e3c575de4ea4712868df5f1997e2b97b2", seed);

            var walletDeposit = hdWallet.GetAccount(0).GetExternalWallet(0);
            var publicKey = walletDeposit.PublicKey;
            var depositAddress = walletDeposit.Address;

            Assert.AreEqual(expected: "0xA2ae76fb87C154580a034c116115cE39441Add6F", actual: depositAddress);
            Console.WriteLine($"Public key[0]: {publicKey.ToHex()}");

            walletDeposit = hdWallet.GetAccount(0).GetExternalWallet(1);
            publicKey = walletDeposit.PublicKey;
            depositAddress = walletDeposit.Address;

            Assert.AreEqual(expected: "0x95820f30d528a41AC753D44702c3073b5Bdc806c", actual: depositAddress);
            Console.WriteLine($"Public key[1]: {publicKey.ToHex()}");

            walletDeposit = hdWallet.GetAccount(0).GetExternalWallet(2);
            publicKey = walletDeposit.PublicKey;
            depositAddress = walletDeposit.Address;

            Assert.AreEqual(expected: "0xCa32f209805C851a4c7D964Dcb7D82F66048F207", actual: depositAddress);
            Console.WriteLine($"Public key[2]: {publicKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateExtendedPrivateKey()
        {
            var privateKey = "xprv9y4J1KoATgJXvDA4qjyZKnfHFR1SEWjYS8qufHbALT9tNmjcZ8LKrmYgqgdgAijM35kWT9KGccPChj1qvWsE94XgJQRKk6rykmgQVfsjDh4";
            var hdWallet = EthereumHDWallet.GetAccountFromMasterKey(privateKey, 0);
            var walletDeposit = hdWallet.GetExternalWallet(0);

            var publicKey = walletDeposit.PublicKey;
            var depositAddress = walletDeposit.Address;
            Assert.AreEqual(expected: "0xA2ae76fb87C154580a034c116115cE39441Add6F", actual: depositAddress);

            Console.WriteLine($"Public key: {publicKey.ToHex()}");
        }

        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "ca5edb4b5db35fc6677d9c6fe223afc3343014653b159ae4291f543a78761c87";
            var ethereumWallet = new EthereumWallet(privateKey);

            var pubKey = ethereumWallet.PublicKey;
            Assert.AreEqual(expected: "02ba33e9bba01836a3c5c8c4aa70abb16ccffc66470d40def867a0d66fa3d40c27", actual: pubKey.ToString());

            var walletAddress = ethereumWallet.Address;
            Assert.AreEqual(expected: "0xA2ae76fb87C154580a034c116115cE39441Add6F", actual: walletAddress);
        }
    }
}