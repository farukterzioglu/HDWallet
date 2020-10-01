namespace HDWallet.Core
{
    public class NullAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}