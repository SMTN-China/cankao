using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Slots.Dto
{
    public class SlotMapProfile :Profile
    {

        public SlotMapProfile()
        {
            CreateMap<Slot, SlotDto>();

            CreateMap<SlotDto, Slot>();
        }
    }
}
