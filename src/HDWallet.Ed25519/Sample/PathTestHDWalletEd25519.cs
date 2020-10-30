using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class PathTestHDWalletEd25519 : HdWalletEd25519<CardanoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.PURPOSE0).Coin(CoinType.CoinType1);

        public PathTestHDWalletEd25519(string seed) : base(seed, _path) {}
    }
}