using System;
using NBitcoin;

namespace HDWallet
{
    public class Account
    {
        public uint AccountIndex { get; set; }
        public ExtKey ExternalChain { get; set; }
        public ExtKey InternalChain { get; set; }

        private IAddressGenerator AddressGenerator;

        public Account(IAddressGenerator addressGenerator)
        {
            AddressGenerator = addressGenerator ?? throw new NullReferenceException(nameof(addressGenerator));    
        }

        private Wallet GetWallet(uint index, bool isInternal)
        {
            var extKey  = isInternal ? InternalChain.Derive(index) : ExternalChain.Derive(index);

            return new Wallet(
                privateKey: extKey.PrivateKey, 
                AddressGenerator, 
                index: (int)index);
        }

        public Wallet GetInternalWallet(uint index)
        {
            return GetWallet(index, isInternal: true);
        }

        public Wallet GetExternalWallet(uint index)
        {
            return GetWallet(index, isInternal: false);
        }
    }
}