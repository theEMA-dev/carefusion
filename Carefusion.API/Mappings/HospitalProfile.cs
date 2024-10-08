﻿using AutoMapper;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Carefusion.Web.Mappings;

public class HospitalProfile : Profile
{
    public HospitalProfile()
    {
        CreateMap<Hospital, HospitalDto>()
            .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments))
            .ReverseMap()
            .ForMember(dest => dest.Identifier, opt => opt.Ignore())
            .ForMember(dest => dest.RecordUpdated, opt => opt.Ignore());
    }
}