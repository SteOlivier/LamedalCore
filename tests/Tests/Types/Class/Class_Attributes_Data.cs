using System.ComponentModel;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;

namespace LamedalCore.Test.Tests.Types.Class
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Data)]
    [BlueprintData_Table(false, Caption = "Class Attribute Data", Description = "This class is used for testing purposes.")]
    public sealed class Class_Attributes_Data
    {
        [BlueprintData_Field(Caption = "What is your name [{0}]? ", Description = "This field is used for testing purposes.")]
        public string Name;

        [BlueprintData_Field(Caption = "What is your surname [{0}]? ")]
        public string Surname;

        [BlueprintRule_Field(Name = "NameIsField3", Description = "This is the description")]
        public string Field3;

        [BlueprintRule_Field(Description = "This is the description")]
        [BlueprintData_Field("Property1 Setting")]
        [Description("test")]
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        private string Property3 { get; set; }

        public string Property4
        {
            get { return Property3; }
        }

        [BlueprintRule_Method(ShortcutClassName = "Class_Shortcut", ShortcutMethodName = "zzTest_Method")]
        [BlueprintRule_MethodAliasDef(MirrorClass = typeof(Class_Attributes_Data), MirrorMethodName = "Method_Test")]
        public void TestMethod(string msg = "")
        {
            
        }

        public void TestMethod2(string msg = "")
        {

        }

        private void TestMethod3(string msg = "")
        {

        }

        [BlueprintData_Description("Constructor()")]
        public Class_Attributes_Data()
        {
            
        }
    }

}