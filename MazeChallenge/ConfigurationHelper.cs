using MazeSolver.Client;
using MazeSolver.Interfaces;
using MazeSolver.PathFinder;
using MazeSolver.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class ConfigurationHelper
    {
        public static void ConfigureServices(IServiceCollection iServices)
        {
            
            iServices.AddSingleton<IMazeClient, MazeClient>()
                    .AddSingleton<IMazeService, MazeService>()
                    //.AddSingleton<IPathFinder, AStar>()
                    .AddOptions<MazeConfig>()
                    .Configure<IConfiguration>((mazeConfig, configuration) => {
                        configuration.Bind("MazeConfig", mazeConfig);
                    });

            IConfiguration configuration = iServices.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var config = configuration.GetRequiredSection("MazeConfig").Get<MazeConfig>();
            iServices.AddHttpClient(config.APISettings.APIname, client => {
                                client.BaseAddress = new Uri(config.APISettings.URI);
                            });

            iServices.RemoveAll<IHttpMessageHandlerBuilderFilter>();

        }
        
    }
}
