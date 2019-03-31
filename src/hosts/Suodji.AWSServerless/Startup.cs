using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artice.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Suodji.Core;
using Suodji.Infrastructure;
using Artice.LogicCore;
using Artice.Telegram;

namespace Suodji.AWSServerless
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var appSettings = new AppSettings()
            {
                TelegramBotToken = Configuration.GetValue<string>("telegram:token")
            };
            services.AddSingleton(appSettings);

            services.AddArtice<ArticeModule>(builder =>
	            builder.UseTelegramProvider(configuration => configuration
		            .SetAccessToken(Configuration["telegram:token"])));

            // Add S3 to the ASP.NET Core dependency injection framework.
            //services.AddAWSService<Amazon.S3.IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseArtice();
			app.UseMvc();
            
        }
    }
}
