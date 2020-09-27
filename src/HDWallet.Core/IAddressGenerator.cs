namespace HDWallet.Core
{
    public interface IAddressGenerator
    {
        string GenerateAddress(byte[] pubKeyBytes);
    }
}