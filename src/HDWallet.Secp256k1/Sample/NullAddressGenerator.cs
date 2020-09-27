using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Sample
{
    public class NullAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}