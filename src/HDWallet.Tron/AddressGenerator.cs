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

    public static class Helper
    {
        // From NBitcoin
        public static byte[] Concat(byte[] arr, params byte[][] arrs)
		{
			var len = arr.Length + arrs.Sum(a => a.Length);
			var ret = new byte[len];
			Buffer.BlockCopy(arr, 0, ret, 0, arr.Length);
			var pos = arr.Length;
			foreach (var a in arrs)
			{
				Buffer.BlockCopy(a, 0, ret, pos, a.Length);
				pos += a.Length;
			}
			return ret;
		}

        public static string ToHexString(this byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }


        public static byte[] FromHexToByteArray(this string input)
        {
            var numberChars = input.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            }
            return bytes;
        }
        
    }
}