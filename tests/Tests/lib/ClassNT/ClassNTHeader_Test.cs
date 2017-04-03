using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.lib;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTHeader;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTStats;
using LamedalCore.zPublicClass;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    public sealed class ClassNTHeader_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        public ClassNTHeader_Test(ITestOutputHelper debug = null) : base(debug) { }

        [Fact]
        [Test_Method("Parse_ClassHeader()")]
        public static void Parse_ClassHeader_Test()
        {
            int ii = 0;
            ClassNTStats_ stats;
            List<string> source;
            List<string> Using, attributes, commentLines;
            string nameSpace, comment;
            bool isClass;

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
                "    [BlueprintRule_(enClassNetwork.Node_Action)]",
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
                "}",
                ""
            };
            stats = new ClassNTStats_();
            isClass = ClassNTHeader_Methods.Parse_ClassHeader(source, out ii, stats, out Using, out attributes, out nameSpace, out comment, out commentLines);
            Assert.True(isClass);
            Assert.Equal("System", Using[0]);
            Assert.Equal("Blueprint.lib.Rules.Types", nameSpace);
            Assert.Equal("Money convertions", comment);
            Assert.Equal("<summary>Money convertions</summary>", commentLines[0]);
            Assert.Equal(1, commentLines.Count);
            // Attributes ==========================
            Assert.Equal(2, attributes.Count);
            Assert.Equal("[BlueprintRule_(enClassNetwork.Node_Action)]", attributes[0]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]", attributes[1]);
            // Statistics ===================
            Assert.Equal(10, stats.ClassTotalLines);
            Assert.Equal(3, stats.ClassTotalCommentLines);
            Assert.Equal(5, stats.ClassTotalCodeLines);
            Assert.Equal(1, stats.ClassTotalBlankLines);
            Assert.Equal(2, stats.TotalAttributes);
            // ===========================
            Assert.Equal(9, ii);
            #endregion

            #region Test2: enum test
            //      ===========================================
            source = new List<string>
            {
                "namespace Access2System.domain.Enumerals",
                "{",
                "    public enum enTodoTime",
                "    {",
                "        [Description = \"Test\"]",
                "        Hours,",
                "        Days,",
                "        Weeks",
                "    }",
                "}"
            };
            stats = new ClassNTStats_();
            isClass = ClassNTHeader_Methods.Parse_ClassHeader(source, out ii, stats, out Using, out attributes, out nameSpace, out comment, out commentLines);
            Assert.False(isClass);
            Assert.Equal(source.Count, ii);
            Assert.Equal(10, stats.ClassTotalLines);
            Assert.Equal(10, stats.ClassTotalCodeLines);
            Assert.Equal(0, stats.ClassTotalCommentLines);
            Assert.Equal(1, stats.TotalAttributes);
            Assert.Equal(1, stats.TotalEnumerals);
            #endregion

            #region Test3: delegate test
            //      ===========================================
            source = new List<string>
            {
                "using System;",
                "using System.Data;",
                "",
                "namespace Blueprint.domain.Events",
                "{",
                "    /// <summary>",
                "    /// This event should fire after the submit to DB has failed.  The idea is to take the user on his word and just commit the data.  If the DB ",
                "    /// constraints do not allow the data to be entered, we look at reasons why.",
                "    /// </summary>",
                "    /// <param name=\"sender\">The sender will be the grid (Infragistics or DevX) that the data tabel is linked to</param>",
                "    /// <param name=\"table\">The data table trying to submit the data</param>",
                "    /// <param name=\"ex\">The exception thrown during the update</param>",
                "    /// <param name=\"reason\">Str containing the reasons why the Commit failed</param>",
                "    /// <returns></returns>",
                "    public delegate bool evDataTable_Commit_PostError(object sender, DataTable table, Exception ex, out string reason);",
                "}",
                ""
            };

            stats = new ClassNTStats_();
            isClass = ClassNTHeader_Methods.Parse_ClassHeader(source, out ii, stats, out Using, out attributes, out nameSpace, out comment, out commentLines);
            Assert.False(isClass);
            Assert.Equal(source.Count, ii);
            Assert.Equal(2, stats.ClassTotalBlankLines);
            Assert.Equal(9, stats.ClassTotalCommentLines);
            Assert.Equal(source.Count, stats.ClassTotalLines);
            Assert.Equal(5, stats.ClassTotalCodeLines);
            #endregion

            #region Test4: interface test
            //      ===========================================
            source = new List<string>
            {
                "using Blueprint.parts.AI.StateEngine;",
                "",
                "namespace Blueprint.domain.Interfaces",
                "{",
                "    public interface IStateEngineTransition",
                "    {",
                "",
                "        /// <summary>",
                "        /// Transition to the next state.",
                "        /// </summary>",
                "        /// <returns>state</returns>",
                "        AI_StateEngine_ Transition_Next(AI_StateEngine_ state = null, bool moveToNextState = true);",
                "",
                "        /// <summary>",
                "        /// Transition to the previous state.",
                "        /// </summary>",
                "        /// <returns>state</returns>",
                "        AI_StateEngine_ Transition_Previous(bool moveToNextState = true);",
                "    }",
                "}"
            };
            stats = new ClassNTStats_();
            isClass = ClassNTHeader_Methods.Parse_ClassHeader(source, out ii, stats, out Using, out attributes, out nameSpace, out comment, out commentLines);
            Assert.False(isClass);
            Assert.Equal(source.Count, ii);
            #endregion
        }

        [Fact]
        [Test_Method("Parse_ClassDefinition()")]
        public static void Parse_ClassDefinition_Test()
        {
            #region Test1: public sealed class String_ : Blueprint_CodeInjection    // namespace Blueprint.Rules.Types.String
            //      ===========================================
            string line = "public sealed class String_SubStr : Blueprint_CodeInjection";
            string nameSpace = "namespace Blueprint.Rules.Types.String";
            string classKind, classScope, className, classBase, classnameGroup, classNameShortVersion;
            ClassNTHeader_Methods.Parse_ClassDefinition(line, nameSpace, out classKind, out classScope, out className, out classBase, out classnameGroup, out classNameShortVersion);
            Assert.Equal("sealed", classKind);
            Assert.Equal("public", classScope);
            Assert.Equal("String_SubStr", className);
            Assert.Equal("Blueprint_CodeInjection", classBase);
            Assert.Equal("String", classnameGroup);
            Assert.Equal("SubStr", classNameShortVersion);
            #endregion
        }

        [Fact]
        [Test_Method("ClassNTHeader()")]
        public void ClassNTHeader_Test2()
        {
            int ii = 0;
            var stats = new ClassNTStats_();
            ClassNTHeader_ header;
            List<string> source;

            #region Test1: enum test
            //      ===========================================
            source = new List<string>
            {
                "namespace Access2System.domain.Enumerals",
                "{",
                "    public enum enTodoTime",
                "    {",
                "        Hours,",
                "        Days,",
                "        Weeks",
                "    }",
                "}"
            };
            ii = 0;
            header = ClassNTHeader_.Create(source, out ii, stats);
            Assert.Equal(null, header);
            #endregion

            #region Test2: public sealed class Types_Money
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
                "    [BlueprintRule_(enClassNetwork.Node_Action)]",
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
                "}",
                ""
            };
            header = ClassNTHeader_.Create(source, out ii, stats);
            Assert.Equal("System", header.NameSpace_UsingLines[0]);
            Assert.Equal("Blueprint.lib.Rules.Types", header.NameSpace_Name);
            Assert.Equal("Money convertions", header.Header_Comment);
            Assert.Equal("[BlueprintRule_(enClassNetwork.Node_Action)]", header.NameSpace_AttributeLines[0]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]", header.NameSpace_AttributeLines[1]);
            Assert.Equal("Types_Money", header.ClassName);
            Assert.Equal("Types", header.ClassName1);
            Assert.Equal("Money", header.ClassName2);
            #endregion

        }


    }
}
