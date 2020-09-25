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
        public string Seed { get; private set; }
        protected IAddressGenerator AddressGenerator;

        ExtKey _masterKey;

        // TODO: Test this
        public HDWallet(ExtKey extKey)
        {
            _masterKey = extKey;
        }

        public HDWallet(string words, string seedPassword, Coin path)
        {
            if( path == null) throw new NullReferenceException(nameof(path));
            if(string.IsNullOrEmpty(words)) throw new NullReferenceException(nameof(words));

            var mneumonic = new Mnemonic(words);
            Seed = mneumonic.DeriveSeed(seedPassword).ToHex();

            var masterKeyPath = new KeyPath(path.CurrentPath);
            _masterKey = new ExtKey(Seed).Derive(masterKeyPath);
        }

        TWallet IHDWallet<TWallet>.GetMasterDepositWallet()
        {
            var masterKey = _masterKey.Derive(new KeyPath("0'/0"));

            var privateKey = masterKey.PrivateKey;
            return new TWallet() {
                PrivateKey = privateKey,
                AddressGenerator = AddressGenerator
            };
        }

        Account<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{accountIndex}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new Account<TWallet>(accountIndex, AddressGenerator, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}