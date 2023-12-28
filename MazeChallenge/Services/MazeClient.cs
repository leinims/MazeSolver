using MazeSolver.Dtos;
using MazeSolver.Entities;
using MazeSolver.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Client
{
    public class MazeClient : IMazeClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MazeConfig _config;
        private readonly ILogger<MazeClient> _logger;
        public MazeClient(IOptions<MazeConfig> options, IHttpClientFactory httpClientFactory, ILogger<MazeClient> logger)
        {
            _config = options.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            
        }

        public async Task<Result<Guid>> NewRandomMaze(int width, int height)
        {
            Result<Guid> result = new Result<Guid>();
            string responseString = "";
            try
            {
                var mazeRequest = new MazeRequest(width, height);
                var client = _httpClientFactory.CreateClient(_config.APISettings.APIname);
                var response = await client.PostAsJsonAsync($"Maze?code={_config.APISettings.AccessCode}", mazeRequest);
                responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result.Success = true;
                    
                    var dataRes = JsonConvert.DeserializeObject<MazeResponse>(responseString);
                    result.Results = dataRes.MazeUid;
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Detail: {response.ReasonPhrase}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = String.Join("\n", ex.Message, responseString);
                _logger.LogError($"Error creating Maze");
                return result;
            }


        }

        public async Task<Result<Guid>> NewGame(Guid mazeUid)
        {
            Result<Guid> result = new Result<Guid>();
            string responseString = "";
            try
            {
                var gameRequest = new GameRequest(PlayerOperation.Start.ToString());
                var client = _httpClientFactory.CreateClient(_config.APISettings.APIname);
                var response = await client.PostAsJsonAsync($"Game/{mazeUid}?code={_config.APISettings.AccessCode}", gameRequest);
                responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result.Success = true;
                    var dataRes = JsonConvert.DeserializeObject<GameDefinition>(responseString);
                    result.Results = dataRes.GameUid;
                }
                else
                {
                    result.Success = false;
                    result.Message = response.ReasonPhrase?? "Response is FailedStatusCode";
                }


                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = String.Join("\n", ex.Message, responseString);
                return result;
            }


        }

        public async Task<Result<Block>> Move(Guid mazeUid, Guid gameUid, string operation)
        {
            Result<Block> result = new Result<Block>();
            string responseString = "";
            try
            {
                var gameRequest = new GameRequest(operation.ToString());
                var client = _httpClientFactory.CreateClient(_config.APISettings.APIname);
                var response = await client.PostAsJsonAsync($"Game/{mazeUid}/{gameUid}?code={_config.APISettings.AccessCode}", gameRequest);
                responseString = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    
                    result.Success = true;
                    
                    var dataRes = JsonConvert.DeserializeObject<GameResponse>(responseString);

                    var neighbours = new List<bool>()
                    {
                        !dataRes.MazeBlockView.NorthBlocked,
                        !dataRes.MazeBlockView.EastBlocked,
                        !dataRes.MazeBlockView.SouthBlocked,
                        !dataRes.MazeBlockView.WestBlocked
                    };

                    result.Results = new Block(dataRes.MazeBlockView.CoordX, dataRes.MazeBlockView.CoordY, neighbours, dataRes.Game.Completed);
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Details: {response.ReasonPhrase}";
                }


                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = String.Join("\n", ex.Message, responseString);
                return result;
            }


        }
        //Take a look of Game
        public async Task<Result<Block>> GetGame(Guid mazeUid, Guid gameUid)
        {
            Result<Block> result = new Result<Block>();
            var responseString = "";
            try
            {
                var client = _httpClientFactory.CreateClient(_config.APISettings.APIname);
                var response = await client.GetAsync($"Game/{mazeUid}/{gameUid}?code={_config.APISettings.AccessCode}");
                responseString = await response.Content.ReadAsStringAsync();
                

                if (response.IsSuccessStatusCode)
                {
                    result.Success = true;
                    var dataRes = JsonConvert.DeserializeObject<GameResponse>(responseString);

                    var neighbours = new List<bool>()
                    {
                        !dataRes.MazeBlockView.NorthBlocked,
                        !dataRes.MazeBlockView.EastBlocked,
                        !dataRes.MazeBlockView.SouthBlocked,
                        !dataRes.MazeBlockView.WestBlocked
                    };

                    result.Results = new Block(dataRes.MazeBlockView.CoordX, dataRes.MazeBlockView.CoordY, neighbours, dataRes.Game.Completed);
                   
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Details: {response.ReasonPhrase}";
                }


                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = String.Join("\n", ex.Message, responseString);
                return result;
            }


        }

    }
}
