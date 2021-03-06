﻿using System;
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
    [BlueprintRule_Class(enBlueprintClassNetworkType.CTIN, Ignore_Namespace1 = "Factory", Ignore_Namespace2 = "zz", Ignore_Namespace3 = "domain", Ignore_Namespace4 = "Testing")]
    public sealed class ClassNTAttributeBlueprint_Test2
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

            #region Test2: [BlueprintRule_Class(enBlueprintClassNetworkType.CTIN, Ignore_Namespace1 = "Factory", Ignore_Namespace2 = "zz", Ignore_Namespace3 = "domain", Ignore_Namespace4 = "Testing")]
            // =========================================================================================================================================
            attributeCode1 = "[BlueprintRule_Class(enBlueprintClassNetworkType.CTIN, Ignore_Namespace1 = \"Factory\", Ignore_Namespace2 = \"zz\", Ignore_Namespace3 = \"domain\", Ignore_Namespace4 = \"Testing\")]";
            isBlueprintRule = ClassNTBlueprintRule_Methods.BlueprintRule_Attributes(attributeCode1, out name, out parameters, out classNetworkType, out ignore1, out ignore2, out ignore3, out ignore4);
            Assert.Equal(enBlueprintClassNetworkType.CTIN, classNetworkType);
            Assert.Equal("Factory", ignore1);
            Assert.Equal("zz", ignore2);
            Assert.Equal("domain", ignore3);
            Assert.Equal("Testing", ignore4);
            Assert.Equal(5, parameters.Count);
            Assert.Equal(true, isBlueprintRule);

            // Parameters
            if (isBlueprintRule)
            {
                ClassNTBlueprintRule_Methods.BlueprintRule_AttributeParameters(parameters, out defaultGroup, out defaultType, out groupName, out ignoreGroup, out ignorePath, out includeObjects, out ShortcutClass);

                Assert.Equal(null, defaultType);
                Assert.Equal(null, defaultGroup);
                Assert.Equal(null, groupName);
                Assert.Equal(false, ignoreGroup);
                Assert.Equal(false, ignorePath);
                Assert.Equal(false, includeObjects);
                Assert.Equal(null, ShortcutClass);
            }

            #endregion

        }
    }
}
