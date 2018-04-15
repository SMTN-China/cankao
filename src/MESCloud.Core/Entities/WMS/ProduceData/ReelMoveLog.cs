using Abp.Domain.Entities;
using MESCloud.Authorization.Users;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class ReelMoveLog : Entity<string>, IBaseEntities
    {
        // 暂时这样 后续有应该会加入 收料单 和 订单 的关联

        [StringLength(30)]

        public string WorkBillId { get; set; }
        public WorkBill WorkBill { get; set; }
        [StringLength(30)]
   
        public string ReadyMBillId { get; set; }
        public ReadyMBill ReadyMBill { get; set; }

        public string WorkBillDetailedId { get; set; }
        [StringLength(36)]
 
        public WorkBillDetailed WorkBillDetailed { get; set; }
        [StringLength(36)]

        public string ReadyMBillDetailedId { get; set; }
        public ReadyMBillDetailed ReadyMBillDetailed { get; set; }
        [StringLength(30)]

        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethod ReelMoveMethod { get; set; }
        [StringLength(60)]

        public string ReelId { get; set; }
        [StringLength(30)]

        public string PartNoId { get; set; }
        public Reel Reel { get; set; }
        [StringLength(30)]

        public string StorageLocationId { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public Slot Slot { get; set; }

        public int? SlotId { get; set; }

        public ReceivedReelBill ReceivedReelBill { get; set; }
        [StringLength(36)]

        public string ReceivedReelBillId { get; set; }

        public int Qty { get; set; }
        public User CreatorUser { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
