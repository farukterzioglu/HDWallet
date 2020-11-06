using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    /// <summary>
    /// Test HD Wallet with purpose=0 & coinType = 1
    /// </summary>
    public class PathTestHDWalletEd25519 : HdWalletEd25519<CardanoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.PURPOSE0).Coin(CoinType.CoinType1);

        public PathTestHDWalletEd25519(string seed) : base(seed, _path) {}
    }
}