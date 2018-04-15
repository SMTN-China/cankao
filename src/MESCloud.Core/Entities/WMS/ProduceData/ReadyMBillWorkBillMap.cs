using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class ReadyMBillWorkBillMap : Entity<string>, IBaseEntities
    {
        [StringLength(30)]

        public string ReadyMBillId { get; set; }
        public ReadyMBill ReadyMBill { get; set; }

        [StringLength(30)]

        public string WorkBillId { get; set; }
        public WorkBill WorkBill { get; set; }
        
        /// <summary>
        /// 相关工单备料套数量
        /// </summary>
        public int Qty { get; set; }
        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }

    }
}
