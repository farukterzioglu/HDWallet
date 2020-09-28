using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            IHDWallet<CardanoWallet> hdWallet = new CardanoHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            var account0 = hdWallet.GetAccount(0);

            var depositWallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("547ad18a1e3e3e3d4ff10b23e47e6795e180a18b213a1c894909cb5bf6d47f6c", depositWallet0.PublicKey.ToHexString());

            var depositWallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("d081709b367a8cb21d6ebb3b7ee40a4069f15d38e3a6db595ae0df8c7e86c9ed", depositWallet1.PublicKey.ToHexString());

            var account1 = hdWallet.GetAccount(1);
            var depositWallet10 = account1.GetExternalWallet(0);
            Assert.AreEqual("575f47e7ebbab331c83b5ce69ee8baa469d0eb18404627dc7659ab44400a1810", depositWallet10.PublicKey.ToHexString());
        }
    }
}