using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQLNetCoreAspNet.Services;

namespace GraphQLNetCoreAspNet.Infrastructure.GraphQl
{
	public class FooQuery : ObjectGraphType
	{
		public FooQuery(IDataService dataService)
		{
			Field<ListGraphType<FooType>>("foo", string.Empty,
				new QueryArguments(new List<QueryArgument>
				{
					new QueryArgument<IntGraphType> { Name = "id" },
					new QueryArgument<StringGraphType> { Name = "category" },
					new QueryArgument<StringGraphType> { Name = "friendlyName" }
				}),
				e => Task.Run(() => dataService.Get(
					e.GetArgument<int?>("id"),
					e.GetArgument<string>("category"),
					e.GetArgument<string>("friendlyName")))
			);
		}
	}
}
