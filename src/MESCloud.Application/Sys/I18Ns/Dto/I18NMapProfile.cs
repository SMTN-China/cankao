using AutoMapper;
using MESCloud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Sys.I18Ns.Dto
{
    public class I18NMapProfile : Profile
    {
        public I18NMapProfile()
        {
            CreateMap<I18N, I18NDto>();

            CreateMap<CreateI18NDto, I18N>();
            CreateMap<I18NDto, I18N>();
        }
    }
}
