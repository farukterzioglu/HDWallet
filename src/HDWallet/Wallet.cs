using System;
using NBitcoin;

namespace HDWallet
{
    public class Wallet
    {
        public readonly Key PrivateKey;
        public readonly PubKey PublicKey;
        public readonly int Index;
        public string Address => AddressGenerator.GenerateAddress(PublicKey);

        private IAddressGenerator AddressGenerator;
        
        public Wallet(Key privateKey, IAddressGenerator addressGenerator, int index = -1)
        {
            AddressGenerator = addressGenerator ?? throw new NullReferenceException(nameof(addressGenerator));
            PrivateKey = privateKey ?? throw new NullReferenceException(nameof(privateKey));
            PublicKey = privateKey.PubKey;
            Index = index;
        }
    }
}