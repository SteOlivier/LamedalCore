using System.Collections.Generic;
using System.Reflection;
using LamedalCore.domain.Attributes;
using LamedalCore.Types;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types.Class
{
    public sealed class Class_Info_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Property_Set()")]
        [Test_Method("Property_Get()")]
        public void Property_Set()
        {
            
            // Age test ]=======================================================//
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            Assert.Equal(1, lassie.Age);
            lassie.BirhthDay();
            Assert.Equal(2, lassie.Age);

            // Owner test]======================================================//
            Assert.Equal("", lassie.OwnderName);
            _lamed.Types.Class.ClassInfo.Property_Set(lassie, "OwnderName", "Jan");
            object Object = _lamed.Types.Class.ClassInfo.Property_Get(lassie, "OwnderName");
            string valueStr = _lamed.Types.Class.ClassInfo.Property_Get<string>(lassie, "OwnderName");
            Assert.Equal("Jan", Object);
            Assert.Equal("Jan", valueStr);

            lassie.OwnderName = "Piet";
            Object = _lamed.Types.Class.ClassInfo.Property_Get(lassie, "OwnderName");
            Assert.Equal("Piet", Object);

            Assert.Equal(enSpecies.Dog, lassie.Species);
            _lamed.Types.Class.ClassInfo.Property_Set(lassie, "Species", enSpecies.Unknown);
            Assert.Equal(enSpecies.Unknown, lassie.Species);
            // =================================================================//
        }

        [Fact]
        [Test_Method("Properties_AsStrList()")]
        public void PropertyNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> properties = _lamed.Types.Class.ClassInfo.Properties_AsStrList(lassie.GetType());
            Assert.Equal(2, properties.Count);
            Assert.True(properties.zContains("Species", "OwnderName"));
        }

        [Fact]
        [Test_Method("Field_AsFieldInfo()")]
        [Test_Method("Field_Get()")]
        public void Field_Set()
        {
            // Age test ]================================================
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            Assert.Equal(1, lassie.Age);
            lassie.BirhthDay();
            Assert.Equal(2, lassie.Age);
            FieldInfo fieldInfo = _lamed.Types.Class.ClassInfo.Field_AsFieldInfo(lassie.GetType(), "Age");  // See if we can get this field
            Assert.True(fieldInfo != null);

            _lamed.Types.Class.ClassInfo.Field_Set(lassie, "Age", 10);
            object value = _lamed.Types.Class.ClassInfo.Field_Get(lassie, "Age");
            int valueInt = _lamed.Types.Class.ClassInfo.Field_Get<int>(lassie, "Age");
            Assert.Equal(10, lassie.Age);
            Assert.Equal(10, value);
            Assert.Equal(10, valueInt);
        }

        [Fact]
        [Test_Method("Fields_AsStrList()")]
        public void FieldNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> fields = _lamed.Types.Class.ClassInfo.Fields_AsStrList(lassie.GetType());
            Assert.Equal(4, fields.Count);
            Assert.True(fields.zContains("Legs", "Age", "Health", "DogType"));
        }

        [Fact]
        [Test_Method("Methods_AsStrList()")]
        public void MethodNames_Test()
        {
            var lassie = new Types_ClassInfo_Dog_Bulldog(1);
            List<string> methods = _lamed.Types.Class.ClassInfo.Methods_AsStrList(lassie.GetType());
            Assert.Equal(2, methods.Count);
            Assert.True(methods.zContains("BirhthDay", "Health_Set"));
        }

        [Fact]
        [Test_Method("Constructor_AsConstructorInfo()")]
        public void Constructor_AsConstructorInfo_Test()
        {
            var dipsie = new Types_ClassInfo_Dog(2);
            var constructor = _lamed.Types.Class.ClassInfo.Constructor_AsConstructorInfo(dipsie.GetType(), "Types_ClassInfo_Dog");
            var constructor2 = _lamed.Types.Class.ClassInfo.Constructor_AsConstructorInfo(dipsie.GetType(), "Types_ClassInfo_Dog");  // Test cashing code
            Assert.Equal("Types_ClassInfo_Dog",constructor.DeclaringType.Name);
        }

        [Fact]
        [Test_Method("Method_AsMethodInfo()")]
        public void Method_AsMethodInfo_Test()
        {
            var dipsie = new Types_ClassInfo_Dog(3);
            var method = _lamed.Types.Class.ClassInfo.Method_AsMethodInfo(dipsie.GetType(), "Health_Set");
            Assert.Equal("Health_Set", method.Name);
        }
    }
}