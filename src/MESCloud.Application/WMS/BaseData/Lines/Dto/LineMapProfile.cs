using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Lines.Dto
{
    public class LineMapProfile : Profile
    {
        public LineMapProfile()
        {
            CreateMap<Line, LineDto>();

            CreateMap<LineDto, Line>();
        }
    }
}
