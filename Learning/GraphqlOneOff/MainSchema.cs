using GraphQL;
using GraphQL.Types;
using GraphqlOneOff.Queries;

namespace GraphqlOneOff
{
    public class MainSchema: Schema
    {
        public MainSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<CategoryQuery>();
        }
    }
}