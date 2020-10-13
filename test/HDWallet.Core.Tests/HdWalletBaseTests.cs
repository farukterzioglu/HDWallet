using NUnit.Framework;

namespace HDWallet.Core.Tests
{
    class SampleWallet : HdWalletBase
    {
        public SampleWallet(string words, string seedPassword) : base(words, seedPassword) {}
    }

    public class HdWalletBaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldCreateFromMnemonic()
        {
            var wallet = new SampleWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor", "");
            Assert.AreEqual("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909", wallet.BIP39Seed);
        }

        [Test]
        public void ShouldCreateFromMnemonicAndPassword()
        {
            var wallet = new SampleWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor", "passphrase");
            Assert.AreEqual("256a23851bd21dbf38c1d38d69ec386eaf402c01fc76ce72de69df093c8e8fbf1caf3f77e09bd76e725020309d57f0ea4c71ecee69b548fe34249305172ded82", wallet.BIP39Seed);
        }
    }
}