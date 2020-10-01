namespace HDWallet.Core
{
    public interface IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        /// <summary>
        /// Returns wallet at m/purpose'/coin_type' 
        /// </summary>
        /// <returns></returns>
        TWallet GetMasterWallet();

        /// <summary>
        /// Returns wallet at m/purpose'/coin_type'/account'
        /// </summary>
        /// <param name="accountIndex"></param>
        /// <returns></returns>
        TWallet GetAccountWallet(uint accountIndex);

        /// <summary>
        /// Returns account to access wallets at m/purpose'/coin_type'/account'/0 and m/purpose'/coin_type'/account'/1
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IAccount<TWallet> GetAccount(uint index);
    }
}