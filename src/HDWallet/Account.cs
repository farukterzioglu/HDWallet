using NBitcoin;

namespace HDWallet
{
    public class Account
    {
        public readonly Key PrivateKey;
        public readonly PubKey PublicKey;
        public readonly int Index = -1;
        public readonly string Address;

        IAddressGenerator _addressGenerator;
        
        public Account(Key privateKey, IAddressGenerator addressGenerator)
        {
            PrivateKey = privateKey;
            PublicKey = privateKey.PubKey;

            Address = addressGenerator.GenerateAddress(PublicKey);
        }

        public Account(Key privateKey, uint index, IAddressGenerator addressGenerator) : this(privateKey, addressGenerator)
        {
            Index = (int)index;
        }
    }
}