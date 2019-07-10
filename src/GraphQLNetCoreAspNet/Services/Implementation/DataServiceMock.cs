using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLNetCoreAspNet.Infrastructure;

namespace GraphQLNetCoreAspNet.Services.Implementation
{
	public class DataServiceMock : IDataService
	{
		private static readonly List<Foo> DataMock = new List<Foo>
		{
			new Foo { Id = 1, Category = "bar", FriendlyName = "Some foo", Date = DateTimeOffset.MinValue },
			new Foo { Id = 2, Category = "bar", FriendlyName = "Some other foo", Date = DateTimeOffset.MaxValue },
			new Foo { Id = 3, Category = "bar", FriendlyName = "Yet another foo", Date = DateTimeOffset.UtcNow }
		};

		public async Task<List<Foo>> Get(int? id, string category, string friendlyName)
		{
			return DataMock
				.Where(e => id == null || e.Id == id)
				.Where(e => category == null || e.Category == category)
				.Where(e => friendlyName == null || e.FriendlyName == friendlyName)
				.ToList();
		}
	}
}
