using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public class Wallet : IWallet
    {
        public byte[] PrivateKey;
        public byte[] PublicKey;
        public int Index;
        public string Address => AddressGenerator.GenerateAddress(PublicKey);

        public IAddressGenerator AddressGenerator;
        
        public Wallet(){}

        public Signature Sign(byte[] message)
        {
            if (message.Length != 32) throw new ArgumentException(paramName: nameof(message), message: "Message should be 32 bytes");

            throw new NotImplementedException();
        }
    }
}