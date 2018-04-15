using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class UPH : Entity<string>, IBaseEntities
    {
        public MPN Product { get; set; }
        [StringLength(30)]
        public string ProductId { get; set; }

        public Line Line { get; set; }
        [StringLength(30)]
        public string LineId { get; set; }

        public int Meter { get; set; }

        public int Pin { get; set; }

        public int Qty { get; set; }
        [StringLength(200)]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
