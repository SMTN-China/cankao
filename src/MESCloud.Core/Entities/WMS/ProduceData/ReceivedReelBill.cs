using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class ReceivedReelBill : Entity<string>, IBaseEntities
    {
        [StringLength(30)]

        public string ReceivedId { get; set; }
        [StringLength(200)]

        public string PoId { get; set; }
        [StringLength(30)]

        public string IQCCheckId { get; set; }
        [StringLength(30)]

        public string PartNoId { get; set; }
        public MPN PartNo{ get; set; }
        public int Qty { get; set; }
        public int ReceivedQty { get; set; }

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
    }
}
