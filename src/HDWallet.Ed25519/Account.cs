using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Ed25519
{
    /// <summary>
    /// Account generated with Elliptic Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : IAccount<TWallet> where TWallet : Wallet, IWallet, new()
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
                PrivateKey = extKey.PrivateKey.ToBytes(), 
                AddressGenerator = AddressGenerator,
                Index = (int)addressIndex
            };
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: true);
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: false);
        }
    }
}