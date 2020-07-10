using APIGame.model;
using APIGame.model.State;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameAPI.Models
{
	public class Game
	{
		public Game(string id, string password) {
			this.id = id;
			this.password = password;
			players = new Dictionary<string, Player>();
			answerPlayer = new Dictionary<string, string>();
			turn = 5;
			status = new Starting(this);
			blackCards = new List<string> {"_______________ was the perfect crime and I would have gotten away with it too, if it weren't for you meddling kids.",
		"This is my new special move. Ultra Ultimate _______________.",
		"This is my new Jutsu. _______________ Jutsu",
		"Nozaki! No I can't do it! _______________ is too embaressing! ",
		"There a new section in the super market. The _______________ section.",
		"As a marketing plot, Microsoft decided to do _______________.",
		"I need art supplys. Can you get: paper, pencils and _______________ plesae. ",
		"\"Why did I say \"_______________\" in the middle of sex! I'm so stupid! Baka\"",
		"Elementary, my dear Watson, Mr Smith got killed by _______________ at 10 pm.",
		"He is my biggest Rival: _______________ the Great.",
		"Connan next hints: _______________!",
		"Look, _______________ get an new cannon arm.",
		"_______________ did 100 push-ups, 100 sit-ups 100 squats and a 10 km run every single day.",
		"I'm _______________ , son of Satan!",
		"This is the great _______________ Senpai!",
		"This is _______________. Hello Kitty's new boyfriend.",
		"We are the lovely warrior of univers 2 : Brianne de Chateau, Sunka-ku and  _______________ , Let's bloom, let's ring. let's sing the song of love and victory. Formation!",
		"Todays case : Love, mystery and _______________!",
		"Gary catch _______________, his new Pokémon!",
		"Oh _______________ , is a lovely name ! Would you please bear my child?",
		"This is _______________, the newest team member of are magical girl team!",
		"This is the newest drink for coke. It's a mix of water, sugar and _______________.",
		"Tamaki-kun, why is there _______________ all over Usa-chan face?",
		"In the next episode of dragon ball, goku is going to face his greatest ennemi: _______________ !",
		"He is the new Shonen protagonist: Aki the pretty high school boy armed with great anti-gravity hair and _______________",
		"Only one _______________ Remaining!" };
		}
		private int turn;
		[JsonProperty("id")]
		private string id { get; set; }

		[JsonProperty("name")]
		private string password { get; set; }

		[JsonProperty("status")]
		public State status { get; set; }

		public List<string> blackCards { get; set; }

		public Dictionary<String, Player> players { get; set; }
		public Dictionary<String, string> answerPlayer { get; set; }
		public Player activePlayer;
		public Player winner;

		public bool addAnswer(string playerId, string answer) {
			if (answerPlayer.ContainsKey(playerId))
			{
				return false;
			}
			else {
				if (activePlayer.name.Equals(playerId))
					return false;
				answerPlayer.Add(playerId, answer);
				return true;
			}
		}

		private String active { get; set; }
		public String toJson() {
			return "{'id':'"+id+"', 'password':'"+password+"'}";
		}

		public string getBlackCard() {
			Random random = new Random();
			int num =  random.Next(0, blackCards.Count());
			return blackCards.ElementAt(num);
		}
		public string addPlayer(Player player, string _password) {
			if (password != _password)
				return "Password Not Valid";
			if (players.ContainsKey(player.name)) {
				return player.name + " is already in the game";
			}
			players.Add(player.name, player);
			return player.name + " was succefully added";
		}
		public bool addPoint(string id) {
			players[id].point += 1;
			turn--;
			activePlayer = players[id];
			answerPlayer = new Dictionary<string, string>();
			return (turn>=0);
		}
		public Player getWinner() { 
			if(winner == null){
				Player topPlayer = null;
				foreach (Player p in players.Values) {
					if (topPlayer == null || topPlayer.point < p.point) {
						topPlayer = p;
					}
				}
				winner = topPlayer;
			}
			return winner;
		}
		public string execute(Requete req) {
			return status.execute(req);
		}
		public string changeState()
		{
			if (status.skippable())
				return status.nextStage();
			return "Not Skippable";
		}


		public void ChangeState(State status)
		{
			this.status = status;
		}
	}

	public class GameInformation
	{

		public GameInformation(string id, string password)
		{
			this.id = id;
			this.password = password;
		}
		private string id { get; set; }
		private string password { get; set; }

	}
}