using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class MPN : Entity<string>, IBaseEntities
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(30)]

        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>

        [StringLength(300)]

        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
 
        public string Remark { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public MPNHierarchy MPNHierarchy { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public MPNLevel MPNLevel { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MPNType MPNType { get; set; }

        /// <summary>
        /// 类型等级
        /// </summary>
        public MSDLevel MSDLevel { get; set; }
        /// <summary>
        /// 来料方式
        /// </summary>
        public IncomingMethod IncomingMethod { get; set; }

        /// <summary>
        /// 原包装数量
        /// </summary>
        public int? MPQ1 { get; set; }
        public int? MPQ2 { get; set; }
        public int? MPQ3 { get; set; }
        public int? MPQ4 { get; set; }
        public int? MPQ5 { get; set; }

        public double ShelfLife { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        [StringLength(30)]

        public string CustomerId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public Customer Customer { get; set; }
        [StringLength(30)]
  
        public string RegisterStorageId { get; set; }
        public Storage RegisterStorage { get; set; }

        public ICollection<MPNStorageAreaMap> StorageAreas { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }

    public enum MPNHierarchy
    {
        Product = 0,
        PartNo
    }

    public enum MPNLevel
    {
        A = 0,
        B,
        C
    }

    public enum MPNType
    {
        Common = 0,
        MSD,
        PCB
    }

    public enum MSDLevel
    {
        Level1 = 0,
        Level2,
        Level3,
        Level4,
        Level5,
    }

    public enum IncomingMethod
    {
        ForCustomer = 0,
        ForSelf,
        Other
    }
}
