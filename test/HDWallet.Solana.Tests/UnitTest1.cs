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

        // Maybe correct
        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5";
            // fkdKBkdNytEWk6fChKNy6j8Ni546y5PzVDNHAUwuyp8TSVzZJcwVt1XXfifMiQZzf6yfxhjKNw9bWRezgpkSvFw
            // f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5
            
            SolanaWallet wallet = new SolanaWallet(privateKey);
            Assert.AreEqual(expected: "87VZwKPMxBU8zppWB77ySodMfBf2HKWT3N6nKmzUiVBb", actual: wallet.Address);
        }

        [Test]
        public void ShouldGenerateFromBytes()
        {
            var privateKeyBytes = new byte[] { 64,195,121,69,216,110,229,136,27,133,1,140,181,138,22,178,198,20,15,35,48,171,4,30,180,37,169,22,13,160,106,91,89,223,80,232,234,49,158,82,7,219,9,174,107,237,201,108,18,171,161,194,197,206,93,48,234,129,72,82,78,117,35,124 };
            SolanaWallet wallet = new SolanaWallet(privateKeyBytes);
            Assert.AreEqual(expected: "73pneJadR1WwkgJGH7btmHyoMVE5cSrU2QfMuiikD7vs", actual: wallet.Address);
        }

        [Test]
        public void ShouldGenerateAddressFromPubkeyBytes()
        {
            var pubkey = "023fd9492685cf839427bfae951ef37d60b253b4b7bf7626abc9ec82bb69aa3d9a";
            var addrCh = SimpleBase.Base58.Bitcoin.Encode(pubkey.FromHexToByteArray());
            Console.WriteLine(addrCh);
            // CK3UH5tLou7koGtaZtHGkGQyL9bNi3ejobJFuzL543sQ
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
        }
    }
}