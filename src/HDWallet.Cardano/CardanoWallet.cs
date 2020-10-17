using System.Text;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Cardano
{
    public class CardanoWallet : Wallet, IWallet
    {
        public CardanoWallet(){}

        public CardanoWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}