using HDWallet.Core;

namespace HDWallet.Solana
{
    public class SolanaAddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            var addrCh = SimpleBase.Base58.Bitcoin.Encode(pubKeyBytes);
            return addrCh;
        }
    }
}