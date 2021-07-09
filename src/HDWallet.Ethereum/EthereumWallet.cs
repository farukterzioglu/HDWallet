using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumWallet : Wallet, IWallet
    {
        public EthereumWallet()
        {
        }

        public EthereumWallet(string privateKey) : base(privateKey)
        {
        }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}