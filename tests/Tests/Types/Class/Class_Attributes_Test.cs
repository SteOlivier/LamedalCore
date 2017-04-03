using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using Xunit;

namespace LamedalCore.Test.Tests.Types.Class
{
    public sealed class Class_Attributes_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Find_ForType()")]
        [Test_Method("Find_Fields()")]
        public void Find_ForType_Test()
        {
            var test = new Class_Attributes_Data();
            test.Name = "Piet";
            test.Surname = "Brown";
            test.Field3 = "Test";

            #region Find_ForType
            // Constructor
            BlueprintData_DescriptionAttribute attributeDesc;
            enAttributeLocation location = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attributeDesc);
            Assert.Equal(enAttributeLocation.Constructor, location);
            Assert.NotEqual(null, attributeDesc);
            Assert.Equal("Constructor()", attributeDesc.Description);

            // Method
            BlueprintRule_MethodAliasDefAttribute attribute2;
            var location2 = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attribute2);
            Assert.Equal(enAttributeLocation.Method, location2);

            // Field
            BlueprintData_FieldAttribute attribute3;
            var location3 = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attribute3);
            Assert.Equal(enAttributeLocation.Field, location3);

            // None
            FactAttribute attribute4;
            var location4 = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attribute4);
            Assert.Equal(enAttributeLocation.None, location4);

            // Property
            DescriptionAttribute attribute5;
            var location5 = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attribute5);
            Assert.Equal(enAttributeLocation.Property, location5);

            // Class
            BlueprintRule_ClassAttribute attribute6;
            var location6 = _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attribute6);
            Assert.Equal(enAttributeLocation.Class, location6);
            #endregion

            #region Check if field attribute exists
            BlueprintData_FieldAttribute attributeField;
            Assert.Equal(enAttributeLocation.Field, _lamed.Types.Class.ClassAttributes.Find_ForType(test.GetType(), out attributeField));
            Assert.True(attributeField != null);
            Assert.Equal("What is your name [{0}]? ", attributeField.Caption);
            #endregion

            #region Check all fields
            var fields = _lamed.Types.Class.ClassAttributes.Find_Fields<BlueprintData_FieldAttribute>(test.GetType());
            Assert.Equal(2,fields.Count);
            Assert.Equal("Name", fields[0].Item1.Name);
            Assert.Equal("Piet", fields[0].Item1.GetValue(test));
            Assert.Equal("What is your name [{0}]? ", fields[0].Item2.Caption);

            Assert.Equal("Surname", fields[1].Item1.Name);
            Assert.Equal("What is your surname [{0}]? ", fields[1].Item2.Caption);

            

            // All public fields
            fields = _lamed.Types.Class.ClassAttributes.Find_Fields<BlueprintData_FieldAttribute>(test.GetType(),true);
            Assert.Equal(false, fields[2].Item1.IsPrivate);
            Assert.Equal(3,fields.Count);

            // All Fields
            fields = _lamed.Types.Class.ClassAttributes.Find_Fields<BlueprintData_FieldAttribute>(test.GetType(), true, true);
            Assert.Equal(false, fields[2].Item1.IsPrivate);
            Assert.Equal(true, fields[3].Item1.IsPrivate);
            Assert.Equal(6, fields.Count);
            #endregion
        }

        [Fact]
        [Test_Method("Find_Properties()")]
        [Test_Method("Method_Execute()")]
        public void Find_Properties_Test()
        {
            var test = new Class_Attributes_Data();
            test.Property1 = "Piet";
            test.Property2 = "Brown";
            //test.Property3 = "Test";

            // BlueprintData_FieldAttribute
            var properties1 = _lamed.Types.Class.ClassAttributes.Find_Properties<BlueprintData_FieldAttribute>(test.GetType());
            Assert.Equal(1, properties1.Count);
            Assert.Equal("Property1", properties1[0].Item1.Name);
            Assert.Equal("Piet", properties1[0].Item1.GetValue(test));

            // All public properties
            var properties2 = _lamed.Types.Class.ClassAttributes.Find_Properties<BlueprintData_FieldAttribute>(test.GetType(),true);
            Assert.Equal(2, properties2.Count);

            // All public & private properties
            var properties3 = _lamed.Types.Class.ClassAttributes.Find_Properties<BlueprintData_FieldAttribute>(test.GetType(), true, true);
            Assert.Equal(4, properties3.Count);
            Assert.Equal(null, test.Property4);

            // Set private property
            // Just be aware that you're very sneaky here.
            MethodInfo setter = properties3[2].Item1.GetSetMethod(/*nonPublic*/ true);
            _lamed.Types.Class.ClassInfo.Method_Execute(test, setter, "Test");
            //if (setter != null) setter.Invoke(test, new object[] { "Test" });  
            Assert.Equal("Test", test.Property4);
        }

        [Fact]
        [Test_Method("Find_Method()")]
        public void Find_Method_Test()
        {
            var test = new Class_Attributes_Data();

            #region BlueprintRule_MethodAttribute
            var methods1 = _lamed.Types.Class.ClassAttributes.Find_Methods<BlueprintRule_MethodAttribute>(test.GetType());
            Assert.Equal(1,methods1.Count);
            Assert.Equal("TestMethod",methods1[0].Item1.Name);
            Assert.Equal("Class_Shortcut", methods1[0].Item2.ShortcutClassName);
            #endregion

            #region BlueprintRule_MethodAliasDefAttribute
            var methods2 = _lamed.Types.Class.ClassAttributes.Find_Methods<BlueprintRule_MethodAliasDefAttribute>(test.GetType());
            Assert.Equal(1,methods2.Count);
            Assert.Equal(typeof(Class_Attributes_Data), methods2[0].Item2.MirrorClass);

            var methods3 = _lamed.Types.Class.ClassAttributes.Find_Methods<BlueprintRule_MethodAliasDefAttribute>(test.GetType(), true);
            var methods4 = _lamed.Types.Class.ClassAttributes.Find_Methods<BlueprintRule_MethodAliasDefAttribute>(test.GetType(), true, true);
            Assert.Equal(2,methods3.Count);
            Assert.Equal(3,methods4.Count);
            #endregion


        }
    }
}
