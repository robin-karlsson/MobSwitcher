using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MobSwitcher.Core.Services;
using MobSwitcher.Core.Services.Git;
using MobSwitcher.Core.Services.MobSwitch;
using MobSwitcher.Core.Services.Shell;

namespace MobSwitcher.Cli.Commands 
{
    [Command(new []{"start", "s"}, Description = "Start mobbing as typist")]
    public class StartCmd
    {
        private readonly IMobSwitchService mobSwitch;
        private readonly ITimerService timer;

        [Argument(0, Description = "Time in minutes", Name = "Time", ShowInHelpText = true)]
        public string Time { get; set; }

        public StartCmd(IMobSwitchService mobSwitch, ITimerService timer)
        {
            this.mobSwitch = mobSwitch;
            this.timer = timer;
        }

        public Task<int> OnExecute()
        {
            this.mobSwitch.Start();
            
            if (double.TryParse(Time, out var time) && time > 0)
            {
                timer.Start(time);
            }
            return Task.FromResult(0);
        }
    }
}