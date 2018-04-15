using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.UPHs.Dto
{
    public class UPHMapProfile : Profile
    {
        public UPHMapProfile()
        {
            CreateMap<UPH, UPHDto>();

            CreateMap<UPHDto, UPH>();
        }
    }
}
