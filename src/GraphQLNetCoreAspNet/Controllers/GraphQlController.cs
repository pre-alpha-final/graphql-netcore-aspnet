using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using GraphQLNetCoreAspNet.Infrastructure.GraphQl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GraphQLNetCoreAspNet.Controllers
{
	public class GraphQlController : Controller
	{
		private readonly IDocumentExecuter _documentExecuter;
		private readonly IDocumentWriter _documentWriter;
		private readonly ISchema _schema;

		public GraphQlController(IDocumentExecuter documentExecuter, IDocumentWriter documentWriter,
			ISchema schema)
		{
			_documentExecuter = documentExecuter;
			_documentWriter = documentWriter;
			_schema = schema;
		}

		[HttpPost("graphql")]
		public async Task<IActionResult> GraphQl([FromBody] GraphQlQueryModel model)
		{
			var result = await _documentExecuter.ExecuteAsync(e =>
			{
				e.Schema = _schema;
				e.OperationName = model.OperationName;
				e.Inputs = model.Variables?.ToInputs();
				e.Query = model.Query;
				e.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
				e.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
			});
			var json = await _documentWriter.WriteToStringAsync(result);
			var response = JObject.Parse(json);

			return result.Errors?.Count > 0
				? (IActionResult)BadRequest(response)
				: Ok(response);
		}
	}
}
