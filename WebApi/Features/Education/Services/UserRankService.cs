using Domain.Users;
using Infrastructure;

namespace WebApi.Features.Education.Services;

public class UserRankService(ApplicationDbContext context)
{
    public UserRank GetUserRank(int userId)
    {
        return context.UserRanks
            .First(rank => rank.UserId == userId);
    }
}