using AutoMapper;
using Carefusion.Entities;
using Carefusion.Core;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Carefusion.Web.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Communication, opt => opt.MapFrom(src => src.Communication))
            .ReverseMap()
            .ForMember(dest => dest.Identifier, opt => opt.Ignore())
            .ForMember(dest => dest.RecordUpdated, opt => opt.Ignore());

        CreateMap<PatientDto, PatientDto>()
            .ForMember(dest => dest.AssignedPractitioner, opt => opt.Ignore());
        
        CreateMap<Communication, CommunicationDto>().ReverseMap();
    }
}