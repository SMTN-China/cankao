using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class MPNStorageAreaMap : Entity<string>, IBaseEntities
    {

        [StringLength(30)]
        public string MPNId { get; set; }
        public MPN MPN { get; set; }

        [StringLength(30)]
        public string StorageAreaId { get; set; }
        public StorageArea StorageArea { get; set; }

        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
    }
}
