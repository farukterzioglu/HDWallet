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
            return Address.NewSecp256k1Address(decompPubKeyBytes).ToString();
        }
    }
}