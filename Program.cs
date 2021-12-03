using System;
using System.Threading.Tasks;
using CodeAdvent.Advent1;
using CodeAdvent.Advent2;
using CodeAdvent.Advent3;
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

            var diagnosticRecord = host.Services.GetRequiredService<IDiagnosticRecord>();
            diagnosticRecord.LoadDiagnostics();
            diagnosticRecord.ComputeMostCommon();
            var diagnosticAnswer = diagnosticRecord.GammaNumber * diagnosticRecord.EpsilonNumber;
            diagnosticRecord.ComputeAtmospherics();
            var atmosphericAnswer = diagnosticRecord.Oxy * diagnosticRecord.Co2;

            Console.WriteLine($"Depth increased {depthAnswer} times");
            Console.WriteLine($"Final position number {posAnswer}. Depth:{positionTracker.Depth}, HPos: {positionTracker.HPosition}");
            Console.WriteLine($"Diagnostic gamma * epsilon is {diagnosticAnswer}. Gamma {diagnosticRecord.GammaNumber}, Epsilon {diagnosticRecord.EpsilonNumber}");
            Console.WriteLine($"Atmospheric Oxy * CO2 is {atmosphericAnswer}. Oxy {diagnosticRecord.Oxy}. CO2 {diagnosticRecord.Co2}");
            Console.WriteLine("Advent Task Done Day 2");
            return host.RunAsync();
        }

        private static IHost CreateHostBuilder(string[] args)
        {
            var defaultBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hbContext, services) => services.AddTransient<IDepthTracker, DepthTracker>())
                .ConfigureServices((hbContext, services) => services.AddTransient<IPositionTracker, PositionTracker>())
                .ConfigureServices((hbContext, services) => services.AddTransient<IDiagnosticRecord, DiagnosticRecord>())
                .Build();
            return defaultBuilder;
        }


    }
}
