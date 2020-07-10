using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameAPI.Models
{
	public class Player
	{
		public Player(string _name) {
			name = _name;
			point = 0;
			strike = 0;
		
		}
		public String name { get; set; }
		public int point { get; set; }
		public int strike { get; set; }
	}
}