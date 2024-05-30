using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Exercises;

namespace Trainer.WebApi.Controllers.Education.DTO;

public record  AssignmentDetailsDto(int Id, Exercise Exercise, Solution? Solution, IList<Suggestion> Suggestions);