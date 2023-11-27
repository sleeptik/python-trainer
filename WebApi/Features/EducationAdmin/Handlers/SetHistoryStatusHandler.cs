using Domain.Trainer;
using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin.Handlers;

public class SetHistoryStatusHandler(ApplicationDbContext context)
    : IRequestHandler<SetHistoryStatusCommand>
{
    public async Task Handle(SetHistoryStatusCommand request, CancellationToken cancellationToken)
    {
        var history = await context.Histories.FindAsync(request.UserId, request.ExerciseId);

        if (history is not null)
            return;

        var newHistory = new History(request.UserId, request.ExerciseId, request.Status);

        await context.Histories.AddAsync(newHistory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}