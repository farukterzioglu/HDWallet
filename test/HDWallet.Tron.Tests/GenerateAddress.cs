using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace HDWallet.Tron.Tests
{
    public class GenerateAddress
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase("86134c8a51446c21b501f3a05844e18fdb72d3a5420867737c8640ce0ec656ca", "TJdDmJVYa9TcMJvCc9WsdaEXEYgeJrGVPq")]
        [TestCase("57e04ac5484dd2c3d97b44c5e232b6203c2759642f38c5ea6787b0e4044de165", "TNmLX3rJZNdq7kxgxs1y39FP3hp8LWHLUX")]
        [TestCase("138a22c03039e688daa2b7c785d1e8d6b9375d4413e6ea82471b1e7a61701a9d", "TASrJ76QANNPRgdDHHikWWApQzxh3HPku4")]
        [TestCase("e83a4958e81654efb162cef269e323ac501aa81d850ba9aed5a7d4f3c26d5a0a", "TNkzaPqNipxKbU5ecUZz7P7UdejiE82zc7")]
        [TestCase("05cdb18a4638d21d3f1f18e6bdb601a60b4debc85ee9bf8b385a2613693da24f", "TWCcS3cAVeNWhX1J6LHMEsEkWGq43t4EXc")]
        [TestCase("b66225af9b24c9eb92ef65e3ff540c5c260de9fc8bb01a51fc44490bafe7ab3e", "TW1QH88er9UqUKhoHLdm8dQTG2NsYU6C2h")]
        [TestCase("0b75b702316f1dcb2c7ca5aee9e1cd9bbdcf747e27fc417c324971caaf59772c", "TKJu6vpKAknBwzovm5NiBZ1j69nWmeXGyw")]
        [TestCase("15e2547daf170c6f0e0dd0d64c35c1259206bc481a0c9d571bac0b1197f51d11", "TQUddX2gBhGV7d33a2kZchVsPuWLdZBeXY")]
        [TestCase("858c97998d9bebddc9320157e538d248dfcc64cd4c5c8ea97dfcb5d8396b37a0", "TXjdePoR6ZRfBeiaZ9QoUyGwdHGhTPdy6x")]
        [TestCase("32d2d45c05758f7de37a542798aac91315bd269565c99eafb33ebfb3a54ac046", "TGJnVM3TcvsKaDL3zpNm92gw2YHrPx8s3Y")]
        public void ShouldGenerateAddressesFromPrivateKey(string privateKey, string address)
        {
            var privKeyStr = Encoders.Hex.DecodeData(privateKey); 
            Key key = new Key(privKeyStr);

            IAddressGenerator addressGenerator =  new AddressGenerator();
            var actulaAddress = addressGenerator.GenerateAddress(key.PubKey.ToBytes());

            Assert.AreEqual(address, actulaAddress);
        }
    }
}