using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.Lines.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq;
using Abp.AutoMapper;
using MESCloud.WMS.BaseData.Storages.Dto;
using AutoMapper;

namespace MESCloud.WMS.BaseData.Lines
{
    [AbpAuthorize(PermissionNames.Pages_Lines)]
    public class LineAppService : AsyncCrudAppService<Line, LineDto, string, PagedResultRequestMESDto, LineDto, LineDto>, ILineAppService
    {
        readonly IRepository<Line, string> _repository;
        readonly IRepository<Storage, string> _repositoryStorage;
        public LineAppService(IRepository<Line, string> repository, IRepository<Storage, string> repositoryStorage) : base(repository)
        {
            _repository = repository;
            _repositoryStorage = repositoryStorage;
        }
        [HttpPost]
        public async override Task<PagedResultDto<LineDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Line>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<LineDto>(tasksCount, taskList.MapTo<List<LineDto>>());
        }

        public override Task<LineDto> Create(LineDto input)
        {
            return base.Create(input);
        }

        public async Task<ICollection<StorageDto>> GetCStorageByKeyName(string keyName)
        {
            var res = await _repositoryStorage.GetAll().Where(c => (c.IncomingMethod == IncomingMethod.ForCustomer || c.IncomingMethod == IncomingMethod.Other) && c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<Storage>, List<StorageDto>>(res);
        }

        public async Task<ICollection<StorageDto>> GetSStorageByKeyName(string keyName)
        {
            var res = await _repositoryStorage.GetAll().Where(c => (c.IncomingMethod == IncomingMethod.ForSelf || c.IncomingMethod == IncomingMethod.Other) && c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<Storage>, List<StorageDto>>(res);
        }
    }
}
