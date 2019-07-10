using Newtonsoft.Json.Linq;

namespace GraphQLNetCoreAspNet.Infrastructure.GraphQl
{
	public class GraphQlQueryModel
	{
		public string OperationName { get; set; }
		public JObject Variables { get; set; }
		public string Query { get; set; }
	}
}
