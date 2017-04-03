using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.Types.String;
using Xunit;

namespace LamedalCore.Test.Tests.Types.String
{
    public sealed class String_SpecialChar_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("SpecialChars_Check()")]
        public void SpecialChars_Check_Test()
        {
            Assert.False(_lamed.Types.String.SpecialChar.SpecialChars_Check("123"));
            Assert.False(_lamed.Types.String.SpecialChar.SpecialChars_Check("abc"));

            Assert.True(_lamed.Types.String.SpecialChar.SpecialChars_Check("@123"));
            Assert.True(_lamed.Types.String.SpecialChar.SpecialChars_Check("abc%"));

            // ™±©€£¼½¾®
            Assert.Equal("test__foo", _lamed.Types.String.SpecialChar.SpecialCharacters_Remove("test_™±©€£¼½¾®_foo"));
        }

        [Fact]
        [Test_Method("SpecialChars_Check2()")]
        public void SpecialChars_Check2_Test()
        {
            Assert.False(_lamed.Types.String.SpecialChar.SpecialChars_Check2("123"));
            Assert.False(_lamed.Types.String.SpecialChar.SpecialChars_Check2("abc"));

            Assert.True(_lamed.Types.String.SpecialChar.SpecialChars_Check2("@123"));
            Assert.True(_lamed.Types.String.SpecialChar.SpecialChars_Check2("abc%"));
        }

        [Fact]
        [Test_Method("Char_Trademark()")]
        [Test_Method("Char_Copyright()")]
        [Test_Method("Char_Cross()")]
        [Test_Method("Char_CrossDouble()")]
        [Test_Method("Char_Registered()")]
        public void Char_Test()
        {
            Assert.Equal("™", _lamed.Types.String.SpecialChar.Char_Trademark(""));
            Assert.Equal("©", _lamed.Types.String.SpecialChar.Char_Copyright(""));
            Assert.Equal("†", _lamed.Types.String.SpecialChar.Char_Cross(""));
            Assert.Equal("‡", _lamed.Types.String.SpecialChar.Char_CrossDouble(""));
            Assert.Equal("®", _lamed.Types.String.SpecialChar.Char_Registered(""));
        }

        [Fact]
        [Test_Method("Function_Alarm()")]
        public void Function_Test()
        {
            Assert.Equal("\a", _lamed.Types.String.SpecialChar.Function_Alarm(""));
            Assert.Equal("\b", _lamed.Types.String.SpecialChar.Function_Backspace(""));
            //Assert.Equal("", _lamed.Types.String.SpecialChar.Function_Del(""));
            //Assert.Equal("", _lamed.Types.String.SpecialChar.Function_ESC(""));
            Assert.Equal(Environment.NewLine, _lamed.Types.String.SpecialChar.Function_NL(""));
            Assert.Equal("\t", _lamed.Types.String.SpecialChar.Function_TAB(""));
        }

        [Fact]
        [Test_Method("Math_QuoterOf3()")]
        [Test_Method("Math_Degree()")]
        [Test_Method("Math_Division()")]
        [Test_Method("Math_Function()")]
        [Test_Method("Math_Half()")]
        [Test_Method("Math_PlusMinus()")]
        [Test_Method("Math_Power2()")]
        [Test_Method("Math_Power3()")]
        [Test_Method("Math_Quoter()")]
        public void Math_Test()
        {
            Assert.Equal("¾", _lamed.Types.String.SpecialChar.Math_QuoterOf3(""));
            Assert.Equal("°", _lamed.Types.String.SpecialChar.Math_Degree(""));
            Assert.Equal("÷", _lamed.Types.String.SpecialChar.Math_Division(""));
            Assert.Equal("ƒ", _lamed.Types.String.SpecialChar.Math_Function(""));
            Assert.Equal("½", _lamed.Types.String.SpecialChar.Math_Half(""));
            Assert.Equal("±", _lamed.Types.String.SpecialChar.Math_PlusMinus(""));
            Assert.Equal("²", _lamed.Types.String.SpecialChar.Math_Power2(""));
            Assert.Equal("³", _lamed.Types.String.SpecialChar.Math_Power3(""));
            Assert.Equal("¼", _lamed.Types.String.SpecialChar.Math_Quoter(""));

        }

        [Fact]
        [Test_Method("Money_Cent()")]
        [Test_Method("Money_Euro()")]
        [Test_Method("Money_Pound()")]
        [Test_Method("Money_Yen()")]
        public void Money_Test()
        {
            Assert.Equal("¢", _lamed.Types.String.SpecialChar.Money_Cent(""));
            Assert.Equal("€", _lamed.Types.String.SpecialChar.Money_Euro(""));
            Assert.Equal("£", _lamed.Types.String.SpecialChar.Money_Pound(""));
            Assert.Equal("¥", _lamed.Types.String.SpecialChar.Money_Yen(""));
        }
    }
}
