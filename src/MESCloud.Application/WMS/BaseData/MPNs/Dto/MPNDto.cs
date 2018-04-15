using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.BaseData.MPNs.Dto
{
    [AutoMapFrom(typeof(MPN))]
    public class MPNDto : IEntityDto<string>
    {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
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

        public int ShelfLife { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public MPNHierarchy MPNHierarchy { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public MPNLevel MPNLevel { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MPNType MPNType { get; set; }

        /// <summary>
        /// 类型等级
        /// </summary>
        public MSDLevel? MSDLevel { get; set; }
        /// <summary>
        /// 来料方式
        /// </summary>
        public IncomingMethod IncomingMethod { get; set; }

        /// <summary>
        /// 原包装数量
        /// </summary>
        public int? MPQ1 { get; set; }
        public int? MPQ2 { get; set; }
        public int? MPQ3 { get; set; }
        public int? MPQ4 { get; set; }
        public int? MPQ5 { get; set; }

        public string CustomerId { get; set; }
        public bool IsActive { get; set; }

    }
}
