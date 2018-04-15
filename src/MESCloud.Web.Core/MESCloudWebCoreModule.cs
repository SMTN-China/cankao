using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using MESCloud.Authentication.JwtBearer;
using MESCloud.Configuration;
using MESCloud.EntityFrameworkCore;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Web.Models;

#if FEATURE_SIGNALR
using Abp.Web.SignalR;
#endif

namespace MESCloud
{
    [DependsOn(
         typeof(MESCloudApplicationModule),
         typeof(MESCloudEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
#if FEATURE_SIGNALR 
        ,typeof(AbpWebSignalRModule)
#endif
     )]
    public class MESCloudWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MESCloudWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                MESCloudConsts.ConnectionStringName
            );

            AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue9825", true);

          

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(MESCloudApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();

            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            //IocManager.Register<IErrorInfoBuilder, MESErrorInfoBuilder>();

            //IocManager.Register<AbpExceptionFilter, MESExceptionFilter>();
            var wwww = typeof(MESCloudWebCoreModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(typeof(MESCloudWebCoreModule).GetAssembly());


        }
    }
}
