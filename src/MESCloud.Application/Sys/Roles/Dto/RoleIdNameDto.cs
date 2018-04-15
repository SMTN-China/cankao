using MESCloud.CommonDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Roles.Dto
{
    public class RoleIdNameDto : IIdNameDto
    {
        public bool Checked { get; set; }
        public int? Id { get; set; }
        public string Name { get ; set; }
    }
}
