using System;
using NBitcoin;

namespace HDWallet
{
    public class Account<TWallet> where TWallet : Wallet, new()
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

        private TWallet GetWallet(uint addressIndex, bool isInternal)
        {
            var extKey  = isInternal ? InternalChain.Derive(addressIndex) : ExternalChain.Derive(addressIndex);

            return new TWallet()
            {
                PrivateKey = extKey.PrivateKey, 
                AddressGenerator = AddressGenerator,
                Index = (int)addressIndex
            };
        }

        public TWallet GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: true);
        }

        public TWallet GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: false);
        }
    }
}