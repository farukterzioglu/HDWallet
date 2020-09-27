using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet
{
    public class HDWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;

        // TODO: Test this
        public HDWallet(ExtKey extKey)
        {
            _masterKey = extKey;
        }

        public HDWallet(string words, string seedPassword, HDWallet.Core.Coin path) : base(words, seedPassword, path)
        {
            var masterKeyPath = new KeyPath(path.CurrentPath);
            _masterKey = new ExtKey(Seed).Derive(masterKeyPath);
        }

        TWallet IHDWallet<TWallet>.GetMasterDepositWallet()
        {
            var masterKey = _masterKey.Derive(new KeyPath("0'/0"));

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