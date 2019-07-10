using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLNetCoreAspNet.Infrastructure;

namespace GraphQLNetCoreAspNet.Services
{
	public interface IDataService
	{
		Task<List<Foo>> Get(int? id, string category, string friendlyName);
	}
}
