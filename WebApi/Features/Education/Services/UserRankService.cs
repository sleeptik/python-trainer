using Domain.Users;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.Services;

public class UserRankService(ApplicationDbContext context)
{
    public UserRank GetUserRank(int userId)
    {
        return context.UserRanks.AsNoTracking()
            .First(rank => rank.UserId == userId);
    }

    public void UpdateRank(UserRank rank)
    {
        context.UserRanks.Update(rank);
        context.SaveChanges();
    }
}