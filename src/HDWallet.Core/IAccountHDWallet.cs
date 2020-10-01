namespace HDWallet.Core
{
    public interface IAccountHDWallet<TWallet> where TWallet : IWallet, new()
    {
        IAccount<TWallet> GetAccount();
    }
}