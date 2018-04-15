using MESCloud.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Users.Dto
{
    public class UserOrgRoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RoleIdNameDto> Roles { get; set; }
    }


}
