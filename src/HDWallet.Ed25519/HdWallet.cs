using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public class HdWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HdWallet(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
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