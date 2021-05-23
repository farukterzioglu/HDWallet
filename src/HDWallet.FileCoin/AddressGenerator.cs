using System;
using System.Text;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.FileCoin
{
    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            var pubKey = new PubKey(pubKeyBytes);
            var decompPubKeyBytes = pubKey.Decompress().ToBytes();
            return Address.WithNetwork(Network.Mainnet).NewSecp256k1Address(decompPubKeyBytes).ToString();
        }

        public string GenerateAddress(byte[] pubKeyBytes, Network network, Protocol protocol)
        {   
            var pubKey = new PubKey(pubKeyBytes);
            var decompPubKeyBytes = pubKey.Decompress().ToBytes();

            var address = Address.WithNetwork(network);

            switch (protocol)
            {
                case Protocol.SECP256K1:
                    address = address.NewSecp256k1Address(decompPubKeyBytes);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return address.ToString();
        }
    }
}