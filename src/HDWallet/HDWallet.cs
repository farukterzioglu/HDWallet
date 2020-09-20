using System;
using System.Collections.Generic;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet
{
    public interface IHDWallet
    {
        Wallet GetMasterDepositWallet();

        Wallet GetWallet(bool isExternal, uint index);
        Account GetAccount(uint index);
    }

    public class HDWallet : IHDWallet
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

        Wallet IHDWallet.GetMasterDepositWallet()
        {
            var privateKey = GetMasterExtKey().PrivateKey;
            return new Wallet(privateKey, AddressGenerator);
        }

        Wallet IHDWallet.GetWallet(bool isExternal, uint index)
        {
            var privateKey = GetKey(isExternal, index);
            return new Wallet(privateKey, AddressGenerator, (int)index);
        }

        Account IHDWallet.GetAccount(uint index)
        {
            var externalKeyPath = new KeyPath(Path.AccountDerivation(isExternal: true));
            var externalMasterKey = new ExtKey(Seed).Derive(externalKeyPath);

            var internalKeyPath = new KeyPath(Path.AccountDerivation(isExternal: false));
            var internalMasterKey = new ExtKey(Seed).Derive(internalKeyPath);

            return new Account(AddressGenerator){
                ExternalChain = externalMasterKey,
                InternalChain = internalMasterKey, 
                AccountIndex = index
            };
        }

        private Dictionary<int, ExtKey> nonHardenedKeys = new Dictionary<int, ExtKey>();
        private Dictionary<int, ExtKey> hardenedKeys = new Dictionary<int, ExtKey>();

        private ExtKey GetMasterExtKey()
        {
            var keyPath = new KeyPath(Path.AccountDerivation(isExternal: true));
            var masterKey = new ExtKey(Seed).Derive(keyPath);
           return masterKey;
        }

        private ExtKey GetExtKey(bool isExternal, int index, bool hardened = false)
        {
            if (!hardened && nonHardenedKeys.ContainsKey(index)) return nonHardenedKeys[index];
            if (hardened && hardenedKeys.ContainsKey(index)) return hardenedKeys[index];

            var publicDerivationPath = Path.AccountDerivation(isExternal); 
            var keyPath = new KeyPath(publicDerivationPath);
            var masterKey = new ExtKey(Seed).Derive(keyPath);

            if (hardened)
            {
                hardenedKeys.Add(index, masterKey.Derive(index, true));
                return hardenedKeys[index];
            }
            else
            {
                nonHardenedKeys.Add(index, masterKey.Derive(index, false));
                return nonHardenedKeys[index];
            }   
        }

        private Key GetPrivateKey(bool isExternal, uint index)
        {
            var key = GetExtKey(isExternal, (int)index);
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