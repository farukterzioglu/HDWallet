using NUnit.Framework;
using System;
using NBitcoin.Secp256k1;
using NBitcoin.DataEncoders;
using System.Text;
using Nethereum.Util;
using HDWallet.Core;

namespace HDWallet.Tron.Tests
{
    public class EncodeAddress
    {
        [SetUp]
        public void Setup()
        {
        }

        // tronWeb.createAccount()
        // >address: {
        // base58: "TPbBpRXnt6ztse8XkCLiJstZyqQZvxW2sx", 
        // hex: "4195679F3AAF5211991781D49B30525DDDFE9A18DE"}
        // privateKey: "08089C24EC3BAEB34254DDF5297CF8FBB8E031496FF67B4EFACA738FF9EBD455"
        // publicKey:  "04EE63599802B5D31A29C95CC7DF04F427E8F0A124BED9333F3A80404ACFC3127659C540D0162DEDB81AC5F74B2DEB4962656EFE112B252E54AC3BA1207CD1FB10"
        // __proto__: Object
        [Test]
        public void ShouldEncodeAddress()
        {
            var privateKey = Encoders.Hex.DecodeData("08089C24EC3BAEB34254DDF5297CF8FBB8E031496FF67B4EFACA738FF9EBD455");

            NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(privateKey));
            ECPubKey pubKey = privKey.CreatePubKey();
            
            var x = pubKey.Q.x.ToBytes();
            var y = pubKey.Q.y.ToBytes();

            var pubKeyUncomp = Helper.Concat(x, y);
            Assert.AreEqual(
                "ee63599802b5d31a29c95cc7df04f427e8f0a124bed9333f3a80404acfc3127659c540d0162dedb81ac5f74b2deb4962656efe112b252e54ac3ba1207cd1fb10",
                Encoders.Hex.EncodeData(pubKeyUncomp)
            );

            var pubKeyHash = new Sha3Keccack().CalculateHash(pubKeyUncomp);
            Assert.AreEqual("0837725ba59e30e8e52ba5ab95679f3aaf5211991781d49b30525dddfe9a18de", pubKeyHash.ToHexString());

            var sha3HashBytes = new byte[20];
            Array.Copy(pubKeyHash, pubKeyHash.Length - 20, sha3HashBytes, 0, 20);

            byte[] PKHWithVersionBytes = Helper.Concat(new byte[] { 65 }, sha3HashBytes);
            var hexAddress = PKHWithVersionBytes.ToHexString();
            Assert.AreEqual("4195679F3AAF5211991781D49B30525DDDFE9A18DE".ToLower(), hexAddress);
            
            var result = Encoders.Base58Check.EncodeData(PKHWithVersionBytes); 
            Assert.AreEqual(
                "TPbBpRXnt6ztse8XkCLiJstZyqQZvxW2sx",
                result
            );
        }
    }

    public static class Utils
    {
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