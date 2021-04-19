using System;
using HDWallet.Core;

namespace HDWallet.Avalanche
{
    public class AddressGenerator : IAddressGenerator
    {
        public string Prefix { get; set; } = "X";
        
        public string HRP { get; set; } = "avax";
        
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return $"{Prefix}-{GetBech32Address(pubKeyBytes)}";
        }

        public string GetBech32Address(byte[] pubKeyBytes) 
        {
            var addr = addressFromPublicKey(pubKeyBytes);
            return Bech32Engine.Encode(HRP, addr);
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