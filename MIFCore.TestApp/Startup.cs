﻿using Hangfire;
using MIFCore.Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace MIFCore.TestApp
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<SomeJob>();
            serviceDescriptors.AddControllers();
        }

        public void Configure()
        {

        }

        public void PostConfigure(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate<SomeJob>("some-job", y => y.DoTheJob(), Cron.Minutely());
            recurringJobManager.AddOrUpdate<SomeJob>("some-job-triggered", y => y.TriggeredJobAction(), Cron.Monthly());

            backgroundJobClient.Enqueue<SomeJob>(y => y.DoTheJob());
            backgroundJobClient.Enqueue<SomeJob>(y => y.DoTheJobButError());
        }
    }
}