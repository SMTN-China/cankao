using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.StorageLocationTypes.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.StorageLocationTypes
{
    [AbpAuthorize(PermissionNames.Pages_StorageLocationTypes)]
    public class StorageLocationTypeAppService : AsyncCrudAppService<StorageLocationType, StorageLocationTypeDto, string, PagedResultRequestMESDto, StorageLocationTypeDto, StorageLocationTypeDto>, IStorageLocationTypeAppService
    {
        readonly IRepository<StorageLocationType, string> _repository;
        public StorageLocationTypeAppService(IRepository<StorageLocationType, string> repository) : base(repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async override Task<PagedResultDto<StorageLocationTypeDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<StorageLocationType>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<StorageLocationTypeDto>(tasksCount, taskList.MapTo<List<StorageLocationTypeDto>>());
        }
    }
}
