using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    /// <summary>
    /// Account generated with Edwards Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : IAccount<TWallet> where TWallet : Wallet, IWallet, new()
    {
        public uint AccountIndex { get; set; }
        Func<string, TWallet> _deriveFunction;

        internal Account(uint accountIndex, Func<string, TWallet> deriveFunction)
        {
            AccountIndex = accountIndex;
            _deriveFunction = deriveFunction;
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            var internalKeyPath = $"{AccountIndex}'/1";
            var internalWallet = _deriveFunction(internalKeyPath);

            internalWallet.Index = addressIndex;

            return internalWallet;
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            var externalKeyPath = $"{AccountIndex}'/0";
            var externalWallet = _deriveFunction(externalKeyPath);

            externalWallet.Index = addressIndex;

            return externalWallet;
        }
    }
}