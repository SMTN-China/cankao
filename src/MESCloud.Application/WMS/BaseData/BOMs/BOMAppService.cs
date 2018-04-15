using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.BOMs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using System.Linq;
using Abp.AutoMapper;
using MESCloud.WMS.BaseData.MPNs.Dto;
using AutoMapper;
using Abp.Authorization;
using MESCloud.Authorization;
using Microsoft.AspNetCore.Mvc;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MESCloud.Utils;
using MESCloud.Sys.I18Ns;
using MESCloud.Entities;
using Microsoft.AspNetCore.Http;
using Abp.Auditing;

namespace MESCloud.WMS.BaseData.BOMs
{
    // [AbpAuthorize(PermissionNames.Pages_BOMs)]
    public class BOMAppService : AsyncCrudAppService<BOM, ProductDto, string, PagedResultRequestMESDto, CreateBOMDto, BOMDto>, IBOMAppService
    {
        readonly IRepository<BOM, string> _repository;
        readonly IRepository<MPN, string> _repositoryMPN;
        MapperConfiguration config;
        public BOMAppService(IRepository<BOM, string> repository, IRepository<MPN, string> repositoryMPN, FileHelperService fileHelper, IRepository<I18N, int> repositoryI18N) : base(repository)
        {
            _repository = repository;
            _repositoryMPN = repositoryMPN;
            config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<MPN, ProductDto>().ForMember(m => m.ItemCount, opt => opt.MapFrom(s => _repository.GetAllList(b => b.ProductId == s.Id).Count));

               cfg.CreateMap<BOM, BOMDto>();
           }
              );

        }
        [HttpPost]
        public async override Task<PagedResultDto<ProductDto>> GetAll(PagedResultRequestMESDto input)
        {
            // 查询 添加组件查询
            // var query = _repositoryMPN.GetAll().Where(m => m.MPNHierarchy == MPNHierarchy.组件);
            input.RequestMESDtos.Add(new RequestMESDto() { LinkOperation = LinkOperation.And, Operation = Operation.Equal, PropertyName = "MPNHierarchy", QueryValue = 0 });

            //获取总数
            var query = MESPagedResult.GetMESPagedResult<MPN>(input, _repositoryMPN.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).Include(b => b.Customer).ToList();

            return new PagedResultDto<ProductDto>(tasksCount, config.CreateMapper().Map<List<MPN>, List<ProductDto>>(taskList));
        }
        [HttpPost]
        public PagedResultDto<BOMDto> GetItemsById(string Id, PagedResultRequestMESDto input)
        {
            // 查询
            // var query = _repository.GetAll().Where(m => m.ProductId == Id);


            var query = config.CreateMapper().Map<List<BOM>, List<BOMDto>>(
             _repository.GetAll().Where(m => m.ProductId == Id).ToList());

            var res = MESPagedResult.GetMESPagedResult<BOMDto>(input, query.AsQueryable());

            var tasksCount = res.Count();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = res.PageBy(input).ToList();

            return new PagedResultDto<BOMDto>(tasksCount, res.ToList());
        }

        public async Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }

        public async Task<ICollection<MPNDto>> GetProductByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(c => c.MPNHierarchy == MPNHierarchy.Product && c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }

    }
}
