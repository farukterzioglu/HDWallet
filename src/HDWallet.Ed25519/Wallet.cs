using System;
using HDWallet.Core;
using Ed25519;

namespace HDWallet.Ed25519
{
    public class Wallet : IWallet
    {
        byte[] privateKey;
        
        public byte[] PrivateKey {
            get {
                return privateKey;
            } set{
                if(value.Length != 32)
                {
                    throw new InvalidOperationException();
                }
                privateKey = value;

                ReadOnlySpan<byte> privateKeySpan = privateKey.AsSpan();
                var publicKey = privateKeySpan.ExtractPublicKey();

                PublicKey = publicKey.ToArray();
            }
        }
        public byte[] PublicKey;
        public int Index;
        public string Address => AddressGenerator.GenerateAddress(PublicKey);

        public IAddressGenerator AddressGenerator;
        
        public Wallet(){}

        public Signature Sign(byte[] message)
        {
            if (message.Length != 32) throw new ArgumentException(paramName: nameof(message), message: "Message should be 32 bytes");

            var signature = Signer.Sign(message, this.PrivateKey, this.PublicKey);
            var signatureHex = signature.ToArray().ToHexString();

            // TODO: 
            return new Signature()
            {
                SignatureHex = signatureHex,
                // R = r.ToBytes(),
                // S = s.ToBytes(),
                // RecId = recId
            };
        }
    }
}