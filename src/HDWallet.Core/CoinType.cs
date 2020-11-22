namespace HDWallet.Core
{
    public enum CoinType : uint 
    {
        Bitcoin = 0,
        BitcoinTestnet = 1,
        CoinType1 = 1,
        Tron = 195,
        Polkadot = 359, // Not sure about path, source : https://github.com/projectmeka/meka-identity/blob/master/README.md
        Cardano = 1815,
        Tezos = 1729
    }
}