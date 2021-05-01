using System;
using HDWallet.Core;

namespace HDWallet.Avalanche
{
    public enum Networks
    {
        Mainnet,
        Fuji
    }

    public enum Chain
    {
        X, 
        P
    }

    public class AddressGenerator : IAddressGenerator
    {
        public string DefaultPrefix { get; set; } = "X";
        
        string DefaultHRP { get; set; } = "avax";
        
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return $"{DefaultPrefix}-{GetBech32Address(pubKeyBytes, DefaultHRP)}";
        }

        private string GetBech32Address(byte[] pubKeyBytes, string hrp) 
        {
            var addr = addressFromPublicKey(pubKeyBytes);
            return Bech32Engine.Encode(hrp, addr);
        }

        public string GenerateAddress(byte[] pubKeyBytes, Networks network = Networks.Mainnet, Chain chain = Chain.X)
        {   
            string prefix = chain == Chain.X ? "X" : chain == Chain.P ? "P" : "X";
            string hrp = network == Networks.Mainnet ? "avax" : network == Networks.Fuji ? "fuji" : "avax" ;

            return $"{prefix}-{GetBech32Address(pubKeyBytes, hrp)}";
        }

        private byte[] addressFromPublicKey(byte[] pubKeyBytes) 
        {
            if(pubKeyBytes.Length == 65) 
            {
                throw new NotImplementedException();
            }

            if(pubKeyBytes.Length == 33) 
            {
                var sha256 =  NBitcoin.Crypto.Hashes.SHA256(pubKeyBytes);
                var ripesha = NBitcoin.Crypto.Hashes.RIPEMD160(sha256, sha256.Length);
                return ripesha;
            }

            throw new NotSupportedException();
        }
    }
}