using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MESCloud.Entities.WMS.BaseData;

namespace MESCloud.WMS.ProduceData.ReadyMBills.Dto
{
    public class ReadyMBillMapProfile : Profile
    {
        public ReadyMBillMapProfile()
        {
            CreateMap<ReadyMBill, ReadyMBillDto>();
                //.ForMember(m => m.Productstr, opt => opt.MapFrom(s => string.Join(" | ", s.WorkBills.Select(w => w.WorkBill.ProductId))))
                //.ForMember(m => m.WorkBilQtys, opt => opt.MapFrom(s => string.Join(" | ", s.WorkBills.Select(w => w.WorkBillId + ":" + w.Qty))))
                //.ForMember(m => m.Linestr, opt => opt.MapFrom(s => string.Join(" | ", s.WorkBills.Select(w => w.WorkBill.LineId))));

            CreateMap<ReadyMBillDto, ReadyMBill>();

            CreateMap<ReadyMBillWorkBillMap, ReadyMBillWorkBillMapDto>()
                .ForMember(m => m.ProductId, opt => opt.MapFrom(s => s.WorkBill.ProductId))
                .ForMember(m => m.LineId, opt => opt.MapFrom(s => s.WorkBill.LineId));

            CreateMap<ReadyMBillWorkBillMapDto, ReadyMBillWorkBillMap>();

            CreateMap<ReadyMBillDetailedDto, ReadyMBillDetailed>();

            CreateMap<ReadyMBillDetailed, ReadyMBillDetailedDto>();

            CreateMap<ReelSendTemp, ReelSendTempDto>();

            CreateMap<ReelShortTemp, ReelShortTempDto>();

            CreateMap<Slot, ReadySlot>();
        }

    }
}
