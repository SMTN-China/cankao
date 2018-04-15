using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageAreas.Dto
{
    [AutoMapFrom(typeof(StorageArea))]
    public class StorageAreaDto : IEntityDto<string>
    {
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public ICollection<string> MPNIds { get; set; }

        public ICollection<string> ShelfNames { get; set; }

        public string Id { get; set; }
    }
}
