using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class SampleWallet : Wallet, IWallet
    {
        public SampleWallet() : base(){}
        
        public SampleWallet(byte[] privateKey) : base(privateKey){}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}