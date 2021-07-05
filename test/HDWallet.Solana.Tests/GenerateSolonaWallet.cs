using System;
using HDWallet.Core;
using HDWallet.Ed25519;
using NUnit.Framework;

namespace HDWallet.Solana.Tests
{
    public class SolanaHdWallet : HdWalletEd25519<SolanaWallet>, IHDWallet<SolanaWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Solana);

        public SolanaHdWallet(string seed) : base(seed, _path) {}
        public SolanaHdWallet(string words, string seedPassword) : base(words, seedPassword, _path) {}
    }
    
    public class SolanaAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            var addrCh = SimpleBase.Base58.Bitcoin.Encode(pubKeyBytes);
            return addrCh;
        }
    }

    public class SolanaWallet : Wallet, IWallet
    {
        public SolanaWallet(){}

        public SolanaWallet(string privateKey) : base(privateKey) {}
        public SolanaWallet(byte[] privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new SolanaAddressGenerator();
        }
    }

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5";
            SolanaWallet wallet = new SolanaWallet(privateKey);
            Assert.AreEqual(expected: "3fD58whN2KJaN9T4r5uE3ELFmzRW1dQNuszrmC6gnhx1", actual: wallet.Address);

            Console.WriteLine(wallet.Address);
            Console.WriteLine(wallet.PublicKey.ToHexString());
            Console.WriteLine($"[{string.Join(", ", wallet.ExpandedPrivateKey)}]");
        }

        [TestCase("73pneJadR1WwkgJGH7btmHyoMVE5cSrU2QfMuiikD7vs", new byte[] { 64,195,121,69,216,110,229,136,27,133,1,140,181,138,22,178,198,20,15,35,48,171,4,30,180,37,169,22,13,160,106,91})]
        [TestCase("2q7pyhPwAwZ3QMfZrnAbDhnh9mDUqycszcpf86VgQxhF", new byte[] { 153,218,149,89,225,94,145,62,233,171,46,83,227,223,173,87,93,163,59,73,190,17,37,187,146,46,51,73,79,73,136,40})]
        [TestCase("CK3UH5tLou7koGtaZtHGkGQyL9bNi3ejobJFuzL543sQ", new byte[] { 198,56,227,12,161,54,119,141,203,235,180,241,24,4,183,102,251,49,224,206,89,157,65,151,183,254,58,245,79,240,194,30})]
        [TestCase("U9N2BXbxhc93pMdhWj8XaTvJ3rfKoGCNWULgPBPB8kS", new byte[] { 31,204,135,36,57,87,92,52,152,229,242,233,174,6,40,79,180,237,58,253,127,184,20,146,83,208,72,137,172,228,238,176})]
        public void ShouldGenerateFromBytes(string address, byte[] privateKeyBytes)
        {
            SolanaWallet wallet = new SolanaWallet(privateKeyBytes);
            Assert.AreEqual(expected: address, actual: wallet.Address);
        }

        [Test]
        public void ShouldGenerateAddressFromPubkeyBytes()
        {
            var pubkey = "278117fc144c72340f67d0f2316e8386ceffbf2b2428c9c51fef7c597f1d426e";
            var actualAddress = SimpleBase.Base58.Bitcoin.Encode(pubkey.FromHexToByteArray());
            Assert.AreEqual("3fD58whN2KJaN9T4r5uE3ELFmzRW1dQNuszrmC6gnhx1", actualAddress);
        }

        [Test]
        public void ShouldGeneratedFromMnemonic()
        {
            var mnemonic = "dust dry odor unique impose craft adapt fatigue home bag kit primary advice rose stable error core shop entry vacuum pitch skill enhance pretty";
            var hdWallet =new SolanaHdWallet(mnemonic, "123456");

            var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            Console.WriteLine(wallet.Address);
            Console.WriteLine(wallet.PublicKey.ToHexString());
            Console.WriteLine(wallet.PrivateKey.ToHexString());
            Console.WriteLine(wallet.ExpandedPrivateKey.ToHexString());
            Console.WriteLine($"[{string.Join(", ", wallet.ExpandedPrivateKey)}]");
        }
    }
}