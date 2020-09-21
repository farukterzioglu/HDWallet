using System;
using NBitcoin;
using NBitcoin.Secp256k1;

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

        public Signature Sign(byte[] message)
        {
            if (message.Length != 32) throw new ArgumentException(paramName: nameof(message), message: "Message should be 32 bytes");

            NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(PrivateKey.ToBytes()));
            if(!privKey.TrySignRecoverable(message, out SecpRecoverableECDSASignature sigRec))
            {
                throw new InvalidOperationException();
            }

            var (r, s, recId) = sigRec;

            return new Signature()
            {
                R = r.ToBytes(),
                S = s.ToBytes(),
                RecId = recId
            };
        }
    }
}