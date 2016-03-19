namespace ComposeMethod.Before
{
    public class TagNode : TagNodeBase
    {

        public TagNode(string tagName) : base(tagName) { }

        public override string ToString()
        {
            var result = string.Empty;
            result += "<" + TagName + " " + Attributes + ">";
            foreach (var child in Children)
            {
                result += child.ToString();
            }
            if (!Value.Equals(""))
                result += Value;
            result += "</" + TagName + ">";
            return result;
        }
    }
}
