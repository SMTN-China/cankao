using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    /// <summary>
    /// 备料单,工单备料的时候生成
    /// </summary>
    public class ReadyMBill : Entity<string>, IBaseEntities
    {
        public ICollection<ReadyMBillDetailed> ReadyMBillDetailed { get; set; }

        public ICollection<ReadyMBillWorkBillMap> WorkBills { get; set; }

        [StringLength(30)]
        public string ReReadyMBillId { get; set; }
        public ReadyMBill ReReadyMBill { get; set; }

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

        public MakeDetailsType MakeDetailsType { get; set; }

        public ReadyMType ReadyMType { get; set; }
        [StringLength(30)]
        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethod ReelMoveMethod { get; set; }

        public int ReadyMTime { get; set; }

        public bool IsUrgent { get; set; }

        public int Priority { get; set; }

        public DateTime DeliverTime { get; set; }

        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        [StringLength(100)]
        public string Productstr { get; set; }

        /// <summary>
        /// 工单数量列表
        /// </summary>
        [StringLength(100)]
        public string WorkBilQtys { get; set; }
        [StringLength(50)]
        public string Linestr { get; set; }
    }

    public enum MakeDetailsType
    {
        BOM = 0,
        Slot,
        Detailed
    }

    public enum ReadyMType
    {
        ALL = 0,
        JIT,
        Other
    }
}


