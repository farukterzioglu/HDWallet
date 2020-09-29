namespace HDWallet.Core
{
    public interface IWallet
    {
        string Address { get; }
        Signature Sign(byte[] message);
    }
}