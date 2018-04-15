using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocationTypes.Dto
{
    [AutoMapFrom(typeof(StorageLocationType))]
    public class StorageLocationTypeDto : IEntityDto<string>
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
        public bool MoreMateriel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }

        public bool IsActive { get; set; }
        public string Id { get; set; }
    }
}
