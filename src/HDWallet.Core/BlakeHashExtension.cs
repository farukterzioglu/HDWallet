using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Data;

namespace HDWallet.Core
{
    public class BlakeHashExtension
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

        public static byte[] Blake2(byte[] ssPrefixed, int start, int count)
        {
            return Blake2Core.Blake2B.ComputeHash(ssPrefixed, start, count);
        }
    }
}