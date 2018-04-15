using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.StorageLocations.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocationTypes.Dto
{
    public class StorageLocationTypeMapProfile : Profile
    {
        public StorageLocationTypeMapProfile()
        {
            CreateMap<StorageLocationTypeDto, StorageLocationType>();
            CreateMap<StorageLocationType, StorageLocationTypeDto>();
        }
    }
}
