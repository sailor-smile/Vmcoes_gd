using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vmcoes_gd.Biz.Car;
using Vmcoes_gd.Biz.Users;
using Vmcoes_gd.Dao;
using Vmcoes_gd.Dao.Users;
using Vmcoes_gd.Enties;
using Vmcoes_gd.Utils;

namespace Vmcoes_gd
{

    public class Startup
    {
        private readonly IHostingEnvironment env;
        public Startup(IConfiguration configuration, IHostingEnvironment _env)
        {
            env = _env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            /************************业务服务注册开始***************************************/
            //string defConnection = Configuration.GetConnectionString("defConnection");
            ////DbConfigurator.SetConnection(defConnection);
            //services.AddDbContext<VmcoesContext>(
            //  options => options.UseSqlServer(defConnection, opt => { opt.UseRowNumberForPaging(); })
            //);
            //services.AddTransient<WebApplication3.Models.SysUserDB>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /************************数据库配置开始***************************************/
            DbConfigurator.emrConnection = Configuration.GetConnectionString("EMR");
            string defConnection = Configuration.GetConnectionString("defConnection");
            DbConfigurator.SetConnection(defConnection);
            services.AddDbContext<VmcoesContext>(
              options => options.UseSqlServer(defConnection, opt => { opt.UseRowNumberForPaging(); })
            );
            /************************数据库配置结束***************************************/
            //系统权限服务
            services.AddScoped<IVmcoesgdDao, VmcoesgdDaolmpl>();
            services.AddScoped<IVmcoesgdService, VmcoesgdServicelmpl>();
            services.AddScoped<ILoginDao, LoginDaolmpl>();
            services.AddScoped<ILoginDaoService, LoginDaoServiceImpl>();


            /************************业务服务注册结束***************************************/
            
            //services.AddSession();
            services.AddSession(options =>
            {
                //设置Session过期时间
                options.IdleTimeout = TimeSpan.FromHours(1000);
                //options.Cookie.HttpOnly = true;
            });
            ServiceUtils.services = services;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd hh:mm:ss";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //Session服务
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Logingo}/{id?}");
            });
        }
    }
}
