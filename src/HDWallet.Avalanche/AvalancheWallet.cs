using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheWallet : Wallet, IWallet
    {
        public AvalancheWallet(){}

        public AvalancheWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }

    public class FujiWallet : Wallet, IWallet
    {
        public FujiWallet(){}

        public FujiWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            var addressGenerator = new AddressGenerator();
            addressGenerator.HRP = "fuji";

            return addressGenerator;
        }
    }
}