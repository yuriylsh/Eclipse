using System.Collections.Generic;

namespace ComposeMethod
{
    public abstract class TagNodeBase
    {
        protected readonly string TagName;

        protected readonly AttributeCollection Attributes = new AttributeCollection();

        protected readonly List<TagNodeBase> Children = new List<TagNodeBase>();

        public string Value { get; set; }

        public TagNodeBase AddAttribute(TagAttribute attribute)
        {
            Attributes.Add(attribute);
            return this;
        }

        public TagNodeBase AddChild(TagNodeBase child)
        {
            Children.Add(child);
            return this;
        }

        protected TagNodeBase(string tagName)
        {
            TagName = tagName;
        }

        public TagNodeBase SetValue(string value)
        {
            Value = value;
            return this;
        }
    }
}
