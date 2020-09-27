using System;
using System.Linq;
using System.Text;
using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace HDWallet.Tron
{
    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GenerateAddress(new PubKey(pubKeyBytes));
        }

        string GenerateAddress(PubKey pubKey)
        {
            var publicKey = pubKey.Decompress();
            
            var pubKeyBytes = publicKey.ToBytes();
            pubKeyBytes = pubKeyBytes.Skip(1).ToArray();

            var pubKeyHash = new Sha3Keccack().CalculateHash(pubKeyBytes);

            var sha3HashBytes = new byte[20];
            Array.Copy(pubKeyHash, pubKeyHash.Length - 20, sha3HashBytes, 0, 20);

            byte[] PKHWithVersionBytes = Helper.Concat(new byte[] { 65 }, sha3HashBytes);
            var hexAddress = PKHWithVersionBytes.ToHexString();

            var address = Encoders.Base58Check.EncodeData(PKHWithVersionBytes); 
            return address;
        }
    }
}