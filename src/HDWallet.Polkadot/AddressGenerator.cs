using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions.Data;
using HDWallet.Core;

namespace HDWallet.Polkadot
{
    public static class AddressPrefixes
    {
        public static byte PolkadotLive = 0x00;
        public static byte GenericSubstrate = 0x42;
    }

    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GetAddressFrom(pubKeyBytes, AddressType.GenericSubstrate);
        }

        public string GenerateAddress(byte[] pubKeyBytes, AddressType addressType)
        {
            return GetAddressFrom(pubKeyBytes, addressType);
        }

        /// <summary> Gets address from. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="bytes"> The bytes. </param>
        /// <returns> The address from. </returns>
        public static string GetAddressFrom(byte[] bytes, AddressType addressType)
        {
            int SR25519_PUBLIC_SIZE = 32;
            int PUBLIC_KEY_LENGTH = 32;

            var plainAddr = Enumerable
                .Repeat((byte) addressType, 35)
                .ToArray();

            bytes.CopyTo(plainAddr.AsMemory(1));

            var ssPrefixed = new byte[SR25519_PUBLIC_SIZE + 8];
            var ssPrefixed1 = new byte[] { 0x53, 0x53, 0x35, 0x38, 0x50, 0x52, 0x45 };
            ssPrefixed1.CopyTo(ssPrefixed, 0);
            plainAddr.AsSpan(0, SR25519_PUBLIC_SIZE + 1).CopyTo(ssPrefixed.AsSpan(7));

            var blake2bHashed = HashExtension.Blake2(ssPrefixed, 0, SR25519_PUBLIC_SIZE + 8);
            plainAddr[1 + PUBLIC_KEY_LENGTH] = blake2bHashed[0];
            plainAddr[2 + PUBLIC_KEY_LENGTH] = blake2bHashed[1];

            var addrCh = SimpleBase.Base58.Bitcoin.Encode(plainAddr).ToArray();

            return new string(addrCh);
        }
    }

    public class HashExtension
    {
        public static byte[] XXHash128(byte[] bytes)
        {
            return BitConverter.GetBytes(XXHash.XXH64(bytes, 0)).Concat(BitConverter.GetBytes(XXHash.XXH64(bytes, 1))).ToArray();
        }

        public static byte[] Blake2(byte[] bytes, int size = 128, IReadOnlyList<byte> key = null)
        {
            var config = new Blake2Core.Blake2BConfig() { OutputSizeInBits = size, Key = null };
            return Blake2Core.Blake2B.ComputeHash(bytes, config);

        }

        public static byte[] Blake2Concat(byte[] bytes, int size = 128)
        {
            var config = new Blake2Core.Blake2BConfig() { OutputSizeInBits = size, Key = null };
            return Blake2Core.Blake2B.ComputeHash(bytes, config).Concat(bytes).ToArray();
        }

        internal static byte[] Blake2(byte[] ssPrefixed, int start, int count)
        {
            return Blake2Core.Blake2B.ComputeHash(ssPrefixed, start, count);
        }
    }
}