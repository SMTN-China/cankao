using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MESCloud.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MESCloud.Entities
{

    public class Org : Entity, IAudited, IMayHaveTenant, IPassivable
    {

        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 组织名字
        /// </summary>
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        [DefaultValue("")]
        [Required]
        public string Remark { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(50)]
        [DefaultValue("")]
        [Required]
        public string Info { get; set; }

        /// <summary>
        /// 父级组织Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 父级组织
        /// </summary>
        public Org Parent { get; set; }

        /// <summary>
        /// 子组织
        /// </summary>
        public ICollection<Org> Children { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public long? LastModifierUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        public int? TenantId { get; set; }
    }
}
