using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GenerateHdWallet
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ShouldGenerateHdWalletFromMnemoni()
        {
            IHDWallet<Secp256k1Wallet> tronHDWallet = new Secp256k1HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
            var account0 = tronHDWallet.GetAccount(0);
            Secp256k1Wallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("cdce32b32436ff20c2c32ee55cd245a82fff4c2dc944da855a9e0f00c5d889e4", wallet0.PrivateKey.ToHex());

            var bitcoinWallet = new Secp256k1Wallet("cdce32b32436ff20c2c32ee55cd245a82fff4c2dc944da855a9e0f00c5d889e4");
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), bitcoinWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.PublicKey, bitcoinWallet.PublicKey);
        }
    }
}