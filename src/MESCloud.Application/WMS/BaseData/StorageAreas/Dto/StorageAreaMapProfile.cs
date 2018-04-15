using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageAreas.Dto
{
    public class StorageAreaMapProfile:Profile
    {
        public StorageAreaMapProfile()
        {
            CreateMap<StorageAreaDto, StorageArea>();
            CreateMap<StorageArea, StorageAreaDto>();

        }
    }
}
