using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Menus.Dto
{
    public class MenuCDto : MenuDto
    {
        public ICollection<MenuCDto> Children { get; set; }
    }
}
