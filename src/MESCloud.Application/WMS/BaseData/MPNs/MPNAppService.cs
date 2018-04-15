using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.MPNs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using MESCloud.WMS.BaseData.Customers.Dto;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using Abp.Web.Models;
using MESCloud.Entities;
using Abp.Configuration;

namespace MESCloud.WMS.BaseData.MPNs
{
    [AbpAuthorize(PermissionNames.Pages_MPNs)]
    public class MPNAppService : AsyncCrudAppService<MPN, MPNDto, string, PagedResultRequestMESDto, MPNDto, MPNDto>, IMPNAppService
    {
        readonly IRepository<MPN, string> _repository;
        readonly IRepository<Customer, string> _repositoryCustomer;
        readonly IRepository<Setting, long> _repositoryT;
        public MPNAppService(IRepository<MPN, string> repository, IRepository<Customer, string> repositoryCustomer, IRepository<Setting, long> repositoryT) : base(repository)
        {
            this._repository = repository;
            this._repositoryCustomer = repositoryCustomer;
            _repositoryT = repositoryT;
        }

        [HttpPost]
        public async override Task<PagedResultDto<MPNDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).Include(m => m.Customer).ToList();

            return new PagedResultDto<MPNDto>(tasksCount, taskList.MapTo<List<MPNDto>>());
        }

        public override Task<MPNDto> Create(MPNDto input)
        {
            return base.Create(input);
        }
        public override Task<MPNDto> Update(MPNDto input)
        {
            return base.Update(input);
        }

        public async Task<ICollection<CustomerDto>> GetCustomerById(string Id)
        {
            var res = await _repositoryCustomer.GetAll().Where(c => c.Id.Contains(Id)).Take(10).ToListAsync();

            return Mapper.Map<List<Customer>, List<CustomerDto>>(res);
        }

        public async Task<ICollection<CustomerDto>> GetCustomerByKeyName(string keyName)
        {
            var res = await _repositoryCustomer.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<Customer>, List<CustomerDto>>(res);
        }

        public async Task<bool> BatchInsOrUpdate(ICollection<MPNDto> input)
        {
            // 查询备损数量
            string registerStorageId = null;

            var readyLossQty = _repositoryT.FirstOrDefault(c => c.TenantId == AbpSession.TenantId && c.Name == "registerStorageId");
            if (readyLossQty != null)
            {
                registerStorageId = readyLossQty.Value;
            }


            try
            {
                foreach (var item in input)
                {
                    try
                    {
                        var nowMPN = _repository.FirstOrDefault(item.Id);
                        if (nowMPN != null && ((
                                nowMPN.Name != item.Name)
                            || (nowMPN.Info != item.Info)
                            || (nowMPN.MPNHierarchy != item.MPNHierarchy)
                            || (nowMPN.MPNLevel != item.MPNLevel)
                            || (nowMPN.MPQ1 != item.MPQ1)
                            || (nowMPN.MPNType != item.MPNType)
                            ))
                        {
                            nowMPN.Name = item.Name;
                            nowMPN.Info = item.Info;
                            nowMPN.MPNHierarchy = item.MPNHierarchy;
                            nowMPN.MPNLevel = item.MPNLevel;
                            nowMPN.MPQ1 = item.MPQ1;
                            nowMPN.MPNType = item.MPNType;
                            // await _repository.UpdateAsync(nowMPN);
                        }
                        else if (nowMPN == null)
                        {
                            nowMPN = Mapper.Map<MPNDto, MPN>(item);
                            nowMPN.RegisterStorageId = registerStorageId;

                            await _repository.InsertAsync(nowMPN);
                        }
                    }
                    catch (Exception ex)
                    {
                        item.Remark += ex.Message;
                    }
                    CurrentUnitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                throw new MesException(ex.Message);
            }

        }

    }
}
