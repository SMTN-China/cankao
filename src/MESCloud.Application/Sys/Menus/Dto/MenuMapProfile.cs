using AutoMapper;
using MESCloud.CommonDto;
using MESCloud.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Menus.Dto
{
    public class MenuMapProfile : Profile
    {
        public MenuMapProfile()
        {
            // 基本映射
            CreateMap<Menu, MenuDto>().ForMember(m => m.ParentName, opt => opt.MapFrom(s => s.Parent.Name));

            CreateMap<CreateMenuDto, Menu>();

            CreateMap<MenuIdNameDto, MenuRoleMap>()
                .ForMember(m => m.MenuId, opt => opt.MapFrom(s => s.Id))
                .ForMember(m => m.Id, opt => opt.MapFrom(s=>s.Name));

            CreateMap<MenuRoleMap, MenuIdNameDto>().ForMember(m => m.Id, opt => opt.MapFrom(s => s.MenuId));

            CreateMap<Menu, NzTreeDto>()
                .ForMember(m => m.Children, opt => opt.MapFrom(s => s.Children));
        }
    }
}
