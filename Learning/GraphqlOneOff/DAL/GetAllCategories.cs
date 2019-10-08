using System.Collections.Generic;
using GraphqlOneOff.Types;

namespace GraphqlOneOff.DAL
{
    public delegate IEnumerable<Category> GetAllCategories();

    public static class FakeDataProvider
    {
        public static IEnumerable<Category> GetAllCategories()
        {
            yield return new Category {Id = 1, Name = "Yuriy"};
            yield return new Category {Id = 1, Name = "Dasha"};
            yield return new Category {Id = 1, Name = "Andrey"};
            yield return new Category {Id = 1, Name = "Natalya"};
        }
    }
}