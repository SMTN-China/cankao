using AutoMapper;
using Abp.Authorization;
using Abp.Authorization.Roles;
using MESCloud.Authorization.Roles;
using Abp.Domain.Repositories;
using MESCloud.Entities;
using MESCloud.CommonDto;

namespace MESCloud.Roles.Dto
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            // Role and permission
            CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

            CreateMap<CreateRoleDto, Role>()
                .ForMember(x => x.Permissions, opt => opt.Ignore())
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.OrgId + "_" + x.Name))
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.OrgId + "_" + x.DisplayName));

            CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore())
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.OrgId + "_" + x.Name))
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.OrgId + "_" + x.DisplayName));

            CreateMap<Role, RoleDto>()
                .ForMember(r => r.OrgName, opt => opt.MapFrom(r => r.Org.Code))
                .ForMember(r => r.Name, opt => opt.MapFrom(r => r.OrgId == null ? r.Name : r.Name.Substring(r.OrgId.ToString().Length + 1)))
                .ForMember(r => r.DisplayName, opt => opt.MapFrom(r => r.OrgId == null ? r.DisplayName : r.DisplayName.Substring(r.OrgId.ToString().Length + 1)));

            CreateMap<RoleIdNameDto, MenuRoleMap>()
                .ForMember(m => m.RoleId, opt => opt.MapFrom(s => s.Id))
                .ForMember(m => m.Id, opt => opt.MapFrom(s => s.Name));

            CreateMap<MenuRoleMap, RoleIdNameDto>()
                .ForMember(m => m.Id, opt => opt.MapFrom(s => s.RoleId));

            CreateMap<Role, RoleIdNameDto>().ForMember(m => m.Checked, opt => opt.MapFrom(s => false));
        }
    }
}
