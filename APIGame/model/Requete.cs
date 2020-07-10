
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.model
{
	public class Requete
	{
		[JsonProperty("gameId")]
		public string gameId { get; set; }

		[JsonProperty("gameCode")]
		public string gameCode { get; set; }

		[JsonProperty("playerID")]
		public string playerID { get; set; }

		[JsonProperty("answer")]
		public string answer { get; set; }
	}
}
