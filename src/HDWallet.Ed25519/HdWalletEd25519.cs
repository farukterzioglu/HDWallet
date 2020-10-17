using System;
using dotnetstandard_bip32;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Ed25519
{
    public abstract class HdWalletEd25519<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {   
        BIP32 bip32 = new BIP32();
        TWallet _coinTypeWallet;

        protected HdWalletEd25519(string seed, CoinPath path) : base(seed)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            var derivePath = bip32.DerivePath(path.ToString(), this.BIP39Seed);

            _coinTypeWallet = new TWallet() {
                PrivateKey = derivePath.Key
            };
        }

        protected HdWalletEd25519(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns wallet for m/[PURPOSE]'/[COINTYPE]' for constructor parameter 'path' (CoinPath)
        /// </summary>
        /// <returns>TWallet</returns>
        public TWallet GetCoinTypeWallet()
        {
            return _coinTypeWallet;
        }

        public TWallet GetWallet(string path)
        {
            var derivePath = bip32.DerivePath(path, this.BIP39Seed);

            return new TWallet() {
                PrivateKey = derivePath.Key
            };
        }

        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            throw new NotImplementedException();
        }

        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            throw new NotImplementedException();
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint index)
        {
            throw new NotImplementedException();
        }
    }
}