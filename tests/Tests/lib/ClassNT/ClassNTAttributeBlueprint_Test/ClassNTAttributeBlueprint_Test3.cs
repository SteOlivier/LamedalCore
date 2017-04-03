using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttribute.ClassNTBlueprintRule;
using Xunit;

namespace LamedalCore.Test.Tests.lib.ClassNT.ClassNTAttributeBlueprint_Test
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action, DefaultType = typeof(string), GroupName = "Str")]
    public sealed class ClassNTAttributeBlueprint_Test3
    {
        [Fact]
        public void BlueprintRule_Class_Test()
        {
            #region Parameters
            string name;
            List<string> parameters;
            string ignore1, ignore2, ignore3, ignore4;
            enBlueprintClassNetworkType classNetworkType;
            string attributeCode1;
            bool isBlueprintRule;
            string defaultGroup, groupName, ShortcutClass;
            Type defaultType;
            bool ignoreGroup, ignorePath, includeObjects;
            #endregion

            #region Test3: [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action, DefaultType = typeof(string), GroupName = "Str")]
            // =========================================================================================================================================
            attributeCode1 = "[BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action, DefaultType = typeof(string), GroupName = \"Str\")]";
            isBlueprintRule = ClassNTBlueprintRule_Methods.BlueprintRule_Attributes(attributeCode1, out name, out parameters, out classNetworkType, out ignore1, out ignore2, out ignore3, out ignore4);
            Assert.Equal(true, isBlueprintRule);
            Assert.Equal(enBlueprintClassNetworkType.Node_Action, classNetworkType);
            Assert.Equal(3, parameters.Count);
            Assert.Equal(null, ignore1);
            Assert.Equal(null, ignore2);
            Assert.Equal(null, ignore3);
            Assert.Equal(null, ignore4);

            // Parameters
            if (isBlueprintRule)
            {
                ClassNTBlueprintRule_Methods.BlueprintRule_AttributeParameters(parameters, out defaultGroup, out defaultType, out groupName, out ignoreGroup, out ignorePath, out includeObjects, out ShortcutClass);

                Assert.Equal(typeof(string), defaultType);
                Assert.Equal(null, defaultGroup);
                Assert.Equal("Str", groupName);
                Assert.Equal(false, ignoreGroup);
                Assert.Equal(false, ignorePath);
                Assert.Equal(false, includeObjects);
                Assert.Equal(null, ShortcutClass);
            }
            #endregion

        }
    }
}
