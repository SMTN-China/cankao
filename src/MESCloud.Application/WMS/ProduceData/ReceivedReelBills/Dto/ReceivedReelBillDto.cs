using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReceivedReelBills.Dto
{

    public class ReceivedReelBillDto : EntityDto<string>
    {

        public string ReceivedId {get;set;}
        public string PoId { get; set; }
        public string IQCCheckId { get; set; }
        public string PartNoId { get; set; }
        public int Qty { get; set; }
        public int ReceivedQty { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
    }
}
