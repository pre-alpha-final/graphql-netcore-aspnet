using System.Reflection;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQLNetCoreAspNet.Infrastructure.GraphQl;
using GraphQLNetCoreAspNet.Services;
using GraphQLNetCoreAspNet.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLNetCoreAspNet
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IDataService, DataServiceMock>();

			services.AddScoped<IDependencyResolver>(e =>
				new FuncDependencyResolver(e.GetRequiredService));
			services.AddScoped<FooSchema>();
			services
				.AddGraphQL()
				.AddGraphTypes(Assembly.GetAssembly(typeof(Startup)), ServiceLifetime.Transient);
			services.AddSingleton<IDocumentExecuter>(new DocumentExecuter());
			services.AddSingleton<IDocumentWriter>(new DocumentWriter(true));
			services.AddSingleton<ISchema>(serviceProvider => new FooSchema(
				new FuncDependencyResolver(serviceProvider.GetService)));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			/*
			 * This is a standard way of adding graphql to project
			 * unfortunately it uses middleware to set up the endpoint
			 * so now way to bind it to controller, which means no
			 * asp authentication etc.
			 * In this example the logic has been moved to the controller
			 */
			//app.UseGraphQL<SearchSchema>();

			app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
			{
				GraphQLEndPoint = "/graphql"
			});

			app.UseMvc();
		}
	}
}
