using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Bitcoin
{
    public class BitcoinHDWallet : HDWallet<BitcoinWallet>, IHDWallet<BitcoinWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Bitcoin);

        public BitcoinHDWallet(string seed) : base(seed, _path) {}
        public BitcoinHDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }
}