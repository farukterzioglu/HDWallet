namespace HDWallet.Core
{
    public interface IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        TWallet GetMasterDepositWallet();

        IAccount<TWallet> GetAccount(uint index);
    }
}