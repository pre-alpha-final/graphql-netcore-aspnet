using GraphQL.Types;

namespace GraphQLNetCoreAspNet.Infrastructure.GraphQl
{
	public class FooType : ObjectGraphType<Foo>
	{
		public FooType()
		{
			Field(e => e.Id);
			Field(e => e.Category);
			Field(e => e.FriendlyName);
			Field(e => e.Date);
		}
	}
}
