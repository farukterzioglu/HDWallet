using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public class AccountHDWallet<TWallet> : IAccountHDWallet<TWallet> where TWallet : Wallet, new()
    {
        ExtKey _masterKey;
        uint _accountIndex;
        IAddressGenerator _addressGenerator;

        public AccountHDWallet(string masterKey, uint accountIndex, IAddressGenerator addressGenerator)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(masterKey);
            _masterKey = bitcoinExtKey.ExtKey;
            _accountIndex = accountIndex;
            _addressGenerator = addressGenerator;
        }

        IAccount<TWallet> IAccountHDWallet<TWallet>.GetAccount()
        {
            var externalKeyPath = new KeyPath("0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(_accountIndex, _addressGenerator, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}