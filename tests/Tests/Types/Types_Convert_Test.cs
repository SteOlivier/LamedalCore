using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.Test.Tests.Types.List;
using LamedalCore.Test.Tests._Data;
using LamedalCore.Types;
using LamedalCore.Types.List;
using Xunit;
using Types_Is_Test_StructSimple = LamedalCore.Test.Tests._Data.Types_Is_Test_StructSimple;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_Convert_Test
    {
        private readonly Types_Convert _convert = LamedalCore_.Instance.Types.Convert;
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        private readonly Types_ _type = LamedalCore_.Instance.Types;
        // system library

        [Fact]
        [Test_Method("Bool_FromObj()")]
        [Test_Method("Bool_FromObj2()")]
        [Test_Method("Bool_FromStr()")]
        public void Bool_FromObj_Test()
        {
            object Object = null;
            Assert.Equal(false, _convert.Bool_FromObj(Object));
            Assert.Equal(true, _convert.Bool_FromObj2(Object, true));

            Object = true;
            Assert.Equal(true, _convert.Bool_FromObj(Object));
            Assert.Equal(true, _convert.Bool_FromObj2(Object, true));

            Object = "true";
            Assert.Equal(true, _convert.Bool_FromObj(Object));

            Object = "1";
            Assert.Equal(true, _convert.Bool_FromObj(Object));
            Assert.Equal(true, _convert.Bool_FromObj("1"));
            Assert.Equal(false, _convert.Bool_FromObj("0"));

            var strBool = "true";
            Assert.Equal(true, _convert.Bool_FromObj(strBool));

            // Bool_FromStr
            Assert.Equal(true, _convert.Bool_FromStr("1"));
            Assert.Equal(false, _convert.Bool_FromStr("0"));

            // Exceptions
            Assert.Throws<FormatException>(() => _convert.Bool_FromObj("abc"));
            Assert.Throws<FormatException>(() => _convert.Bool_FromStr("abcd"));
        }

        [Fact]
        [Test_Method("DateTime_FromObj()")]
        public void DateTime_FromObj_Test()
        {
            object nullValue = null;
            DateTime date1 = _convert.DateTime_FromObj(null);
            DateTime date2 = _convert.DateTime_FromObj(nullValue);
            Assert.Equal(date1, DateTime.MinValue);
            Assert.Equal(date2, DateTime.MinValue);

            DateTime now = DateTime.Now;
            object nowObject = now;
            Assert.Equal(now, _convert.DateTime_FromObj(nowObject));

            DateTime dateFromStr = _convert.DateTime_FromObj("2010/11/12");
            var dateResult = new DateTime(2010,11,12);
            Assert.Equal(dateResult, dateFromStr);

            Assert.Throws<FormatException>(() => _convert.DateTime_FromObj("20101/11/12"));
        }

        [Fact]
        [Test_Method("Double_FromObj()")]
        public void Double_FromObj_Test()
        {
            // Object to Double
            object Object = 5.4326;
            var doubleValue = _convert.Double_FromObj(Object);
            Assert.Equal(doubleValue, 5.4326);

            // IsString to Double
            var strDouble = "2.12383";
            doubleValue = _convert.Double_FromObj(strDouble);
            Assert.Equal(doubleValue, 2.12383);

        }

        [Fact]
        [Test_Method("Guid_FromStr()")]
        [Test_Method("Guid_FromObj()")]
        public void Guid_Test()
        {
            // Guid_FromStr
            Assert.Equal(Guid.Empty, _convert.Guid_FromStr(""));
            Assert.Equal(new Guid("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8"), _convert.Guid_FromStr("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8"));

            // Guid_FromObj
            object guidObject = Guid.Empty;
            Assert.Equal(Guid.Empty, _convert.Guid_FromObj(guidObject));
            Assert.Equal(Guid.Empty, _convert.Guid_FromObj(null));
            Assert.Equal(Guid.Empty, _convert.Guid_FromObj(123));
            Assert.Equal(Guid.Empty, _convert.Guid_FromObj("123"));
            Assert.Equal(Guid.Empty, _convert.Guid_FromObj("abc"));
        }

        [Fact]
        [Test_Method("Int_FromObj()")]
        public void Int_FromObj_Test()
        {
            object Object = 1234.6;
            var intValue = _convert.Int_FromObj(Object);
            Assert.True(intValue == 1235, "As.Int()");

            Object = "1234.6";
            intValue = _convert.Int_FromObj(Object);
            Assert.True(intValue == 1235, "As.Int(\"1234.6\")");
            var intValue2 = _convert.Int_FromObj("1234.6");
            Assert.True(intValue2 == 1235, "As.Int(\"1234.6\")");

            Object = null;
            intValue = _convert.Int_FromObj(Object);
            Assert.True(intValue == 0, "As.Int(null)");

            Object = true;
            intValue = _convert.Int_FromObj(Object);
            Assert.True(intValue == 1, "As.Int(true)");

            //Object = "2014/02/26";
            //DateTime dateValue = Date(Object);
            //Assert.True();
        }

        [Fact]
        [Test_Method("Int_FromObj2()")]
        [Test_Method("Int_FromChar()")]
        public void Int_FromObj2_Test()
        {
            // Int_FromObj2
            object Object = null;
            Assert.Equal(-1, _convert.Int_FromObj2(Object, -1));
            Assert.Equal(123, _convert.Int_FromObj2("123", -1));
            Assert.Equal(123, _convert.Int_FromObj2(123, -1));
            Assert.Equal(123, _convert.Int_FromObj2(123.123, -1));
            Assert.Equal(-1, _convert.Int_FromObj2(this, -1));

            // Int_FromChar
            Assert.Equal(7, _lamed.Types.Convert.Int_FromChar('7'));
        }

        [Fact]
        [Test_Method("IsEnumerable()")]
        public void IsEnumerable_Test()
        {
            //var test = enum_Test.Test1;
            Debug.WriteLine("1. enum_Test");
            var enum1 = List_Convert_Data.Test1;
            Assert.True(_type.Enum.IsEnumerable(enum1.GetType()));
        }

        [Fact]
        [Test_Method("Str_FromInt()")]
        public void Str_FromInt_Test()
        {
            Assert.Equal("00052", _lamed.Types.Convert.Str_FromInt(52, 5));
            Assert.Equal("00000", _lamed.Types.Convert.Str_FromInt(0, 5));
            Assert.Equal("####0", _lamed.Types.Convert.Str_FromInt(0, 5, '#'));
            Assert.Equal("00(0)", _lamed.Types.Convert.Str_FromInt(0, 5, '0', "(0)"));
            Assert.Equal("##(0)", _lamed.Types.Convert.Str_FromInt(0, 5, '#', "(0)"));
        }

        [Fact]
        [Test_Method("Str_FromObj()")]
        [Test_Method("Str_FromBool()")]
        public void Str_FromObj_Test()
        {
            object value = 54;
            Assert.Equal("00054", _convert.Str_FromObj(value, 5));

            // bool
            Assert.Equal("true", _convert.Str_FromBool(true));
            Assert.Equal("true", _convert.Str_FromObj(true));
            Assert.Equal("True", _convert.Str_FromObj(enBool.True));

        }

        [Fact]
        [Test_Method("Str_FromObj()")]
        [Test_Method("Type_FromStr()")]
        public void Str_FromObj_Test2()
        {
            #region Test1: string & Enum
            //      ===========================================
            // String
            Assert.Equal("string", _convert.Str_FromObj(typeof(string)));
            Assert.Equal(typeof(string), _convert.Type_FromStr("string"));

            // Enum
            Assert.Equal("Enum", _convert.Str_FromObj(typeof(Enum)));
            Assert.Equal(typeof(Enum), _convert.Type_FromStr("Enum"));
            #endregion

            // Objects
            Assert.Equal("LamedalCore.Test.Tests.Types.Types_Convert_Test", _convert.Str_FromObj(this.GetType()));

            // Guid
            Assert.Equal("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8", _convert.Str_FromObj(new Guid("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8")));

            // Assembly
            Assert.Equal(typeof(Assembly), _convert.Type_FromStr("Assembly"));
            //var strType = _lamed.Types.Convert.GetType().FullName;   // If the next line gives error, recalc the object string value
            Assert.Equal(_lamed.Types.Convert.GetType(), _convert.Type_FromStr("LamedalCore.Types.Types_Convert"));
            Assert.Throws<InvalidOperationException>(() => _convert.Type_FromStr("int"));
        }

    }
}
