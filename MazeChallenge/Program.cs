using Newtonsoft.Json;
using System;
using MazeSolver.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MazeSolver.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MazeSolver.Interfaces;

namespace MazeSolver
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args);
            
            builder.ConfigureHostConfiguration(configuration => configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    );

            builder.ConfigureServices(services => ConfigurationHelper.ConfigureServices(services));
            
            var app =  builder.Build();


            var mazeService =  app.Services.GetRequiredService<IMazeService>();
            
            if (await mazeService.CreateMaze())
                    await mazeService.SolveMaze();

        }
    }
}
