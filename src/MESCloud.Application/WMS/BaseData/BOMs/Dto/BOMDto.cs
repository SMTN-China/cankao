using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.BOMs.Dto
{
    [AutoMapFrom(typeof(BOM))]
    public class BOMDto: CreateBOMDto, IEntityDto<string>
    {
       
        public string Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
