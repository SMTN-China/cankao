using AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReelSupplyTemps.Dto
{
    public class ReelSupplyTempMapProfile : Profile
    {
        public ReelSupplyTempMapProfile()
        {
            CreateMap<ReelSupplyTemp, ReelSupplyTempDto>();

            CreateMap<ReelSupplyTempDto, ReelSupplyTemp>();
        }
    }
}
