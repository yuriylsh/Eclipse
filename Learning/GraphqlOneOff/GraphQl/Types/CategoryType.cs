using GraphQL.Types;
using GraphqlOneOff.DAL;

namespace GraphqlOneOff.GraphQl.Types
{
    public class CategoryType: ObjectGraphType<Category>
    {
        public CategoryType(GetDescendants getDescendants)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field<ListGraphType<CategoryType>>("descendants", resolve: x => getDescendants(x.Source.Id));
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}