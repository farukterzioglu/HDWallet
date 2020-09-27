using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet<TronWallet>
    {
        private static readonly HDWallet.Core.Coin _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Tron);

        public TronHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}