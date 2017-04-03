using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.SolutionNT.ClassNT;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttribute.ClassNTBlueprintRule;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTHeader;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTStats;
using LamedalCore.zPublicClass;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]
    public sealed class ClassNT_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        public ClassNT_Test(ITestOutputHelper debug = null) : base(debug) {}

        [Fact]
        [Test_Method("ClassNT_.Create()")]
        public void ClassNT_ReadWrite_Test()
        {
            List<string> source;
            ClassNTBlueprintRule_ blueprintRule;

            #region Test1: public sealed class Types_Money

            //      ===========================================
            source = new List<string>
            {
                "using System;",
                "",
                "namespace Blueprint.lib.Rules.Types",
                "{",
                "    /// <summary>",
                "    /// Money convertions",
                "    /// </summary>",
                "    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]",
                "    public sealed class Types_Money",
                "    {",
                "        /// <summary>",
                "        ///     A Double extension method that converts the @this to a money.",
                "        /// </summary>",
                "        /// <param name=\"this\">The @this to act on.</param>",
                "        /// <returns>@this as a Double.</returns>",
                "        public Double ToMoney(Double @this)",
                "        {",
                "            return Math.Round(@this, 2);",
                "        }",
                "    }",
                "}",
                ""
            };
            bool error;
            var Class = ClassNT_.Create(source, out error, out blueprintRule);
            Assert.Equal(1, Class.Header.NameSpace_UsingLines.Count);
            Assert.Equal("Blueprint.lib.Rules.Types", Class.Header.NameSpace_Name);
            Assert.Equal("Money convertions", Class.Header.Header_Comment);
            Assert.Equal(1, Class.Header.Namespace_Attributes.Items.Count);
            Assert.Equal(enBlueprintClassNetworkType.Node_Action, blueprintRule.ClassType);

            #endregion

            #region Test2: Write & Read the class object

            var folderPath = Config_Info.Config_File_Test(_Debug) + @"Text/ClassNT/"; //@"C:\test\stream\header.txt";
            _lamed.lib.IO.Folder.Create(folderPath);
            var file = folderPath + "classTest.txt";

            // Writing
            string json1 = _lamed.lib.Test.Object_2JsonStr(Class);
            1f.zIO().RW.File_Write(file, json1, true);

            // Reading
            string json2 = 1f.zIO().RW.File_Read2Str(file);
            var Class2 = _lamed.lib.Test.Object_FromJsonStr<ClassNT_>(json2);

            // Testing
            Assert.Equal(json1, json2);
            Assert.Equal(1, Class2.Header.NameSpace_UsingLines.Count);
            Assert.Equal("Blueprint.lib.Rules.Types", Class2.Header.NameSpace_Name);
            Assert.Equal("Money convertions", Class2.Header.Header_Comment);
            Assert.Equal(1, Class2.Header.Namespace_Attributes.Items.Count);

            #endregion

           

        }

        [Fact]
        [Test_Method("ClassNTStats_.Create()")]
        [Test_Method("ClassNTHeader_.Create()")]
        public void ClassHeaderReadWrite_Test()
        {
            #region Test1: public sealed class Types_Money

            //      ===========================================
            var source = new List<string>
            {
                "using System;",
                "",
                "namespace Blueprint.lib.Rules.Types",
                "{",
                "    /// <summary>",
                "    /// Money convertions",
                "    /// </summary>",
                "    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]",
                "    [BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]",
                "    public sealed class Types_Money",
                "    {",
                "        /// <summary>",
                "        ///     A Double extension method that converts the @this to a money.",
                "        /// </summary>",
                "        /// <param name=\"this\">The @this to act on.</param>",
                "        /// <returns>@this as a Double.</returns>",
                "        public Double ToMoney(Double @this)",
                "        {",
                "            return Math.Round(@this, 2);",
                "        }",
                "    }",
                "}"
            };

            // Write the lines
            var folderPath = Config_Info.Config_File_Test(_Debug) + @"Text/ClassNT/"; //@"C:\test\stream\header.txt";
            _lamed.lib.IO.Folder.Create(folderPath);
            var file = folderPath + "Types_Money.txt";
            _lamed.lib.IO.RW.File_Write(file, source.ToArray(), true);

            // Reading the lines back
            var sourceRead = _lamed.lib.IO.RW.File_Read2StrArray(file).ToList();
            Assert.Equal(source, sourceRead);

            #endregion

            #region Test3: Create header class 

            int ii;
            ClassNTStats_ stats = ClassNTStats_.Create();
            var header1 = ClassNTHeader_.Create(source, out ii, stats);
            Assert.Equal("System", header1.NameSpace_UsingLines[0]);
            Assert.Equal("Blueprint.lib.Rules.Types", header1.NameSpace_Name);
            Assert.Equal("Money convertions", header1.Header_Comment);
            Assert.Equal("[BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]", header1.NameSpace_AttributeLines[0]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]",
                header1.NameSpace_AttributeLines[1]);
            Assert.Equal("Types_Money", header1.ClassName);
            Assert.Equal("Types", header1.ClassName1);
            Assert.Equal("Money", header1.ClassName2);

            #endregion

            #region Test4: Write, Read and re-Create the header class 
            file = folderPath + "ClassNTHeader.txt";

            string json1 = _lamed.lib.Test.Object_2JsonStr(header1);
            1f.zIO().RW.File_Write(file, json1, overwrite: true);

            // Read the object and test it
            string json2 = 1f.zIO().RW.File_Read2Str(file);
            var header2 = _lamed.lib.Test.Object_FromJsonStr<ClassNTHeader_>(json2);
            // Testing
            Assert.Equal(json1, json2);
            Assert.Equal("System", header2.NameSpace_UsingLines[0]);
            Assert.Equal("Blueprint.lib.Rules.Types", header2.NameSpace_Name);
            Assert.Equal("Money convertions", header2.Header_Comment);
            Assert.Equal("[BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]", header2.NameSpace_AttributeLines[0]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]",
                header2.NameSpace_AttributeLines[1]);
            Assert.Equal("Types_Money", header2.ClassName);
            Assert.Equal("Types", header2.ClassName1);
            Assert.Equal("Money", header2.ClassName2);

            #endregion

        }

        [Fact]
        [Test_Method("ClassNT_.Create()")]
        [BlueprintRule_Method(Ignore = true)]
        public void ClassWriteRead_Test()
        {
            #region Create the test data
            //      ===========================================
            var source = new List<string>
            {
                "using System;",
                "",
                "namespace Blueprint.lib.Rules.Types",
                "{",
                "    /// <summary>",
                "    /// Money convertions",
                "    /// </summary>",
                "    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]",
                "    [BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]",
                "    public sealed class Types_Money",
                "    {",
                "        public string Method1()",
                "        {",
                "            return \"Test\";",
                "        }",
                "        ",
                "        /// <summary>",
                "        ///     A Double extension method that converts the @this to a money.",
                "        /// </summary>",
                "        /// <param name=\"this\">The @this to act on.</param>",
                "        /// <returns>@this as a Double.</returns>",
                "        public Double ToMoney(Double @this)",
                "        {",
                "            return Math.Round(@this, 2);",
                "        }",
                "    }",
                "}"
            };

            // Write the lines
            var folderPath = Config_Info.Config_File_Test(_Debug, @"Text/cSharp/"); //@"C:\test\stream\header.txt";
            var file = folderPath + "Types_Money.cs";
            _lamed.lib.IO.RW.File_Write(file, source.ToArray(), true);


            #endregion

            bool error;
            ClassNTBlueprintRule_ methodRule;
            var Class1 = ClassNT_.Create(source, out error, out methodRule, file);
            var Class2 = ClassNT_.Create(file, out error, out methodRule);
            MethodNT_ method = Class1.Method_Find("public Double ToMoney(Double @this)");

            string errorMsg;
            Assert.True(_lamed.Types.Object.IsEqual(Class1, Class2, out errorMsg),errorMsg);
            //Assert.Equal(Class1, Class2);
            Assert.NotNull(method);
            Assert.Equal("public Double ToMoney(Double @this)", method.Header.Method_HeaderLine);

            // Test exceptions
            var method1 = Class1.Method_Find("public Double ToMoney(Double @this)", true);
            var method2 = Class1.Method_Find("public Double ToMoney(Double @this)");
            Assert.Equal(method, method1);
            Assert.Equal(null, method2);
        }

        [Fact]
        [Test_Method("ClassNT_.Create()")]
        public void String_Test()
        {
            #region String_.cs
            var folderPath = Config_Info.Config_File_Test(_Debug, @"Text/cSharp/");
            bool error;
            ClassNTBlueprintRule_ blueprintRule;
            var Class = ClassNT_.Create(folderPath + "String_.cs", out error, out blueprintRule);
            Assert.Equal(enBlueprintClassNetworkType.Node_Link, Class.BlueprintRule.ClassType);
            Assert.Equal(7, Class.Properties.Count);
            Assert.Equal(0, Class.Methods.Count);
            Assert.Equal("String_", Class.ClassName);
            Assert.Equal("String_", Class.Header.ClassName);
            Assert.Equal("LamedalCore.Types.String", Class.Header.NameSpace_Name);
            Assert.Equal(115, Class.Statistics.ClassTotalLines);
            Assert.Equal(1, Class.Statistics.ClassTotalBlankLines);
            Assert.Equal(12, Class.Statistics.ClassTotalCodeLines);
            Assert.Equal(3, Class.Statistics.ClassTotalCommentLines);
            Assert.Equal(0, Class.Statistics.TotalMethods);
            Assert.Equal(0, Class.Statistics.TotalFields);
            Assert.Equal(0, Class.Statistics.CodeComplexity);
            Assert.Equal(0, Class.Statistics.CodeMaintainability);
            Assert.Equal(0, Class.Statistics.TotalProperties);
            #endregion

            #region InvalidFile.cs
            Assert.Throws<InvalidOperationException>(() => ClassNT_.Create(folderPath + "InvalidFile.cs", out error, out blueprintRule));
            #endregion
        }

        [Fact]
        [Test_Method("ClassNT_.Create()")]
        public void String_Edit_Test()
        {
            var folderPath = Config_Info.Config_File_Test(_Debug, @"Text/cSharp/");
            bool error;
            ClassNTBlueprintRule_ blueprintRule;
            var Class = ClassNT_.Create(folderPath + "String_Edit.cs", out error, out blueprintRule);
            Assert.Equal(0, Class.Properties.Count);
            Assert.Equal(13, Class.Methods.Count);
            Assert.Equal("String_Edit", Class.ClassName);
            Assert.Equal("String_Edit", Class.Header.ClassName);
            Assert.Equal("String", Class.Header.ClassName1);
            Assert.Equal("Edit", Class.Header.ClassName2);
            Assert.Equal("LamedalCore.Types.String", Class.Header.NameSpace_Name);
            Assert.Equal(218, Class.Statistics.ClassTotalLines);
            Assert.Equal(1, Class.Statistics.ClassTotalBlankLines);
            Assert.Equal(12, Class.Statistics.ClassTotalCodeLines);
            Assert.Equal(49, Class.Statistics.ClassTotalCommentLines);
            Assert.Equal(13, Class.Statistics.TotalMethods);
            Assert.Equal(0, Class.Statistics.TotalFields);
            Assert.Equal(32, Class.Statistics.CodeComplexity);
            Assert.Equal(17, Class.Statistics.CodeMaintainability);
            Assert.Equal(0, Class.Statistics.TotalProperties);
        }
    }
}
