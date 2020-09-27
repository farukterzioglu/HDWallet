namespace HDWallet.Core
{
    public interface IAccount<TWallet> where TWallet : IWallet, new()
    {
        TWallet GetInternalWallet(uint addressIndex);
        TWallet GetExternalWallet(uint addressIndex);
    }
}