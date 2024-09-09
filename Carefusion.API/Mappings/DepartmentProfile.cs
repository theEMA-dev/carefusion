using AutoMapper;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Carefusion.Web.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>()
            .ReverseMap()
            .ForMember(dest => dest.RecordUpdated, opt => opt.Ignore());
    }
}