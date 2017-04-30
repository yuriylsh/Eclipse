using FluentAssertions;
using Solutions.Cracking_the_Coding_Interview;
using Xunit;

namespace Tests
{
    public class HashTables_RansomeNoteTests
    {
        [Fact]
        public void CanConstructNote_NoteWordsMatchesMagazine_ReturnsTrue()
        {
            var canConstruct = HashTables_RansomeNote.CanConstructNote("give me one grand today night".Split(' '), "give one grand today".Split(' '));

            canConstruct.Should().BeTrue();
        }

        [Fact]
        public void CanConstructNote_NoteWordsMatchesMagazineButDifferByCase_ReturnsFalse()
        {
            var canConstruct = HashTables_RansomeNote.CanConstructNote("give me one grand today night".Split(' '), "Give One grand today".Split(' '));

            canConstruct.Should().BeFalse();
        }


        [Fact]
        public void CanConstructNote_NoteWordsDoNotMatchMagazine_ReturnsFalse()
        {
            var canConstruct = HashTables_RansomeNote.CanConstructNote("give me one grand today night".Split(' '), "give one grand tomorrow".Split(' '));

            canConstruct.Should().BeFalse();
        }
    }
}