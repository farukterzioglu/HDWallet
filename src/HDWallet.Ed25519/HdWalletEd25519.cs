using System;
using dotnetstandard_bip32;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public abstract class HdWalletEd25519Base : HdWalletBase
    {
        protected BIP32 bip32 = new BIP32();

        protected HdWalletEd25519Base(string seed) : base(seed){}
        protected HdWalletEd25519Base(string words, string seedPassword) : base(words, seedPassword){}

        public TWallet GetWalletFromPath<TWallet>(string path) where TWallet : Wallet, new()
        {
            var derivePath = bip32.DerivePath(path, this.BIP39Seed);

            return new TWallet() {
                Path = path,
                PrivateKey = derivePath.Key
            };
        }

    }

    public abstract class HdWalletEd25519<TWallet> : HdWalletEd25519Base, IHDWallet<TWallet> where TWallet : Wallet, IWallet, new()
    {
        TWallet _coinTypeWallet;

        string _path;

        [Obsolete("Only for testing")]
        protected HdWalletEd25519(string seed) : base(seed) {}

        protected HdWalletEd25519(string seed, string path) : base(seed)
        {
            _path = path;

            var derivePath = bip32.DerivePath(path, this.BIP39Seed);

            _coinTypeWallet = new TWallet() {
                PrivateKey = derivePath.Key
            };
        }
        protected HdWalletEd25519(string seed, CoinPath path) : this(seed, path.ToString()) {}

        protected HdWalletEd25519(string words, string seedPassword, string path) : base(words, seedPassword)
        {
            _path = path;

            var derivePath = bip32.DerivePath(path, this.BIP39Seed);

            _coinTypeWallet = new TWallet() {
                PrivateKey = derivePath.Key
            };
        }
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

        public TWallet GetSubWallet(string subPath)
        {
            var keyPath = $"{_path}/{subPath}";
            return GetWalletFromPath<TWallet>(keyPath);
        }

        public TWallet GetMasterWallet()
        {
            return GetWalletFromPath<TWallet>(_path);
        }

        public TWallet GetAccountWallet(uint accountIndex)
        {
            var keyPath = $"{_path}/{accountIndex}'";
            return GetWalletFromPath<TWallet>(keyPath);
        }

        public IAccount<TWallet> GetAccount(uint accountIndex)
        {
            Func<string, TWallet> deriveFunction = GetWalletFromPath<TWallet>;
            return new Account<TWallet>(accountIndex, GetSubWallet);
        }
    }
}