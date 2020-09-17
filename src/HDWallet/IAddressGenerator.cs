using NBitcoin;

namespace HDWallet
{
    public interface IAddressGenerator
    {
        string GenerateAddress(PubKey pubKey);
    }
}