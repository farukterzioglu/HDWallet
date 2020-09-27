using System;
using NBitcoin;

namespace HDWallet
{
    public interface IAccount<TWallet> where TWallet : Wallet, new()
    {
        TWallet GetInternalWallet(uint addressIndex);
        TWallet GetExternalWallet(uint addressIndex);
    }

    /// <summary>
    /// Account generated with Elliptic Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : IAccount<TWallet> where TWallet : Wallet, new()
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