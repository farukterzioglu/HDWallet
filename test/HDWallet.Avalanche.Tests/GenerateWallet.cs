using System;
using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Avalanche.Tests
{
    public class GenerateWallet
    {

        [Test]
        public void ShouldGenerateWalletFromMnemonic()
        {
            IHDWallet<AvalancheWallet> avaxHDWallet = new AvalancheHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn");
            var account0 = avaxHDWallet.GetAccount(0);
            AvalancheWallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("X-avax1as0rhx4ejjfm3vzmhxycs58v4lu4u9h7amfqmx",wallet0.Address );

            var avalancheWallet = new AvalancheWallet("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d");
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), avalancheWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.Address, avalancheWallet.Address);

            var account = avaxHDWallet.GetAccount(0);
            Console.WriteLine("Address list;");
            for (var i = 0; i < 10; i++)
            {
                AvalancheWallet wallet = account.GetExternalWallet((uint)i);
                Console.WriteLine($"{wallet.PrivateKey.ToHex()} - {wallet.Address}");
            }
        }

        [Test]
        public void ShouldGenerateFujiWalletFromMnemonic()
        {
            IHDWallet<FujiWallet> avaxHDWallet = new FujiHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn", "");
            var account0 = avaxHDWallet.GetAccount(0);
            FujiWallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("X-fuji1as0rhx4ejjfm3vzmhxycs58v4lu4u9h73fdlhe",wallet0.Address );

            var account = avaxHDWallet.GetAccount(0);
            Console.WriteLine("Address list;");
            for (var i = 0; i < 10; i++)
            {
                FujiWallet wallet = account.GetExternalWallet((uint)i);
                Console.WriteLine(wallet.Address);
            }
        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
            var avalancheWallet = new FujiWallet("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d");
            Assert.AreEqual("X-fuji1as0rhx4ejjfm3vzmhxycs58v4lu4u9h73fdlhe", avalancheWallet.Address);
        }
    }
}