using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReelMoveMethods.Dto
{
    [AutoMapFrom(typeof(ReelMoveMethod))]
    public class ReelMoveMethodDto : EntityDto<string>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }

        public ICollection<RMMStorageMapDto> OutStorages { get; set; }

        public ICollection<AllocationType> AllocationTypes { get; set; }
        public string AllocationTypesStr { get; set; }
        public string OutStorageIds { get; set; }
        public bool IsActive { get; set; }
        public string InStorageId { get; set; }
    }
}
