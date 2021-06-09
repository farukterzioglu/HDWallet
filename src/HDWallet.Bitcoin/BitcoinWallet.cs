using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Bitcoin
{
    public enum AddressType
    {
        Mainnet = 0,
        Testnet = 0
    }
    
    public class BitcoinWallet : Wallet, IWallet
    {
        public BitcoinWallet(){}

        public BitcoinWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}