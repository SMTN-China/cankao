using MESCloud.CommonDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Menus.Dto
{
    public class MenuIdNameDto : IIdNameDto
    {
        public int? Id { get; set ; }
        public string Name { get; set; }
    }
}
