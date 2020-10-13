using HDWallet.Core;
using NBitcoin.DataEncoders;
using NUnit.Framework;
using HDWallet.Secp256k1.Sample;

namespace HDWallet.Tests.Signature
{
    public class SignWithAccount
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IHDWallet<BitcoinWallet> bitcoinHDWallet = new BitcoinHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
            var depositWallet0 = bitcoinHDWallet.GetAccount(0).GetExternalWallet(0);        
            var address = depositWallet0.Address;

            var messageBytes = Encoders.Hex.DecodeData("159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145");
            var signature = depositWallet0.Sign(messageBytes);

            Assert.AreEqual("84de8230e66c6507dea6de6d925c76ac0db85d99ddd3c069659d0970ade8876a", signature.R.ToHexString());
            Assert.AreEqual("0dcd4adb2e40fcf257da419b88c1e7dd4d92750b63381d8379b96f3b7b8a4498", signature.S.ToHexString());
            Assert.AreEqual(1, signature.RecId);
        }
    }
}