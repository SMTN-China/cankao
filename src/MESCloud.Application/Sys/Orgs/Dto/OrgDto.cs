using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Orgs.Dto
{
    [AutoMapTo(typeof(Org))]
    public class OrgDto : CreateOrgDto, IEntityDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级菜单名
        /// </summary>
        public string ParentName { get; set; }
    }
}
