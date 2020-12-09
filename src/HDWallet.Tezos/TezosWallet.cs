using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace HDWallet.Tezos
{
    public class TezosWallet : Wallet, IWallet
    {
        public TezosWallet() { }

        public TezosWallet(string privateKey) : base(privateKey) { }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}