using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocations.Dto
{
    [AutoMapFrom(typeof(StorageLocation))]
    public class StorageLocationDto :IEntityDto<string>
    {

        public string Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

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

        public string StorageId { get; set; }

        public int MainBoardId { get; set; }

        public int PositionId { get; set; }

        public string StorageLocationTypeId { get; set; }

        public bool IsBright { get; set; }

        public bool IsActive { get; set; }
    }
}
