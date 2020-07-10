using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.model.State
{
	public interface State 
	{
		string execute(Requete req);
		string nextStage();
		bool skippable();
	}

	public class Starting : State
	{
		private GameAPI.Models.Game mainGame { get; set; }

		public Starting(GameAPI.Models.Game mainGame)
		{
			this.mainGame = mainGame;
		}

		public string execute(Requete req)
		{
			return mainGame.addPlayer(new GameAPI.Models.Player(req.playerID), req.gameCode);
		}

		public string nextStage()
		{
			if (mainGame.players.Count > 2) {
				mainGame.ChangeState(new Draw(mainGame));
				return "Next phase -> Draw";
			}
			return "Not enough Players";
			
		}

		public bool skippable()
		{
			return true;
		}
	}
	class Draw : State
	{
		private GameAPI.Models.Game mainGame { get; set; }

		public Draw(GameAPI.Models.Game mainGame)
		{
			this.mainGame = mainGame;
		}

		public string execute(Requete req)
		{
			if (mainGame.activePlayer.name != req.playerID) {
				return"Not player judge";
			}
			nextStage();
			return mainGame.getBlackCard();
		}

		public string nextStage()
		{
			mainGame.ChangeState(new Gaterring(mainGame));
			return "Next Gattering";
		}
		public bool skippable()
		{
			return false;
		}
	}
	class Gaterring : State
	{
		private GameAPI.Models.Game mainGame { get; set; }

		public Gaterring(GameAPI.Models.Game mainGame)
		{
			this.mainGame = mainGame;
		}

		public string execute(Requete req)
		{
			if (mainGame.addAnswer(req.playerID, req.answer))
			{
				if (mainGame.answerPlayer.Count == mainGame.players.Count - 1)
				{
					nextStage();
				}
				return "Answer accepted";

			}
			else { 
				return "Answer not accepted";
			}
		}

		public string nextStage()
		{
			mainGame.ChangeState(new Voting(mainGame));
			return "Next Voting";
		}
		public bool skippable()
		{
			return true;
		}
	}
	class Voting : State
	{
		private GameAPI.Models.Game mainGame { get; set; }

		public Voting(GameAPI.Models.Game mainGame)
		{
			this.mainGame = mainGame;
		}

		public string execute(Requete req)
		{
			if (mainGame.addPoint(req.answer)) {
				nextStage();
				return "Point added to " + req.answer;
			}
			mainGame.ChangeState(new Result(mainGame));
			return "The Game Had ended";
		}

		public string nextStage()
		{
			mainGame.ChangeState(new Gaterring(mainGame));
			return "Next Gaterring";
		}
		public bool skippable()
		{
			return false;
		}
	}
	class Result : State
	{
		private GameAPI.Models.Game mainGame { get; set; }

		public Result(GameAPI.Models.Game mainGame)
		{
			this.mainGame = mainGame;
		}

		public string execute(Requete req)
		{
			return "Winner is " + mainGame.winner.name;
		}

		public string nextStage()
		{
			return "No next";
		}
		public bool skippable()
		{
			return false;
		}
	}
}
