using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using NBitcoin;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GenerateAccountHDWallet
    {
        [Test]
        public void ShouldCreateAccount()
        {
            // Account Extended Private Key for m/44'/0'/0' of mnemonic;
            // conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch
            // Checked from https://iancoleman.io/bip39
            var accountExtendedPrivateKey = "xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy";

            IAccountHDWallet<BitcoinWallet> accountHDWallet = new AccountHDWallet<BitcoinWallet>(accountExtendedPrivateKey, 0);
            
            // m/44'/0'/0'/0/0
            var depositWallet0 = accountHDWallet.GetAccount().GetExternalWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKey.ToHex());

            // m/44'/0'/0'/0/1
            var depositWallet1 = accountHDWallet.GetAccount().GetExternalWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKey.ToHex());
        }
    }
}