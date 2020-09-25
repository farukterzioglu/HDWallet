namespace HDWallet.Sample
{
    public class BitcoinHDWallet : HDWallet<BitcoinWallet>
    {
        private static readonly Path _path = new Path(purpose: Purpose.BIP44, coinType: CoinType.Bitcoin);

        public BitcoinHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new NullAddressGenerator();
        }
    }
}