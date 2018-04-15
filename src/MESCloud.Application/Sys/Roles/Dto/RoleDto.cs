using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using MESCloud.Authorization.Roles;
using MESCloud.Entities;
using Newtonsoft.Json;
using MESCloud.Menus.Dto;

namespace MESCloud.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleDto : EntityDto<int>
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

        /// <summary>
        /// ÃèÊö
        /// </summary>
        [StringLength(50)]
        public string Info { get; set; }


        /// <summary>
        /// ±¸×¢
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        
        public string OrgName { get; set; }

        /// <summary>
        /// ×´Ì¬
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// µÈ¼¶
        /// </summary>
        public int Grade { get; set; }

        public List<string> Permissions { get; set; }

        public int OrgId { get; set; }

        [JsonIgnore]
        public Org Org { get; set; }

        public ICollection<MenuIdNameDto> Menus { get; set; }
    }
}
