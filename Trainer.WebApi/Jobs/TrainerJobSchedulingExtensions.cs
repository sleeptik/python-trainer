using Quartz;

namespace Trainer.WebApi.Jobs;

public static class TrainerJobSchedulingExtensions
{
    public static IServiceCollectionQuartzConfigurator ScheduleTrainerJobs(
        this IServiceCollectionQuartzConfigurator configurator
    )
    {
        var verifySolutionsJobKey = new JobKey("VerifySolutionsJob");
        var verifySolutionsTriggerKey = new TriggerKey("VerifySolutionsTrigger");

        configurator.ScheduleJob<VerifySolutionsJob>(
            triggerConfigurator => triggerConfigurator
                .WithIdentity(verifySolutionsTriggerKey)
                .ForJob(verifySolutionsJobKey)
                .WithSimpleSchedule(builder => builder
                    .WithIntervalInMinutes(5)
                    .RepeatForever()
                )
                .StartAt(DateTimeOffset.Now.AddMinutes(1)),
            jobConfigurator => jobConfigurator
                .WithIdentity(verifySolutionsJobKey)
        );

        return configurator;
    }
}