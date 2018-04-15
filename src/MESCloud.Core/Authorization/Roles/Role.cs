using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using MESCloud.Authorization.Users;
using System.Collections.Generic;
using MESCloud.Entities;
using Abp.Domain.Entities;

namespace MESCloud.Authorization.Roles
{
    public class Role : AbpRole<User>, IPassivable
    {
        public const int MaxDescriptionLength = 5000;

        public Role()
        {
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }

        [StringLength(MaxDescriptionLength)]
        public string Description {get; set;}

        /// <summary>
        /// 组织Id
        /// </summary>
        public int? OrgId { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public virtual Org Org { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string Info { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public  ICollection<MenuRoleMap> Menus { get; set; }
    }
}
