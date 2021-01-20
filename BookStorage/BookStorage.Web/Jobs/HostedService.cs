using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace WebApplication.Jobs
{
    public class HostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private static IScheduler Scheduler { get; set; }

        public HostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            await ConfigureSimpleJob();
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // ReSharper disable once PossibleNullReferenceException
            await Scheduler?.Shutdown(cancellationToken);
            Scheduler = null;
        }
       
        private async Task ConfigureSimpleJob()
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(SimpleJob))
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(360).RepeatForever())
                .Build();
            var job = JobBuilder.Create<SimpleJob>().WithIdentity(nameof(SimpleJob)).Build();
            await Scheduler.ScheduleJob(job, trigger);
        }
    }
}