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
using MESCloud.Authorization;
using MESCloud.Authorization.Users;
using MESCloud.Authorization.Roles;
using MESCloud.Users.Dto;
using MESCloud.Roles.Dto;
using MESCloud.Entities;
using AutoMapper;
using MESCloud.CommonDto;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Abp.Web.Models;

namespace MESCloud.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestMESDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        IRepository<User, long> _repository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Org> _orgRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IRepository<Org> orgRepository)
            : base(repository)
        {
            _repository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _orgRepository = orgRepository;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await _userManager.CreateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [HttpPost]
        public async override Task<PagedResultDto<UserDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<User>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<UserDto>(tasksCount, taskList.MapTo<List<UserDto>>());
        }
        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestMESDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            return await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestMESDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<List<UserOrgRoleDto>> GetUserOrgRole(int Id)
        {
            // 查询用户组织
            List<UserOrgRoleDto> res = new List<UserOrgRoleDto>();

            var user = await _userManager.Users.Where(u => u.Id == Id).Include(u => u.Roles).FirstOrDefaultAsync();

            var userRoles = user.Roles;

            var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Role, RoleIdNameDto>()
                            .ForMember(m => m.Checked, opt => opt.MapFrom(s => userRoles.FirstOrDefault(r => r.RoleId == s.Id) != null))
                            .ForMember(m => m.Id, opt => opt.MapFrom(s => s.Id))
                            .ForMember(m => m.Name, opt => opt.MapFrom(s => s.Name));
                    }
                );

            var orgs = _roleRepository.GetAllIncluding(r => r.Org).Where(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id)).GroupBy(r => r.Org);

            foreach (var item in orgs)
            {
                res.Add(new UserOrgRoleDto()
                {
                    Id = item.Key.Id,
                    Name = item.Key.Code,
                    Roles = config.CreateMapper().Map<List<Role>, List<RoleIdNameDto>>(item.Key.Roles.ToList())
                });
            }

            return res;
        }

        public async Task<List<string>> GetUserRole(int Id)
        {
            var userId = AbpSession.UserId;
            var user = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Roles).FirstOrDefaultAsync();

            var role = user.Roles.Select(r => r.RoleId.ToString());

            return role.ToList();            
        }

        [HttpPost]
        public async Task<UserDto> RePwd(int Id)
        {
            // 获取当前用户
            var user = await _userManager.FindByIdAsync(Id.ToString());
            var res = await _userManager.ChangePasswordAsyncNoValid(user,"123qwe");
            if (res.Succeeded)
            {
                return Mapper.Map<User, UserDto>(user);
            }
            else
            {
                throw new MesException(res.Errors);
            }
        }
    }
}
