using System;
using System.Collections.Generic;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet
{
    public interface IHDWallet
    {
        Wallet GetMasterWallet();

        Wallet GetWallet(uint index);
    }

    public class HDWallet : IHDWallet
    {
        public HDWallet(string words, string seedPassword, string path)
        {
            Path = path ?? throw new NullReferenceException(nameof(path));
            InitialiseSeed(
                words ?? throw new NullReferenceException(nameof(words)), 
                seedPassword ?? throw new NullReferenceException(nameof(seedPassword)));
        }

        #region NETHEREUM
        
        // public string PATH;
        private Dictionary<int, ExtKey> nonHardenedKeys = new Dictionary<int, ExtKey>();
        private Dictionary<int, ExtKey> hardenedKeys = new Dictionary<int, ExtKey>();

        // public HDWallet(Wordlist wordList, WordCount wordCount, string path, string seedPassword = null,
        //     IRandom random = null) : this(path, random)
        // {
        //     InitialiseSeed(wordList, wordCount, seedPassword);
        // }

        // public HDWallet(string words, string seedPassword, string path, IRandom random = null) : this(path,
        //     random)
        // {
        //     InitialiseSeed(words, seedPassword);
        // }

        // public HDWallet(byte[] seed, string path, IRandom random = null) : this(path, random)
        // {
        //     Seed = seed.ToHex();
        //     var keyPath = new KeyPath(GetMasterPath());
        //     _masterKey = new ExtKey(Seed).Derive(keyPath);
        // }

//         private HDWallet(string path, IRandom random = null)
//         {
//             Path = path;
// #if NETCOREAPP2_1 || NETCOREAPP3_1 || NETSTANDARD2_0 
//             if (random == null) random = new RandomNumberGeneratorRandom();
// #else
//             if (random == null) random = new SecureRandom();
// #endif
//             Random = random;
//         }

        // private IRandom Random
        // {
        //     get => RandomUtils.Random;
        //     set => RandomUtils.Random = value;
        // }

        public string Seed { get; private set; }
        public string[] Words { get; private set; }

        public bool IsMneumonicValidChecksum { get; private set; }

        public string Path { get; }

        private ExtKey _masterKey;

        // private void InitialiseSeed(Wordlist wordlist, WordCount wordCount, string seedPassword = null)
        // {
        //     var mneumonic = new Mnemonic(wordlist, wordCount);
        //     Seed = mneumonic.DeriveSeed(seedPassword).ToHex();
        //     Words = mneumonic.Words;
        //     IsMneumonicValidChecksum = mneumonic.IsValidChecksum;
        //     var keyPath = new KeyPath(GetMasterPath());
        //     _masterKey = new ExtKey(Seed).Derive(keyPath);
        // }

        private void InitialiseSeed(string words, string seedPassword = null)
        {
            var mneumonic = new Mnemonic(words);
            Seed = mneumonic.DeriveSeed(seedPassword).ToHex();
            Words = mneumonic.Words;
            IsMneumonicValidChecksum = mneumonic.IsValidChecksum;
            var keyPath = new KeyPath(GetMasterPath());
            _masterKey = new ExtKey(Seed).Derive(keyPath);
        }

        // private string GetIndexPath(int index)
        // {
        //     return Path.Replace("x", index.ToString());
        // }

        private string GetMasterPath()
        {
            return Path.Replace("/x", "");
        }

        public ExtKey GetMasterExtKey()
        {
           return _masterKey;
        }

        // public ExtPubKey GetMasterExtPubKey()
        // {
        //     return GetMasterExtKey().Neuter();
        // }

        // public PublicWallet GetMasterPublicWallet()
        // {
        //     return new PublicWallet(GetMasterExtPubKey());
        // }

        public ExtKey GetExtKey(int index, bool hardened = false)
        {
            if (!hardened && nonHardenedKeys.ContainsKey(index)) return nonHardenedKeys[index];
            if (hardened && hardenedKeys.ContainsKey(index)) return hardenedKeys[index];
            if (hardened)
            {
                hardenedKeys.Add(index,_masterKey.Derive(index, true));
                return hardenedKeys[index];
            }
            else
            {
                nonHardenedKeys.Add(index,_masterKey.Derive(index, false));
                return nonHardenedKeys[index];
            }
            
        }

        // public ExtPubKey GetExtPubKey(int index, bool hardened = false)
        // {
          
        //     var key = GetExtKey(index, hardened);
        //     return key.Neuter();
        // }

        // public byte[] GetPrivateKeyBytes(int index)
        // {
        //     var key = GetExtKey(index);
        //     return key.PrivateKey.ToBytes();
        // }

        #endregion 

        protected IAddressGenerator AddressGenerator;

        Wallet IHDWallet.GetMasterWallet()
        {
            var privateKey = GetMasterExtKey().PrivateKey;
            return new Wallet(privateKey, AddressGenerator);
        }

        Wallet IHDWallet.GetWallet(uint index)
        {
            var privateKey = GetKey(index);
            return new Wallet(privateKey, AddressGenerator, (int)index);
        }

        private Key GetPrivateKey(uint index)
        {
            var key = GetExtKey((int)index);
            return key.PrivateKey;
        }

        private Dictionary<uint, Key> keys =  new Dictionary<uint, Key>();
        private Key GetKey(uint index)
        {
            if (keys.ContainsKey(index)) return keys[index];
            var privateKey = GetPrivateKey(index);
            keys.Add(index, privateKey);
            return keys[index];
        }
    }
}