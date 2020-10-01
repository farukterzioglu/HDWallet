using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoHDWallet : HDWallet<CardanoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Cardano);
        public CardanoHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path, new NullAddressGenerator()) {}
    }
}