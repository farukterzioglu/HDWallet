using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Cardano
{
    public class CardanoHDWallet : HdWalletEd25519<CardanoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Cardano);

        internal CardanoHDWallet(string seed, HDWallet.Core.CoinPath path) : base(seed, _path) {}
        public CardanoHDWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<CardanoWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            throw new NotImplementedException();
        }
    }
}