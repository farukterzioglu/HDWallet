using System;
using HDWallet.Core;
using NBitcoin;
using NBitcoin.Secp256k1;

namespace HDWallet
{
    public class Wallet : IWallet
    {
        public Key PrivateKey;
        public PubKey PublicKey => PrivateKey.PubKey;
        public int Index;
        public string Address => AddressGenerator.GenerateAddress(PublicKey.ToBytes());

        public IAddressGenerator AddressGenerator;
        
        public Wallet(){}

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