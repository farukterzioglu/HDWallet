namespace HDWallet.Core
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
}