using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.BarCodeAnalysiss.Dto
{
    public class BarCodeAnalysisMapProfile : Profile
    {
        public BarCodeAnalysisMapProfile()
        {
            CreateMap<BarCodeAnalysis, BarCodeAnalysisDto>();

            CreateMap<BarCodeAnalysisDto, BarCodeAnalysis>();
        }
    }
}
