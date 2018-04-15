using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Authorization.Roles;
using MESCloud.CommonDto;
using MESCloud.Orgs.Dto;
using MESCloud.Roles.Dto;
using MESCloud.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.Orgs
{
    public interface IOrgAppService : IAsyncCrudAppService<OrgDto, int, PagedResultRequestMESDto, CreateOrgDto, OrgDto>
    {
        Task<List<OrgDto>> ChildOrg(int id = -1);

        Task<UserOrgRoleDto> GetOrgRoles(int Id);

      
    }
}
