using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.Storages.Dto
{
    [AutoMapFrom(typeof(Storage))]
    public class StorageDto : IEntityDto<string>
    {
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string Info { get; set; }
        public IncomingMethod IncomingMethod { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }
        [StringLength(500)]
        public string Address { get; set; }

        public bool IsActive { get; set; }
    }
}
