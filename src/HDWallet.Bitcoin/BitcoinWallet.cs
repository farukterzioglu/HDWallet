using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Bitcoin
{
    public enum NetworkType
    {
        Mainnet = 0,
        Testnet = 1
    }

    public class BitcoinWallet : Wallet, IWallet
    {
        public BitcoinWallet()
        {
        }

        public BitcoinWallet(string privateKey) : base(privateKey)
        {
        }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress(NetworkType network = NetworkType.Mainnet)
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes(), network);
        }
    }
}