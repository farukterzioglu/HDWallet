using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheHDWallet : HDWallet<AvalancheWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Avalanche);

        public AvalancheHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) {}
    }
}
