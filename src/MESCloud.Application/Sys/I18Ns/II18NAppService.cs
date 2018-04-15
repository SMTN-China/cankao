using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using MESCloud.CommonDto;
using MESCloud.Sys.I18Ns.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.Sys.I18Ns
{
 
    public interface II18NAppService:IAsyncCrudAppService<I18NDto, int, PagedResultRequestMESDto, CreateI18NDto, I18NDto>
    {
        List<string> GetDtoByKeyName(string keyName);

        List<string> GetPropertyByDtoName(string dtoName);
    }
}
