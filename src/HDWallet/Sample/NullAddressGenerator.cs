using NBitcoin;

namespace HDWallet.Sample
{
    public class NullAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(PubKey pubKey)
        {
            throw new System.NotImplementedException();
        }
    }
}