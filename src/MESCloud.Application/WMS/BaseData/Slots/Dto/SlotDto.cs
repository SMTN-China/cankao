using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.Slots.Dto
{
    [AutoMapFrom(typeof(Slot))]
    public class SlotDto: EntityDto<int>
    {
        /// <summary>
        /// 机种ID
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 物料ID
        /// </summary>
        [Required]
        public string PartNoId { get; set; }

        public int Qty { get; set; }

        /// <summary>
        /// 线别
        /// </summary>
        public string LineId { get; set; }

        public Line Line { get; set; }

        public SideType BoardSide { get; set; }

        public SideType LineSide { get; set; }
        public string Machine { get; set; }
        public string Table { get; set; }
        public string SlotName { get; set; }
        public SideType Side { get; set; }
        public string MachineType { get; set; }
        public string Location { get; set; }
        public string Feeder { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(20)]
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
