using AutoMapper;
using Domain.Trainer;

namespace WebApi.Education;

public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Exercise, GetNewExercisesResponse>()
            .ForMember(
                dest => dest.Subjects,
                src => src.MapFrom(exercise => exercise.Subjects.Select(subject => subject.Name))
            )
            .ForMember(
                dest => dest.Difficulty,
                src => src.MapFrom(exercise => exercise.Difficulty.Name)
            );
    }
}