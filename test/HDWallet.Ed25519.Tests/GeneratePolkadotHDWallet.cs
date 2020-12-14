using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Data;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
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
    
    public enum AddressType
    {
        PolkadotLive = 0,
        GenericSubstrate = 42
    }
    
    public class PolkadotWallet : Wallet, IWallet
    {
        public PolkadotWallet(){}

        public PolkadotWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetNetworkAddress(AddressType addressType)
        {
            return ((AddressGenerator)base.AddressGenerator).GenerateAddress(base.PublicKey, addressType);
        }
    }

    public class GeneratePolkadotHDWallet
    {
        private const string mnemonic = "identify fatal close west parent myself awake impact shoot wide wrong derive ship doctor mushroom weather absent vacant armed chuckle swarm hip music wing";
        string seed = "fd18bdbc7382ea5356efef894d30dd3676a7ce37072e7947dd7e9076f9dd15b829b51d4d5fe9de7364f8dd6ad2c05320d942c69f3aebbad9228395e472d27a35";

        [TestCase("m/44'/354'/0'/0'/0'")]
        [TestCase("m/44'/354'/0'/0'/1'")]
        [TestCase("m/44'/354'/1'/0'/0'")]
        [TestCase("m/44'/354'/2'/0'/0'")]
        [TestCase("m/44'/434'/0'/0'/0'")]
        public void ShouldGenerateMasterWalletFromPurposeAndPath(string path)
        {
            // 15fn3g9Ehu9FYTBUSzWPihcVfHnfm2AfFNtMtPNMHGkoWTEg m/44'/354'/0'/0'/0'
            // 1446qjMShfFgAtGzsLd5ykqC5cHXXDumcbNbPQTd9ZHsoePY m/44'/354'/1'/0'/0'
            // 13KpDbPsYHBsT8YDCP3LaDbZkKaTUMXQq4tM8y7hrYD2Hq6m m/44'/354'/2'/0'/0'
            // 14qrzk2kjH4gfs4Q2PCEKy6pWS3JeqHkEcvP3TFASm4mVtAW m/44'/354'/0'/0'/1'

            // TestHDWalletEd25519 hdWallet = new TestHDWalletEd25519(mnemonic, "");
            TestHDWalletEd25519 hdWallet = new TestHDWalletEd25519(seed);
            var coinTypeWallet = hdWallet.GetWalletFromPath<PolkadotWallet>(path);
 
            var address = coinTypeWallet.GetNetworkAddress(AddressType.PolkadotLive);

            Console.WriteLine($"Path: {path}, address: {address}");
            Console.WriteLine($"Public key: {coinTypeWallet.PublicKey.ToHexString()}");
            Console.WriteLine($"Private key: {coinTypeWallet.PrivateKey.ToHexString()}");
        }
    }
}