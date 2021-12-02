using System;
using System.Threading.Tasks;
using CodeAdvent.Advent1;
using CodeAdvent.Advent2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeAdvent
{
    class Program
    {
        static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args);

            var depthTracker = host.Services.GetRequiredService<IDepthTracker>();
            var depthAnswer = depthTracker.CountWindowIncrease(3);

            var positionTracker = host.Services.GetRequiredService<IPositionTracker>();
            positionTracker.LoadCourse();
            positionTracker.FollowCourse();
            var posAnswer = positionTracker.GetFinalValus();

            Console.WriteLine($"Depth increased {depthAnswer} times");
            Console.WriteLine($"Final position number {posAnswer}. Depth:{positionTracker.Depth}, HPos: {positionTracker.HPosition}");
            Console.WriteLine("Advent Task Done Day 2");
            return host.RunAsync();
        }

        private static IHost CreateHostBuilder(string[] args)
        {
            var defaultBuilder = Host.CreateDefaultBuilder(args);
            defaultBuilder.ConfigureServices((hbContext, services) => services.AddTransient<IDepthTracker, DepthTracker>());
            defaultBuilder.ConfigureServices((hbContext, services) => services.AddTransient<IPositionTracker, PositionTracker>());
            return defaultBuilder.Build();
        }


    }
}
