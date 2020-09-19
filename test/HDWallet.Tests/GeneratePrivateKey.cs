using HDWallet.Sample;
using NUnit.Framework;

namespace HDWallet.Tests
{
    public class GeneratePublicKey
    {
        [Test]
        public void ShouldCreateBitcoinPrivateKey()
        {
            IHDWallet bitcoinHDWallet = new BitcoinHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            var wallet0 = bitcoinHDWallet.GetWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c",wallet0.PublicKey.ToHex());

            var wallet1 = bitcoinHDWallet.GetWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c",wallet1.PublicKey.ToHex());
        }
    }
}