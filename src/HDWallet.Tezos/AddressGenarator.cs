using System;
using Base58Check;
using HDWallet.Core;
using Konscious.Security.Cryptography;
using NBitcoin;

namespace HDWallet.Tezos
{
    public class AddressGenerator : IAddressGenerator
    {
        public static readonly byte[] sppk = { 3, 254, 226, 86 };
        private const int PKHASH_BIT_SIZE = 20 * 8;
        public static readonly byte[] tz2 = { 6, 161, 161 };

        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GenerateAddress(new PubKey(pubKeyBytes));
        }

        string GenerateAddress(PubKey pubKey)
        {
            var pk = pubKey.ToBytes();
            HMACBlake2B blake2b = new HMACBlake2B(PKHASH_BIT_SIZE);

            blake2b.Initialize();

            var pkNew = blake2b.ComputeHash(pk);

            int prefixLen = tz2.Length;

            byte[] msg = new byte[prefixLen + pkNew.Length];

            Array.Copy(tz2, 0, msg, 0, tz2.Length);
            Array.Copy(pkNew, 0, msg, prefixLen, pkNew.Length);

            return Base58CheckEncoding.Encode(msg);
        }
    }
}