using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using System.ComponentModel.DataAnnotations;

namespace MESCloud.WMS.ProduceData.ReelMoveMethods.Dto
{
    [AutoMapFrom(typeof(RMMStorageMap))]
    public class RMMStorageMapDto: IEntityDto<string>
    {
        [Required]
        public string ReelMoveMethodId { get; set; }

        [Required]
        public string StorageId { get; set; }

        public bool IsActive { get; set; }
        public string Id { get ; set; }
    }
}