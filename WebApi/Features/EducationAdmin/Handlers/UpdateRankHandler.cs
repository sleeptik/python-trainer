using Infrastructure;
using MediatR;
using WebApi.Features.Education.Services;
using WebApi.Features.EducationAdmin.Notifications;

namespace WebApi.Features.EducationAdmin.Handlers;

public class UpdateRankHandler(ApplicationDbContext context, AssignmentService assignmentService)
    : INotificationHandler<AssignmentVerifiedNotification>
{
    public async Task Handle(AssignmentVerifiedNotification notification, CancellationToken cancellationToken)
    {
        var rank = await context.Students.FindAsync(
            new object[] { notification.UserId }, cancellationToken
        );

        throw new NotImplementedException();

        await context.SaveChangesAsync(cancellationToken);
    }
}