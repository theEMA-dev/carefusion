using AutoMapper;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Carefusion.Web.Mappings;

public class LabTestProfile : Profile
{
    public LabTestProfile()
    {
        CreateMap<LabTest, LabTestDto>()
            .ReverseMap()
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.Identifier, opt => opt.Ignore())
            .ForMember(dest => dest.RecordUpdated, opt => opt.Ignore());
    }
}