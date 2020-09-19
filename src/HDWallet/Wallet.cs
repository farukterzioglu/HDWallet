using NBitcoin;

namespace HDWallet
{
    public class Wallet
    {
        public readonly Key PrivateKey;
        public readonly PubKey PublicKey;
        public readonly int Index;
        public string Address => _addressGenerator.GenerateAddress(PublicKey);

        IAddressGenerator _addressGenerator;
        
        public Wallet(Key privateKey, IAddressGenerator addressGenerator, int index = -1)
        {
            PrivateKey = privateKey;
            PublicKey = privateKey.PubKey;
            Index = index;
        }
    }
}