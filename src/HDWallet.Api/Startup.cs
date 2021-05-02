using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDWallet.Core;
using HDWallet.Secp256k1;
using HDWallet.Tron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HDWallet.Api", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "HDWallet.Api", Version = "v2" });
            });

            var settings = new Settings();
            Configuration.Bind(settings);
            settings.Validate();
            services.AddSingleton<Settings>(settings);

            // TODO: Implement for all blockchains
            IAccountHDWallet<TronWallet> accountHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.AccountHDKey) == false)
            {
                accountHDWallet = new AccountHDWallet<TronWallet>(settings.AccountHDKey, settings.AccountNumber.Value);
            }
            services.AddSingleton<Func<IAccountHDWallet<TronWallet>>>( () => accountHDWallet);
            
            IHDWallet<TronWallet> tronHDWallet = null;
            if(string.IsNullOrWhiteSpace(settings.Mnemonic) == false) 
            {
                tronHDWallet = new TronHDWallet(settings.Mnemonic, settings.Passphrase);
            }
            services.AddSingleton<Func<IHDWallet<TronWallet>>>( () => tronHDWallet);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDWallet.Api v1");
                        c.SwaggerEndpoint("/swagger/v2/swagger.json", "HDWallet.Api v2");
                    }
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
