using MESCloud.Menus.Dto;
using Abp.Application.Services;
using MESCloud.Entities;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using MESCloud.CommonDto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MESCloud.Authorization.Roles;
using MESCloud.Authorization.Users;
using Abp.Authorization;
using MESCloud.Authorization;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace MESCloud.Menus
{

    [AbpAuthorize(PermissionNames.Pages_Menus)]
    public class MenuAppService : AsyncCrudAppService<Menu, MenuDto, int, PagedResultRequestMESDto, CreateMenuDto, MenuDto>, IMenuAppService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly UserManager _userManager;
        public MenuAppService(IRepository<Menu> menuRepository, UserManager userManager) : base(menuRepository)
        {
            _menuRepository = menuRepository;
            _userManager = userManager;
        }
        public override Task<MenuDto> Create(CreateMenuDto input)
        {
            input.TenantId = AbpSession.TenantId;
            return base.Create(input);
        }

        [HttpPost]
        public async override Task<PagedResultDto<MenuDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Menu>(input, _menuRepository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).Include(m=>m.Parent).ToList();

            return new PagedResultDto<MenuDto>(tasksCount, taskList.MapTo<List<MenuDto>>());
        }

        public async Task<List<MenuDto>> ChildMenu(int id = -1)
        {
            if (id == -1)
            {
                var res = await _menuRepository.GetAllListAsync(m => m.Group);

                return Mapper.Map<List<Menu>, List<MenuDto>>(res);
            }
            else
            {
                var res = await _menuRepository.GetAllListAsync(m => m.ParentId == id);

                return Mapper.Map<List<Menu>, List<MenuDto>>(res);
            }
        }

      

        public async Task<List<NzTreeDto>> GetNzTreeMenu()
        {
            var groupMenu = await _menuRepository.GetAllIncluding(m => m.Children).Where(m => m.Group).ToListAsync();
            var nzTree = Mapper.Map<List<Menu>, List<NzTreeDto>>(groupMenu);

            return nzTree;
        }

        
    }
}
