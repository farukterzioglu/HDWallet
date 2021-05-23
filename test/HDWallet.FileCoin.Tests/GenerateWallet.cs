using System;
using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;
using NBitcoin;
using NUnit.Framework;

namespace HDWallet.FileCoin.Tests
{
    public class GenerateWallet
    {

        [Test]
        public void ShouldGenerateWalletFromMnemonic()
        {
            IHDWallet<FileCoinWallet> avaxHDWallet = new FileCoinHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn");
            var account = avaxHDWallet.GetAccount(0);

            Console.WriteLine("Address list;");
            for (var i = 0; i < 10; i++)
            {
                FileCoinWallet wallet = account.GetExternalWallet((uint)i);
                Console.WriteLine($"{wallet.PrivateKey.ToHex()} - {wallet.Address}");
            }
        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
            var priv = "3074e388727396185b22a9615ddee4368c40be3ea10db8449cdfca405633f801";

            Wallet wallet = new FileCoinWallet(priv);
            Assert.AreEqual("f16xffbfk6zhpdeeogrjucbn4degf5bs6tyw7vzqq", wallet.Address);
        }

        [Test]
        public void ShouldGenerateWalletForTestNet()
        {
            var wallet = new FileCoinWallet("3074e388727396185b22a9615ddee4368c40be3ea10db8449cdfca405633f801");
            var testNetAddress = wallet.GetAddress(Network.Testnet);
            Assert.AreEqual("f16xffbfk6zhpdeeogrjucbn4degf5bs6tyw7vzqq", wallet.Address);
            Assert.AreEqual("t16xffbfk6zhpdeeogrjucbn4degf5bs6tyw7vzqq", testNetAddress);
        }

        [Test]
        public void ShouldGenerateFujiWalletFromMnemonic()
        {
            IHDWallet<FileCoinWallet> avaxHDWallet = new FileCoinHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn", "");
            var account0 = avaxHDWallet.GetAccount(0);
            FileCoinWallet wallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("f1gdrlcunry4lagktmexhudtfxmndtlm7wijcu35a", wallet0.Address);
            Assert.AreEqual("t1vwa6kfpfmhlihikvdmd7mji3tkzwau744326twa", wallet0.GetAddress(Network.Testnet));
            
            FileCoinWallet wallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("f1fer5pooes55ght6k2msg2iqdj5yzunwvtos7zsq", wallet1.Address);
        }

        [Test]
        public void ShouldGeneratePublicKeyFromPrivateKey()
        {
            var priv = "181ff77d7286b81334b7166f290c0a509b5106c21505aba3f004bbc3bd78027b";

            Wallet wallet = new FileCoinWallet(priv);
            var pubKeyDecomp = wallet.PublicKey.Decompress();

            Assert.AreEqual(
                expected: "04e2c906303ddcc8a52c6e9e42c5c83c1e43fc4ca3e15357ae85478adbc029cacdddf97830cf46720d41e04ab4592b7db219fda58f289e1df170d34fd174672cc3",
                actual: pubKeyDecomp.ToHex()
            );

            var pubKey = new PubKey(wallet.PublicKey.ToBytes());
            Assert.AreEqual(
                expected: "04e2c906303ddcc8a52c6e9e42c5c83c1e43fc4ca3e15357ae85478adbc029cacdddf97830cf46720d41e04ab4592b7db219fda58f289e1df170d34fd174672cc3",
                actual: pubKey.Decompress().ToHex()
            );
        }
    }
}