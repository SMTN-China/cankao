using System.Linq;
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

namespace MESCloud.EntityFrameworkCore.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly MESCloudDbContext _context;

        public HostRoleAndUserCreator(MESCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            // 初始化组织
            var adminOrgForHost = _context.Orgs.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Code == StaticRoleNames.Host.Org);
            if (adminOrgForHost == null)
            {
                adminOrgForHost = _context.Orgs.Add(new Entities.Org() { Code = StaticRoleNames.Host.Org, IsActive = true }).Entity;
                _context.SaveChanges();
            }

            // 初始化菜单
            var adminMenuForHost = _context.Menus.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null);
            if (adminMenuForHost == null)
            {
                adminMenuForHost = _context.Menus.Add(new Entities.Menu()
                {
                    Name = "主菜单",
                    Group = true,
                    Translate = "主菜单",
                    IsActive = true

                }).Entity;

                adminMenuForHost = _context.Menus.Add(new Entities.Menu()
                {
                    Name = "系统维护",                   
                    Translate = "系统维护",
                    Icon = "icon-settings",
                    ParentId = adminMenuForHost.Id,
                    IsActive = true
                }).Entity;

                _context.Menus.Add(new Entities.Menu() { Name = "用户管理", Translate = "用户管理", Link = PermissionNames.Pages_Users, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "组织管理", Translate = "组织管理", Link = PermissionNames.Pages_Orgs, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "菜单管理", Translate = "菜单管理", Link = PermissionNames.Pages_Menus, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "角色管理", Translate = "角色管理", Link = PermissionNames.Pages_Roles, ParentId = adminMenuForHost.Id, IsActive = true });
                _context.Menus.Add(new Entities.Menu() { Name = "租户管理", Translate = "租户管理", Link = PermissionNames.Pages_Tenants, ParentId = adminMenuForHost.Id, IsActive = true });

                _context.SaveChanges();
            }


            // Admin role for host

            var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == adminOrgForHost.Id + "_" + StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, adminOrgForHost.Id + "_" + StaticRoleNames.Host.Admin, adminOrgForHost.Id + "_" + StaticRoleNames.Host.Admin)
                { IsStatic = true, IsDefault = true, OrgId = adminOrgForHost.Id, IsActive = true }).Entity;
                _context.SaveChanges();
            }

            // 添加菜单权限
            var adminMRForHost = _context.MenuRoleMap.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.RoleId == adminRoleForHost.Id);
            if (adminMRForHost == null)
            {
                List<MenuRoleMap> mr = new List<MenuRoleMap>();
                foreach (var item in _context.Menus.Select(m => m.Id))
                {
                    mr.Add(new MenuRoleMap() { MenuId = item, RoleId = adminRoleForHost.Id });
                }
                _context.MenuRoleMap.AddRange(mr.ToArray());
                _context.SaveChanges();
            }

            // Admin user for host

            var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "admin",
                    Surname = "admin",
                    EmailAddress = "admin@aspnetboilerplate.com",
                    IsEmailConfirmed = true,
                    IsActive = true,
                    Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" // 123qwe
                };

                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                // Grant all permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new MESCloudAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = null,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();

                // User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();
            }

            // 初始化角色菜单权限
            var adminRoleMenu = _context.MenuRoleMap.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.RoleId == adminRoleForHost.Id);
            if (adminRoleMenu == null)
            {
                foreach (var item in _context.Menus.Where(m => m.TenantId == null))
                {
                    _context.MenuRoleMap.Add(new Entities.MenuRoleMap() { MenuId = item.Id, RoleId = adminRoleForHost.Id, TenantId = null, CreatorUserId = adminUserForHost.Id, CreationTime = DateTime.Now });
                }
                _context.SaveChanges();
            }
        }
    }
}
