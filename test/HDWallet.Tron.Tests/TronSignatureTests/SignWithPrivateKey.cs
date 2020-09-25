using System.Threading.Tasks;
using NUnit.Framework;
using HDWallet.Tron;

namespace HDWallet.Tron.Tests.TronSignatureTests
{
    public class SignWithPrivateKey
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldSignWithWallet()
        {
            IHDWallet<TronWallet> wallet = new TronHDWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor");
            TronWallet wallet0 = wallet.GetAccount(0).GetExternalWallet(0);

            var txId = "9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b".FromHexToByteArray();
            Signature signature = wallet0.Sign(txId);
            TronSignature tronSignature = new TronSignature(signature);
            var signatureHex = Helper.ToHexString(tronSignature.SignatureBytes);
            
            Assert.AreEqual("f6e6fed529ebca249dbe2a98e53e8f6fec3fe459e6c9dff86c74ef109d2a6cff3a2d031ba5c04277b75d243e702b2522b831aabe2fa7e1e8de2a705bcdb7fd5d00000000", signatureHex);
        }
    }
}