using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MESCloud.Authorization.Roles;
using System;

namespace MESCloud.Entities
{
    public class MenuRoleMap : Entity, ICreationAudited, IMayHaveTenant
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public int? TenantId { get; set; }
    }
}