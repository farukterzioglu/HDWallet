using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumHDWallet : HDWallet<EthereumWallet>, IHDWallet<EthereumWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Ethereum);

        public EthereumHDWallet(string seed) : base(seed, _path)
        {
        }

        public EthereumHDWallet(string words, string seedPassword) : base(words, seedPassword, _path)
        {
        }
    }
}