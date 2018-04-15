using Abp.Domain.Repositories;
using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.MPNs.Dto
{
    public class MPNMapProfile : Profile
    {
        public MPNMapProfile()
        {
            CreateMap<MPNDto, MPN>();
            CreateMap<MPN, MPNDto>();

        }
    }
}
