using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class BitcoinWallet : Wallet, IWallet
    {
        public BitcoinWallet(){}
        
        public BitcoinWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}