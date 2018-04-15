using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.BarCodeAnalysiss.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.BarCodeAnalysiss
{
    public interface IBarCodeAnalysisAppService : IAsyncCrudAppService<BarCodeAnalysisDto, string, PagedResultRequestMESDto, BarCodeAnalysisDto, BarCodeAnalysisDto>
    {
        Task<List<string>> TestAnalysis(TestAnalysisDto testAnalysisDto);


        Task<AnalysisResDto> Analysis(AnalysisDto analysisDto);
    }
}
