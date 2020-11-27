using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Polkadot
{
    public class PolkadotHDWallet : HdWalletEd25519<PolkadotWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Polkadot);

        internal PolkadotHDWallet(string seed, HDWallet.Core.CoinPath path) : base(seed, _path) {}
        public PolkadotHDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }
}