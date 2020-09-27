namespace HDWallet.Core
{
    public class Coin
    {
        public string CurrentPath => $"m/{(ushort)Purpose}'/{(uint)CoinType}'";
        
        public readonly PurposeNumber Purpose;
        public readonly CoinType CoinType;

        public Coin(PurposeNumber purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;
        }
    }
}