using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoSampleWallet : Wallet, IWallet
    {
        public CardanoSampleWallet() : base(){}
        
        public CardanoSampleWallet(byte[] privateKey) : base(privateKey){}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}