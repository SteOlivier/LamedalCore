using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("IsNumeric")]
        public void IsNumeric_Test()
        {
            Assert.True(_lamed.Types.Test.IsNumeric("1234"));
            Assert.True(_lamed.Types.Test.IsNumeric("1234.234"));
            Assert.False(_lamed.Types.Test.IsNumeric("a"));
            Assert.False(_lamed.Types.Test.IsNumeric(""));
            Assert.False(_lamed.Types.Test.IsNumeric(" "));
            Assert.False(_lamed.Types.Test.IsNumeric("1234.b234"));
        }

        [Fact]
        [Test_Method("IsValidStr()")]
        public void IsValidStr_Test()
        {
            Assert.Equal(false, _lamed.Types.Test.IsValidStr("\0"));
            var esc = _lamed.Types.String.SpecialChar.Function_ESC("");
            Assert.Equal(false, _lamed.Types.Test.IsValidStr(esc));
        }

    }
}
