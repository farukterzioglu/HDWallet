namespace HDWallet.Core
{
    public interface IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        TWallet GetMasterWallet();

        IAccount<TWallet> GetAccount(uint index);
    }
}