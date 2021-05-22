using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.FileCoin
{
    public class FileCoinWallet : Wallet, IWallet
    {
        public FileCoinWallet(){}

        public FileCoinWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}