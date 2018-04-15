using Abp.Domain.Entities;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class StorageLocation : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
        public string Code { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
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

        [StringLength(60)]
        public string ReelId { get; set; }

        public int MainBoardId { get; set; }

        public int PositionId { get; set; }
        [StringLength(30)]
        public string StorageLocationTypeId { get; set; }

        public StorageLocationType StorageLocationType { get; set; }
        [StringLength(30)]
        public string StorageAreaId { get; set; }

        public StorageArea StorageArea { get; set; }
        [StringLength(30)]
        public string StorageId { get; set; }

        public Storage Storage { get; set; }

        public bool IsBright { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
