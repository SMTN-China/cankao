using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Storages.Dto
{
  public  class StorageMapProfile:Profile
    {
        public StorageMapProfile()
        {
            CreateMap<StorageDto, Storage>();

            CreateMap<Storage, StorageDto>();

        }
    }
}
