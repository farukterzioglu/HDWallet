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

            var settings = new Settings();
            Configuration.Bind(settings);
            settings.Validate();
            services.AddSingleton<Settings>(settings);

            services
                .AddAvalanche(settings)
                .AddPolkadot(settings)
                .AddTron(settings);
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
        public static IServiceCollection AddAvalanche(this IServiceCollection services, Settings settings)
        {
            IAccountHDWallet<AvalancheWallet> accountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                accountHDWallet = new AccountHDWallet<AvalancheWallet>(settings.AccountHDKey, settings.AccountNumber);
            }
            services.AddSingleton<Func<IAccountHDWallet<AvalancheWallet>>>( () => accountHDWallet);
            
            IHDWallet<AvalancheWallet> hdWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                hdWallet = new AvalancheHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<AvalancheWallet>>>( () => hdWallet);

            return services;
        }

        public static IServiceCollection AddPolkadot(this IServiceCollection services, Settings settings)
        {
            IHDWallet<PolkadotWallet> hdWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                hdWallet = new PolkadotHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<PolkadotWallet>>>( () => hdWallet);

            return services;       
        }

        public static IServiceCollection AddTron(this IServiceCollection services, Settings settings)
        {
            IAccountHDWallet<TronWallet> accountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                accountHDWallet = new AccountHDWallet<TronWallet>(settings.AccountHDKey, settings.AccountNumber);
            }
            services.AddSingleton<Func<IAccountHDWallet<TronWallet>>>( () => accountHDWallet);
            
            IHDWallet<TronWallet> hdWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                hdWallet = new TronHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<TronWallet>>>( () => hdWallet);

            return services;
        }
    }
}
