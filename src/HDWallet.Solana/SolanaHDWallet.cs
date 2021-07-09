using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Solana
{
    public class SolanaHdWallet : HdWalletEd25519<SolanaWallet>, IHDWallet<SolanaWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Solana);

        public SolanaHdWallet(string seed) : base(seed, _path) { }
        public SolanaHdWallet(string words, string seedPassword) : base(words, seedPassword, _path) { }
    }
}