using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using MESCloud.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Menus.Dto
{
    [AutoMapFrom(typeof(Menu))]
    public class MenuDto :CreateMenuDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
