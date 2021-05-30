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
            var willBeHiddenRoutes = swaggerDoc.Paths
                .Where(x => !x.Key.ToLower().Contains("account") == shouldHdWalletVisible)
                .ToList();
            willBeHiddenRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}