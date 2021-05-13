using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Polkadot
{
    public class KusamaHDWallet : HdWalletEd25519<PolkadotWallet>, IHDWallet<PolkadotWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Kusama);

        public KusamaHDWallet(string seed) : base(seed, _path) {}
        public KusamaHDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }

    public class PolkadotHDWallet : HdWalletEd25519<PolkadotWallet>, IHDWallet<PolkadotWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Polkadot);

        public PolkadotHDWallet(string seed) : base(seed, _path) {}
        public PolkadotHDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }
}