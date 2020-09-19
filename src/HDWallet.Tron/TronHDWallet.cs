using System;
using System.Linq;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet
    {
        private static readonly Path _path = new Path(purpose: Purpose.BIP44, coinType: CoinType.Tron, account: 0);

        public TronHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}