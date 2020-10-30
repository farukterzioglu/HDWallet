using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GenerateCardanoHDWallet
    {
        // TODO: 

        private const string ReferenceSeed2 = "fffcf9f6f3f0edeae7e4e1dedbd8d5d2cfccc9c6c3c0bdbab7b4b1aeaba8a5a29f9c999693908d8a8784817e7b7875726f6c696663605d5a5754514e4b484542";
        
        [TestCase("762843f584c04573608a9b11a5ef4d4a4ef9864c6374044920f5d90b463d94e8", "00bab81ed6a545a7e8ece6ee5601b2e50be8943f3daaf988a145a99a3d0da27d30")]
        public void ShouldGenerateCardanoFromSeed(string privateKey, string publicKey)
        {
            CardanoHDWalletEd25519 hdWallet = new CardanoHDWalletEd25519(ReferenceSeed2);
            CardanoWallet wallet = hdWallet.GetCoinTypeWallet();

            Assert.AreEqual(privateKey, wallet.PrivateKey.ToHexString());
            Assert.AreEqual(publicKey, $"00{wallet.PublicKey.ToHexString()}");
        }
    }
}