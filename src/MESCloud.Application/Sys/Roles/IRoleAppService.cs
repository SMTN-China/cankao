using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Roles.Dto;
using System.Collections.Generic;
using MESCloud.Entities;
using MESCloud.Menus.Dto;
using MESCloud.CommonDto;

namespace MESCloud.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestMESDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<ICollection<NzTreeDto>> GetNzTreeMenuByRoleId(int Id);
    }
}
