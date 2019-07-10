using GraphQL;
using GraphQL.Types;

namespace GraphQLNetCoreAspNet.Infrastructure.GraphQl
{
	public class FooSchema : Schema
	{
		public FooSchema(IDependencyResolver dependencyResolver)
			: base(dependencyResolver)
		{
			Query = dependencyResolver.Resolve<FooQuery>();
		}
	}
}
