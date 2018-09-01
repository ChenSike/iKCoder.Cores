using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions;
using iKCoderComps;


namespace CoreBasic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSession(o =>
			{
				o.IdleTimeout = TimeSpan.FromHours(4);
			});
			AppLoader.LoadConfiguration_AllowCros(ref services);
			services.AddDistributedMemoryCache();
			services.AddMvc();
			services.AddScoped<Filter.Filter_UserAuthrization>();
			services.AddScoped<Filter.Filter_InitServices>();
			services.AddScoped<Filter.Filter_ConnectDB>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			app.UseSession();
			app.UseMvc();
		}
    }
}
