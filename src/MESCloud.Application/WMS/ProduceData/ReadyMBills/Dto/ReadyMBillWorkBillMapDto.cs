using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReadyMBills.Dto
{
    [AutoMapFrom(typeof(ReadyMBillWorkBillMap))]
    public class ReadyMBillWorkBillMapDto : IEntityDto<string>
    {
        [Required]
        public string ReadyMBillId { get; set; }
        [Required]
        public string WorkBillId { get; set; }
        public string ProductId { get; set; }
        public string LineId { get; set; }
        /// <summary>
        /// 相关工单备料套数量
        /// </summary>
        public int Qty { get; set; }
        public bool IsActive { get; set; }
        public string Id { get; set; }
    }
}
