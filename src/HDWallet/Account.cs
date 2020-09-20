using NBitcoin;

namespace HDWallet
{
    public class Account
    {
        public uint AccountIndex { get; set; }
        public ExtKey ExternalChain { get; set; }
        public ExtKey InternalChain { get; set; }
    }
}