using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class QuartzHelper
    {
        private static IScheduler scheduler = null;

        public class TimeJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                
            }
        }


    }
}
