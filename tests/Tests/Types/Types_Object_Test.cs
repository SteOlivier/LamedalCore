using System;
using System.Collections.Generic;
using System.Diagnostics;
using LamedalCore.domain.Attributes;
using LamedalCore.Test.Tests.Types.Class;
using LamedalCore.Test.Tests._Data;
using LamedalCore.Types;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_Object_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library
        private readonly Types_Object _object = LamedalCore_.Instance.Types.Object;

        [Fact]
        [Test_Method("Between()")]
        public void Between_Test()
        {
            // Numbers
            var myNumber = 3;
            Assert.True(_lamed.Types.Object.Between(myNumber, 3, 7));
            Assert.True(_lamed.Types.Object.Between(4, 3, 7));
            Assert.True(_lamed.Types.Object.Between(7, 3, 7));

            Assert.False(_lamed.Types.Object.Between(1, 3, 7));

            // Strings
            Assert.True(_lamed.Types.Object.Between("a", "a", "z"));
            Assert.True(_lamed.Types.Object.Between("f", "a", "z"));
            Assert.False(_lamed.Types.Object.Between("1", "a", "z"));
        }

        [Fact]
        public void CastTo_Test()
        {
            // Type checking
            Types_ClassInfo_Animal lassie = new Types_ClassInfo_Dog(1);
            Assert.True((lassie is Types_ClassInfo_Animal));                   // true 
            Assert.True(lassie is Types_ClassInfo_Dog);                   // true 
            Assert.True(lassie.GetType() == typeof(Types_ClassInfo_Dog));    // true
            Assert.False(lassie.GetType() == typeof(Types_ClassInfo_Animal)); // false 

            object annimal = _lamed.Types.Object.CastTo(lassie, typeof(Types_ClassInfo_Dog));
            Assert.True(annimal.GetType() == typeof(Types_ClassInfo_Dog));    // true
            Assert.False(annimal.GetType() == typeof(Types_ClassInfo_Animal)); // false 
        }

        [Fact]
        [Test_Method("IsIDictionary()")]
        public void IDictionary_Test()
        {
            var dic1 = new Dictionary<string, string>();
            Assert.True(_object.IsIDictionary(dic1));
            Assert.True(_object.IsComplexType(dic1));
        }

        [Fact]
        [Test_Method("IsIList()")]
        public void IList_Test()
        {
            var list = new List<string>();
            Assert.True(_object.IsIList(list));
        }

        [Fact]
        [Test_Method("IsComplexType()")]
        public void IsComplexType_Test()
        {
            // Complex type
            object intVar = 123;
            var intVar2 = 123;
            var lassie = new Types_ClassInfo_Dog(1);            
            Assert.True(_lamed.Types.Object.IsComplexType(lassie));
            Assert.False(_lamed.Types.Object.IsComplexType(intVar));
            Assert.False(_lamed.Types.Object.IsComplexType(intVar2));
            Assert.True(_lamed.Types.Object.IsSimpleType(intVar2));
        }

        [Fact]
        [Test_Method("IsNull()")]
        public void IsNull_Test()
        {
            object nullVar1 = null;
            object nullVar2 = "\u0002";
            object nullVar3 = "\0";

            Assert.True(_lamed.Types.Object.IsNull(nullVar1));
            Assert.True(_lamed.Types.Object.IsNull(nullVar2));
            Assert.True(_lamed.Types.Object.IsNull(nullVar3));
        }

        [Fact]
        [Test_Method("IsNumber()")]
        public void IsNumber2_Test()
        {
            object intVar = 123;
            object floatVar = 123.123;
            Assert.True(_lamed.Types.Object.IsNumber(intVar));
            Assert.True(_lamed.Types.Object.IsNumber(floatVar));

            Assert.False(_object.IsNumber(null));
            Assert.True(_object.IsNumber(0));
            Assert.False(_object.IsNumber("0"));
            Assert.False(_object.IsNumber("one"));
        }
        [Fact]
        [Test_Method("IsNull")]
        public void ObjectNull_Test()
        {
            Debug.WriteLine("1. Test normal input");
            Assert.True(_object.IsNull(null));
            Assert.False(_object.IsNull(0));
            object o = null;
            Assert.True(_object.IsNull(o));

            Debug.WriteLine("2. Test abnormal input");
            Assert.True(_object.IsNull("\u0002"));
            Assert.True(_object.IsNull("\0"));
            Assert.True(_object.IsNull(null));
        }

        [Fact]
        [Test_Method("IsNull()")]
        public void ObjectNull_TestFail()
        {
            Assert.True(_object.IsNull(null));   // Show error
            Assert.True(_object.IsNull(null, false, ""));   // Show error
        }

        [Fact]
        [Test_Method("IsNull")]
        public void ObjectNull_TestFail2()
        {
            Assert.ThrowsAny<InvalidOperationException>(() => ObjectNull_TestFail3());
        }
        private void ObjectNull_TestFail3()
        {
            Assert.True(_object.IsNull(null, true, "Error Message!"));   // Show error
        }

        [Fact]
        [Test_Method("IsSimpleType()")]
        public void SimpleType_Test()
        {
            Assert.Equal(true, _object.IsSimpleType(5));
            Assert.Equal(true, _object.IsSimpleType("string"));
            Assert.Equal(false, _object.IsSimpleType(null));
        }

        [Fact]
        [Test_Method("IsStruct()")]
        [Test_Method("IsString()")]
        public void Struct_Test()
        {
            var struct1 = new Types_Is_Test_StructSimple();
            Assert.Equal(true, _object.IsStruct(struct1));
            Assert.Equal(false, _object.IsStruct("string value"));

            // IsString
            Assert.Equal(false, _object.IsString(struct1));
            Assert.Equal(true, _object.IsString("string value"));
        }

        [Fact]
        [Test_Method("DefaultValue()")]
        public void DefaultValue_Test()
        {
            Assert.Equal(null, _lamed.Types.Object.DefaultValue<string>());
            Assert.Equal(null, _lamed.Types.Object.DefaultValue(typeof(string)));
            Assert.Equal(0, _lamed.Types.Object.DefaultValue<int>());
            Assert.Equal(0, _lamed.Types.Object.DefaultValue(typeof(int)));
        }

        [Fact]
        [Test_Method("In()")]
        public void In_Test()
        {
            Assert.Equal(true, _lamed.Types.Object.In(5, 1, 2, 3, 4, 5));
            Assert.Equal(false, "5".zIn("1", "2", "3", "55"));
        }

        [Fact]
        [Test_Method("Create()")]
        public void Create_Test()
        {
            var obj1 = _lamed.Types.Object.Create<Types_Enum_Data>();
            var obj2 = new Types_Enum_Data();
            Assert.Equal(obj1, obj2);
            Assert.Equal(true, _lamed.Types.Object.Compare(obj1, obj2));
        }

        [Fact]
        [Test_Method("IsBetween()")]
        public void IsBetween_Test()
        {
            Assert.Equal(true,  _lamed.Types.Number.IsBetween(5,4,6));
            Assert.Equal(false, _lamed.Types.Number.IsBetween(5,5,6));
            Assert.Equal(false, _lamed.Types.Number.IsBetween(5,4,5));
            Assert.Equal(false, _lamed.Types.Number.IsBetween(5,6,10));
        }

        [Fact]
        [Test_Method("ToMoney()")]
        public void ToMoney_Test()
        {
            Assert.Equal(12.45, _lamed.Types.Number.ToMoney(12.451));
        }
    }

}