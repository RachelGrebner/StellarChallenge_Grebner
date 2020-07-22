using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StellarChallenge.Models;

namespace StellarChallenge.DataStore
{
	public sealed class DataStore
	{
		private static readonly Lazy<DataStore>
			lazy =
				new Lazy<DataStore>
					(() => new DataStore());

		public static DataStore Instance { get { return lazy.Value; } }

		public List<SnippetModel> Snippets { get; set; } = new List<SnippetModel>();

		private DataStore()
		{
		}
	}
}
