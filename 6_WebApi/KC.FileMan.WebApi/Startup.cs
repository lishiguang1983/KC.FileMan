using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KC.FileMan.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment HostingEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(x => {
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            services.AddCors(x=> {
                x.AddPolicy("AllowSpecificOrigin", build=>build.WithOrigins("http://localhost:15557").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //映射配置文件
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
            ModelConfig.BuildSessionFactories(HostingEnvironment, Configuration.GetConnectionString("HibernateConfigFile"));
            //使用AutoFac进行注入
            return new AutofacServiceProvider(DependencyConfig.Configure(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
