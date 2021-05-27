using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.FileCoin
{
    public class FileCoinWallet : Wallet, IWallet
    {
        public FileCoinWallet(){}

        public FileCoinWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        /// <summary>
        /// Signs transaction hash (digested message) of a FileCoin message
        /// </summary>
        /// <param name="messageDigest"></param>
        /// <returns></returns>
        public FileCoinSignature SignMessage(byte[] messageDigest)
        {
            return new FileCoinSignature(base.Sign(messageDigest));
        }

        public string GetAddress(Network network = Network.Mainnet, Protocol protocol = Protocol.SECP256K1)
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes(), network, protocol);
        }
    }
}