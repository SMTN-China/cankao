using AutoMapper;
using MESCloud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Orgs.Dto
{
  public  class OrgMapProfile : Profile
    {
        public OrgMapProfile()
        {
            // 基本映射
            CreateMap<Org, OrgDto>().ForMember(m => m.ParentName, opt => opt.MapFrom(s => s.Parent.Code));

            CreateMap<CreateOrgDto, Org>();

        }
    }
}
