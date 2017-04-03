using System;
using System.Collections.Generic;
using System.ComponentModel;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttribute;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttribute.ClassNTBlueprintRule;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttributes;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTHeader;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.XUnitTestMethods)]
    public sealed class ClassNTAttribute_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("ClassNTAttribute_Parameter_.Create()")]
        public void ClassNTAttribute_Parameter_Test1()
        {
            var parmeter = ClassNTAttribute_Parameter_.Create("BlueprintRule_", "Name = \"CTI Transformation\"");
            Assert.Equal("Name", parmeter.Name);
            Assert.Equal("CTI Transformation", parmeter.Value);
            Assert.Equal(false, parmeter.IsEnumeral);

            parmeter = ClassNTAttribute_Parameter_.Create("BlueprintRule_", "HasMethods = true");
            Assert.Equal("HasMethods", parmeter.Name);
            Assert.Equal(true, parmeter.Value);
            Assert.Equal(false, parmeter.IsEnumeral);
        }

        [Fact]
        [Test_Method("ClassNTAttribute_Parameter_.Create()")]
        public void ClassNTAttribute_Parameter_Test2()
        {
            var parmeter = ClassNTAttribute_Parameter_.Create("BlueprintRule_", "enBlueprintClassNetworkType.Node_State");
            Assert.Equal("BlueprintRule_", parmeter.Name);
            Assert.Equal(true, parmeter.IsEnumeral);
            Assert.Equal("enBlueprintClassNetworkType.Node_State", parmeter.Value);
            Assert.Equal(enBlueprintClassNetworkType.Node_State, _lamed.Types.Enum.Str_2EnumValue<enBlueprintClassNetworkType>(parmeter.Value.zObject().AsStr()));
        }

        [Fact]
        [Test_Method("ClassNTAttributes_.Create()")]
        [Category("Appearance")]
        [Description("Set Cancel Visible property")]
        [BlueprintRule_Method(Ignore = false)]
        public void ClassNTAttributes_Test()
        {
            // ====================
            // Test the class setup
            // ====================
            var source = new List<string>();
            source.Add("        [Fact]");
            source.Add("        [Test_Method(\"ClassNTAttributes_.Create()\")]");
            source.Add("        [Category(\"Appearance\")]");
            source.Add("        [Description(\"Set Cancel Visible property\")]");
            source.Add("        [BlueprintRule_Method(Ignore = false)]");
            var attributes = ClassNTAttributes_.Create(source);
            List<ClassNTAttribute_> attributeList = attributes.Items;

            Assert.Equal(5, attributes.Items.Count);
            Assert.Equal("Fact", attributes.Items[0].AttributeName);
            // Test_Method
            Assert.Equal("Test_Method", attributes.Items[1].AttributeName);
            Assert.Equal("\"ClassNTAttributes_.Create()\"", attributes.Items[1].Parameters[0].Value);
            // Category
            Assert.Equal("Category", attributes.Items[2].AttributeName);
            Assert.Equal("\"Appearance\"", attributes.Items[2].Parameters[0].Value);
            // Setup method is not tested against more values because it is based on Blueprint methods that contain their own unit tests.
        }

        [Fact]
        [Test_Method("Attribute_Parts()")]
        public static void Attribute_Parts_Test()
        {
            string name;
            List<string> parameters;
            #region Test1:  [Test]
            // ==============================================================
            var attributeCode1 = "[Test]";
            ClassNTAttributes_Methods.Attribute_Parts(attributeCode1, out name, out parameters);
            Assert.Equal("Test", name);
            Assert.Equal(0, parameters.Count);
            #endregion

            #region Test2: [TestedMethod_("Attribute_Parts()")]
            // =============================================================
            var attributeCode2 = "[TestedMethod_(\"Attribute_Parts()\")]";
            ClassNTAttributes_Methods.Attribute_Parts(attributeCode2, out name, out parameters);
            Assert.Equal("TestedMethod_", name);
            Assert.Equal("\"Attribute_Parts()\"", parameters[0]);
            Assert.Equal(1, parameters.Count);
            #endregion

            #region Test3: [ActiveTemplate_(Description = "This code will link different classes into a CTI network", Parameter1_Caption = "Class typename:\", Parameter1_Name= \"ClassHeading_NewTypeName\", Parameter1_Value= enATemplate_Value.Value_FromClipboard, Parameter2_Caption = "Property name:", Parameter2_Name = "NewTypeName")]
            // =======================================================
            // [ActiveTemplate_(Description = "This code will link different classes into a CTI network",
            // Parameter1_Caption = "Class typename:", Parameter1_Name= "ClassHeading_NewTypeName", Parameter1_Value= enATemplate_Value.Value_FromClipboard, 
            // Parameter2_Caption = "Property name:", Parameter2_Name = "NewTypeName")]
            var attLine = "[ActiveTemplate_(Description = \"This code will link different classes into a CTI network\", Parameter1_Caption = \"Class typename:\", Parameter1_Name= \"ClassHeading_NewTypeName\", Parameter1_Value= enATemplate_Value.Value_FromClipboard, Parameter2_Caption = \"Property name:\", Parameter2_Name = \"NewTypeName\")]";
            ClassNTAttributes_Methods.Attribute_Parts(attLine, out name, out parameters);
            Assert.Equal("ActiveTemplate_", name);
            Assert.Equal("Description = \"This code will link different classes into a CTI network\"", parameters[0]);
            Assert.Equal("Parameter1_Caption = \"Class typename:\"", parameters[1]);
            Assert.Equal("Parameter1_Name= \"ClassHeading_NewTypeName\"", parameters[2]);
            Assert.Equal("Parameter1_Value= enATemplate_Value.Value_FromClipboard", parameters[3]);
            Assert.Equal("Parameter2_Caption = \"Property name:\"", parameters[4]);
            Assert.Equal("Parameter2_Name = \"NewTypeName\"", parameters[5]);
            Assert.Equal(6, parameters.Count);
            #endregion

            #region Test4: Illigal input "", "[bla", "bla]"
            Assert.Throws<InvalidOperationException>(() => ClassNTAttributes_Methods.Attribute_Parts("", out name, out parameters));
            Assert.Throws<InvalidOperationException>(() => ClassNTAttributes_Methods.Attribute_Parts("[bla", out name, out parameters));
            Assert.Throws<InvalidOperationException>(() => ClassNTAttributes_Methods.Attribute_Parts("bla]", out name, out parameters));

            #endregion
        }

        [Fact]
        [Test_Method("Attributes_FromCode()")]
        public static void Attributes_FromCode_Test()
        {
            var source = new List<string>();
            List<string> attributeLines;

            #region Test1:  [Test], [Pure]
            // =======================================================
            source.Add("        [Test]");
            source.Add("        [Pure]");
            attributeLines = ClassNTAttributes_Methods.Attributes_FromCode(source);
            Assert.Equal("[Test]", attributeLines[0]);
            Assert.Equal("[Pure]", attributeLines[1]);
            Assert.Equal(2, attributeLines.Count);
            #endregion

            #region Test2: [Fact, Test_Method("Attributes_FromCode()")]
            // =======================================================
            source.Clear();
            source.Add("        [Fact, Test_Method(\"Attributes_FromCode()\")]");
            source.Add("        [BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]");
            attributeLines = ClassNTAttributes_Methods.Attributes_FromCode(source);
            Assert.Equal(3, attributeLines.Count);
            Assert.Equal("[Fact]", attributeLines[0]);
            Assert.Equal("[Test_Method(\"Attributes_FromCode()\")]", attributeLines[1]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]", attributeLines[2]);
            #endregion

            #region Test3: [Test, TestedMethod("Key_Add()"), TestedMethod("Key_Find()"), TestedMethod("Key_Remove()")]
            // =======================================================
            // [Pure]
            // [Category("Appearance")]
            // [Description("Set Cancel Visible property")]
            // [Test, TestedMethod("Key_Add()"), TestedMethod("Key_Find()"), TestedMethod("Key_Remove()")]
            source.Clear();
            source.Add("        [Pure]");
            source.Add("        [Category(\"Appearance\")]");
            source.Add("        [Description(\"Set Cancel Visible property\")]");
            source.Add("        [Test, TestedMethod(\"Key_Add()\"), TestedMethod(\"Key_Find()\"), TestedMethod(\"Key_Remove()\")]");
            attributeLines = ClassNTAttributes_Methods.Attributes_FromCode(source);
            Assert.Equal("[Pure]", attributeLines[0]);
            Assert.Equal("[Category(\"Appearance\")]", attributeLines[1]);
            Assert.Equal("[Description(\"Set Cancel Visible property\")]", attributeLines[2]);
            Assert.Equal("[Test]", attributeLines[3]);
            Assert.Equal("[TestedMethod(\"Key_Add()\")]", attributeLines[4]);
            Assert.Equal("[TestedMethod(\"Key_Find()\")]", attributeLines[5]);
            Assert.Equal("[TestedMethod(\"Key_Remove()\")]", attributeLines[6]);
            Assert.Equal(7, attributeLines.Count);
            #endregion

            #region Test4: [ActiveTemplate_(Description = "This code will link different classes into a CTI network", Parameter1_Caption = "Class typename:", Parameter1_Name= "ClassHeading_NewTypeName", Parameter1_Value= enATemplate_Value.Value_FromClipboard, Parameter2_Caption = "Property name:", Parameter2_Name = "NewTypeName")]
            // =======================================================
            // [ActiveTemplate_(Description = "This code will link different classes into a CTI network",
            // Parameter1_Caption = "Class typename:", Parameter1_Name= "ClassHeading_NewTypeName", Parameter1_Value= enATemplate_Value.Value_FromClipboard, 
            // Parameter2_Caption = "Property name:", Parameter2_Name = "NewTypeName")]
            source.Clear();
            source.Add("        [ActiveTemplate_(Description = \"This code will link different classes into a CTI network\",");
            source.Add("        Parameter1_Caption = \"Class typename:\", Parameter1_Name= \"ClassHeading_NewTypeName\", Parameter1_Value= enATemplate_Value.Value_FromClipboard, ");
            source.Add("        Parameter2_Caption = \"Property name:\", Parameter2_Name = \"NewTypeName\")]");
            attributeLines = ClassNTAttributes_Methods.Attributes_FromCode(source);
            var attLine = "[ActiveTemplate_(Description = \"This code will link different classes into a CTI network\", Parameter1_Caption = \"Class typename:\", Parameter1_Name= \"ClassHeading_NewTypeName\", Parameter1_Value= enATemplate_Value.Value_FromClipboard, Parameter2_Caption = \"Property name:\", Parameter2_Name = \"NewTypeName\")]";
            Assert.Equal(attLine, attributeLines[0]);
            Assert.Equal(1, attributeLines.Count);
            #endregion
        }

        [Fact]
        [Test_Method("Method_Attributes()")]
        [BlueprintRule_Method(ShortcutClassName = "ShortClassName", ShortcutMethodName = "ShortMethodName")]
        public void BlueprintRule_Method_Test()
        {
            string attributeStr;
            string name, ShortcutClassName, ShortcutMethodName;
            //string MirrorParameter1, MirrorClass, MirrorMethodName;
            bool ignore, isBlueprintRule;

            #region Test1: [BlueprintRule_Method(Ignore = true)]
            //      ===========================================
            attributeStr = "[BlueprintRule_Method(Ignore = true)]";
            isBlueprintRule = ClassNTBlueprintMethodRule_Methods.Method_Attributes(attributeStr, out name, out ignore, out ShortcutClassName, out ShortcutMethodName);
            Assert.Equal(true, isBlueprintRule);
            Assert.Equal(true, ignore);
            #endregion

            #region Test2: [BlueprintRule_Method(ShortcutClassName = "ShortClassName", ShortcutMethodName = "ShortMethodName")]
            // ===========================================
            attributeStr = "[BlueprintRule_Method(ShortcutClassName = \"ShortClassName\", ShortcutMethodName = \"ShortMethodName\")]";
            isBlueprintRule = ClassNTBlueprintMethodRule_Methods.Method_Attributes(attributeStr, out name, out ignore, out ShortcutClassName, out ShortcutMethodName);
            Assert.Equal(true, isBlueprintRule);
            Assert.Equal(false, ignore);
            Assert.Equal("ShortClassName", ShortcutClassName);
            Assert.Equal("ShortMethodName", ShortcutMethodName);
            #endregion

            #region Test3: [Fact]
            //      ===========================================
            attributeStr = "[Fact]";
            isBlueprintRule = ClassNTBlueprintMethodRule_Methods.Method_Attributes(attributeStr, out name, out ignore, out ShortcutClassName, out ShortcutMethodName);
            Assert.Equal(false, isBlueprintRule);
            #endregion
        }

        [Fact]
        [Test_Method("()")]
        [BlueprintRule_MethodAliasDef(MirrorClass = typeof(ClassNTAttribute_Test), MirrorMethodName = "mirrorMethod", MirrorParameter1 = "parName")]
        public void BlueprintRule_MethodAliasDef_Test()
        {
            string attributeStr;
            string name;  //, ShortcutClassName, ShortcutMethodName;
            string MirrorParameter1, MirrorClass, MirrorMethodName;

            #region [BlueprintRule_MethodAliasDef(MirrorClass = "mirrorClass", MirrorMethodName = "mirrorMethod", MirrorParameter1 = "parName")]
            attributeStr = "[BlueprintRule_MethodAliasDef(MirrorClass = \"mirrorClass\", MirrorMethodName = \"mirrorMethod\", MirrorParameter1 = \"parName\")]";
            var result = ClassNTBlueprintMethodRuleAliasDef_Methods.Attribute_AliasDefinition(attributeStr, out name, out MirrorParameter1, out MirrorClass, out MirrorMethodName);
            Assert.Equal(true, result);
            Assert.Equal("BlueprintRule_MethodAliasDef", name);
            Assert.Equal("mirrorClass", MirrorClass);
            Assert.Equal("mirrorMethod", MirrorMethodName);
            Assert.Equal("parName", MirrorParameter1);

            List<string> source = MethodNTHeader_Methods.Str2StrList(attributeStr);
            var attrDef = ClassNTBlueprintMethodRuleAliasDef_.Create(source);
            Assert.Equal("mirrorMethod", attrDef.MirrorMethodName);
            #endregion

            #region [Fact]
            attributeStr = "[Fact]";
            result = ClassNTBlueprintMethodRuleAliasDef_Methods.Attribute_AliasDefinition(attributeStr, out name, out MirrorParameter1, out MirrorClass, out MirrorMethodName);
            Assert.Equal(false, result);
            #endregion

        }

        [Fact]
        public void ClassNTBlueprintRule_Methods_Test1()
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

            #region Test0: [Test]
            // =========================================================================================================================================
            attributeCode1 = "[Test]";
            isBlueprintRule = ClassNTBlueprintRule_Methods.BlueprintRule_Attributes(attributeCode1, out name, out parameters, out classNetworkType, out ignore1, out ignore2, out ignore3, out ignore4);
            Assert.Equal(false, isBlueprintRule);
            Assert.Equal(enBlueprintClassNetworkType.Undefined, classNetworkType);
            Assert.Equal(0, parameters.Count);
            Assert.Equal(null, ignore1);
            Assert.Equal(null, ignore2);
            Assert.Equal(null, ignore3);
            Assert.Equal(null, ignore4);
            #endregion
        }


        [Fact]
        [Test_Method("ClassNTBlueprintRule()")]
        public void ClassNTBlueprintRule_Test1()
        {
            List<string> attributes;

            #region Test1: [Fact]
            //      ===========================================
            attributes = new List<string>
            {
                "[Fact]"
            };
            var rule1 = ClassNTBlueprintRule_.Create(attributes);
            Assert.Equal(null, rule1);
            #endregion

            #region Test2: [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]
            //      ===========================================
            attributes = new List<string>
            {
                "[Fact]",
                "[BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]",
                "[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]"
            };
            var rule2 = ClassNTBlueprintRule_.Create(attributes);
            Assert.Equal(enBlueprintClassNetworkType.Node_Action, rule2.ClassType);
            #endregion


        }
    }
}
