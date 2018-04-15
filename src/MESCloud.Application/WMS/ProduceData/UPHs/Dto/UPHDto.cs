using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.UPHs.Dto
{
   public class UPHDto : EntityDto<string>
    {
        public string ProductId { get; set; }

        public string LineId { get; set; }

        public int Meter { get; set; }

        public int Pin { get; set; }
        public int Qty { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
