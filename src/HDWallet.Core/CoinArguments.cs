using System.Collections.Generic;

namespace HDWallet.Core
{
    public static class CoinArguments
    {
        public static readonly List<CoinArgumentModel> EndpointList =
            new List<CoinArgumentModel>
            {
                new CoinArgumentModel
                {
                    Argument = "avax",
                    EndpointFilter = "Avalanche"
                },
                new CoinArgumentModel
                {
                    Argument = "fil",
                    EndpointFilter = "Filecoin"
                },
                new CoinArgumentModel
                {
                    Argument = "trx",
                    EndpointFilter = "Tron"
                }
            };
    }

    public class CoinArgumentModel
    {
        public string Argument { get; set; }
        public string EndpointFilter { get; set; }
    }
}