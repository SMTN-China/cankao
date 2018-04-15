using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.Storages.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using System.Linq;
using Abp.AutoMapper;

namespace MESCloud.WMS.BaseData.Storages
{
    [AbpAuthorize(PermissionNames.Pages_Storages)]
    public class StorageAppService : AsyncCrudAppService<Storage, StorageDto, string, PagedResultRequestMESDto, StorageDto, StorageDto>, IStorageAppService
    {
        readonly IRepository<Storage, string> _repository;
        public StorageAppService(IRepository<Storage, string> repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async override Task<PagedResultDto<StorageDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Storage>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<StorageDto>(tasksCount, taskList.MapTo<List<StorageDto>>());
        }


        public override Task<StorageDto> Create(StorageDto input)
        {
            return base.Create(input);
        }

        public override Task<StorageDto> Update(StorageDto input)
        {
            return base.Update(input);
        }
    }
}
