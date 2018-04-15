using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using MESCloud.Authorization.Roles;

using Abp.Authorization.Roles;
using MESCloud.Menus.Dto;

namespace MESCloud.Roles.Dto
{
    [AutoMapTo(typeof(Role))]
    public class CreateRoleDto
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }
        
        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }

        [Required]
        public int OrgId { get; set; }

        public List<string> Permissions { get; set; }

        public ICollection<MenuIdNameDto> Menus { get; set; }
    }
}
