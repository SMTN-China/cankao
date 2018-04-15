using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Sys.I18Ns.Dto
{
    public class I18NDto : CreateI18NDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
