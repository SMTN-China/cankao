using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.WorkBills.Dto
{
    public class WorkBillMapProfile:Profile
    {
        public WorkBillMapProfile()
        {
            CreateMap<WorkBill, WorkBillDto>();

            CreateMap<WorkBillDto, WorkBill>();
        }
    }
}
