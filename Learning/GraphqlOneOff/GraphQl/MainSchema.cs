using GraphQL;
using GraphQL.Types;
using GraphqlOneOff.GraphQl.Queries;

namespace GraphqlOneOff.GraphQl
{
    public class MainSchema: Schema
    {
        public MainSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            var query = dependencyResolver.Resolve<CategoryQuery>();
            Query = query;
        }
    }
}