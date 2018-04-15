using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.BaseData
{
    public class Slot : Entity, IBaseEntities
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

        public int Qty { get; set; }

        public int Index { get; set; }

        /// <summary>
        /// 线别
        /// </summary>
        [StringLength(30)]
        public string LineId { get; set; }

        public Line Line { get; set; }

        public SideType BoardSide { get; set; }

        public SideType LineSide { get; set; }
        [StringLength(30)]

        public string Machine { get; set; }
        [StringLength(10)]
        public string Table { get; set; }

        [StringLength(30)]
        public string SlotName { get; set; }
        public SideType Side { get; set; }

        [StringLength(30)]
        public string MachineType { get; set; }
        [StringLength(1000)]

        public string Location { get; set; }
        [StringLength(50)]

        public string Feeder { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(10)]
        public string Version { get; set; }
        public bool IsActive { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }


    }

    public enum SideType
    {
        T = 0,
        B,
        L,
        R
    }
}
