using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MESCloud.WMS.ProduceData.ReelMoveMethods.Dto
{
    public class ReelMoveMethodMapProfile : Profile
    {

        public ReelMoveMethodMapProfile()
        {
            CreateMap<ReelMoveMethod, ReelMoveMethodDto>()
                .ForMember(m => m.OutStorageIds, opt => opt.MapFrom(s => string.Join(" | ", s.OutStorages.Select(w => w.StorageId))));

            CreateMap<ReelMoveMethodDto, ReelMoveMethod>()
                 .ForMember(m => m.AllocationTypesStr, opt => opt.MapFrom(s => string.Join(" | ", s.AllocationTypes.Select(e => e.ToString())))); ;

            CreateMap<RMMStorageMap, RMMStorageMapDto>();

            CreateMap<RMMStorageMapDto, RMMStorageMap>();
        }
    }
}
