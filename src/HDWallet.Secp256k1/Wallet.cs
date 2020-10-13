using System;
using System.Text;
using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Secp256k1;

namespace HDWallet.Secp256k1
{
    public abstract class Wallet : IWallet
    {
        public Key PrivateKey;
        public PubKey PublicKey => PrivateKey.PubKey;
        public uint Index;
        public string Address => AddressGenerator.GenerateAddress(PublicKey.ToBytes());

        public IAddressGenerator AddressGenerator {get; private set; }
        
        public Wallet(){
            AddressGenerator = GetAddressGenerator();
        }

        protected abstract IAddressGenerator GetAddressGenerator();

        public Wallet(string privateKey) : this()
        {
            byte[] privKeyPrefix = new byte[] { (128) };
            byte[] prefixedPrivKey = Helper.Concat(privKeyPrefix, Encoders.Hex.DecodeData(privateKey));

            byte[] privKeySuffix = new byte[] { (1) };
            byte[] suffixedPrivKey = Helper.Concat(prefixedPrivKey, privKeySuffix);

            Base58CheckEncoder base58Check = new Base58CheckEncoder();
            string privKeyEncoded = base58Check.EncodeData(suffixedPrivKey);
            PrivateKey = Key.Parse(privKeyEncoded, Network.Main);
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