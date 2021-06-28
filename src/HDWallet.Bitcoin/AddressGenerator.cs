using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Bitcoin
{
    public static class AddressPrefixes
    {
    }

    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GetAddressFrom(pubKeyBytes, NetworkType.Mainnet);
        }

        public string GenerateAddress(byte[] pubKeyBytes, NetworkType networkType)
        {
            return GetAddressFrom(pubKeyBytes, networkType);
        }

        private string GetAddressFrom(byte[] bytes, NetworkType networkType)
        {
            var pubKey = new PubKey(bytes);
            var network = networkType == NetworkType.Mainnet ? Network.Main : Network.TestNet;
            var address = pubKey.WitHash.GetAddress(network);
            return address.ToString();
        }
    }
}