using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public abstract class HDWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;

        public HDWallet(string words, string seedPassword, CoinPath path, IAddressGenerator addressGenerator) : base(words, seedPassword, addressGenerator)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(BIP39Seed).Derive(masterKeyPath);
        }

        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            var masterKey = new ExtKey(BIP39Seed);

            var privateKey = masterKey.PrivateKey;
            return new TWallet() {
                PrivateKey = privateKey,
                AddressGenerator = AddressGenerator
            };
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{accountIndex}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(accountIndex, AddressGenerator, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}