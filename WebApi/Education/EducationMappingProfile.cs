using AutoMapper;
using Domain.Trainer;

namespace WebApi.Education;

public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Subject, string>(MemberList.Destination)
            .ConstructUsing(subject => subject.Name);

        CreateMap<Difficulty, string>(MemberList.Destination)
            .ConstructUsing(difficulty => difficulty.Name);

        CreateMap<Exercise, GetNewExercisesResponse>(MemberList.Destination)
            .ForMember(
                dest => dest.Subjects,
                src => src.MapFrom(exercise => exercise.Subjects)
            )
            .ForMember(
                dest => dest.Difficulty,
                src => src.MapFrom(exercise => exercise.Difficulty)
            );
    }
}