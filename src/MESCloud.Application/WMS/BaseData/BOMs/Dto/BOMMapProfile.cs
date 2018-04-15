using Abp.Domain.Repositories;
using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.BOMs.Dto
{
    public class BOMMapProfile : Profile
    {
        public BOMMapProfile()
        {
            CreateMap<string, MPN>().ConstructUsing(s => null);

            CreateMap<ProductDto, MPN>();
          

            CreateMap<CreateBOMDto, BOM>();

            CreateMap<BOM, ProductDto>();

            CreateMap<MPN, ProductDto>();
        }
    }
}
