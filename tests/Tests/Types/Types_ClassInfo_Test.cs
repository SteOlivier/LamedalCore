using System.Collections.Generic;
using System.Reflection;
using LamedalCore.Test.TestData;
using LamedalCore.Types;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_ClassInfo_Test
    {
        private static readonly Types_ _types = LamedalCore_.Instance.Types;

        [Fact]
        public void Property_Set()
        {
            
            // Age test ]=======================================================//
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            Assert.Equal(1, lassie.Age);
            lassie.BirhthDay();
            Assert.Equal(2, lassie.Age);

            // Owner test]======================================================//
            Assert.Equal("", lassie.OwnderName);
            _types.ClassInfo.Property_Set(lassie, "OwnderName", "Jan");
            var value = _types.ClassInfo.Property_Get(lassie, "OwnderName");
            Assert.Equal("Jan", value);

            lassie.OwnderName = "Piet";
            value = _types.ClassInfo.Property_Get(lassie, "OwnderName");
            Assert.Equal("Piet", value);

            Assert.Equal(enSpecies.Dog, lassie.Species);
            _types.ClassInfo.Property_Set(lassie, "Species", enSpecies.Unknown);
            Assert.Equal(enSpecies.Unknown, lassie.Species);
            // =================================================================//
        }

        [Fact]
        public void PropertyNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> properties = _types.ClassInfo.Properties_AsStrList(lassie);
            Assert.Equal(2, properties.Count);
            Assert.True(properties.zContains("Species", "OwnderName"));
        }

        [Fact]
        public void Field_Set()
        {
            // Age test ]================================================
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            Assert.Equal(1, lassie.Age);
            lassie.BirhthDay();
            Assert.Equal(2, lassie.Age);
            FieldInfo fieldInfo = _types.ClassInfo.Dictionary.FieldInfo_Get(lassie.GetType(), "Age");  // See if we can get this field
            Assert.True(fieldInfo != null);

            _types.ClassInfo.Field_Set(lassie, "Age", 10);
            var value = _types.ClassInfo.Field_Get(lassie, "Age");
            Assert.Equal(10, lassie.Age);
            Assert.Equal(10, value);
        }

        [Fact]
        public void FieldNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> fields = _types.ClassInfo.Fields_AsStrList(lassie);
            Assert.Equal(4, fields.Count);
            Assert.True(fields.zContains("Legs", "Age", "Health", "DogType"));
        }

        [Fact]
        public void MethodNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> methods = _types.ClassInfo.Methods_AsStrList(lassie);
            Assert.Equal(2, methods.Count);
            Assert.True(methods.zContains("BirhthDay", "Health_Set"));
        }
    }
}