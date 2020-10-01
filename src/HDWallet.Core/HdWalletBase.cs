using System;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet.Core
{
    public abstract class HdWalletBase
    {
        public string BIP39Seed { get; private set; }
        protected IAddressGenerator AddressGenerator { get; private set; }

        public HdWalletBase(string words, string seedPassword, IAddressGenerator addressGenerator)
        {
            if(string.IsNullOrEmpty(words)) throw new NullReferenceException(nameof(words));

            var mneumonic = new Mnemonic(words);
            BIP39Seed = mneumonic.DeriveSeed(seedPassword).ToHex();

            this.AddressGenerator = addressGenerator;
        }
    }
}