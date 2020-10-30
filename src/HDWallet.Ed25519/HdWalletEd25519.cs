using System;
using dotnetstandard_bip32;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public abstract class HdWalletEd25519<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {   
        BIP32 bip32 = new BIP32();
        TWallet _coinTypeWallet;

        string _path;

        protected HdWalletEd25519(string seed) : base(seed) {}

        protected HdWalletEd25519(string seed, string path) : base(seed)
        {
            _path = path;

            var derivePath = bip32.DerivePath(path.ToString(), this.BIP39Seed);

            _coinTypeWallet = new TWallet() {
                PrivateKey = derivePath.Key
            };
        }
        protected HdWalletEd25519(string seed, CoinPath path) : this(seed, path.ToString()) {}

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="words"></param>
        /// <param name="seedPassword"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected HdWalletEd25519(string words, string seedPassword, string path) : base(words, seedPassword)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="words"></param>
        /// <param name="seedPassword"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected HdWalletEd25519(string words, string seedPassword, CoinPath path) : this(words, seedPassword, path.ToString()) {}

        /// <summary>
        /// Returns wallet for m/[PURPOSE]'/[COINTYPE]' for constructor parameter 'path' (CoinPath)
        /// </summary>
        /// <returns>TWallet</returns>
        public TWallet GetCoinTypeWallet()
        {
            if(_coinTypeWallet == null) throw new NullReferenceException(nameof(_coinTypeWallet));
            return _coinTypeWallet;
        }

        public TWallet GetWalletFromPath(string path)
        {
            var derivePath = bip32.DerivePath(path, this.BIP39Seed);

            return new TWallet() {
                PrivateKey = derivePath.Key
            };
        }

        public TWallet GetSubWallet(string subPath)
        {
            var keyPath = $"{_path}/{subPath}";
            var derivePath = bip32.DerivePath(keyPath, this.BIP39Seed);

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

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            Func<string, TWallet> deriveFunction = GetWalletFromPath;

            return new Account<TWallet>(accountIndex, GetSubWallet );
        }
    }
}