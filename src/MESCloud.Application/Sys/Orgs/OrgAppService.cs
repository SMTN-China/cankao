using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Orgs.Dto;
using MESCloud.Entities;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using MESCloud.Authorization.Roles;
using Microsoft.EntityFrameworkCore;
using MESCloud.Roles.Dto;
using MESCloud.Users.Dto;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace MESCloud.Orgs
{
    [AbpAuthorize(PermissionNames.Pages_Orgs)]
    public class OrgAppService : AsyncCrudAppService<Org, OrgDto, int, PagedResultRequestMESDto, CreateOrgDto, OrgDto>, IOrgAppService
    {
        private readonly IRepository<Org> _orgRepository;
        private readonly RoleManager _roleManager;
      

        public OrgAppService(IRepository<Org> repository, RoleManager roleManager) : base(repository)
        {
            this._orgRepository = repository;
            this._roleManager = roleManager;
          
        }

        [HttpPost]
        public async override Task<PagedResultDto<OrgDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Org>(input, _orgRepository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).Include(o=>o.Parent).ToList();

            return new PagedResultDto<OrgDto>(tasksCount, taskList.MapTo<List<OrgDto>>());
        }

        public async override Task<OrgDto> Create(CreateOrgDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var org = await base.Create(input);
            await _roleManager.CreateAsync(new Role() { Name = org.Id + "_成员", DisplayName = org.Id + "_成员", OrgId = org.Id });
            return org;
        }

        public async Task<List<OrgDto>> ChildOrg(int id = -1)
        {
            List<Org> res;
            if (id == -1)
            {
                res = await _orgRepository.GetAllListAsync(o => o.ParentId == null);
            }
            else
            {
                res = await _orgRepository.GetAllListAsync(o => o.ParentId == id);
            }


            return Mapper.Map<List<Org>, List<OrgDto>>(res);
        }

        public async Task<UserOrgRoleDto> GetOrgRoles(int Id)
        {
            var org = await _orgRepository.GetAll().Where(o => o.Id == Id).Include(o => o.Roles).FirstOrDefaultAsync();

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Role, RoleIdNameDto>()
                        .ForMember(m => m.Checked, opt => opt.MapFrom(s => false))
                        .ForMember(m => m.Id, opt => opt.MapFrom(s => s.Id))
                        .ForMember(m => m.Name, opt => opt.MapFrom(s => s.Name));
                }
              );

            return new UserOrgRoleDto() { Id = org.Id, Name = org.Code, Roles = config.CreateMapper().Map<List<Role>, List<RoleIdNameDto>>(org.Roles.ToList()) };
        }

        
    }
}
