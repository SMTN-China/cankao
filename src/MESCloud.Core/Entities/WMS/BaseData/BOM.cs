using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class BOM : Entity<string>, IBaseEntities
    {

        /// <summary>
        /// 机种ID
        /// </summary>
        [StringLength(30)]
        public string ProductId { get; set; }

        public MPN Product { get; set; }

        /// <summary>
        /// 物料ID
        /// </summary>
        [StringLength(30)]
        public string PartNoId { get; set; }

        public MPN PartNo { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 允许超发
        /// </summary>
        public bool AllowableMoreSend { get; set; }
        /// <summary>
        /// 超发百分比
        /// </summary>
        public double MoreSendPercentage { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(10)]
        public string Version { get; set; }



        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
