using System.Threading.Tasks;
using Abp.Application.Services;
using MESCloud.Authorization.Accounts.Dto;
using MESCloud.Menus.Dto;
using System.Collections.Generic;
using MESCloud.Sys.I18Ns.Dto;
using MESCloud.Users.Dto;

namespace MESCloud.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);


        Task<ICollection<MenuCDto>> GetMenu();

        Task<ICollection<TenantNzSelectDto>> GetTenantByKeyName(string keyName);


        Task<ICollection<I18NDto>> GetI18NByDtoName(string dtoName, string i18nKey);

        Task<UserDto> ChangePwd(string oldPwd, string newPwd);

        Task<UserDto> ChangeUserInfoAsync(UserDto user);
    }
}
