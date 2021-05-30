using System;
using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Avalanche.Tests
{
    public class GenerateAccountHDWallet
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "wire sort once settle balcony bright awkward pottery derive noodle absorb combine quick account cluster dash material yard people layer fold royal add learn";
            IHDWallet<AvalancheWallet> wallet = new AvalancheHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); 
            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", account0wallet0.PrivateKey.ToHex());

            // Account Extended Private Key for m/44'/9000'/0';
            var accountExtendedPrivateKey = "xprv9ygCPYxKvwkSoQvKtcsfc4AYx7YBMWqkSZ8u7yAD1Ydz9muWdjNgZN6vdg1QBPZ9rYZdKbhPnmseYmHbJCSqkuxPJUzPHc5i6PQto4gvz6M";
            IAccount<AvalancheWallet> accountHDWallet = AvalancheHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey, 0);
            
            // m/44'/9000'/0'/0/0
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("6f5139852a78fdb4bd790a46fbb34a98cabb1a946a724917efa94a2a41d82d7d", depositWallet0.PrivateKey.ToHex());

            Assert.AreEqual(account0wallet0.PublicKey, depositWallet0.PublicKey);
        }

        [Test]
        public void ShouldCreateAddressFromMnemonic()
        {
            string words = "conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch";
            IHDWallet<AvalancheWallet> wallet = new AvalancheHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); 
            Console.WriteLine(account0wallet0.PrivateKey.ToHex());
            Assert.AreEqual("X-avax1wn9s0qlpeur87pk2ccxajlj68d5wt3tw3tts8z", account0wallet0.Address);
        }

        [Test]
        public void ShouldCreateAddressFromPrivateKey()
        {
            var wallet = new AvalancheWallet("c878c962bdebe816addda5dd12aff7f54f5bf1173c32e91dcb4441980ecd3123");
            Assert.AreEqual("X-avax1wn9s0qlpeur87pk2ccxajlj68d5wt3tw3tts8z", wallet.Address);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9ygCPYxKvwkSoQvKtcsfc4AYx7YBMWqkSZ8u7yAD1Ydz9muWdjNgZN6vdg1QBPZ9rYZdKbhPnmseYmHbJCSqkuxPJUzPHc5i6PQto4gvz6M";
            IAccount<AvalancheWallet> accountHDWallet = AvalancheHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey, 0);
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("X-avax1as0rhx4ejjfm3vzmhxycs58v4lu4u9h7amfqmx", depositWallet0.Address);
        }
    }
}