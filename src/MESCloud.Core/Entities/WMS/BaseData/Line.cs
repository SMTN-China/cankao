using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class Line : Entity<string>, IBaseEntities
    {
        /// <summary>
        /// 名称
        /// </summary>
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

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        [StringLength(30)]
        public string ForCustomerMStorageId { get; set; }
        [StringLength(30)]
        public string ForSelfMStorageId { get; set; }
        public Storage ForCustomerMStorage { get; set; }
        public Storage ForSelfMStorage { get; set; }
    }
}
