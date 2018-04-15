using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.Reels.Dto
{
    [AutoMapFrom(typeof(Reel))]
    public class ReelOutLifeDto : EntityDto<string>
    {
        public string PartNoId { get; set; }
        public int Qty { get; set; }
        public string Supplier { get; set; }
        public DateTime MakeDate { get; set; }
        public string DateCode { get; set; }
        public string LotCode { get; set; }
        public string BatchCode { get; set; }
        public bool IsUseed { get; set; }
        public int ExtendShelfLife { get; set; }
        public int ShelfLife { get; set; }
        public OutLifeType OutLifeType { get; set; }
        public DateTime OutLifeDate { get; set; }
        public int OutLifeDay { get; set; }
        public DateTime WarnLifeDate { get; set; }
        public int WarnLifeDay { get; set; }
        public double WarningDay { get; set; }
        public string ReadyMBillId { get; set; }
        public string WorkBillDetailedId { get; set; }
        public string ReadyMBillDetailedId { get; set; }
        public string WorkBillId { get; set; }
        public string StorageLocationId { get; set; }
        public string StorageId { get; set; }
        public int? SlotId { get; set; }
        public bool IsActive { get; set; }
    }

    public enum OutLifeType
    {
        OutLife = 0,
        Warning
    }
}
