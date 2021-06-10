using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class Secp256k1HDWallet : HDWallet<Secp256k1Wallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Bitcoin);

        public Secp256k1HDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
        public Secp256k1HDWallet(string seed) : base(seed, _path) {}
    }
}