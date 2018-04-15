using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReceivedReelBills.Dto
{
    public class ReceivedReelBillMapProfile : Profile
    {
        public ReceivedReelBillMapProfile()
        {
            CreateMap<ReceivedReelBill, ReceivedReelBillDto>();
            CreateMap<ReceivedReelBillDto, ReceivedReelBill>();
        }
    }
}
