using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HDWallet.Avalanche;
using HDWallet.Core;
using HDWallet.Secp256k1;
using HDWallet.Tron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.PlatformAbstractions;
using HDWallet.Polkadot;

namespace HDWallet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = ApiVersion.Default;
            });
            services.AddVersionedApiExplorer(options =>  
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;  
            });  
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments( XmlCommentsFilePath );
                } );
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "HDWallet.Api", Version = "v1" });
            //     c.SwaggerDoc("v2", new OpenApiInfo { Title = "HDWallet.Api", Version = "v2" });
            // });

            var settings = new Settings();
            Configuration.Bind(settings);
            settings.Validate();
            services.AddSingleton<Settings>(settings);

            // TODO: Implement for all blockchains
            // TRON
            IAccountHDWallet<TronWallet> accountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                accountHDWallet = new AccountHDWallet<TronWallet>(settings.AccountHDKey, settings.AccountNumber);
            }
            services.AddSingleton<Func<IAccountHDWallet<TronWallet>>>( () => accountHDWallet);
            
            IHDWallet<TronWallet> tronHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                tronHDWallet = new TronHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<TronWallet>>>( () => tronHDWallet);

            // AVALANCHE
            IAccountHDWallet<AvalancheWallet> avaxAccountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                avaxAccountHDWallet = new AccountHDWallet<AvalancheWallet>(settings.AccountHDKey, settings.AccountNumber);
            }
            services.AddSingleton<Func<IAccountHDWallet<AvalancheWallet>>>( () => avaxAccountHDWallet);
            
            IHDWallet<AvalancheWallet> avalancheHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                avalancheHDWallet = new AvalancheHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<AvalancheWallet>>>( () => avalancheHDWallet);

            // IHDWallet<PolkadotWallet> polkadotHDWallet = null;
            IHDWallet<PolkadotWallet> polkadotHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                polkadotHDWallet = new PolkadotHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<PolkadotWallet>>>( () => polkadotHDWallet);

            services.AddSingleton<Func<string, IHDWallet<HDWallet.Secp256k1.Wallet>>>( (string coin) => {
                switch (coin.ToLower())
                {
                    case "tron":
                        return tronHDWallet as IHDWallet<HDWallet.Secp256k1.Wallet>;
                    default:
                        throw new NotImplementedException();
                }
            });

            services.AddSingleton<Func<string, IHDWallet<IWallet>>>( (string coin) => {
                switch (coin.ToLower())
                {
                    case "polkadot":
                        return (IHDWallet<IWallet>) polkadotHDWallet;
                    default:
                        throw new NotImplementedException();
                }
            });

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                // app.UseSwaggerUI(c => 
                //     {
                //         c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDWallet.Api v1");
                //         c.SwaggerEndpoint("/swagger/v2/swagger.json", "HDWallet.Api v2");
                //     }
                // );
                app.UseSwaggerUI(
                    options =>
                    {
                        // build a swagger endpoint for each discovered API version
                        foreach ( var description in provider.ApiVersionDescriptions )
                        {
                            options.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant() );
                        }
                    } );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof( Startup ).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine( basePath, fileName );
            }
        }
    }
}
