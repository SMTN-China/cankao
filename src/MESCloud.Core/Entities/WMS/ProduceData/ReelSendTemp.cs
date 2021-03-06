﻿using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{

    public class ReelSendTemp : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
     
        public string ReReadyMBillId { get; set; }
        public ReadyMBill ReReadyMBill { get; set; }
        [StringLength(36)]
  
        public string ReadyMBillDetailedId { get; set; }

        public ReadyMBillDetailed ReadyMBillDetailed { get; set; }
        [StringLength(30)]
  
        public string FisrtStorageLocationId { get; set; }
        [StringLength(30)]

        public string StorageLocationId { get; set; }
        public StorageLocation StorageLocation { get; set; }
        [StringLength(36)]

        public string BOMId { get; set; }
        public BOM BOM { get; set; }

        public int? SlotId { get; set; }
        public Slot Slot { get; set; }
        [StringLength(30)]

        public string PartNoId { get; set; }

        public MPN PartNo { get; set; }
        [StringLength(30)]

        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethod ReelMoveMethod { get; set; }
        /// <summary>
        /// 此料号总需求数量
        /// </summary>
        public int DemandQty { get; set; }
        /// <summary>
        /// 此料号总挑料数量
        /// </summary>
        public int SelectQty { get; set; }
        /// <summary>
        /// 料盘数量
        /// </summary>
        public int Qty { get; set; }
        public int DemandSendQty { get; set; }
        /// <summary>
        /// 需发数量
        /// </summary>
        public int SendQty { get; set; }
        /// <summary>
        /// 是否剪短
        /// </summary>
        public bool IsCut { get; set; }
        public bool IsSend { get; set; }
        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
