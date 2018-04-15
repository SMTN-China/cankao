using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using MESCloud.Authorization.Roles;
using MESCloud.Authorization.Users;
using MESCloud.Authorization;
using MESCloud.Roles.Dto;
using AutoMapper;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using MESCloud.Entities;
using MESCloud.Menus.Dto;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;

namespace MESCloud.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedResultRequestMESDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<MenuRoleMap> _menuRoleMapRepository;
        private readonly IRepository<Menu> _menuRepository;
        public RoleAppService(IRepository<Role> roleRepository, RoleManager roleManager, UserManager userManager, IRepository<MenuRoleMap> menuRoleMapRepository, IRepository<Menu> menuRepository)
            : base(roleRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
            _menuRoleMapRepository = menuRoleMapRepository;
            _menuRepository = menuRepository;
        }
        [HttpPost]
        public override async Task<PagedResultDto<RoleDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Role>(input, _roleRepository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).Include(r => r.Org).ToList();

            return new PagedResultDto<RoleDto>(tasksCount, taskList.MapTo<List<RoleDto>>());
        }

        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);

            // 修改组织
            role.Menus.ToList().ForEach(mr => mr.TenantId = AbpSession.TenantId);

            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            // 根据菜单配置权限
            input.Permissions = _menuRoleMapRepository.GetAll().Where(m => m.RoleId == role.Id).Include(mr => mr.Menu).Select(mr => mr.Menu).Select(m => LinkToPermissions(m.Link)).ToList();

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        string LinkToPermissions(string link)
        {
            if (link==null)
            {
                return link;
            }
            var links = link.Split('/',System.StringSplitOptions.RemoveEmptyEntries);

            if (links.Length > 2)
            {
                return "/" + links[0] + "/" + links[1];
            }
            return link;
        }

        public override async Task<RoleDto> Update(RoleDto input)
        {
            CheckUpdatePermission();
            // 删除角色现有菜单
            await _menuRoleMapRepository.DeleteAsync(m => m.RoleId == input.Id);

            CurrentUnitOfWork.SaveChanges();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            // 修改组织
            role.Menus.ToList().ForEach(mr => mr.TenantId = AbpSession.TenantId);

            CheckErrors(await _roleManager.UpdateAsync(role));

            // 根据菜单配置权限

            input.Permissions = _menuRoleMapRepository.GetAll().Where(m => m.RoleId == role.Id).Include(mr => mr.Menu).Select(mr => mr.Menu).Select(m => LinkToPermissions(m.Link)).ToList();




            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            if (role.IsStatic)
            {
                throw new UserFriendlyException(L("CanNotDeleteStaticRole"));
            }

            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedResultRequestMESDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions);
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedResultRequestMESDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<ICollection<NzTreeDto>> GetNzTreeMenuByRoleId(int Id)
        {

            var groupMenu = await _menuRepository.GetAll().Where(m => m.Group)
                 .Include(m => m.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Roles).ThenInclude(r => r.Role).ToListAsync();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, NzTreeDto>()
                    .ForMember(m => m.Children, opt => opt.MapFrom(s => s.Children))
                    .ForMember(m => m.Checked, opt => opt.MapFrom(s => CheckNtChecked(s, Id)))
                    .ForMember(m => m.HalfChecked, opt => opt.MapFrom(s => CheckNtHalfChecked(s, Id)));
            }
                );

            var menu = config.CreateMapper().Map<List<Menu>, List<NzTreeDto>>(groupMenu);
            return menu;
        }

        bool CheckNtHalfChecked(Menu m, int? Id)
        {
            if (CheckNtChecked(m, Id))
            {
                return false;
            }
            else
            {
                if (m.Children != null && m.Children.Count > 0)
                {
                    foreach (var item in m.Children)
                    {
                        if (item.Roles.FirstOrDefault(r => r.RoleId == Id) != null)
                        {
                            return true;
                        }
                        if (item.Children != null && item.Children.Count > 0)
                        {
                            return CheckNtHalfChecked(item, Id);
                        }
                    }
                }
                return false;
            }
        }

        bool CheckNtChecked(Menu m, int? Id)
        {
            if (m.Roles.Select(r => r.RoleId).ToList().Contains(Id.Value))
            {
                if (m.Children != null)
                {
                    foreach (var item in m.Children)
                    {
                        if (!CheckNtChecked(item, Id))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
