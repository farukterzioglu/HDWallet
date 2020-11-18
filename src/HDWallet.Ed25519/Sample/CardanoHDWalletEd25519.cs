using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoHDWalletEd25519 : HdWalletEd25519<CardanoSampleWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.CIP1852).Coin(CoinType.Cardano);
        public CardanoHDWalletEd25519(string seed) : base(seed, _path) {}
        public CardanoHDWalletEd25519(string words, string seedPassword) : base(words, seedPassword, _path) {}

        // TODO: Test this 
        CardanoSampleWallet GetRewardWallet(uint accountIndex)
        {
            var externalKeyPath = $"{accountIndex}'/2'/0'";

            var rewardWallet = GetSubWallet(externalKeyPath);
            rewardWallet.Index = 0;

            return rewardWallet;
        }
    }
}