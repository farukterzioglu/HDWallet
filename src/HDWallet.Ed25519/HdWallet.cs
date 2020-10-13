using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Ed25519
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
                PrivateKey = privateKey.ToBytes()
            };
        }

        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            return new TWallet()
            {
                PrivateKey = _masterKey.PrivateKey.ToBytes(),
                Index = accountIndex
            };
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint index)
        {
            var externalKeyPath = new KeyPath($"{index}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{index}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(index, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}