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

        public string AccountDerivation(bool isExternal)
        {
            return $"m/{(ushort)Purpose}'/{(uint)CoinType}'/{Account}'/{(isExternal ? 0 : 1)}";
        }

        private string PathString(bool isExternal, uint addressIndex)
        {
            return $"m/{(ushort)Purpose}'/{(uint)CoinType}'/{Account}'/{(isExternal ? 0 : 1)}/{addressIndex}";
        }

        public string ChangePath(uint addressIndex)
        {
            return PathString(isExternal: true, addressIndex);
        }

        public string DepositPath(uint addressIndex)
        {
            return PathString(isExternal: false, addressIndex);
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