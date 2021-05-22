using System;
using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;
using NUnit.Framework;

namespace HDWallet.FileCoin.Tests
{
    public class GenerateWallet
    {

        [Test]
        public void ShouldGenerateWalletFromMnemonic()
        {
            IHDWallet<FileCoinWallet> avaxHDWallet = new FileCoinHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn");
            var account0 = avaxHDWallet.GetAccount(0);
            FileCoinWallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("X-avax1as0rhx4ejjfm3vzmhxycs58v4lu4u9h7amfqmx",wallet0.Address );

            var avalancheWallet = new FileCoinWallet("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d");
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), avalancheWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.Address, avalancheWallet.Address);

            var account = avaxHDWallet.GetAccount(0);
            Console.WriteLine("Address list;");
            for (var i = 0; i < 10; i++)
            {
                FileCoinWallet wallet = account.GetExternalWallet((uint)i);
                Console.WriteLine($"{wallet.PrivateKey.ToHex()} - {wallet.Address}");
            }
        }

        [Test]
        public void ShouldGenerateWalletForMainnet()
        {
            var avalancheWallet = new FileCoinWallet("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d");
            // Assert.AreEqual("X-avax1as0rhx4ejjfm3vzmhxycs58v4lu4u9h7amfqmx", avalancheWallet.GetAddress(Networks.Mainnet, Chain.X));
        }

        [Test]
        public void ShouldGenerateFujiWalletFromMnemonic()
        {
            IHDWallet<FileCoinWallet> avaxHDWallet = new FileCoinHDWallet("wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn", "");
            var account0 = avaxHDWallet.GetAccount(0);
            FileCoinWallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", wallet0.PrivateKey.ToHex());
            // Assert.AreEqual("X-fuji1as0rhx4ejjfm3vzmhxycs58v4lu4u9h73fdlhe",wallet0.GetAddress(Networks.Fuji, Chain.X) );

            var account = avaxHDWallet.GetAccount(0);
            Console.WriteLine("Address list;");
            for (var i = 0; i < 10; i++)
            {
                FileCoinWallet wallet = account.GetExternalWallet((uint)i);
                // Console.WriteLine($"{wallet.PrivateKey.ToHex()} - {wallet.GetAddress(Networks.Fuji)}");
                Console.WriteLine(wallet.Address);
            }
        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
            // 6ECPCno3PrdpMbm6bRUtaTs7BW5vvp1EHPbleBlp0sI=
            // var priv = "e8408f0a7a373eb76931b9ba6d152d693b3b056e6fbe9d441cf6e5781969d2c2";

            // var priv = "51a738ee884ccc8829deb85d0b248e6ea62dd3966b0d612efda628df78dbc61c";
            // byte[] bytes = Encoding.ASCII.GetBytes(priv);
            // var hexPriv = bytes.ToHexString();
            
            // e8408f0a7a373eb76931b9ba6d152d693b3b056e6fbe9d441cf6e5781969d2c2 // decoded
            // 6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d // ref
            // 7b2254797065223a22736563703235366b31222c22507269766174654b6579223a22
            // 6170707a474e4f6b69706e4b363357726d64435a67354a4a50726235786459375842553876556533612b55
            // 7b2254797065223a22736563703235366b31222c22507269766174654b6579223a226170707a474e4f6b69706e4b363357726d64435a67354a4a50726235786459375842553876556533612b553d227d
            // 7b2254797065223a22736563703235366b31222c22507269766174654b6579223a2236454350436e6f33507264704d626d36625255746154733742573576767031454850626c65426c703073493d227d


            // MHTjiHJzlhhbIqlhXd7kNoxAvj6hDbhEnN/KQFYz+AE=
            var priv = "3074e388727396185b22a9615ddee4368c40be3ea10db8449cdfca405633f801";
            // f1jjw3j45q5gsl5r6zjwr4b6r3peg53mhk3frm33i

            Wallet wallet = new FileCoinWallet(priv);
            Assert.AreEqual("f1jjw3j45q5gsl5r6zjwr4b6r3peg53mhk3frm33i", wallet.Address);
        }
    }
}