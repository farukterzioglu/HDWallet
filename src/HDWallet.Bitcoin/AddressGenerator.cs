using System;
using System.Linq;
using HDWallet.Core;

namespace HDWallet.Bitcoin
{
    public static class AddressPrefixes
    {
    }

    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GetAddressFrom(pubKeyBytes, AddressType.Mainnet);
        }

        public string GenerateAddress(byte[] pubKeyBytes, AddressType addressType)
        {
            return GetAddressFrom(pubKeyBytes, addressType);
        }

        public static string GetAddressFrom(byte[] bytes, AddressType addressType)
        {
            throw new NotImplementedException();
        }
    }
}