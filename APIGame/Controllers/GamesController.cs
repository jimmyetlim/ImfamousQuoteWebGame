using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGame.model;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace APIGame.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GamesController : ControllerBase
	{

		private readonly ILogger<GamesController> _logger;

		public GamesController(ILogger<GamesController> logger)
		{
			_logger = logger;
		}

		public static Dictionary<string, Game> games { get; set; }

		// Post /game/add/
		[HttpPost("/game/new")]
		public object AddGame([FromBody]Requete gameInfo)
		{
			if (gameInfo.gameCode.Length < 3) {
				return "{'message':'Password to short'}";
			}
			String name = generator();
			Game game = new Game(name, gameInfo.gameCode);
			getCollectionGames().Add(name, game);
			Console.Out.Write("Game added");
			//return new GameInformation(name, password);
			return game.toJson();
		}

		public Dictionary<string, Game> getCollectionGames() {
			if (games == null) {
				games = new Dictionary<string, Game>();
			}
			return games;
		}

		// Post /game/execute/
		[HttpPost("/game/execute")]
		public String execute([FromBody]Requete gameInfo)
		{
			Dictionary<string, Game> dicGame = getCollectionGames();
			if (dicGame.ContainsKey(gameInfo.gameId)) {
				Game game = dicGame[gameInfo.gameId];
				return game.execute(gameInfo);
			}
			return "Game not found!";
			
		}

		// Post /game/start/
		[HttpPost("/game/start")]
		public String startGame([FromBody]Requete gameInfo)
		{
			Dictionary<string, Game> dicGame = getCollectionGames();
			if (dicGame.ContainsKey(gameInfo.gameId)) {
				Game game = dicGame[gameInfo.gameId];
				return game.changeState();
			}
			return "Game not found!";
		}


		String[] name = { "Luigi","Kirby", "MasterChef","Forza" , "Kratos",
			"2b", "Ryu","Korosensei" ,"Karma","Nagisa",
			"Goku","Friza","Takoyami", "Kirishima", "Hisoka","Kilua","Rukia"};
		private string generator()
		{
			Random random = new Random();
			int randomCode = random.Next(1000, 9999);
			int randomIndex = random.Next(0, 17);
			return name[randomIndex] + randomCode;
		}
		

		[HttpGet]
		public String[] Get()
		{
			return name;
		}



	}

	public class GameInfoProto
	{
		public string id { get; set; }
		public String password { get; set; }
		public string playerId { get; set; }
		public string playerPassword { get; set; }
		public string cards { get; set; }
		public string status { get; set; }
	}
}
