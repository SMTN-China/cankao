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
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.StorageAreas.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.StorageAreas
{
    [AbpAuthorize(PermissionNames.Pages_StorageArea)]
    public class StorageAreaAppService : AsyncCrudAppService<StorageArea, StorageAreaDto, string, PagedResultRequestMESDto, StorageAreaDto, StorageAreaDto>, IStorageAreaAppService
    {
        readonly IRepository<StorageArea, string> _repository;
        readonly IRepository<MPN, string> _repositoryMPN;
        readonly IRepository<MPNStorageAreaMap, string> _repositoryMPNSM;

        readonly IRepository<StorageLocation, string> _repositorySL;
        public StorageAreaAppService(IRepository<StorageArea, string> repository,
            IRepository<MPN, string> repositoryMPN,
            IRepository<StorageLocation, string> repositorySL,
            IRepository<MPNStorageAreaMap, string> repositoryMPNSM


            ) : base(repository)
        {
            _repository = repository;
            _repositoryMPN = repositoryMPN;
            _repositorySL = repositorySL;
            _repositoryMPNSM = repositoryMPNSM;
        }

        [HttpPost]
        public override async Task<PagedResultDto<StorageAreaDto>> GetAll(PagedResultRequestMESDto input)
        {

            var query = MESPagedResult.GetMESPagedResult(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<StorageAreaDto>(tasksCount, taskList.MapTo<List<StorageAreaDto>>());
        }

        public async override Task<StorageAreaDto> Create(StorageAreaDto input)
        {

            var StorageArea = await base.Create(input);

            CurrentUnitOfWork.SaveChanges();


            // 更新库位区域
            var StorageLocations = _repositorySL.GetAll().Where(r => input.ShelfNames.Contains(r.Name)).ToArray();

            foreach (var StorageLocation in StorageLocations)
            {
                StorageLocation.StorageAreaId = StorageArea.Id;
            }

            // 更新料号区域
            var MPNS = _repositoryMPN.GetAll().Where(r => input.MPNIds.Contains(r.Id)).ToArray();

            foreach (var mpn in MPNS)
            {
                _repositoryMPNSM.Insert(new MPNStorageAreaMap() { MPNId = mpn.Id, StorageAreaId = StorageArea.Id });
            }

            return StorageArea;
        }

        public async override Task<StorageAreaDto> Update(StorageAreaDto input)
        {
            // 删除区域现有货架
            var OldStorageLocations = _repositorySL.GetAll().Where(r => r.StorageAreaId == input.Id).ToArray();
            foreach (var StorageLocation in OldStorageLocations)
            {
                StorageLocation.StorageAreaId = null;
            }

            // 清空区域现有物料
            var MPNStorageAreaMaps = _repositoryMPNSM.GetAll().Where(r => r.StorageAreaId == input.Id).ToArray();
            foreach (var MPNStorageAreaMap in MPNStorageAreaMaps)
            {
                _repositoryMPNSM.Delete(MPNStorageAreaMap);
            }

            CurrentUnitOfWork.SaveChanges();

            var StorageArea = await base.Update(input);

            CurrentUnitOfWork.SaveChanges();

            // 更新库位区域
            var StorageLocations = _repositorySL.GetAll().Where(r => input.ShelfNames.Contains(r.Name)).ToArray();

            foreach (var StorageLocation in StorageLocations)
            {
                StorageLocation.StorageAreaId = StorageArea.Id;
            }

            // 更新料号区域
            var MPNS = _repositoryMPN.GetAll().Where(r => input.MPNIds.Contains(r.Name)).ToArray();

            foreach (var mpn in MPNS)
            {
                _repositoryMPNSM.Insert(new MPNStorageAreaMap() { MPNId = mpn.Id, StorageAreaId = StorageArea.Id });
            }

            return StorageArea;
        }

        public override Task Delete(EntityDto<string> input)
        {
            // 删除区域现有货架
            var OldStorageLocations = _repositorySL.GetAll().Where(r => r.StorageAreaId == input.Id).ToArray();
            foreach (var StorageLocation in OldStorageLocations)
            {
                StorageLocation.StorageAreaId = null;
            }

            // 清空区域现有物料
            var MPNStorageAreaMaps = _repositoryMPNSM.GetAll().Where(r => r.StorageAreaId == input.Id).ToArray();
            foreach (var MPNStorageAreaMap in MPNStorageAreaMaps)
            {
                _repositoryMPNSM.Delete(MPNStorageAreaMap);
            }

            CurrentUnitOfWork.SaveChanges();


            return base.Delete(input);
        }



        public async Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }

        public async Task<ICollection<string>> GetShelfByKeyName(string keyName)
        {
            var res = await _repositorySL.GetAll().Where(r => r.StorageAreaId == null).GroupBy(r => r.Name).Select(r => r.Key).Where(r => r.Contains(keyName)).Take(10).ToListAsync();

            return res;
        }
    }
}
