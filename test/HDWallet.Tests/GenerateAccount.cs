using HDWallet.Core;
using HDWallet.Sample;
using NUnit.Framework;

namespace HDWallet.Tests
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            IHDWallet<BitcoinWallet> bitcoinHDWallet = new BitcoinHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            var account0 = bitcoinHDWallet.GetAccount(0);

            var depositWallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKey.ToHex());

            var depositWallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKey.ToHex());

            var changeWallet1 = account0.GetInternalWallet(0);
            Assert.AreEqual("02c1100b710dc70152106bf936cf57cfdafff5505b03790f5dcad45bcdef722921", changeWallet1.PublicKey.ToHex());


            var account1 = bitcoinHDWallet.GetAccount(1);

            var depositWallet10 = account1.GetExternalWallet(0);
            Assert.AreEqual("02b9a6e20677667522d6d710d182950dd5e67f60cee9e704e30f65395fd2cb866e", depositWallet10.PublicKey.ToHex());
            var depositWallet11 = account1.GetExternalWallet(1);
            Assert.AreEqual("02cb36da84d699131d9d9c680d94d5f8aa24a2029752b0b3a706dc4e25282493a0", depositWallet11.PublicKey.ToHex());

            var changeWallet10 = account1.GetInternalWallet(0);
            Assert.AreEqual("020edfe3b196fa8f853887221fd72edc343b168487437dba01b0047fccf96818c8", changeWallet10.PublicKey.ToHex());
            var changeWallet11 = account1.GetInternalWallet(1);
            Assert.AreEqual("02786e8b010eaeb52f22ce7a59c98741299847f1fa9d2bd3ba0bf10a48d613e35e", changeWallet11.PublicKey.ToHex());
        }
    }
}