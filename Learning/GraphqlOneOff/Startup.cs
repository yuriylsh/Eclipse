using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphqlOneOff.DAL;
using GraphqlOneOff.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphqlOneOff
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(sp => new FuncDependencyResolver(sp.GetRequiredService));
            services.AddGraphQL(x => { x.ExposeExceptions = true; }).AddGraphTypes();
            services.AddScoped<CategoryQuery>();
            services.AddScoped<MainSchema>();
            
            services.AddSingleton<GetAllCategories>(FakeDataProvider.GetAllCategories);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapGet("/", async context =>
//                {
//                    await context.Response.WriteAsync("Hello World!");
//                });
//            });
            app.UseGraphQL<MainSchema>(); // by default /graphql
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions{Path = "/"}); // by default /ui/playground
        }
    }
}
