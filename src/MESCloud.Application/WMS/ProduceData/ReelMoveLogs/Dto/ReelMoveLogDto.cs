using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReelMoveLogs.Dto
{
    [AutoMapFrom(typeof(ReelMoveLog))]
    public class ReelMoveLogDto
    {
        public string WorkBillId { get; set; }

        public string ReadyMBillId { get; set; }

        public string WorkBillDetailedId { get; set; }

        public string ReadyMBillDetailedId { get; set; }

        public string ReelMoveMethodId { get; set; }

        public string ReelId { get; set; }

        public string PartNoId { get; set; }


        public string StorageLocationId { get; set; }

        public int Qty { get; set; }

        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
