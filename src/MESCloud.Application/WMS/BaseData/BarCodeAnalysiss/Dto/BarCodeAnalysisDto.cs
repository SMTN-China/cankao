using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.BarCodeAnalysiss.Dto
{
    [AutoMapFrom(typeof(BarCodeAnalysis))]
    public class BarCodeAnalysisDto : IEntityDto<string>
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }

        [StringLength(2000)]
        public string RegEX { get; set; }
        public bool IsActive { get; set; }
        public string Id { get; set; }

        public bool IsReplace { get; set; }

        public string Test { get; set; }

        public string TestValue { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
