using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpeechSynth.Lib;
using SpeechSynth.Lib.Impl;
using SpeechSynth.Lib.Interfaces;
using System;

namespace SpeechSynth
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            try
            {
                Application.Run(ServiceProvider.GetRequiredService<Form1>());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        

        private static IServiceProvider? ServiceProvider { get; set; }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    //Logger

                    //Custom Options
                    services.AddSingleton<Options>();

                    //Services
                    services.AddScoped<ISpeechToText, SpeechToText>();
                    services.AddScoped<IMicrophoneRecorder, NAudioMicrophoneRecorderWaveIn>();

                    services.AddSingleton(new KeyboardHook(true));
                    services.AddSingleton<KeyboardSender>();

                    //Views
                    services.AddSingleton<Form1>();

                });
        }
    }
}