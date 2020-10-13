using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public class AccountHDWallet<TWallet> : IAccountHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;
        uint _accountIndex;

        public AccountHDWallet(string accountMasterKey, uint accountIndex)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey);
            _masterKey = bitcoinExtKey.ExtKey;
            _accountIndex = accountIndex;
        }

        IAccount<TWallet> IAccountHDWallet<TWallet>.Account => GetAccount();
        
        IAccount<TWallet> GetAccount()
        {
            var externalKeyPath = new KeyPath("0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(_accountIndex, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}