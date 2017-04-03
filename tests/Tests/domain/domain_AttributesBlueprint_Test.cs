using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.Words.WordsList;
using LamedalCore.Test.Tests.Types.Class;
using Xunit;

namespace LamedalCore.Test.Tests.domain
{
    public sealed class domain_AttributesBlueprint_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("BlueprintRule_FieldAttribute")]
        public void BlueprintRule_FieldAttribute_Test()
        {
            var testClass = new Class_Attributes_Data();
            var blueprint = _lamed.Types.Class.ClassInfo.Blueprint_Attributes(testClass.GetType());
            var blueprint2 = _lamed.Types.Class.ClassInfo.Blueprint_Attributes(testClass.GetType());    // execute line again - test cashing code
            var blueprint3 = _lamed.Types.Class.ClassInfo.Blueprint_Attributes(this.GetType());         // Test not found condition
            Assert.NotEqual(null, blueprint2.Class_Rule);
            Assert.Equal(null, blueprint3.Class_Rule);

            // Data
            Assert.Equal("What is your name [{0}]? ", blueprint.Data_Property("Name").Caption);
            Assert.Equal("This field is used for testing purposes.", blueprint.Data_Property("Name").Description);
            Assert.Equal("Property1 Setting", blueprint.Data_Property("Property1").Caption);
            Assert.Equal("Class Attribute Data", blueprint.Class_DataTableInfo.Caption);

            // Rules
            Assert.Equal(enBlueprintClassNetworkType.Node_Data, blueprint.Class_Rule.ClassType);
            Assert.Equal("NameIsField3", blueprint.Rule_PropertyField("Field3").Name);
            Assert.Equal("This is the description", blueprint.Rule_PropertyField("Property1").Description);
            Assert.Equal("zzTest_Method", blueprint.Rule_Method("TestMethod").ShortcutMethodName);
            Assert.Equal("Method_Test", blueprint.Rule_MethodAlias("TestMethod").MirrorMethodName);

            // Info
            Assert.Equal("Name", blueprint.PropertyField_Info("Name").Name);
        }

    }
}
