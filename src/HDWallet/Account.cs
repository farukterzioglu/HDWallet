using System;
using NBitcoin;

namespace HDWallet
{
    public class Account
    {
        public uint AccountIndex { get; set; }
        private ExtKey ExternalChain { get; set; }
        private ExtKey InternalChain { get; set; }

        private IAddressGenerator AddressGenerator;

        public Account(uint accountIndex, IAddressGenerator addressGenerator, ExtKey externalChain, ExtKey internalChain)
        {
            ExternalChain = externalChain;
            InternalChain = internalChain;
            AccountIndex = accountIndex;
            AddressGenerator = addressGenerator ?? throw new NullReferenceException(nameof(addressGenerator));    
        }

        private Wallet GetWallet(uint addressIndex, bool isInternal)
        {
            var extKey  = isInternal ? InternalChain.Derive(addressIndex) : ExternalChain.Derive(addressIndex);

            return new Wallet(
                privateKey: extKey.PrivateKey, 
                AddressGenerator, 
                index: (int)addressIndex);
        }

        public Wallet GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: true);
        }

        public Wallet GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: false);
        }
    }
}