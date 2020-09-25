namespace HDWallet.Sample
{
    public class BitcoinHDWallet : HDWallet<BitcoinWallet>
    {
        private static readonly Coin _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Bitcoin);

        public BitcoinHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new NullAddressGenerator();
        }
    }
}