using System;
using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.FileCoin.Tests
{
    public class GenerateHDWallet
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn";
            IHDWallet<FileCoinWallet> wallet = new FileCoinHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); 
            Assert.AreEqual("efdeb7af4fddd16982be22789ebf0842963df505e79fd4bb723d77179a1938ae", account0wallet0.PrivateKey.ToHex());

            // Account Extended Private Key for m/44'/461'/0';
            var accountExtendedPrivateKey = "xprv9yELkvBV1wjRSMoTeGuhQaxwLUwdoRskUQjnHP4cy83mkfkyyrs9P9ax3mRg7P1M7XG5heBy2M9e5huLEmQnnub24EMmTDUoyt3pgW8SvcK";
            IAccount<FileCoinWallet> accountHDWallet = FileCoinHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey, 0);
            
            // m/44'/461'/0'/0/0
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("efdeb7af4fddd16982be22789ebf0842963df505e79fd4bb723d77179a1938ae", depositWallet0.PrivateKey.ToHex());
            Assert.AreEqual(account0wallet0.PrivateKey, depositWallet0.PrivateKey);

            Assert.AreEqual("02495816397ce56afd5345ba60ae6eb52d3f27f586df536ebc2632b8698be5387c", depositWallet0.PublicKey);
            Assert.AreEqual(account0wallet0.PublicKey, depositWallet0.PublicKey);
        }

        [Test]
        public void ShouldCreateAddressFromMnemonic()
        {
            string words = "conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch";
            IHDWallet<FileCoinWallet> wallet = new FileCoinHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); 
            Assert.AreEqual("f1d5m6e523gek3jsbn6kl7bsurd2vyuvs6wyvnmyy", account0wallet0.Address);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9yELkvBV1wjRSMoTeGuhQaxwLUwdoRskUQjnHP4cy83mkfkyyrs9P9ax3mRg7P1M7XG5heBy2M9e5huLEmQnnub24EMmTDUoyt3pgW8SvcK";
            IAccount<FileCoinWallet> accountHDWallet = FileCoinHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey, 0);
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("f1gdrlcunry4lagktmexhudtfxmndtlm7wijcu35a", depositWallet0.Address);
        }
    }
}