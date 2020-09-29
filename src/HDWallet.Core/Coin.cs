namespace HDWallet.Core
{
    public class CoinPath
    {
        string _path => $"m/{(ushort)Purpose}'/{(uint)CoinType}'";
        
        public readonly PurposeNumber Purpose;
        public readonly CoinType CoinType;

        public CoinPath(PurposeNumber purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;
        }

        public override string ToString()
        {
            return _path;
        }
    }
}