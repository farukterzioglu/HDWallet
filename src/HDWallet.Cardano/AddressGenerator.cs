using System;
using System.Linq;
using System.Text;
using HDWallet.Core;

namespace HDWallet.Cardano
{
    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            throw new NotImplementedException();
        }
    }
}