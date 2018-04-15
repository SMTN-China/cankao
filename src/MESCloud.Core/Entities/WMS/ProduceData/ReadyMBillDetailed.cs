using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    /// <summary>
    /// 备料单详细
    /// </summary>
    public class ReadyMBillDetailed : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
        public string ReadyMBillId { get; set; }
        public ReadyMBill ReadyMBill { get; set; }

        /// <summary>
        /// 物料ID
        /// </summary>
        [StringLength(30)]
        public string PartNoId { get; set; }

        public MPN PartNo { get; set; }
        [StringLength(30)]

        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethod ReelMoveMethod { get; set; }
        /// <summary>
        /// 合并后的需求数
        /// </summary>
        public int DemandQty { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public int Qty { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// 发料数量
        /// </summary>
        public int SendQty { get; set; }
        [StringLength(36)]

        public string BOMId { get; set; }
        public BOM BOM { get; set; }
        [StringLength(50)]

        public string Suppliers { get; set; }

        /// <summary>
        /// 指定批次号,之间 “|”分割
        /// </summary>
        [StringLength(50)]

        public string BatchCodes { get; set; }

        /// <summary>
        /// 指定替代料,之间 “|”分割
        /// </summary>
        [StringLength(50)]


        public string ReplacePNs { get; set; }

        /// <summary>
        /// 优先替代料
        /// </summary>
        public bool PriorityReplacePN { get; set; }

        public bool IsCut { get; set; }

        /// <summary>
        /// 退料数量
        /// </summary>
        public int ReturnQty { get; set; }
        /// <summary>
        /// 沿用数量
        /// </summary>
        public int FollowQty { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
