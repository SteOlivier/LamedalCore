using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_int_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("ToRoman()")]
        [Test_Method("ToInt()")]
        [Test_Method("IsValid()")]
        public void ToRoman()
        {
            // intRomanNumbers
            Assert.Equal("MDXXXIV", _lamed.Types.intRomanNumbers.ToRoman(1534));
            Assert.Equal(1534, _lamed.Types.intRomanNumbers.ToInt("MDXXXIV"));
            Assert.Equal(true, _lamed.Types.intRomanNumbers.IsValid("MDXXXIV"));

            // Exceptions
            Assert.Throws<ArgumentOutOfRangeException>(() => _lamed.Types.intRomanNumbers.ToRoman(0));
            Assert.Throws<ArgumentNullException>(() => _lamed.Types.intRomanNumbers.ToInt(null));
        }
    }
}
