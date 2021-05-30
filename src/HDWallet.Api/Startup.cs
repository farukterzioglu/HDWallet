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
using HDWallet.FileCoin;
using HDWallet.Ed25519;

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
                    // add a custom document filter which sets visible routes
                    options.DocumentFilter<SwaggerDocumentFilter>();

                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments( XmlCommentsFilePath );
                } );

            var settings = new Settings();
            Configuration.Bind(settings);
            settings.Validate();
            services.AddSingleton<Settings>(settings);

            services
                .AddSecp256k1Coin<AvalancheWallet, AvalancheHDWallet>(settings)
                .AddEd25519Coin<PolkadotWallet, PolkadotHDWallet>(settings)
                .AddSecp256k1Coin<TronWallet, TronHDWallet>(settings)
                .AddSecp256k1Coin<FileCoinWallet, FileCoinHDWallet>(settings)
                ;
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

    public static class Extensions 
    {
        public static IServiceCollection AddSecp256k1Coin<TWallet, THDwallet>(this IServiceCollection services, Settings settings) 
            where TWallet : Secp256k1.Wallet, new() where THDwallet : HDWallet<TWallet>
        {
            IAccountHDWallet<TWallet> accountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                accountHDWallet = new AccountHDWallet<TWallet>(settings.AccountHDKey, settings.AccountNumber);
            }
            services.AddSingleton<Func<IAccountHDWallet<TWallet>>>( () => accountHDWallet);
            
            IHDWallet<TWallet> hdWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                hdWallet = (THDwallet)Activator.CreateInstance(typeof(THDwallet), new object[] { settings.Mnemonic, settings.Passphrase });
            }
            services.AddSingleton<Func<IHDWallet<TWallet>>>( () => hdWallet);

            return services;
        }

        public static IServiceCollection AddEd25519Coin<TWallet, THDwallet>(this IServiceCollection services, Settings settings) 
            where TWallet : Ed25519.Wallet, new() where THDwallet : HdWalletEd25519<TWallet>
        {
            // TODO : Implement account wallet for Ed25519 then activate this
            // IAccountHDWallet<TWallet> accountHDWallet = null;
            // if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            // {
            //     accountHDWallet = new AccountHDWallet<TWallet>(settings.AccountHDKey, settings.AccountNumber);
            // }
            // services.AddSingleton<Func<IAccountHDWallet<TWallet>>>( () => accountHDWallet);
            
            IHDWallet<TWallet> hdWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                hdWallet = (THDwallet)Activator.CreateInstance(typeof(THDwallet), new object[] { settings.Mnemonic, settings.Passphrase });
            }
            services.AddSingleton<Func<IHDWallet<TWallet>>>( () => hdWallet);

            return services;
        }
    }
}
