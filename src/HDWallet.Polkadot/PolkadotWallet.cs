using System.Text;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Polkadot
{
    public class PolkadotWallet : Wallet, IWallet
    {
        public PolkadotWallet(){}

        public PolkadotWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}