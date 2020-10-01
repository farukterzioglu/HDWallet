using System;
using System.Linq;
using System.Text;
using NBitcoin.DataEncoders;
using NUnit.Framework;
using PeterO.Cbor;

namespace HDWallet.Cardano.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DecodeFromAddress()
        {
            string address = "37btjrVyb4KEB2STADSsj3MYSAdj52X5FrFWpw2r7Wmj2GDzXjFRsHWuZqrw7zSkwopv8Ci3VWeg6bisU9dgJxW5hb2MZYeduNKbQJrqz3zVBsu9nT";
            byte[] decodedAddress = Encoders.Base58.DecodeData(address);
            string hexDecoded = decodedAddress.ToHexString(); // 82d818584983581c9c708538a763ff27169987a489e35057ef3cd3778c05e96f7ba9450ea201581e581c9c1722f7e446689256e1a30260f3510d558d99d0c391f2ba89cb697702451a4170cb17001a6979126c
            
            var byronAddress = CBORObject.DecodeFromBytes(decodedAddress, CBOREncodeOptions.Default);
                var addressPayload = CBORObject.DecodeFromBytes(byronAddress[0].GetByteString(), CBOREncodeOptions.Default);
                    var addressRoot = addressPayload[0].GetByteString().ToHexString(); // 9c708538a763ff27169987a489e35057ef3cd3778c05e96f7ba9450e
                    var a = addressPayload[1];
                    var addressAttributes = addressPayload[1].Values.ToArray();
                        var derivationPathValue = addressAttributes[0].GetByteString().ToHexString(); // (encrypted) 581c9c1722f7e446689256e1a30260f3510d558d99d0c391f2ba89cb6977
                        var networkMagicValue = addressAttributes[1].GetByteString().ToHexString(); //1A4170CB17
                    var addressType = addressPayload[2].AsInt32(); // 0
                var crc = byronAddress[1].AsInt32(); // 1769542252
        }

        [Test]
        public void EncodeToCbor()
        {   
            var addressRoot = "9c708538a763ff27169987a489e35057ef3cd3778c05e96f7ba9450e";
            var addressType = 0;
            var derivationPathValue = "581c9c1722f7e446689256e1a30260f3510d558d99d0c391f2ba89cb6977";
            var networkMagicValue = "1A4170CB17";
            var crc = 1769542252;

            var addressAttributes = CBORObject.NewMap()
                .Add(1, derivationPathValue.FromHexToByteArray())
                .Add(2, networkMagicValue.FromHexToByteArray());

            var addressPayload = CBORObject.NewArray()
                .Add(addressRoot.FromHexToByteArray())
                .Add(addressAttributes)
                .Add(addressType).EncodeToBytes();

            var byronAddress = CBORObject.NewArray()
                .Add(CBORObject.FromObject(addressPayload).WithTag(24))
                .Add(crc); 
            
            var encodedAddress = Encoders.Base58.EncodeData(byronAddress.EncodeToBytes());
            Assert.AreEqual("37btjrVyb4KEB2STADSsj3MYSAdj52X5FrFWpw2r7Wmj2GDzXjFRsHWuZqrw7zSkwopv8Ci3VWeg6bisU9dgJxW5hb2MZYeduNKbQJrqz3zVBsu9nT", encodedAddress);
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