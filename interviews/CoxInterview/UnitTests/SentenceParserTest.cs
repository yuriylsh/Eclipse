using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SentenceParsing;

namespace UnitTests
{
    [TestClass]
    public class SentenceParserTest
    {
        [TestMethod]
        public void EmptyInputTest()
        {
            Assert.IsNull(SentenceEncoder.Encode(null));
            Assert.AreEqual(string.Empty, SentenceEncoder.Encode(string.Empty));
        }

        [TestMethod]
        public void LessThanThreeLetterWordsEdgeCasesTest()
        {
            Assert.AreEqual("A0", SentenceEncoder.Encode("A"));
            Assert.AreEqual("A0B", SentenceEncoder.Encode("AB"));
        }

        [TestMethod]
        public void WordEncodingTest()
        {
            Assert.AreEqual("A1Z", SentenceEncoder.Encode("ABZ"));
            Assert.AreEqual("A1Z", SentenceEncoder.Encode("ABBZ"));
            Assert.AreEqual("A1Z", SentenceEncoder.Encode("ABBBZ"));
                               
            Assert.AreEqual("A2Z", SentenceEncoder.Encode("AbcZ"));
            Assert.AreEqual("A2Z", SentenceEncoder.Encode("AbcbZ"));
            Assert.AreEqual("A2Z", SentenceEncoder.Encode("AbccbbZ"));
            Assert.AreEqual("S3h", SentenceEncoder.Encode("Smooth"));

            Assert.AreEqual("А5а", SentenceEncoder.Encode("Абракадабра")); // ASSUMPTION: just to make sure there werent some assumption like English only or ASCII only, etc.
        }

        [TestMethod]
        public void NonAlpahbeticCharactersFormAndLocationTest()
        {
            Assert.AreEqual(".", SentenceEncoder.Encode("."));
            Assert.AreEqual("  ", SentenceEncoder.Encode("  "));
            Assert.AreEqual("\"- T1e d3k i0s S3h!\n\t\" - I1n't0 i0t?\" ", SentenceEncoder.Encode("\"- The drink is Smooth!\n\t\" - Isn't it?\" "));
            Assert.AreEqual(" П4т! К1к д2а?", SentenceEncoder.Encode(" Привет! Как дела?")); // ASSUMPTION: just to make sure there werent some assumption like English only or ASCII only, etc.
        }
    }
}
