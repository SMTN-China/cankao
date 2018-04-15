using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MESCloud.Entities
{

    public class Menu : Entity, IAudited, IMayHaveTenant, IPassivable
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        [StringLength(20)]
        [DefaultValue("")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// i18n主键
        /// </summary>
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string Translate { get; set; }
        /// <summary>
        /// 是否为菜单组
        /// </summary>
        public bool Group { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(200)]
        [DefaultValue("")]
        [Required]
        public string Link { get; set; }
        /// <summary>
        /// 外部链接
        /// </summary>
        [StringLength(200)]
        [DefaultValue("")]
        [Required]
        public string ExternalLink { get; set; }
        /// <summary>
        /// 链接 target
        ///  枚举 target?: '_blank' | '_self' | '_parent' | '_top'
        /// </summary>
        [StringLength(10)]
        [DefaultValue("")]
        [Required]
        public string Target { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(50)]
        [DefaultValue("")]
        [Required]
        public string Icon { get; set; }
        /// <summary>
        /// 徽标色。（注：`group:true` 无效）
        /// </summary>
        
        public int Index { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public  Menu Parent { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public  ICollection<Menu> Children { get; set; }

        /// <summary>
        /// 所拥有角色
        /// </summary>
        public  ICollection<MenuRoleMap> Roles { get; set; }


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
