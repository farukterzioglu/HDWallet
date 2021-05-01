using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public abstract class HDWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;

        public HDWallet(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(BIP39Seed).Derive(masterKeyPath);
        }

        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            var masterKey = new ExtKey(BIP39Seed);

            var privateKey = masterKey.PrivateKey;
            return new TWallet() {
                PrivateKey = privateKey
            };
        }

        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            return new TWallet()
            {
                PrivateKey = _masterKey.PrivateKey, 
                Index = accountIndex
            };
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{accountIndex}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(accountIndex, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TWallet> accountHDWallet = new AccountHDWallet<TWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}