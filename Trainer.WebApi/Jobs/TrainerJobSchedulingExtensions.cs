using Quartz;

namespace Trainer.WebApi.Jobs;

public static class TrainerJobSchedulingExtensions
{
    public static IServiceCollectionQuartzConfigurator ScheduleTrainerJobs(
        this IServiceCollectionQuartzConfigurator configurator
    )
    {
        var verifySolutionsJobKey = new JobKey("VerifySolutionsJob");
        configurator.AddJob<VerifySolutionsJob>(conf => conf.WithIdentity(verifySolutionsJobKey));
        configurator.AddTrigger(conf =>
            conf.ForJob(verifySolutionsJobKey)
                .WithSimpleSchedule(builder => builder
                    .WithIntervalInMinutes(5)
                    .RepeatForever()
                ).StartAt(DateTimeOffset.Now.AddMinutes(1))
        );

        return configurator;
    }
}