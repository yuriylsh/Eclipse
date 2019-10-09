using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlOneOff.DAL;

namespace GraphqlOneOff.GraphQl.Types
{
    public class CategoryType: ObjectGraphType<Category>
    {
        public CategoryType(IDataLoaderContextAccessor dataLoaderAccessor, GetDescendantsBatched getDescendantsBatched)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            FieldAsync<ListGraphType<CategoryType>>("descendants", resolve: async x =>
            {
                var fetchFunc =
                    new Func<IEnumerable<int>, CancellationToken, Task<ILookup<int, Category>>>(getDescendantsBatched);
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader("get descendants for a category", fetchFunc);
                    return await loader.LoadAsync(x.Source.Id);
            });
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}