using HDWallet.Sample;
using NUnit.Framework;

namespace HDWallet.Tests
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            IHDWallet bitcoinHDWallet = new BitcoinHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            var account = bitcoinHDWallet.GetAccount(0);

            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKey.ToHex());

            var depositWallet1 = account.GetExternalWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKey.ToHex());
        }
    }
}