using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class RMMStorageMap : Entity<string>, IBaseEntities
    {

        [StringLength(30)]
        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethod ReelMoveMethod { get; set; }


        [StringLength(30)]
        public string StorageId { get; set; }
        public Storage Storage { get; set; }

        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
