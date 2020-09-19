using System;
using System.Linq;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet
    {
        private const string _path = "m/44'/195'/0'/0/x";

        public TronHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}