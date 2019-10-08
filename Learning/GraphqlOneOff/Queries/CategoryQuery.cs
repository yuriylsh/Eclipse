using GraphQL.Types;
using GraphqlOneOff.DAL;
using GraphqlOneOff.Types;

namespace GraphqlOneOff.Queries
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