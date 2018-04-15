using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using MESCloud.Authorization;
using MESCloud.Authorization.Roles;
using MESCloud.Authorization.Users;
using System.Collections.Generic;
using System;
using MESCloud.Entities;

namespace MESCloud.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly MESCloudDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(MESCloudDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // 初始化组织            
            var adminOrgForHost = _context.Orgs.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Code == StaticRoleNames.Host.Org);
            if (adminOrgForHost == null)
            {
                adminOrgForHost = _context.Orgs.Add(new Entities.Org() { Code = StaticRoleNames.Host.Org, TenantId = _tenantId, IsActive = true }).Entity;
                _context.SaveChanges();
            }

            // 初始化菜单
            var adminMenuForHost = _context.Menus.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId);
            if (adminMenuForHost == null)
            {
                adminMenuForHost = _context.Menus.Add(new Entities.Menu()
                {
                    Name = "主菜单",
                    Group = true,
                    Translate = "主菜单",
                    TenantId = _tenantId,
                    IsActive = true
                }).Entity;

                adminMenuForHost = _context.Menus.Add(new Entities.Menu()
                {
                    Name = "系统维护",
                    Translate = "系统维护",
                    Icon = "icon-settings",
                    ParentId = adminMenuForHost.Id,
                    TenantId = _tenantId,
                    IsActive = true
                }).Entity;

                _context.Menus.Add(new Entities.Menu() { Name = "用户管理", Translate = "用户管理", TenantId = _tenantId, Link = PermissionNames.Pages_Users, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "组织管理", Translate = "组织管理", TenantId = _tenantId, Link = PermissionNames.Pages_Orgs, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "菜单管理", Translate = "菜单管理", TenantId = _tenantId, Link = PermissionNames.Pages_Menus, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "角色管理", Translate = "角色管理", TenantId = _tenantId, Link = PermissionNames.Pages_Roles, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.SaveChanges();
            }


            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == adminOrgForHost.Id + "_" + StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, adminOrgForHost.Id + "_" + StaticRoleNames.Tenants.Admin, adminOrgForHost.Id + "_" + StaticRoleNames.Tenants.Admin) { IsStatic = true, OrgId = adminOrgForHost.Id, IsActive = true }).Entity;
                _context.SaveChanges();

                // Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new MESCloudAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }
            // 添加菜单权限
            var adminMRForHost = _context.MenuRoleMap.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.RoleId == adminRole.Id);
            if (adminMRForHost == null)
            {
                List<MenuRoleMap> mr = new List<MenuRoleMap>();
                foreach (var item in _context.Menus.IgnoreQueryFilters().Where(r => r.TenantId == _tenantId).Select(r => r.Id))
                {
                    mr.Add(new MenuRoleMap() { MenuId = item, RoleId = adminRole.Id, TenantId = _tenantId });
                }
                _context.MenuRoleMap.AddRange(mr.ToArray());
                _context.SaveChanges();
            }



            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();

                // User account of admin user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = adminUser.Id,
                        UserName = AbpUserBase.AdminUserName,
                        EmailAddress = adminUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }

            // 初始化角色菜单权限
            var adminRoleMenu = _context.MenuRoleMap.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.RoleId == adminRole.Id);
            if (adminRoleMenu == null)
            {
                foreach (var item in _context.Menus.Where(m => m.TenantId == _tenantId))
                {
                    _context.MenuRoleMap.Add(new Entities.MenuRoleMap() { MenuId = item.Id, RoleId = adminRole.Id, TenantId = _tenantId, CreatorUserId = adminUser.Id, CreationTime = DateTime.Now });
                }
                _context.SaveChanges();
            }
        }
    }
}
