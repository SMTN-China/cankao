﻿using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class ReelShortTemp : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
   
        public string ReReadyMBillId { get; set; }
        public ReadyMBill ReReadyMBill { get; set; }
        [StringLength(36)]
  
        public string ReadyMBillDetailedId { get; set; }

        public ReadyMBillDetailed ReadyMBillDetailed { get; set; }
        [StringLength(36)]
    
        public string BOMId { get; set; }
        public BOM BOM { get; set; }

        public int? SlotId { get; set; }
        public Slot Slot { get; set; }
        [StringLength(30)]
 
        public string PartNoId { get; set; }

        public MPN PartNo { get; set; }

        /// <summary>
        /// 此料号总需求数量
        /// </summary>
        public int DemandQty { get; set; }
        public int DemandSendQty { get; set; }
        /// <summary>
        /// 此料号总挑料数量
        /// </summary>
        public int SelectQty { get; set; }
        /// <summary>
        /// 缺料数量
        /// </summary>
        public int ShortQty { get; set; }
        
        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
