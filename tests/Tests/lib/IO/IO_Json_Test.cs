using System;
using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.IO;
using LamedalCore.Test.Tests._Data;
using LamedalCore.zPublicClass;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.lib.IO
{
    public sealed class IO_Json_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        private readonly IO_Json _json = LamedalCore_.Instance.lib.IO.Json;

        public IO_Json_Test(ITestOutputHelper debug = null) : base(debug) { } 

        [Fact]
        [Test_Method("Convert_FromObject()")]
        [Test_Method("Convert_ToType()")]
        [Test_Method("Object_Set()")]
        public void Object_ToJson_Test()
        {
            // Setup the data

            #region Test1: json = Convert_FromObject(json_TestClass);
            //      ===========================================
            // Null class test
            TestData_Json json_TestClass = null;
            string json1 = _json.Convert_FromObject(json_TestClass);
            Assert.Equal("", json1);

            // Instanciated class
            json_TestClass = JsonTestClass();
            json1 = _json.Convert_FromObject(json_TestClass, false);
            var json1b = _json.Convert_FromObject(json_TestClass, true);
            Assert.Equal(json1, json1b);
            string jsonResult =
@"{
  ""FieldObject"": {
    ""Text"": ""Undefined""
  },
  ""Email"": ""james@example.com"",
  ""Active"": true,
  ""CreatedDate"": ""2013-01-20T00:00:00Z"",
  ""Roles"": [
    ""User"",
    ""Admin""
  ],
  ""Name"": ""json_Test_Data""
}";
            Assert.Equal(jsonResult, json1);

            #endregion

            #region Test2: Convert_ToType
            // ===========================================
            var json_TestClass2 = _json.Convert_ToType<TestData_Json>(json1);
            Assert.Equal(json_TestClass.Email, json_TestClass2.Email);
            Assert.Equal(json_TestClass.Active, json_TestClass2.Active);
            Assert.Equal(json_TestClass.CreatedDate, json_TestClass2.CreatedDate);
            Assert.Equal(json_TestClass.Roles.Count, json_TestClass2.Roles.Count);
            Assert.Equal(json_TestClass.Roles[0], json_TestClass2.Roles[0]);
            Assert.Equal(json_TestClass.Roles[1], json_TestClass2.Roles[1]);
            string error;
            Assert.True(_lamed.Types.Object.IsEqual(json_TestClass, json_TestClass2, out error), error);
            #endregion

            #region Convert to json and filter for fields
            string json2 = _json.Convert_FromObject(json_TestClass, false, "Email", "Active");
            string json2Result =
@"{
  ""Email"": ""james@example.com"",
  ""Active"": true
}";
            Assert.Equal(json2Result, json2);

            // Filter for field 'Active2' that does not exists
            var ex = Assert.Throws<InvalidOperationException>(() => _json.Convert_FromObject(json_TestClass, false, "Email", "Active", "Active2"));
            Assert.Equal("Error! The following fields were not found: Active2", ex.Message);

            #endregion
        }

        [Fact]
        [Test_Method("Object_Set()")]
        public void Object_Set_Test()
        {
            // Sync
            // ===========================================
            var json_TestClass = JsonTestClass();
            Assert.Equal("json_Test_Data", json_TestClass.Name);

            string jsonResult =
@"{
  ""FieldObject"": {
    ""Text"": ""Undefined""
  },
  ""Email"": ""james@example.com"",
  ""Active"": true,
  ""CreatedDate"": ""2013-01-20T00:00:00Z"",
  ""Roles"": [
    ""User"",
    ""Admin""
  ],
  ""Name"": ""json_Test_Data"",
}";
            var syncStr = jsonResult.Replace("james@example.com", "james@example_sync.com");
            _json.Object_Set(json_TestClass, syncStr);
            Assert.Equal("james@example_sync.com", json_TestClass.Email);
            Assert.Equal(json_TestClass.Active, true);

            Assert.Equal(json_TestClass.Name, "json_Test_Data");
            _json._Object_SetProperty(json_TestClass, "Name", "name123");
            _json._Object_SetProperty(json_TestClass, "Active", false);
            Assert.Equal(json_TestClass.Name, "json_Test_Data");
            Assert.Equal(json_TestClass.Active, false);

            // SetField test
            var json = "{".NL() +
                            "  \"Text\": \"Defined\",".NL() +
                            "  \"Name\": \"FieldObject\"".NL() +
                            "}";
            _lamed.lib.IO.Json.Object_SetField(json_TestClass, json);
            Assert.Equal("Defined", json_TestClass.FieldObject.Text);

            //var jsonField = "{\"Name\": \"james bond\"}";
            //_json.Object_SetField(json_TestClass, jsonField);

            // Change a field name
            var syncStr2 = jsonResult.Replace("\"Email\": ", "\"Email2\": ");
            var ex = Assert.Throws<InvalidOperationException>(() => _json.Object_Set(json_TestClass, syncStr2));
            Assert.Equal("Error! Property / Field 'Email2' does not exist in object: 'LamedalCore.Test.Tests._Data.TestData_Json'.", ex.Message);
        }


        private TestData_Json JsonTestClass()
        {
            var json_TestClass = new TestData_Json();
            Assert.Equal("json_Test_Data", json_TestClass.Name);
            json_TestClass.Email = "james@example.com";
            json_TestClass.Active = true;
            json_TestClass.CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc);
            json_TestClass.Roles.Clear();
            json_TestClass.Roles.Add("User");
            json_TestClass.Roles.Add("Admin");
            return json_TestClass;
        }

        [Fact]
        public void Json_Equal_Test()
        {
            TestData_Json testClass1 = JsonTestClass();

            #region Test1: Convert_CloneType()
            //      ===========================================
            var testClass2 = _json.Convert_CloneType(testClass1);

            string error;
            Assert.True(_json.Object_IsEqual(testClass1, testClass2, out error));
            #endregion

            #region Test2:
            // ===========================================
            DebugLog("Test:Change on property and show error");
            DebugLog("======================================");
            testClass2.Roles[0] = "new value";
            Assert.False(_json.Object_IsEqual(testClass1, testClass2, out error));
            DebugLog(error);
            var errorRestult =
@"Error! No match found at index = 8.
Values differ at pos = 6.

Value1: '    ""User"",' != 
Value2: '    ""new value"",'; 
Diff??: ------^";
            Assert.Equal(errorRestult, error);
            #endregion

        }
    }
}
