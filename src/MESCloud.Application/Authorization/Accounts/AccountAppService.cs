using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using MESCloud.Authorization.Accounts.Dto;
using MESCloud.Authorization.Users;
using MESCloud.Menus.Dto;
using System.Collections.Generic;
using MESCloud.Entities;
using Abp.Domain.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MESCloud.MultiTenancy;
using MESCloud.Sys.I18Ns.Dto;
using Abp.Reflection.Extensions;
using MESCloud.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Abp.MultiTenancy;
using Abp.Runtime.Session;

namespace MESCloud.Authorization.Accounts
{
    public class AccountAppService : MESCloudAppServiceBase, IAccountAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IRepository<Menu> _menuRepository;
        private readonly UserManager _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly IRepository<I18N> _repositoryI18N;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager, IRepository<Menu> menuRepository, ITenantCache tenantCache, LogInManager logInManager, UserManager userManager, IRepository<I18N> repositoryI18N, IPasswordHasher<User> passwordHasher)
        {
            _userRegistrationManager = userRegistrationManager;
            _menuRepository = menuRepository;
            _userManager = userManager;
            _repositoryI18N = repositoryI18N;
            _logInManager = logInManager;
            _passwordHasher = passwordHasher;
            _tenantCache = tenantCache;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        public async Task<ICollection<MenuCDto>> GetMenu()
        {
            var userId = AbpSession.UserId;

            var user = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Roles).FirstOrDefaultAsync();

            var userRoles = user.Roles.Select(r => r.RoleId).ToList();

            var groupMenu = await _menuRepository.GetAll().Where(m => m.Group)
                 .Include(m => m.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Children).ThenInclude(r => r.Roles).ThenInclude(r => r.Role)
                 .Include(m => m.Roles).ThenInclude(r => r.Role).ToListAsync();

            groupMenu = OrderByIndex(groupMenu);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuCDto>()
                    .ForMember(m => m.Children, opt => opt.MapFrom(s => s.Children.Where(m => m.Roles.Select(r => r.RoleId).Intersect(userRoles).Count() > 0)))
                   ;
            });

            var menu = config.CreateMapper().Map<List<Menu>, List<MenuCDto>>(groupMenu);

            return menu;
        }

        List<Menu> OrderByIndex(List<Menu> menus)
        {
            menus = menus.OrderBy(m => m.Index).ToList();

            foreach (var itemMenus in menus)
            {
                if (itemMenus.Children != null && itemMenus.Children.Count > 0)
                {
                    itemMenus.Children = OrderByIndex(itemMenus.Children.ToList());
                }
            }

            return menus.ToList();
        }

        public async Task<ICollection<TenantNzSelectDto>> GetTenantByKeyName(string keyName)
        {
            List<TenantNzSelectDto> host = new List<TenantNzSelectDto>() { new TenantNzSelectDto() { TenantId = 0, Name = "主机", State = TenantAvailabilityState.Available } };
            var res = await TenantManager.Tenants.Where(t => t.Name.Contains(keyName)).ToListAsync();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tenant, TenantNzSelectDto>()
                .ForMember(m => m.TenantId, opt => opt.MapFrom(s => s.Id));
            }
               );

            return host.Union(config.CreateMapper().Map<List<Tenant>, List<TenantNzSelectDto>>(res)).ToList();
        }

        public async Task<ICollection<I18NDto>> GetI18NByDtoName(string dtoName, string i18nKey)
        {
            var thisAssembly = typeof(MESCloudApplicationModule).GetAssembly().ExportedTypes.Where(t => t.Name.ToLower() == dtoName.ToLower()).FirstOrDefault();

            var dbI18N = await _repositoryI18N.GetAll().Where(i => i.ClassName == dtoName && i.I18NKey == i18nKey).ToListAsync();

            var p = thisAssembly.GetProperties();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<System.Reflection.PropertyInfo, I18NDto>()
                    .ForMember(m => m.ClassName, opt => opt.MapFrom(s => dtoName))
                    .ForMember(m => m.PropertyName, opt => opt.MapFrom(s => s.Name.ToLower()[0] + s.Name.Substring(1)))
                    .ForMember(m => m.I18NKey, opt => opt.MapFrom(s => i18nKey))
                    .ForMember(m => m.DisplayName, opt => opt.MapFrom(s =>
                        dbI18N.FirstOrDefault(i => i.PropertyName == s.Name) == null ? s.Name : dbI18N.FirstOrDefault(i => i.PropertyName == s.Name).DisplayName));
            }
             );

            return config.CreateMapper().Map<List<System.Reflection.PropertyInfo>, List<I18NDto>>(p.ToList());
        }

        public async Task<UserDto> ChangePwd(string oldPwd, string newPwd)
        {
            // 获取当前用户
            var user = await UserManager.FindByIdAsync(AbpSession.UserId.ToString());

            var loginResult = await _logInManager.LoginAsync(user.UserName, oldPwd, GetTenancyNameOrNull());

            // 校验旧密码是否正确
            if (loginResult.Identity == null)
            {
                throw new MesException("旧密码输入错误");
            }

            var res = await UserManager.ChangePasswordAsyncNoValid(user, newPwd);
            if (res.Succeeded)
            {
                return Mapper.Map<User, UserDto>(loginResult.User);
            }
            else
            {
                throw new MesException(res.Errors);
            }
        }

        [HttpPut]
        public async Task<UserDto> ChangeUserInfoAsync(UserDto input)
        {

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return Mapper.Map<User, UserDto>(user);
        }

        void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        
    }
}
