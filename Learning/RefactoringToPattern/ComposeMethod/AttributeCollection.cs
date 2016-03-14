using System.Collections.Generic;

namespace ComposeMethod
{
    public class AttributeCollection: List<TagAttribute>
    {
        public override string ToString()
        {
            return string.Join(" ", this);
        }
    }
}
