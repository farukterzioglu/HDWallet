using HDWallet.Core;
using NBitcoin;
using System;
using Nethereum.Signer;

namespace HDWallet.Ethereum
{
    public class AddressGenerator : IAddressGenerator
    {
        //Referenced By: https://github.com/MetacoSA/NBitcoin/issues/565
        public string GenerateAddress(byte[] pubKeyBytes)
        {
            var dd = EthECKey.GenerateKey().GetPubKey();

            var ETH_publickKey = new PubKey(pubKeyBytes);
            byte[] byte_ETH_publicKey = ETH_publickKey.Decompress().ToBytes();

            var PubKeyNoPrefix = new byte[byte_ETH_publicKey.Length - 1];
            Array.Copy(byte_ETH_publicKey, 1, PubKeyNoPrefix, 0, PubKeyNoPrefix.Length);
            var initaddr = new Nethereum.Util.Sha3Keccack().CalculateHash(PubKeyNoPrefix);
            var addr = new byte[initaddr.Length - 12];
            Array.Copy(initaddr, 12, addr, 0, initaddr.Length - 12);
            var hex_addr = BitConverter.ToString(addr).Replace("-", "");
            string public_address = new Nethereum.Util.AddressUtil().ConvertToChecksumAddress(hex_addr);
            return public_address;
        }
    }
}