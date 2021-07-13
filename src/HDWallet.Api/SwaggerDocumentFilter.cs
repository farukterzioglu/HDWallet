using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HDWallet.Api
{
    /// <summary>
    /// Represents the Swagger/Swashbuckle document filter used to set the visible API routes.
    /// </summary>
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        private readonly Settings _settings;

        public SwaggerDocumentFilter(Settings settings)
        {
            _settings = settings;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var shouldHdWalletVisible = !string.IsNullOrWhiteSpace(_settings.Mnemonic);
            var shouldSelectedCoinsVisible =
                _settings.SelectedCoinEndpoints != null && _settings.SelectedCoinEndpoints.Length > 0;

            // optional filter by selected coins
            var hiddenRoutesBySelectedCoins = swaggerDoc.Paths.Where(x =>
                shouldSelectedCoinsVisible && !_settings.SelectedCoinEndpoints.Any(y => x.Key.Contains(y)));

            // mandatory fiter by wallet type
            var hiddenRoutesByWalletType =
                swaggerDoc.Paths.Where(x => !x.Key.ToLower().Contains("account") == shouldHdWalletVisible);

            // merge filters
            var mergedHiddenRoutes = hiddenRoutesBySelectedCoins.Union(hiddenRoutesByWalletType);

            mergedHiddenRoutes.ToList().ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}