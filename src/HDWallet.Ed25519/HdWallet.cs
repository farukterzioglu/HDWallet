using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public class HDWallet<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HDWallet(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
        {
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint index)
        {
            string seed = base.Seed;

            throw new NotImplementedException();
            
            // var privateKey = masterKey.PrivateKey;
            
            // return new TWallet() {
            //     PrivateKey = privateKey,
            //     AddressGenerator = AddressGenerator
            // };
        }

        TWallet IHDWallet<TWallet>.GetMasterDepositWallet()
        {
            throw new NotImplementedException();
        }
    }
}