using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.ProduceData.ReceivedReelBills.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.Reels.Dto
{
    public class GetReceivedsResult
    {
        public string Msg { get; set; }

        public ICollection<ReceivedReelBillDto> ReceivedReelBills { get; set; }
    }
}
