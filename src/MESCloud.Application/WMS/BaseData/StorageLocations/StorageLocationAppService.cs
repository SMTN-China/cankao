using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.StorageLocations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using MESCloud.WMS.BaseData.Storages.Dto;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using MESCloud.WMS.BaseData.StorageLocationTypes.Dto;

namespace MESCloud.WMS.BaseData.StorageLocations
{
    [AbpAuthorize(PermissionNames.Pages_StorageLocations)]
    public class StorageLocationAppService : AsyncCrudAppService<StorageLocation, StorageLocationDto, string, PagedResultRequestMESDto, StorageLocationDto, StorageLocationDto>, IStorageLocationAppService
    {
        IRepository<StorageLocationType, string> _repositoryT;
        IRepository<Storage, string> _repositoryS;
        readonly LightService LightService;

        IRepository<StorageLocation, string> _repository;
        public StorageLocationAppService(
            IRepository<StorageLocation, string> repository,
            IRepository<StorageLocationType, string> repositoryT,
            IRepository<Storage, string> repositoryS,
             LightService lightService
            ) : base(repository)
        {
            _repositoryT = repositoryT;
            _repository = repository;
            _repositoryS = repositoryS;
            LightService = lightService;
        }

        public async Task AddByHF(HFDto hFDto)
        {
            List<StorageLocationDto> list = new List<StorageLocationDto>();

            for (int i = 0; i < hFDto.Bus.Length; i++)
            {
                List<string[]> lightBars = new List<string[]>();
                switch (hFDto.Bus[i])
                {
                    case 1:
                        for (int j = 1; j <= hFDto.LayerCount; j++)
                        {
                            lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "1", "009" });
                        }
                        if (hFDto.MoreSurface)
                        {
                            for (int j = 11; j <= 11 + hFDto.LayerCount - 1; j++)
                            {
                                lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "1", "019" });
                            }
                        }

                        break;
                    case 2:
                        for (int j = 51; j <= 51 + hFDto.LayerCount - 1; j++)
                        {
                            lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "2", "059" });
                        }
                        if (hFDto.MoreSurface)
                        {
                            for (int j = 61; j <= 61 + hFDto.LayerCount - 1; j++)
                            {
                                lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "2", "069" });
                            }
                        }
                        break;
                    case 3:
                        for (int j = 101; j <= 101 + hFDto.LayerCount - 1; j++)
                        {
                            lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "3", "109" });
                        }
                        if (hFDto.MoreSurface)
                        {
                            for (int j = 111; j <= 111 + hFDto.LayerCount - 1; j++)
                            {
                                lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "3", "119" });
                            }
                        }
                        break;
                    case 4:
                        for (int j = 151; j <= 151 + hFDto.LayerCount - 1; j++)
                        {
                            lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "4", "159" });
                        }
                        if (hFDto.MoreSurface)
                        {
                            for (int j = 161; j <= 161 + hFDto.LayerCount - 1; j++)
                            {
                                lightBars.Add(new string[] { j.ToString().PadLeft(3, '0'), "4", "169" });
                            }
                        }
                        break;
                    default:
                        break;
                }
                GetStorageLocationDto(list, lightBars.ToArray(), hFDto.LastIP, hFDto.ShelfCode[i], hFDto.StorageId, hFDto.StorageLocationTypeId);
            }

            foreach (var item in list)
            {
                if (_repository.FirstOrDefault(s => s.Id == item.Id) == null)
                {
                    await Create(item);
                }
                else
                {
                    await Update(item);
                }
            }
            // throw new NotImplementedException();
        }

        void GetStorageLocationDto(List<StorageLocationDto> list, string[][] lightBars, string ip, string shelfCode, string storageId, string storageLocationTypeId)
        {
            foreach (var item in lightBars)
            {
                for (int i = 1; i < 101; i++)
                {
                    list.Add(new StorageLocationDto()
                    {
                        Code = shelfCode,
                        Name = shelfCode,
                        IsActive = true,
                        MainBoardId = int.Parse(ip + item[1] + item[2]),
                        PositionId = int.Parse(item[1] + item[0] + i.ToString().PadLeft(3, '0')),
                        StorageId = storageId,
                        StorageLocationTypeId = storageLocationTypeId,
                        Id = shelfCode + GetABNo(item[0]) + i.ToString().PadLeft(3, '0'),
                    });
                }
            }
        }
        string GetABNo(string lightBar)
        {
            int[] list1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 51, 52, 53, 54, 55, 56, 57, 101, 102, 103, 104, 105, 106, 107, 151, 152, 153, 154, 155, 156, 157 };
            int bar = int.Parse(lightBar);
            if (list1.Contains(bar))
            {
                return "A" + bar.ToString().Substring(bar.ToString().Length - 1);
            }
            else
            {
                return "B" + bar.ToString().Substring(bar.ToString().Length - 1);
            }
        }

        public async Task AddByLY(LYDto lYDto)
        {
            List<StorageLocationDto> list = new List<StorageLocationDto>();
            for (int i = 0; i < lYDto.ShelfCode.Length; i++)
            {
                for (int j = 1; j <= lYDto.Count; j++)
                {
                    list.Add(new StorageLocationDto()
                    {
                        Code = lYDto.ShelfCode[i],
                        Name = lYDto.ShelfCode[i],
                        Id = lYDto.ShelfId[i] + j.ToString().PadLeft(4, '0'),
                        MainBoardId = j > 700 ? lYDto.mainId[i][1] : lYDto.mainId[i][0],
                        PositionId = j,
                        IsActive = true,
                        StorageId = lYDto.StorageId,
                        StorageLocationTypeId = lYDto.StorageLocationTypeId
                    });
                }
            }



            foreach (var item in list)
            {
                if (_repository.FirstOrDefault(s => s.Id == item.Id) == null)
                {
                    await Create(item);
                }
                else
                {
                    await Update(item);
                }
            }
        }

        [HttpPost]
        public async override Task<PagedResultDto<StorageLocationDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<StorageLocation>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<StorageLocationDto>(tasksCount, taskList.MapTo<List<StorageLocationDto>>());
        }

        public async Task<bool> GetIsHave(string id)
        {
            var res = await _repository.FirstOrDefaultAsync(id);

            return res != null;
        }

        public async Task<ICollection<StorageDto>> GetStorageByKeyName(string keyName)
        {
            var res = await _repositoryS.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<Storage>, List<StorageDto>>(res);
        }

        public async Task<ICollection<StorageLocationTypeDto>> GetStorageLocationTypeByKeyName(string keyName)
        {
            var res = await _repositoryT.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<StorageLocationType>, List<StorageLocationTypeDto>>(res);
        }

        public async Task AllBright()
        {
            var lights = await _repository.GetAll().GroupBy(r => r.MainBoardId).Select(r => new AllLight { lightOrder = 1, MainBoardId = r.Key }).Distinct().ToListAsync();

            // 小灯,灯塔
            LightService.AllLightOrder(lights);

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        public async Task AllExtinguished()
        {
            var lights = await _repository.GetAll().GroupBy(r => r.MainBoardId).Select(r => new AllLight { lightOrder = 0, MainBoardId = r.Key }).Distinct().ToListAsync();

            // 小灯
            LightService.AllLightOrder(lights);


            // 灯塔
            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        public async Task NonReelBright()
        {
            // 查询所有空库位

            var lights = await _repository.GetAll().Where(r => r.ReelId == null).Select(r => new StorageLight { lightOrder = 1, MainBoardId = r.MainBoardId, ContinuedTime = 10, RackPositionId = r.PositionId }).Distinct().ToListAsync();

            // 小灯
            LightService.LightOrder(lights);


            // 灯塔
            var mains = lights.GroupBy(r => new AllLight { lightOrder = 0, MainBoardId = r.MainBoardId }).Select(r => r.Key).ToList();

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }
    }

}
