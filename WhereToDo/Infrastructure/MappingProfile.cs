////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: MappingProfile.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Providing a mapping profile for AutoMapper to map between DB Entities and API Models.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using AutoMapper;
using WhereToDo.Entities;
using WhereToDo.Models;

namespace WhereToDo.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ListEntity, List>()
                    .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                        Link.To(nameof(Controllers.ListController.GetListByIdAsync), new { listId = src.Id })));
            });


            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.UsersController.GetUserById),
                    new { userId = src.Id })));
        }
    }
}
