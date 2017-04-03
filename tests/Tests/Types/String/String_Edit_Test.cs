using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types.String
{
    public sealed class String_Edit_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("SubStr_Cut()")]
        public void Cut_Test()
        {
            var input = "This is the test and the answer";
            var output = _lamed.Types.String.Edit.SubStr_Cut(input, "the ");
            Assert.True(output == "This is test and answer");
        }

        [Fact]
        [Test_Method("SubStr_Right()")]
        [Test_Method("SubStr_Index()")]
        [Test_Method("SubStr_Left()")]
        public void SubStr_Right_Test()
        {
            // SubStr_Right()
            Assert.Equal("54321", _lamed.Types.String.Edit.SubStr_Right("0987654321", 5));
            Assert.Equal("tests", _lamed.Types.String.Edit.SubStr_Right("tests", 25));

            // SubStr_Index()
            Assert.Equal("", _lamed.Types.String.Edit.SubStr_Index("", 25));
            Assert.Equal("sts", _lamed.Types.String.Edit.SubStr_Index("tests",2, 25));
            Assert.Equal("st", _lamed.Types.String.Edit.SubStr_Index("tests",2, 4));

            // SubStr_Left()
            Assert.Equal("", _lamed.Types.String.Edit.SubStr_Left("", 2));
            Assert.Equal("sts", _lamed.Types.String.Edit.SubStr_Left("tests", 2));
            Assert.Equal("sts", _lamed.Types.String.Edit.SubStr_Left("tests", 2,10));
            Assert.Equal("s", _lamed.Types.String.Edit.SubStr_Left("tests", 2,1));
        }

        [Fact]
        [Test_Method("Replace_Between()")]
        public void Replace_Between_Test()
        {
            Assert.Equal("<start>new value</start>", _lamed.Types.String.Edit.Replace_Between("<start>", "</start>", "<start>old value</start>", "new value"));
            Assert.Equal("This is 'new value' the test and the answer", _lamed.Types.String.Edit.Replace_Between("This is", "the t", "This is the test and the answer", " 'new value' "));
        }

        [Fact]
        [Test_Method("Replace_All()")]
        [Test_Method("Repeat()")]
        public void Replace_All_Test()
        {
            Assert.Equal("aaa_bbb_ccc_ddd_ee_ff_gg_hh", _lamed.Types.String.Edit.Replace_All("aaa!bbb#ccc@ddd$ee%ff^gg&hh", "_", "!","@","#","$", "%", "^", "&"));
            Assert.Equal("----------", _lamed.Types.String.Edit.Repeat("-",10));

        }

        [Fact]
        [Test_Method("Case_2Title()")]
        [Test_Method("Case_FirstLetter2Lower()")]
        [Test_Method("Case_FirstLetter2Upper()")]
        public void Case_2Title_Test()
        {
            #region Test1: bpTooltip_AsStr -> Tooltip_AsStr
            //      ===========================================
            Assert.Equal("Checkbox", _lamed.Types.String.Edit.Case_2Title("CheckBox"));
            Assert.Equal("Checkbox", _lamed.Types.String.Edit.Case_2Title("CheckBOX"));
            Assert.Equal("", _lamed.Types.String.Edit.Case_2Title(""));
            #endregion

            Assert.Equal("hi There", _lamed.Types.String.Edit.Case_FirstLetter2Lower("Hi There"));
            Assert.Equal("", _lamed.Types.String.Edit.Case_FirstLetter2Lower(""));

            Assert.Equal("Hi There", _lamed.Types.String.Edit.Case_FirstLetter2Upper("hi There"));
            Assert.Equal("", _lamed.Types.String.Edit.Case_FirstLetter2Upper(""));
        }

        [Fact]
        [Test_Method("Remove_ExtraSpaces()")]
        public void Remove_ExtraSpaces_Test()
        {
            Assert.Equal("this is a test", _lamed.Types.String.Edit.Remove_ExtraSpaces(" this  is  a  test "));
            Assert.Equal(" this is a test ", _lamed.Types.String.Edit.Remove_ExtraSpaces(" this  is  a  test ", false));
        }

        [Fact]
        [Test_Method("Remove_Prefix()")]
        public void Remove_Prefix_Test()
        {
            Assert.Equal("bp", _lamed.Types.String.Edit.Remove_Prefix("bp", "bp"));
            Assert.Equal("zz", _lamed.Types.String.Edit.Remove_Prefix("zz", "zz"));
            Assert.Equal("List()", _lamed.Types.String.Edit.Remove_Prefix("zzList()", "zz"));
        }
    }
}
