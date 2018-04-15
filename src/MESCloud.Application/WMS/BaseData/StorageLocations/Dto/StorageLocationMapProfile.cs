using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocations.Dto
{
    public class MShelfMapProfile : Profile
    {
        public MShelfMapProfile()
        {
            CreateMap<StorageLocationDto, StorageLocation>();
            CreateMap<StorageLocation, StorageLocationDto>();

        }
    }
}
