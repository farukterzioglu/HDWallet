using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class Secp256k1Wallet : Wallet, IWallet
    {
        public Secp256k1Wallet(){}
        
        public Secp256k1Wallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}