using System;
using System.Linq;
using System.Text;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin.Secp256k1;
using NUnit.Framework;

namespace HDWallet.Tests.Signature
{
    public class SignWithPrivateKey
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SignWithECPrivKey()
        {
            var privKeyStr = Encoders.Hex.DecodeData("cdce32b32436ff20c2c32ee55cd245a82fff4c2dc944da855a9e0f00c5d889e4"); 
            Key key = new Key(privKeyStr);
            NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(key.ToBytes()));

            var messageToSign = "159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145";
            var messageBytes = Encoders.Hex.DecodeData(messageToSign);

            privKey.TrySignRecoverable(messageBytes, out SecpRecoverableECDSASignature sigRec);
            var (r, s, v) = sigRec;

            var R = r.ToBytes().ToHexString();
            var S = s.ToBytes().ToHexString();

            Assert.AreEqual("84de8230e66c6507dea6de6d925c76ac0db85d99ddd3c069659d0970ade8876a", R);
            Assert.AreEqual("0dcd4adb2e40fcf257da419b88c1e7dd4d92750b63381d8379b96f3b7b8a4498", S);
            Assert.AreEqual(1, v);
        }

        [Test]
        public void MultipleSignatureWays()
        {
            var privKeyStr = Encoders.Hex.DecodeData("8e812436a0e3323166e1f0e8ba79e19e217b2c4a53c970d4cca0cfb1078979df"); 
            Key key = new Key(privKeyStr);
            Assert.AreEqual("04a5bb3b28466f578e6e93fbfd5f75cee1ae86033aa4bbea690e3312c087181eb366f9a1d1d6a437a9bf9fc65ec853b9fd60fa322be3997c47144eb20da658b3d1", key.PubKey.Decompress().ToHex());

            var messageToSign = "159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145";
            var messageBytes = Encoders.Hex.DecodeData(messageToSign);

            ECDSASignature signature = key.Sign(new uint256(messageBytes), true);
            SecpECDSASignature.TryCreateFromDer(signature.ToDER(), out SecpECDSASignature sig);
            var (r,s) = sig;

            var R = r.ToBytes();
            var S = s.ToBytes();

            Assert.AreEqual("38b7dac5ee932ac1bf2bc62c05b792cd93c3b4af61dc02dbb4b93dacb758123f", R.ToHexString());
            Assert.AreEqual("08bf123eabe77480787d664ca280dc1f20d9205725320658c39c6c143fd5642d", S.ToHexString());

            // Compact signature
            byte[] signatureCompact = key.SignCompact(new uint256(messageBytes), true);
            if (signatureCompact.Length != 65)
				throw new ArgumentException(paramName: nameof(signatureCompact), message: "Signature truncated, expected 65");

            var ss = signatureCompact.AsSpan();

            int recid = (ss[0] - 27) & 3;
            if ( ! (
                SecpRecoverableECDSASignature.TryCreateFromCompact(ss.Slice(1), recid, out SecpRecoverableECDSASignature sigR) && sigR is SecpRecoverableECDSASignature
                ) 
            )
			{
                throw new InvalidOperationException("Impossible to recover the public key");
			}

            // V from comapct signature
            var (r1, s1, v1) = sigR;

            Assert.AreEqual(v1, 0);

            // Recoverable signature with Secp256k1 lib
            NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(key.ToBytes()));
            Assert.AreEqual(key.PubKey.ToBytes(), privKey.CreatePubKey().ToBytes());

            privKey.TrySignRecoverable(messageBytes, out SecpRecoverableECDSASignature sigRec);

            var (r2, s2, v2) = sigRec;

            Assert.AreEqual(r2, r);
            Assert.AreEqual(s2, s);
            Assert.AreEqual(v2, v1);
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