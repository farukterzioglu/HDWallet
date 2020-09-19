namespace HDWallet
{
    public class Path
    {
        public readonly Purpose Purpose;
        public readonly uint Account;
        public readonly CoinType CoinType;

        public Path(Purpose purpose, CoinType coinType, uint account)
        {
            Purpose = purpose;
            CoinType = coinType;
            Account = account;
        }

        public string PublicDerivation(bool isChange)
        {
            return $"m/{(ushort)Purpose}'/{(uint)CoinType}'/{Account}'/{(isChange ? 1 : 0)}";
        }

        private string PathString(bool isChange, uint addressIndex)
        {
            return $"m/{(ushort)Purpose}'/{(uint)CoinType}'/{Account}'/{(isChange ? 1 : 0)}/{addressIndex}";
        }

        public string ChangePath(uint addressIndex)
        {
            return PathString(isChange: true, addressIndex);
        }

        public string DepositPath(uint addressIndex)
        {
            return PathString(isChange: false, addressIndex);
        }
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
}