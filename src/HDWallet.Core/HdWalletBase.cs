using System;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet.Core
{
    public abstract class HdWalletBase
    {
        protected string Seed { get; private set; }
        protected IAddressGenerator AddressGenerator;

        public HdWalletBase(){}

        public HdWalletBase(string words, string seedPassword)
        {
            if(string.IsNullOrEmpty(words)) throw new NullReferenceException(nameof(words));

            var mneumonic = new Mnemonic(words);
            Seed = mneumonic.DeriveSeed(seedPassword).ToHex();
        }
    }
}