﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.Slots.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.Slots
{
    [AbpAuthorize(PermissionNames.Pages_Slots)]
    public class SlotAppService : AsyncCrudAppService<Slot, SlotDto, int, PagedResultRequestMESDto, SlotDto, SlotDto>, ISlotAppService
    {
        readonly IRepository<Slot, int> _repositorySlot;
        readonly IRepository<UPH, string> _repositoryUPH;
        readonly IRepository<MPN, string> _repositoryMPN;
        readonly IRepository<Line, string> _repositoryLine;

        public SlotAppService(IRepository<Slot, int> repositorySlot, IRepository<MPN, string> repositoryMPN, IRepository<Line, string> repositoryLine, IRepository<UPH, string> repositoryUPH) : base(repositorySlot)
        {
            _repositorySlot = repositorySlot;
            _repositoryMPN = repositoryMPN;
            _repositoryLine = repositoryLine;
            _repositoryUPH = repositoryUPH;
        }
        [HttpPost]
        public async override Task<PagedResultDto<SlotDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult(input, _repositorySlot.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<SlotDto>(tasksCount, taskList.MapTo<List<SlotDto>>());
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

        public async Task<ICollection<LineDto>> GetLineByKeyName(string keyName)
        {
            var res = await _repositoryLine.GetAll().Where(l => l.Id.Contains(keyName)).Take(10).ToListAsync();

            return Mapper.Map<List<Line>, List<LineDto>>(res);
        }

        public async Task<ICollection<BatchSlotListDto>> BatchEdit(BatchSlotDto batchSlot)
        {
            var res = new List<BatchSlotListDto>();

            // 查询机种
            var product = _repositoryMPN.FirstOrDefault(batchSlot.ProductId);
            if (product == null)
            {
                throw new MesException(1, "半成品料号" + batchSlot.ProductId + "不存在");
            }

            // 查询所有 线别
            var lines = _repositoryLine.GetAll().Where(l => l.Id.StartsWith(batchSlot.LineId));

            if (lines == null || lines.Count() < 1)
            {
                throw new MesException(2, "线别" + batchSlot.LineId + "不存在");
            }
            try
            {
                List<Slot> listSlot = new List<Slot>();

                // 将所有线别插入料表和Pin信息
                foreach (var line in lines)
                {
                    // 检查是否有Pin信息
                    var uph = _repositoryUPH.FirstOrDefault(u => u.ProductId == batchSlot.ProductId && u.LineId == line.Id);

                    if (uph == null)
                    {
                        _repositoryUPH.Insert(new UPH()
                        {
                            ProductId = batchSlot.ProductId,
                            LineId = line.Id,
                            IsActive = true,
                            Pin = batchSlot.Pin,
                            Remark = "料站表导入自动添加的UPH信息",
                            Meter = 1,
                            Qty = 999999
                        });
                    }
                    else
                    {
                        uph.Pin = batchSlot.Pin;
                    }
                    foreach (var slot in batchSlot.Slots)
                    {
                        // 判断物料是否存在
                        var mpn = _repositoryMPN.FirstOrDefault(slot.PartNoId);

                        if (mpn == null)
                        {
                            if (res.FirstOrDefault(s => s.Index == slot.Index) == null)
                            {
                                res.Add(slot);
                            }
                            continue;
                        }

                        // 查询是否有料站表
                        var dtSlot = _repositorySlot.FirstOrDefault(s =>
                        s.BoardSide == batchSlot.BoardSide
                        && s.Machine == slot.Machine
                        && s.ProductId == batchSlot.ProductId
                        && s.LineId == line.Id
                        && s.Table == slot.Table
                        && s.PartNoId == slot.PartNoId
                        && s.SlotName == slot.SlotName);


                        if (dtSlot == null)
                        {
                            // 没有
                            listSlot.Add(new Slot()
                            {
                                BoardSide = batchSlot.BoardSide,
                                Feeder = slot.Feeder,
                                IsActive = true,
                                LineId = line.Id,
                                Machine = slot.Machine,
                                PartNoId = slot.PartNoId,
                                Version = batchSlot.Version,
                                Table = slot.Table,
                                SlotName = slot.SlotName,
                                Side = slot.Side,
                                Qty = slot.Qty,
                                ProductId = batchSlot.ProductId,
                                MachineType = slot.MachineType,
                                Location = string.Join(",", slot.Location.Trim().Replace("\"", "").Replace("-", "").Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries).Length > 1 ?
                                        s.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries)[1] :
                                        s.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries)[0]).OrderBy(s => s)),
                                LineSide = slot.LineSide,
                                Index = slot.Index
                            });
                        }
                        else
                        {
                            // 有

                            dtSlot.BoardSide = batchSlot.BoardSide;
                            dtSlot.Feeder = slot.Feeder;
                            dtSlot.IsActive = true;
                            dtSlot.LineId = line.Id;
                            dtSlot.Machine = slot.Machine;
                            dtSlot.PartNoId = slot.PartNoId;
                            dtSlot.Version = batchSlot.Version;
                            dtSlot.Table = slot.Table;
                            dtSlot.SlotName = slot.SlotName;
                            dtSlot.Side = slot.Side;
                            dtSlot.Qty = slot.Qty;
                            dtSlot.ProductId = batchSlot.ProductId;
                            dtSlot.MachineType = slot.MachineType;
                            dtSlot.Location = string.Join(",", slot.Location.Trim().Replace("\"", "").Replace("-", "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).OrderBy(s => s));
                            dtSlot.LineSide = slot.LineSide;
                            dtSlot.Index = slot.Index;
                            listSlot.Add(dtSlot);
                        }

                    }


                }

                foreach (var item in listSlot)
                {
                    _repositorySlot.InsertOrUpdate(item);
                }

                await CurrentUnitOfWork.SaveChangesAsync();

                // 将不在本次料表中的站位更新为无效
                // 查询该机种该线别该版本
                var NoActives = _repositorySlot.GetAll().Where(r => r.BoardSide == listSlot.FirstOrDefault().BoardSide && r.ProductId == listSlot.FirstOrDefault().ProductId && r.LineId == listSlot.FirstOrDefault().LineId && !listSlot.Select(s => s.Id).Contains(r.Id)).ToArray();

                foreach (var item in NoActives)
                {
                    item.IsActive = false;
                    _repositorySlot.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw new MesException(ex.Message, ex);
            }

            return res;

        }
    }
}
