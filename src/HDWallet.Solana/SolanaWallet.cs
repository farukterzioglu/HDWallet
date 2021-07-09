using System.Text;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Solana
{
    public class SolanaWallet : Wallet, IWallet
    {
        public SolanaWallet() { }

        public SolanaWallet(string privateKey) : base(privateKey) { }
        public SolanaWallet(byte[] privateKey) : base(privateKey) { }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new SolanaAddressGenerator();
        }
    }
}