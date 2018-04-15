using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Configuration;
using MESCloud.Configuration.Dto;

namespace MESCloud.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);

        Task<ICollection<ISettingValue>> GetAppConfig(string[] names);

        Task SetAppConfig(SettingValue[] settings);
    }
}
