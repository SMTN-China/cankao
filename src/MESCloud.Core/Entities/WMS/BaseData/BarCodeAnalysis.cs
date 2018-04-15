using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class BarCodeAnalysis : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string ClassName { get; set; }
        [StringLength(30)]
        public string PropertyName { get; set; }

        [StringLength(2000)]
        public string RegEX { get; set; }
        public bool IsReplace { get; set; }
        public bool IsActive { get; set; }
        [StringLength(500)]
        public string Test { get; set; }
        [StringLength(100)]
        public string TestValue { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }

    }
}
