using System.Text;

namespace ComposeMethod.After
{
    public class TagNode : TagNodeBase
    {
        public TagNode(string tagName) : base(tagName) {}

        public override string ToString()
        {
            var result = new StringBuilder();
            WriteOpenTagTo(result);
            WriteChildrenTo(result);
            WriteValueTo(result);
            WriteEndTagTo(result);
            return result.ToString();
        }

        private void WriteOpenTagTo(StringBuilder result)
        {
            result.Append("<").Append(TagName);

            string attributesString = Attributes.ToString();
            if(!string.IsNullOrEmpty(attributesString))
                result.Append(" ").Append(attributesString);

            result.Append(">");
        }

        private void WriteChildrenTo(StringBuilder result)
        {
            foreach (TagNodeBase child in Children)
            {
                result.Append(child);
            }
        }

        private void WriteEndTagTo(StringBuilder result) => result.Append("</" + TagName + ">");

        private void WriteValueTo(StringBuilder result)
        {
            if (!string.IsNullOrEmpty(Value))
                result.Append(Value);
        }
    }
}