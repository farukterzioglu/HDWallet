using NBitcoin;

namespace HDWallet
{
    public interface IHdWallet
    {   
        Account GetAccount(uint accountNumber);
        Account GetSeedAccount();
    }
    
    // 25 agutos 24 eylul
    public abstract class HDWallet : Wallet, IHdWallet
    {
        protected IAddressGenerator AddressGenerator;
        private readonly byte[] _adddressPrefix;
        private readonly string _path;

        public HDWallet(Mnemonic mneumonic, string path) : base(mneumonic)
        {
            _path = path;
        }

        public HDWallet(Mnemonic mneumonic, string passphrase, string path) : base(mneumonic, passphrase)
        {
            _path = path;
        }

        public HDWallet(string mnemonic, string path) : this(new Mnemonic(mnemonic), path)
        {
        }

        public Account GetSeedAccount()
        {
            return new Account(base._masterKey.PrivateKey, AddressGenerator);
        }

        public Account GetAccount(uint accountNumber)
        {
            // TODO: Derive
            return new Account(base._masterKey.PrivateKey, accountNumber, AddressGenerator );
        }
    }
}