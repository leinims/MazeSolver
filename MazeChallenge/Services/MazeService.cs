using MazeSolver.Entities;
using MazeSolver.Interfaces;
using MazeSolver.PathFinder;
using MazeSolver.View;
using MazeSolver.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace MazeSolver.Services
{
    public class MazeService : IMazeService
    {
        private Maze? maze;
        private readonly IMazeClient _client;
        private readonly ILogger<MazeService> _logger;
        private readonly MazeConfig _config;
        private const int MaxSteps = 1000;

        public MazeService(IOptions<MazeConfig> options,  ILogger<MazeService> logger, IMazeClient client)
        {
            _logger = logger;
            _client = client;
            _config = options.Value;
            
        }
        public async Task<bool> CreateMaze()
        {
            _logger.LogInformation("Creating a new maze...");
            var response = await _client.NewRandomMaze(_config.Width, _config.Width);

            if (!response.Success)
            {
                _logger.LogError($"Error creating maze");
                return false;
            }
            maze = new Maze(response.Results, _config.Width, _config.Width);
            _logger.LogInformation($"New maze created with UID: {response.Results.ToString()}");

            return true;
        }
        public async Task<bool> SolveMaze()
        {

            Game game;
            Guid gameUid;

            List<string> directions = _config.Directions;

            _logger.LogInformation("Starting a new Game...");

            var gameResult = await _client.NewGame(maze.MazeUid);
            if (!gameResult.Success)
            {
                _logger.LogError($"Error starting game: {gameResult.Message}");
                return false;
            }
            gameUid = gameResult.Results;

            _logger.LogInformation($"Started new game with UID: {gameUid}");

            var result = await _client.GetGame(maze.MazeUid, gameUid).ConfigureAwait(false);

            if (!result.Success || result.Results == null)
            {
                _logger.LogError($"Error getting game inf: {result.Message}");
                return false;
            }

            game = new Game(gameUid, result.Results);

            var pathFinder = new GreedyAlgorithm(new Vector2Int(0, 0), new Vector2Int(24, 24));

            pathFinder.FindNextPosition(ref game);

            var mazeViewer = new MazePrinter(new Vector2Int(maze.Width, maze.Height));
            mazeViewer.UpdateScreen(game);
            while (game.Steps < MaxSteps)
            {
                mazeViewer.UpdateScreen(game);
                result = await _client.Move(maze.MazeUid, game.GameUid, directions[game.Direction]).ConfigureAwait(false);
                if (!result.Success || result.Results == null)
                {
                    _logger.LogError($"Error Moving position on game UID: {game.GameUid} \n" +
                        $"Details: {result.Message}");
                    return false;
                }

                if (result.Results.IsLastBlock)
                {
                    _logger.LogInformation($"Finished Game!\n " +
                        $"Maze UId: {maze.MazeUid} \n" +
                        $"Game UId: {game.GameUid} \n" +
                        $"Steps: {game.Steps} \n");
                    return true;
                }
                game.CurrentBlock = result.Results;
                pathFinder.FindNextPosition(ref game);

                
                game.Steps++;
            }

            return false;
        }

    }
}
