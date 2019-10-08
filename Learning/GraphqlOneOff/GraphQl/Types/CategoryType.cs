using System.Collections.Generic;
using GraphQL.Types;

namespace GraphqlOneOff.GraphQl.Types
{
    public class CategoryType: ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field<ListGraphType<CategoryType>>("descendats", resolve: x => x.Source.Descendants);
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Category> Descendants { get; set; }
    }
}