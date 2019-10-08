using GraphQL.Types;
using GraphqlOneOff.DAL;
using GraphqlOneOff.GraphQl.Types;

namespace GraphqlOneOff.GraphQl.Queries
{
    public class CategoryQuery: ObjectGraphType
    {
        public CategoryQuery(GetAllCategories getAllCategories)
        {
            Field<ListGraphType<CategoryType>>(
                "categories",
                resolve: ctx => getAllCategories()) ;
        }
    }
}