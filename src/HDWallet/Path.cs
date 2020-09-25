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
        public string CurrentPath => $"m/{(ushort)Purpose}'/{(uint)CoinType}'";
        
        public readonly PurposeNumber Purpose;
        public readonly CoinType CoinType;

        public Coin(PurposeNumber purpose, CoinType coinType)
        {
            Purpose = purpose;
            CoinType = coinType;
        }
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
}