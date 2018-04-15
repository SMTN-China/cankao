using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class WorkBillDetailed : Entity<string>, IBaseEntities
    {
        [StringLength(30)]

        public string WorkBillId { get; set; }
        public WorkBill WorkBill { get; set; }
        /// <summary>
        /// 物料ID
        /// </summary>

        [StringLength(30)]
        public string PartNoId { get; set; }

        public MPN PartNo { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public int Qty { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// 扫描数量
        /// </summary>
        public int SendQty { get; set; }
        [StringLength(36)]

        public string BOMId { get; set; }
        public BOM BOM { get; set; }

        public int? SlotId { get; set; }
        public Slot Slot { get; set; }

        /// <summary>
        /// 退料数量
        /// </summary>
        public int ReturnQty { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
