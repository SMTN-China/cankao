using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.ProduceData.WorkBills.Dto
{
    [AutoMapFrom(typeof(WorkBill))]
    public class WorkBillDto: EntityDto<string>
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string LineId { get; set; }

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

        public int Qty { get; set; }

        public bool IsActive { get; set; }

        public DateTime PlanStartTime { get; set; }

        public DateTime PlanEndTime { get; set; }

        public WorkBillStatus WorkBillStatus { get; set; }
    }
}
