using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.BarCodeAnalysiss;

namespace MESCloud
{
    [DependsOn(
        typeof(MESCloudCoreModule),
        typeof(AbpAutoMapperModule))]
    public class MESCloudApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MESCloudAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MESCloudApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            IocManager.Register<CommonDto.MSSqlHelper>();

            IocManager.Register<HttpHelp>(Abp.Dependency.DependencyLifeStyle.Singleton);

            IocManager.Register<LightService>(Abp.Dependency.DependencyLifeStyle.Singleton);

            IocManager.Register<NotificationService>(Abp.Dependency.DependencyLifeStyle.Singleton);


            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}
