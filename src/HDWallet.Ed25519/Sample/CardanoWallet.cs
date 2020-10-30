using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoWallet : Wallet, IWallet
    {
        public CardanoWallet() : base(){}
        
        public CardanoWallet(byte[] privateKey) : base(privateKey){}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}