using System;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.PropertyNT;
using Xunit;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    public sealed class ClassNTProperty_Test
    {
        [Fact]
        [Test_Method("Property_Parse()")]
        public static void Property_Parse_Test()
        {
            string propertyLine, scope, name, type, typePart1, typePart2, typePart3;

            #region Test1: public Types_DateTime DateTime
            //      ===========================================
            propertyLine = "public Types_DateTime DateTime";
            PropertyNT_Methods.Property_Parse(propertyLine, out scope, out type, out name, out typePart1, out typePart2, out typePart3);
            Assert.Equal("public", scope);
            Assert.Equal("Types_DateTime", type);
            Assert.Equal("DateTime", name);
            Assert.Equal("Types", typePart1);
            Assert.Equal("DateTime", typePart2);
            Assert.Equal("", typePart3);
            #endregion

            #region Test2: public Rules_ Rules
            // ===========================================
            propertyLine = "public Rules_ Rules";
            PropertyNT_Methods.Property_Parse(propertyLine, out scope, out type, out name, out typePart1, out typePart2, out typePart3);
            Assert.Equal("public", scope);
            Assert.Equal("Rules_", type);
            Assert.Equal("Rules", name);
            Assert.Equal("Rules", typePart1);
            Assert.Equal("", typePart2);
            Assert.Equal("", typePart3);
            #endregion

            #region Test3: public Types_DateTime DateTime
            //      ===========================================
            propertyLine = "public Types_DateTime_123 DateTime";
            PropertyNT_Methods.Property_Parse(propertyLine, out scope, out type, out name, out typePart1, out typePart2, out typePart3);
            Assert.Equal("Types", typePart1);
            Assert.Equal("DateTime_123", typePart2);
            Assert.Equal("123", typePart3);
            #endregion

            #region Test4: error conditions
            propertyLine = "public Types_DateTime DateTime invalidcode";
            Assert.Throws<InvalidOperationException>( () => PropertyNT_Methods.Property_Parse(propertyLine, out scope, out type, out name, out typePart1, out typePart2, out typePart3));
            #endregion
        }
    }
}
