using System;

namespace StellarChallenge.Models
{
	public class SnippetModel
	{
		public string Name { get; set; }
		public DateTime Expires_At { get; set; }
		public string Snippet { get; set; }
		public int Likes { get; set; }
	}
}
