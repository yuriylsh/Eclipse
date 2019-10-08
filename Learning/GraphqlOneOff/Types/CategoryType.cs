using GraphQL.Types;

namespace GraphqlOneOff.Types
{
    public class CategoryType: ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}