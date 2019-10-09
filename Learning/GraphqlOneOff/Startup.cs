using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphqlOneOff.DAL;
using GraphqlOneOff.GraphQl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphqlOneOff
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(AllowSynchronousIO); // https://github.com/graphql-dotnet/graphql-dotnet/issues/1116
            
            services.AddScoped<IDependencyResolver>(sp => new FuncDependencyResolver(sp.GetRequiredService));
            services.AddGraphQL(x => { x.ExposeExceptions = true; }).AddGraphTypes();
            services.AddScoped<MainSchema>();
            
            
            services.AddSingleton<GetAllCategories>(FakeDataProvider.GetAllCategories);
            services.AddSingleton(FakeDataProvider.GetDescendantCategories());
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            
            app.UseGraphQL<MainSchema>(); // by default /graphql
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions{Path = "/"}); // by default /ui/playground
        }

        private static void AllowSynchronousIO(KestrelServerOptions options) => options.AllowSynchronousIO = true;
    }
}
