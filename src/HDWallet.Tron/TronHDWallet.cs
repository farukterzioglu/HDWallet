using System;
using System.Linq;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet<TronWallet>
    {
        private static readonly Coin _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Tron);

        public TronHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}