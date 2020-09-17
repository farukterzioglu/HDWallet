using System;
using System.Linq;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet
    {
        // TODO: Set path
        private const string _path = "";

        public TronHDWallet(Mnemonic mneumonic) : base(mneumonic, _path) 
        {
            base.AddressGenerator = new AddressGenerator();
        }

        public TronHDWallet(Mnemonic mneumonic, string passphrase) : base(mneumonic, passphrase, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }

        public TronHDWallet(string mnemonic) : base(mnemonic, _path)
        {
            base.AddressGenerator = new AddressGenerator();
        }
    }
}