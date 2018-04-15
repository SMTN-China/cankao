using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.Lines.Dto
{
    [AutoMapFrom(typeof(Line))]
    public class LineDto : IEntityDto<string>
    {
        public string Id { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        public string ForCustomerMStorageId { get; set; }

        public string ForSelfMStorageId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
