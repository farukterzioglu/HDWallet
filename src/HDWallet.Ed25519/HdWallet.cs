using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public class HdWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HdWallet(string words, string seedPassword, HDWallet.Core.Coin path) : base(words, seedPassword, path)
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