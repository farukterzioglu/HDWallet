using System;
using System.Collections.Generic;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet
{
    public interface IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        TWallet GetMasterDepositWallet();

        Account<TWallet> GetAccount(uint index);
    }

    public class HDWallet<TWallet> : IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        // public string Path { get; }
        public readonly Path Path;

        public string Seed { get; private set; }

        protected IAddressGenerator AddressGenerator;

        // private ExtKey _masterKey;

        public HDWallet(string words, string seedPassword, Path path)
        {
            if( path == null) throw new NullReferenceException(nameof(path));
            if(string.IsNullOrEmpty(words)) throw new NullReferenceException(nameof(words));

            Path = path;
            InitialiseSeed(words, seedPassword);
        }

        private void InitialiseSeed(string words, string seedPassword = null)
        {
            var mneumonic = new Mnemonic(words);
            Seed = mneumonic.DeriveSeed(seedPassword).ToHex();
        }

        TWallet IHDWallet<TWallet>.GetMasterDepositWallet()
        {
            var privateKey = GetMasterExtKey().PrivateKey;
            return new TWallet() {
                PrivateKey = privateKey,
                AddressGenerator = AddressGenerator
            };
        }

        Account<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalPath = Path.Account(accountIndex).External().Path;
            var externalKeyPath = new KeyPath(externalPath);
            var externalMasterKey = new ExtKey(Seed).Derive(externalKeyPath);

            var internalKeyPath = new KeyPath(Path.Account(accountIndex).Internal().Path);
            var internalMasterKey = new ExtKey(Seed).Derive(internalKeyPath);

            return new Account<TWallet>(accountIndex, AddressGenerator, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }

        private Dictionary<uint, ExtKey> nonHardenedKeys = new Dictionary<uint, ExtKey>();
        private Dictionary<uint, ExtKey> hardenedKeys = new Dictionary<uint, ExtKey>();

        private ExtKey GetMasterExtKey()
        {
            var keyPath = new KeyPath(Path.Account(0).External().Path);
            var masterKey = new ExtKey(Seed).Derive(keyPath);
           return masterKey;
        }

        private ExtKey GetExtKey(bool isExternal, uint index, bool hardened = false)
        {
            if (!hardened && nonHardenedKeys.ContainsKey(index)) return nonHardenedKeys[index];
            if (hardened && hardenedKeys.ContainsKey(index)) return hardenedKeys[index];

            var publicDerivationPath = Path.Account(index).Change(isExternal).Path; 
            var keyPath = new KeyPath(publicDerivationPath);
            var masterKey = new ExtKey(Seed).Derive(keyPath);

            if (hardened)
            {
                hardenedKeys.Add(index, masterKey.Derive((int)index, true));
                return hardenedKeys[index];
            }
            else
            {
                nonHardenedKeys.Add(index, masterKey.Derive((int)index, false));
                return nonHardenedKeys[index];
            }   
        }

        private Key GetPrivateKey(bool isExternal, uint index)
        {
            var key = GetExtKey(isExternal, index);
            return key.PrivateKey;
        }

        private Dictionary<uint, Key> keys =  new Dictionary<uint, Key>();
        private Key GetKey(bool isExternal, uint index)
        {
            if (keys.ContainsKey(index)) return keys[index];
            var privateKey = GetPrivateKey(isExternal, index);
            keys.Add(index, privateKey);
            return keys[index];
        }
    }
}