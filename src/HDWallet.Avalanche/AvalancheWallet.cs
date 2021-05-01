using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheWallet : Wallet, IWallet
    {
        /// <summary>
        /// Returns address for Mainnet and X-Chain 
        /// To get address for other networks (e.g. Fuji) or other chains (e.g. P-Chain) use 'GetAddress(Networks, Chain)'
        /// </summary>
        public new string Address => base.Address;
        
        public AvalancheWallet(){}

        public AvalancheWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress(Networks network = Networks.Mainnet, Chain chain = Chain.X)
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes(), network, chain);
        }
    }
}