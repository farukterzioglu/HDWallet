using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoHDWalletEd25519 : HdWalletEd25519<CardanoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Cardano);
        public CardanoHDWalletEd25519(string seed) : base(seed, _path) {}
        public CardanoHDWalletEd25519(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }
}