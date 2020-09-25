using System;
using System.Linq;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet<TronWallet>
    {
        private static readonly Path _path = new Path(purpose: Purpose.BIP44, coinType: CoinType.Tron);

        public TronHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}