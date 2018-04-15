using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MESCloud.Configuration;

namespace MESCloud.Web.Host.Startup
{
    [DependsOn(
       typeof(MESCloudWebCoreModule))]
    public class MESCloudWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MESCloudWebHostModule(IHostingEnvironment env)
        {         
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MESCloudWebHostModule).GetAssembly());
        }
    }
}
