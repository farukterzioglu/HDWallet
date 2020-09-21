namespace HDWallet
{
    public class Path
    {
        public readonly Purpose Purpose;
        public readonly CoinType CoinType;

        public Path(Purpose purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;
        }

        public AccountPath Account(uint index)
        {
            return new AccountPath(this, index);
        }
    }

    public class AccountPath 
    {
        Path _path;
        uint _accountIndex;

        public AccountPath(Path path, uint accountIndex)
        {
            _path = path;
            _accountIndex = accountIndex;
        }

        public ChangePath Change(bool isExternal)
        {

            return isExternal ? ((ChangePath) new InternalPath(_path, _accountIndex)) : ((ChangePath) new ExternalPath(_path, _accountIndex));
        }

        public ChangePath Internal()
        {
            return new InternalPath(_path, _accountIndex);
        }

        public ChangePath External()
        {
            return new ExternalPath(_path, _accountIndex);
        }
    }

    public abstract class ChangePath
    {
        Path _path;
        bool _isInternal;
        uint _accountIndex;

        public ChangePath(Path path, uint accountIndex, bool isInternal)
        {
            _isInternal = isInternal;
            _path = path;
            _accountIndex = accountIndex;
        }

        public string Path => $"m/{(ushort)_path.Purpose}'/{(uint)_path.CoinType}'/{_accountIndex}'/{((ushort)(_isInternal ? Change.Internal : Change.External))}";

        public AddressPath Index(uint addressIndex)
        {
            return new AddressPath(_path, _accountIndex, _isInternal, addressIndex);
        }
    }

    public class InternalPath : ChangePath
    {
        public InternalPath(Path path, uint accountIndex) : base(path, accountIndex, true) {}
    }

    public class ExternalPath : ChangePath
    {
        public ExternalPath(Path path, uint accountIndex) : base(path, accountIndex, false) {}
    }

    public class AddressPath
    {
        Path _path;
        bool _isInternal;
        uint _accountIndex;
        uint _addressIndex;

        public AddressPath(Path path, uint accountIndex, bool isInternal, uint addressIndex)
        {
            _isInternal = isInternal;
            _path = path;
            _accountIndex = accountIndex;
            _addressIndex = addressIndex;
        }

        public string Path => $"m/{(ushort)_path.Purpose}'/{(uint)_path.CoinType}'/{_accountIndex}'/{(ushort)(_isInternal ? Change.Internal : Change.External)}/{_addressIndex}";
    }

    public enum Purpose : ushort
    {
        BIP44 = 44,
        BIP49 = 49,
        BIP84 = 84,
    }

    public enum CoinType : uint 
    {
        Bitcoin = 0,
        Tron = 195
    }

    public enum Change : ushort
    {
        External = 0,
        Internal = 1
    }
}