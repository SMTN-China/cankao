using Abp.Domain.Entities;
using MESCloud.Authorization.Users;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class Storage : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(50)]
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]

        public string Remark { get; set; }
        [StringLength(200)]
        public string Address { get; set; }

        public int? AboutUserId { get; set; }
        public User AboutUser { get; set; }

        public IncomingMethod IncomingMethod { get; set; }
        public ICollection<RMMStorageMap> ReelMoveMethods { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }

}
