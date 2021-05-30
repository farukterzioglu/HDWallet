using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Tron.Tests
{
    public class GenerateWallet
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
            IHDWallet<TronWallet> tronHDWallet = new TronHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
            var account0 = tronHDWallet.GetAccount(0);
            TronWallet wallet0 = account0.GetExternalWallet(0);

            Assert.AreEqual("fa0a0d3dcd475a04d99cf777dc166e2160f88fbd1c8bdeca74bdffb61430e7d9", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ",wallet0.Address );

            var tronWallet = new TronWallet("fa0a0d3dcd475a04d99cf777dc166e2160f88fbd1c8bdeca74bdffb61430e7d9");
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), tronWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.Address, tronWallet.Address);
        }
    }
}