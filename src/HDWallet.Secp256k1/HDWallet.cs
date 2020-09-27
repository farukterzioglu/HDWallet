using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public class HDWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;

        // TODO: Test this
        public HDWallet(ExtKey extKey)
        {
            _masterKey = extKey;
        }

        public HDWallet(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
        {
            var masterKeyPath = new KeyPath(path.ToString());
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