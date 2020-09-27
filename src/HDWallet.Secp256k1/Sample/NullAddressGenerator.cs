using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1.Sample
{
    public class NullAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}