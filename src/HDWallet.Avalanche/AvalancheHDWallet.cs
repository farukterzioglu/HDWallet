using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheHDWallet : HDWallet<AvalancheWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Avalanche);

        public AvalancheHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) {}

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<AvalancheWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<AvalancheWallet> accountHDWallet = new AccountHDWallet<AvalancheWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }

    public class FujiHDWallet : HDWallet<FujiWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Avalanche);

        public FujiHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) {}

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<FujiWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<FujiWallet> accountHDWallet = new AccountHDWallet<FujiWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}
