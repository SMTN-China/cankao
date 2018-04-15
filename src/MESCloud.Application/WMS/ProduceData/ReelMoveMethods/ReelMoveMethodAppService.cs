using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.Storages.Dto;
using MESCloud.WMS.ProduceData.ReelMoveMethods.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReelMoveMethods
{
    [AbpAuthorize(PermissionNames.Pages_ReelMoveMethods)]
    public class ReelMoveMethodAppService : AsyncCrudAppService<ReelMoveMethod, ReelMoveMethodDto, string, PagedResultRequestMESDto, ReelMoveMethodDto, ReelMoveMethodDto>, IReelMoveMethodAppService
    {
        readonly IRepository<ReelMoveMethod, string> _repository;

        readonly IRepository<RMMStorageMap, string> _repositoryRMMStorageMap;

        readonly IRepository<Storage, string> _repositoryStorage;
        public ReelMoveMethodAppService(IRepository<ReelMoveMethod, string> repository, IRepository<RMMStorageMap, string> repositoryRMMStorageMap, IRepository<Storage, string> repositoryStorage) : base(repository)
        {
            _repository = repository;
            _repositoryRMMStorageMap = repositoryRMMStorageMap;
            _repositoryStorage = repositoryStorage;
        }

        [HttpPost]
        public async override Task<PagedResultDto<ReelMoveMethodDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<ReelMoveMethod>(input, _repository.GetAll());



            var tasksCount = await query.CountAsync();
            query = query.Include(r => r.OutStorages);

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<ReelMoveMethodDto>(tasksCount, taskList.MapTo<List<ReelMoveMethodDto>>());
        }


        public override Task<ReelMoveMethodDto> Update(ReelMoveMethodDto input)
        {
            foreach (var item in _repositoryRMMStorageMap.GetAll().Where(r => r.ReelMoveMethodId == input.Id).ToList())
            {
                _repositoryRMMStorageMap.Delete(item.Id);
            }
            CurrentUnitOfWork.SaveChanges();


            return base.Update(input);
        }

        public async Task<ICollection<StorageDto>> GetStorageKeyName(string keyName)
        {
            var res = await _repositoryStorage.GetAll().Where(w => w.Id.Contains(keyName)).Take(10).ToListAsync(); ;

            return Mapper.Map<List<Storage>, List<StorageDto>>(res);
        }
    }
}
