﻿using AutoMapper;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Carefusion.Web.Mappings;

public class PractitionerProfile : Profile
{
    public PractitionerProfile()
    {
        CreateMap<Practitioner, PractitionerDto>()
            .ForMember(dest => dest.Communication, opt => opt.MapFrom(src => src.Communication))
            .ReverseMap()
            .ForMember(dest => dest.Identifier, opt => opt.Ignore())
            .ForMember(dest => dest.RecordUpdated, opt => opt.Ignore());
    }
}