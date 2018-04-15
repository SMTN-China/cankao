using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReelMoveLogs.Dto
{
    public class ReelMoveLogMapProfile : Profile
    {
        public ReelMoveLogMapProfile()
        {
            CreateMap<ReelMoveLog, ReelMoveLogDto>()
                 .ForMember(m => m.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.Name));
        }
    }
}
