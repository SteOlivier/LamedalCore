using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.zPublicClass;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.Types.String
{
    public sealed class String_Search_Test: pcTest
    {
        public String_Search_Test(ITestOutputHelper debug = null) : base(debug) { }

        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Contains()")]
        public void Contains_Test()
        {
            #region Test1: Contains("This is THE test", "the") -> true
            // =================================================
            Assert.Equal(true, _lamed.Types.String.Search.Contains("This is THE test", "the"));
            Assert.Equal(true, _lamed.Types.String.Search.Contains("This is the test", "the"));
            Assert.Equal(false, _lamed.Types.String.Search.Contains("This is THE test", "the", StringComparison.CurrentCulture));
            #endregion
        }

        [Fact]
        [Test_Method("Contains_Any()")]
        public void Contains_Any_Test()
        {
            #region Test1: 
            // =================================================
            string findValue;
            Assert.Equal(true, _lamed.Types.String.Search.Contains_Any("This is THE test", out findValue, StringComparison.CurrentCulture, "is","the"));
            Assert.Equal("is", findValue);
            Assert.Equal(true, _lamed.Types.String.Search.Contains_Any("This is THE test", "the", "is"));
            Assert.Equal(false, _lamed.Types.String.Search.Contains_Any("This is THE test", "the"));
            Assert.Equal(true, _lamed.Types.String.Search.Contains_Any("This is THE test", StringComparison.CurrentCultureIgnoreCase, "the"));
            #endregion

        }

        [Fact]
        [Test_Method("Contains_All()")]
        public void Contains_All_Test()
        {
            Assert.True(_lamed.Types.String.Search.Contains_All("Word_Last(string,string) : string", "string", "last") == true);
        }

        [Fact]
        [Test_Method("Contains_AsStr()")]
        public void Contains_AsStr_Test()
        {
            string myText = "Text to analyze for words, bar, foo";
            Assert.Equal("", _lamed.Types.String.Search.Contains_AsStr(myText, "Foo"));
            Assert.Equal("foo", _lamed.Types.String.Search.Contains_AsStr(myText, "Foo",StringComparison.CurrentCultureIgnoreCase));
            Assert.Equal("analyze", _lamed.Types.String.Search.Contains_AsStr(myText, "AnAlYzE",StringComparison.CurrentCultureIgnoreCase));
        }

        [Fact]
        [Test_Method("Contains_Index()")]
        public void Contains_Index_Test()
        {
            string myText = "Text to analyze for words, bar, foo";
            Assert.Equal(myText.IndexOf("foo", StringComparison.OrdinalIgnoreCase), _lamed.Types.String.Search.Contains_Index(myText, "foo"));
            Assert.Equal(myText.IndexOf("Foo", StringComparison.OrdinalIgnoreCase), _lamed.Types.String.Search.Contains_Index(myText, "Foo", StringComparison.OrdinalIgnoreCase));
            Assert.Equal(myText.IndexOf("AnAlYzE", StringComparison.OrdinalIgnoreCase), _lamed.Types.String.Search.Contains_Index(myText, "AnAlYzE", StringComparison.OrdinalIgnoreCase));

            Assert.Equal(-1, _lamed.Types.String.Search.Contains_Index("", "foo"));
        }

        [Fact]
        [Test_Method("ContainsIndex_Test")]
        public void ContainsIndex_Test()
        {
            Assert.True(_lamed.Types.String.Search.Contains_Index("abcde", "de") == 3); // OK
            Assert.True(_lamed.Types.String.Search.Contains_Index("ababaa", "a") == 0); // fails, returns -1
            Assert.True(_lamed.Types.String.Search.Contains_Index("ababaa", "ba") == 1); // OK
            Assert.True(_lamed.Types.String.Search.Contains_Index("ababaa", "abaa") == 2); // fails, returns -1
            Assert.True(_lamed.Types.String.Search.Contains_Index("ababaacc", "abaa") == 2); // fails, returns -1
            Assert.True(_lamed.Types.String.Search.Contains_Index("aabbbaaabbsces", "sc") == 10);
        }

        [Fact]
        [Test_Method("Equal_Percent()")]
        public void Equal_Percent_Test()
        {
            string str1 = "ðə ɻɛd fɑks ɪz hʌŋgɻi";
            string str2 = "ðæt ɪt foks ɪn ðʌ sʌn ɻe͡i";

            Assert.Equal(34.615384615384613, _lamed.Types.String.Search.Equal_Percent(str1, str2));
            Assert.Equal(100, _lamed.Types.String.Search.Equal_Percent("same", "same")); //100
            Assert.Equal(100, _lamed.Types.String.Search.Equal_Percent("", "")); //100
            Assert.Equal(0, _lamed.Types.String.Search.Equal_Percent("", "abcd")); //0  
        }

        [Fact]
        [Test_Method("Equal_")]
        public void Equal_Str_Test()
        {
            #region Test1: 'This is way one' == 'This is way two'
            // =================================================
            string str1 = "This is way one";
            string str2 = "This is way two";
            var index2 = 13;
            var errorMsg = _lamed.Types.String.Search.Equal_StrError(str1, str2, index2);
            DebugLog(errorMsg);
            DebugLog("============================");

            string error;
            int index;
            var test1 = _lamed.Types.String.Search.Equal_(str1, str2, out error, out index);
            Assert.Equal(false, test1);
            Assert.Equal(index, index2);
            Assert.Equal(errorMsg, error);
            #endregion

            Assert.Equal(true, _lamed.Types.String.Search.Equal_("same", "same", out error, out index)); //100
            Assert.Equal(true, _lamed.Types.String.Search.Equal_("", "", out error, out index)); //100
        }

        [Fact]
        [Test_Method("Var_3Values()")]
        public void Var_3Values_Test()
        {
            #region Test1:
            //      ===========================================
            string val1;
            string val2;
            string val3;
            _lamed.Types.String.Search.Var_3Values("LaMedal.Access2System.->lamed.Root.=>Blueprint_.Instance.Root.", out val1, out val2, out val3, "->", "=>");
            Assert.Equal("LaMedal.Access2System.", val1);
            Assert.Equal("lamed.Root.", val2);
            Assert.Equal("Blueprint_.Instance.Root.", val3);
            #endregion

            #region Test2:
            // ===========================================

            #endregion

        }

        [Fact]
        [Test_Method("Var_Id")]
        public void Var_Id_Test()
        {
            var line = "id1,Value1, id2,Value2";
            var test = _lamed.Types.String.Search.Var_Id(line, ",");
            Assert.True(test == "id1");
        }

        [Fact]
        [Test_Method("Var_Next")]
        public void Var_Next_Test()
        {
            var line = "id1,Value1, id2,Value2";
            var test = _lamed.Types.String.Search.Var_Next(ref line, ",");
            Assert.True(test == "id1");
            test = _lamed.Types.String.Search.Var_Next(ref line, ",");
            Assert.True(test == "Value1");
            test = _lamed.Types.String.Search.Var_Next(ref line, ",");
            Assert.True(test == "id2");
            test = _lamed.Types.String.Search.Var_Next(ref line, ",");
            Assert.True(test == "Value2");

            line = "";
            Assert.Equal("", _lamed.Types.String.Search.Var_Next(ref line, ","));

        }

        [Fact]
        [Test_Method("Var_Value_Test()")]
        public void Var_Value_Test()
        {
            var line = "Name = \"CTI Transformation\"";
            var value = _lamed.Types.String.Search.Var_Value(line, "=");
            Assert.Equal("\"CTI Transformation\"", value);
        }

        [Fact]
        [Test_Method("Var_Value")]
        public void Var_Value2_Test()
        {
            var line = "id1,Value1, id2,Value2";
            var test = _lamed.Types.String.Search.Var_Value(line, ",");
            Assert.True(test == "Value1, id2,Value2");
        }
    }
}
