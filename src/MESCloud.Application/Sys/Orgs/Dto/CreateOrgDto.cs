using Abp.Auditing;
using Abp.AutoMapper;
using MESCloud.Authorization.Roles;
using MESCloud.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Orgs.Dto
{
    [AutoMapTo(typeof(Org))]
    public class CreateOrgDto
    {
       
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(50)]
        public string Info { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 父级组织Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<Role> Roles { get; set; }

        public int? TenantId { get; set; }
    }
}
