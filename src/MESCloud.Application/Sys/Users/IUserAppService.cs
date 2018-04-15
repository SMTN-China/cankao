using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Roles.Dto;
using MESCloud.Users.Dto;
using System.Collections.Generic;
using MESCloud.CommonDto;

namespace MESCloud.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestMESDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task<List< UserOrgRoleDto>> GetUserOrgRole(int Id);

        Task<List<string>> GetUserRole(int Id);

        Task<UserDto> RePwd(int Id);
    }
}
