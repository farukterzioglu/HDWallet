namespace HDWallet.Ed25519.Sample
{
    public class TestHDWalletEd25519 : HdWalletEd25519Base
    {
        public TestHDWalletEd25519(string seed) : base(seed) {}
        public TestHDWalletEd25519(string words, string seedPassword) : base(words, seedPassword) {}
    }
}