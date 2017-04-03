using System;
using System.Collections.Generic;

namespace LamedalCore.Test.Tests._Data
{
    public sealed class TestData_Json
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Readonly property of type list
        public IList<string> Roles { get { return _Roles; }}

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = "json_Test_Data";

        private List<string> _Roles = new List<string>();


        public TestData_Json_FieldObject FieldObject = new TestData_Json_FieldObject();
    }

    public sealed class TestData_Json_FieldObject
    {
        public string Text = "Undefined";
    }
}