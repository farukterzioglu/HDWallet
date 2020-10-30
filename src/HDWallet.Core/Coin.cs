namespace HDWallet.Core
{
    public class CoinPath
    {
        string _path { get; set; }
        
        readonly PurposeNumber Purpose;
        readonly CoinType CoinType;

        public CoinPath(string path)
        {
            _path = path;
        }
        
        public CoinPath(PurposeNumber purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;

            _path = $"m/{(ushort)Purpose}'/{(uint)CoinType}'";
        }

        public override string ToString()
        {
            return _path;
        }
    }
}