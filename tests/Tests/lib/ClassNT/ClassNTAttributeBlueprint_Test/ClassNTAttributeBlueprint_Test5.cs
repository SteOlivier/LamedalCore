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
    [BlueprintRule_Class(enBlueprintClassNetworkType.Transformation_Extention, DefaultGroup = "default group", DefaultType = typeof(string), GroupName = "group name", IgnoreGroup = true, IgnoreGroupPath = true, Ignore_Namespace1 = "ignore 1", ShortcutClass = "Shortcut Class")]
    public sealed class ClassNTAttributeBlueprint_Test5
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

            #region Test5: [BlueprintRule_Class(enBlueprintClassNetworkType.Transformation_Extention, DefaultGroup = "default group", DefaultType = typeof(string), GroupName = "group name", IgnoreGroup = true, IgnoreGroupPath = true, Ignore_Namespace1 = "ignore 1", ShortcutClass = "Shortcut Class")]
            // =========================================================================================================================================
            attributeCode1 = "[BlueprintRule_Class(enBlueprintClassNetworkType.Transformation_Extention, DefaultGroup = \"default group\", DefaultType = typeof(string), GroupName = \"group name\", IgnoreGroup = true, IgnoreGroupPath = true, Ignore_Namespace1 = \"ignore 1\", ShortcutClass = \"Shortcut Class\")]";
            isBlueprintRule = ClassNTBlueprintRule_Methods.BlueprintRule_Attributes(attributeCode1, out name, out parameters, out classNetworkType, out ignore1, out ignore2, out ignore3, out ignore4);
            Assert.Equal(true, isBlueprintRule);
            Assert.Equal(enBlueprintClassNetworkType.Transformation_Extention, classNetworkType);
            Assert.Equal(8, parameters.Count);
            Assert.Equal("ignore 1", ignore1);
            Assert.Equal(null, ignore2);
            Assert.Equal(null, ignore3);
            Assert.Equal(null, ignore4);

            // Parameters
            if (isBlueprintRule)
            {
                ClassNTBlueprintRule_Methods.BlueprintRule_AttributeParameters(parameters, out defaultGroup, out defaultType, out groupName, out ignoreGroup, out ignorePath, out includeObjects, out ShortcutClass);
                Assert.Equal("default_group", defaultGroup);
                Assert.Equal(typeof(string), defaultType);
                Assert.Equal("group_name", groupName);
                Assert.Equal(true, ignoreGroup);
                Assert.Equal(true, ignorePath);
                Assert.Equal(false, includeObjects);
                Assert.Equal("Shortcut_Class", ShortcutClass);
            }
            #endregion
        }
    }
}
