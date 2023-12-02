using AutoMapper;
using Domain.Trainer;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin;

public class EducationAdminMappingProfile : Profile
{
    public EducationAdminMappingProfile()
    {
        CreateMap<Assignment, GetUnverifiedAssignmentResponse>();
    }
}