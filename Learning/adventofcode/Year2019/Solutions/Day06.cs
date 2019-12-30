using System.IO;
using Solutions.Shared;

namespace Solutions
{
    public static class Day06
    {
        public static int Part1(FileInfo input)
        {
            var source = File.ReadAllLines(input.FullName);
            var centerOfMass = Tree.Build(source);
            var totalDepth = 0;
            
            Tree.DepthFirstPreorder(centerOfMass, x => totalDepth += x.Depth);

            return totalDepth;
        }

        public static int Part2(FileInfo input)
        {
            var source = File.ReadAllLines(input.FullName);
            var centerOfMass = Tree.Build(source);
            Node GetByLabel(string label) => Tree.GetByLabel(centerOfMass, label);
            var you = GetByLabel("YOU");
            var santa = GetByLabel("SAN");
            var commonAncesstor = Tree.LowestCommonAncestor(you, santa);

            return you.Depth + santa.Depth - 2 * commonAncesstor.Depth - 2;
        }
    }
}