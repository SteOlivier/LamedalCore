using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types.String
{
    /// <summary>
    /// String testing method
    /// </summary>
    public sealed class String_Word_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;


        [Fact]
        [Test_Method("Word_LastWord_Remove()")]
        public void Word_LastWord_Remove_Test()
        {
            #region Test1: "Remove last word" -> "Remove last"
            // =================================================
            Assert.Equal("Remove last", _lamed.Types.String.Word.Word_LastWord_Remove("Remove last word", " "));
            Assert.Equal("", _lamed.Types.String.Word.Word_LastWord_Remove("", " "));
            #endregion
        }

        [Fact]
        [Test_Method("Word_Last()")]
        public void Word_Last_Test()
        {
            #region Test1: "This is the sentence" -> "sentence"
            // =================================================
            Assert.Equal("sentence", _lamed.Types.String.Word.Word_Last("This is the sentence"));
            Assert.Equal("", _lamed.Types.String.Word.Word_Last(""));
            #endregion
        }

        [Fact]
        [Test_Method("Word_RemoveAdjacentDuplicates()")]
        public void Word_RemoveAdjacentDuplicates_Test()
        {
            #region Test1: "This is is the the sentence one two two" -> "This is the sentence one two"
            // =================================================
            Assert.Equal("This is the sentence one two", _lamed.Types.String.Word.Word_RemoveAdjacentDuplicates("This is is the the sentence one two two"));
            Assert.Equal("", _lamed.Types.String.Word.Word_RemoveAdjacentDuplicates(""));
            #endregion
        }

        [Fact]
        [Test_Method("Word_Words_FromCamelCase()")]
        public void Word_Words_FromCamelCase_Test()
        {
            #region Test1: "thisIsTheSentence" -> "this is the sentence"
            // =================================================
            Assert.Equal("this Is The Sentence", _lamed.Types.String.Word.Word_Words_FromCamelCase("thisIsTheSentence"));
            Assert.Equal("this is the sentence", _lamed.Types.String.Word.Word_Words_FromCamelCase("thisIsTheSentence", true));
            Assert.Equal("", _lamed.Types.String.Word.Word_Words_FromCamelCase(""));
            #endregion
        }

        [Fact]
        [Test_Method("Word_SwapLast2UnderscoreWords()")]
        public void Word_SwapLast2UnderscoreWords_Test()
        {
            #region Test1: "One_Two" -> "Two_One"
            // =================================================
            Assert.Equal("Two One", _lamed.Types.String.Word.Word_SwapLast2UnderscoreWords("One_Two"));
            Assert.Equal("One Three Two", _lamed.Types.String.Word.Word_SwapLast2UnderscoreWords("One_Two_Three"));
            Assert.Equal("One Two Four Three", _lamed.Types.String.Word.Word_SwapLast2UnderscoreWords("One_Two_Three_Four"));
            Assert.Equal("", _lamed.Types.String.Word.Word_SwapLast2UnderscoreWords(""));
            Assert.Equal("Word", _lamed.Types.String.Word.Word_SwapLast2UnderscoreWords("Word"));
            #endregion
        }

        [Fact]
        [Test_Method("Word_Total()")]
        public void Word_Total_Test()
        {
            #region Test1: "This is the sentence the one the two", "the" -> 3
            // =================================================
            Assert.Equal(3, _lamed.Types.String.Word.Word_Total("This is the sentence the one the two", "the"));
            Assert.Equal(0, _lamed.Types.String.Word.Word_Total("", "the"));
            #endregion
        }

        [Fact]
        [Test_Method("Word_SplitOnLast()")]
        public void Word_SplitOnLast_Test()
        {
            #region Test1: "This is the sentence the one the two", "the" -> 3
            // =================================================
            string firstPart, lastPart;
            _lamed.Types.String.Word.Word_SplitOnLast("This is the sentence", out firstPart, out lastPart);
            Assert.Equal("This is the", firstPart); 
            Assert.Equal("sentence", lastPart);

            _lamed.Types.String.Word.Word_SplitOnLast("", out firstPart, out lastPart);
            Assert.Equal("", firstPart);
            Assert.Equal("", lastPart);

            #endregion
        }
    }
}
