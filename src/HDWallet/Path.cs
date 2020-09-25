namespace HDWallet
{
    public class Purpose
    {
        PurposeNumber _purposeNumber;
        Purpose(PurposeNumber purposeNumber)
        {
            _purposeNumber = purposeNumber;
        }

        public static Purpose Create(PurposeNumber purposeNumber)
        {
            return new Purpose(purposeNumber);
        }

        public Coin Coin(CoinType coinType)
        {
            return new Coin(_purposeNumber, coinType);
        }
    }

    public class Coin
    {
        public readonly PurposeNumber Purpose;
        public readonly CoinType CoinType;

        public Coin(PurposeNumber purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;
        }

        public AccountPath Account(uint index)
        {
            return new AccountPath(Purpose, CoinType, index);
        }
    }

    public class AccountPath 
    {
        Coin _path;
        uint _accountIndex;

        PurposeNumber _purpose;
        CoinType _coinType;

        public AccountPath(PurposeNumber purpose, CoinType coinType, uint accountIndex)
        {
            _purpose = purpose;
            _coinType = coinType;
            _path = Purpose.Create(purpose).Coin(coinType);
            _accountIndex = accountIndex;
        }

        public ChangePath Change(bool isExternal)
        {
            return isExternal ? ((ChangePath) new InternalPath(_path, _accountIndex)) : ((ChangePath) new ExternalPath(_path, _accountIndex));
        }

        public ChangePath InternalPath()
        {
            return new InternalPath(_path, _accountIndex);
        }

        public ChangePath ExternalPath()
        {
            return new ExternalPath(_path, _accountIndex);
        }
    }

    public abstract class ChangePath
    {
        Coin _path;
        bool _isInternal;
        uint _accountIndex;

        public ChangePath(Coin path, uint accountIndex, bool isInternal)
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
        public InternalPath(Coin path, uint accountIndex) : base(path, accountIndex, true) {}
    }

    public class ExternalPath : ChangePath
    {
        public ExternalPath(Coin path, uint accountIndex) : base(path, accountIndex, false) {}
    }

    public class AddressPath
    {
        Coin _path;
        bool _isInternal;
        uint _accountIndex;
        uint _addressIndex;

        public AddressPath(Coin path, uint accountIndex, bool isInternal, uint addressIndex)
        {
            _isInternal = isInternal;
            _path = path;
            _accountIndex = accountIndex;
            _addressIndex = addressIndex;
        }

        public string Path => $"m/{(ushort)_path.Purpose}'/{(uint)_path.CoinType}'/{_accountIndex}'/{(ushort)(_isInternal ? Change.Internal : Change.External)}/{_addressIndex}";
    }

    public enum PurposeNumber : ushort
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