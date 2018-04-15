using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.ProduceData.ReelMoveLogs.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MESCloud.WMS.ProduceData.ReelMoveLogs
{
    [AbpAuthorize(PermissionNames.Pages_ReelMoveLogs)]
    public class ReelMoveLogAppService : ApplicationService, IReelMoveLogAppService
    {
        readonly IRepository<ReelMoveLog, string> _repositoryReelMoveLog;
        public ReelMoveLogAppService(IRepository<ReelMoveLog, string> repositoryReelMoveLog)
        {
            _repositoryReelMoveLog = repositoryReelMoveLog;
        }

        [HttpPost]
        public async Task<PagedResultDto<ReelMoveLogDto>> GetAllAsync(string reelMoveMethodId, PagedResultRequestMESDto input)
        {
            input.RequestMESDtos.Add(new RequestMESDto() { PropertyName = "ReelMoveMethodId", Operation = Operation.Equal, LinkOperation = LinkOperation.And, QueryValue = reelMoveMethodId });

            var query = MESPagedResult.GetMESPagedResult<ReelMoveLog>(input, _repositoryReelMoveLog.GetAllIncluding(s => s.CreatorUser));

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<ReelMoveLogDto>(tasksCount, taskList.MapTo<List<ReelMoveLogDto>>());
        }
    }
}
