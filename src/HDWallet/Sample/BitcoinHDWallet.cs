namespace HDWallet.Sample
{
    public class BitcoinHDWallet : HDWallet
    {
        private const string _path = "m/44'/0'/0'/0/x";

        public BitcoinHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new NullAddressGenerator();
        }
    }
}