using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class Reel : Entity<string>, IBaseEntities, IDeletionAudited
    {
        [StringLength(30)]
        [Required]
        public string PartNoId { get; set; }
        public int Qty { get; set; }
        [StringLength(30)]

        public string Supplier { get; set; }
        public DateTime MakeDate { get; set; }
        [StringLength(15)]

        public string DateCode { get; set; }
        [StringLength(50)]

        public string LotCode { get; set; }
        [StringLength(30)]

        public string BatchCode { get; set; }

        public bool IsUseed { get; set; }

        public double ExtendShelfLife { get; set; }
        [StringLength(30)]

        public string ReadyMBillId { get; set; }
        [StringLength(36)]

        public string WorkBillDetailedId { get; set; }
        [StringLength(36)]

        public string ReadyMBillDetailedId { get; set; }
        [StringLength(30)]

        public string WorkBillId { get; set; }
        [StringLength(30)]
        public string StorageLocationId { get; set; }
        [StringLength(30)]

        public string StorageId { get; set; }
        /// <summary>
        /// 收料单
        /// </summary>
        [StringLength(36)]

        public string ReceivedId { get; set; }
        /// <summary>
        /// Po
        /// </summary>
        [StringLength(30)]

        public string PoId { get; set; }
        /// <summary>
        /// 检验单号
        /// </summary>
        [StringLength(30)]

        public string IQCCheckId { get; set; }

        public bool IsFirstSelected { get; set; }

        public WorkBillDetailed WorkBillDetailed { get; set; }
        public ReadyMBillDetailed ReadyMBillDetailed { get; set; }
        public WorkBill WorkBill { get; set; }
        public ReadyMBill ReadyMBill { get; set; }
        public MPN PartNo { get; set; }
        public Storage Storage { get; set; }
        public int? SlotId { get; set; }
        public Slot Slot { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }



        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
