using AutoMapper;
using Domain.Trainer;
using WebApi.Features.Education.Responses;

namespace WebApi.Features.Education;

public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Subject, string>(MemberList.Destination)
            .ConstructUsing(subject => subject.Name);

        CreateMap<Rank, string>(MemberList.Destination)
            .ConstructUsing(difficulty => difficulty.Name);

        CreateMap<Exercise, GetNewExerciseResponse>(MemberList.Destination)
            .ForMember(
                dest => dest.Subjects,
                src => src.MapFrom(exercise => exercise.Subjects)
            )
            .ForMember(
                dest => dest.Difficulty,
                src => src.MapFrom(exercise => exercise.Rank)
            );
    }
}