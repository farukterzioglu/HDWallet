namespace HDWallet.Ed25519
{
    public class HdWalletEd<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HdWalletEd(string words, string seedPassword, HDWallet.Core.Coin path) : base(words, seedPassword, path)
        {
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint index)
        {
            throw new NotImplementedException();
        }

        TWallet IHDWallet<TWallet>.GetMasterDepositWallet()
        {
            throw new NotImplementedException();
        }
    }
}