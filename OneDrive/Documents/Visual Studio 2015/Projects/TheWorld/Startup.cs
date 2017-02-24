using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;

namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        // configure services cannot accept more input params, so inject them in constructor and use throught
        public Startup(IHostingEnvironment env)
        {
            _env = env;

            //to add configuration from json file for mail services:
            var builder = new ConfigurationBuilder()
                //path to config path throught env variable
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            //addtransient creates instance of debugmailservice when needed
            //scoped-reused within scope of a single request


            if (_env.IsEnvironment("Development")|| _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // implement real mail service
            }

            // register mvc services to use its methods
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // auto load index.html
            // app.UseDefaultFiles();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            // to call static files in wwwroot folder order imp! after default files
            app.UseStaticFiles();

            // use mvc as middleware-- to add routes
            // for specific routes-- use labda to configure mvc
            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    // template- url   ?-optional
                    template: "{controller}/{action}/{id?}",
                    // if path is not specified- use below specified default values
                    defaults: new { controller="App", action="Index"});
            });
           
        }
    }
}
