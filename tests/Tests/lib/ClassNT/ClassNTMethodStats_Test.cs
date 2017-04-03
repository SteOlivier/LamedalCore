using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTHeader;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTstats;
using Xunit;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    public sealed class ClassNTMethodStats_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Code_Simplify()")]
        public void MethodSimplify_Test()
        {
            #region TesCase
            var source = new List<string>
            {
                "{",
                "  // Remove this comment",
                "  var projectPath = bp.lib.Rules.Types.String.Word_LastWord_Remove(projectPath, \" \");",
                "  if (chars > inputStr.Length) return \"\";",
                "  int position = inputStr.Length - chars;",
                "  return inputStr.Substring(position, chars);",
                "  /*",
                "   Remove this also",
                "   This too",
                "  */",
                "}"
            };

            var bodyResult =
@"{
 var projectPath = bp.lib.Rules.Types.String.Word_LastWord_Remove(projectPath, );
 if (chars > inputStr.Length) return ;
 int position = inputStr.Length - chars;
 return inputStr.Substring(position, chars);
}";
            #endregion

            var body = MethodNTstats_Methods.Code_Simplify(source);
            Assert.Equal(bodyResult, body);

            Assert.Equal(2, MethodNTstats_Methods.Method_Complexity(body));
            Assert.Equal(1, MethodNTstats_Methods.Method_Maintainability(body,0));
            Assert.Equal(0, MethodNTstats_Methods.Method_Maintainability(body,1));
            Assert.Equal(0, MethodNTstats_Methods.Method_Maintainability(body,2));
        }


        [Fact]
        [Test_Method("Method_CalculateStats()")]
        public static void MethodBody_Parse_Test()
        {
            var source = new List<string>();
            //int ii = 0, iiEnd = 0
            int complexity, maintainability;
            List<string> ReferenceCalls;

            #region Test1:
            //      ===========================================
            source = new List<string>
            {
                "{",
                "  var projectPath = _lamed.lib.Rules.Types.String.Word_LastWord_Remove(projectPath, \" \");",
                "  if (chars > inputStr.Length) return \"\";",
                "  int position = inputStr.Length - chars;",
                "  return inputStr.Substring(position, chars);",
                "}"
            };
            MethodNTstats_Methods.Method_Stats(source, out complexity, out maintainability, out ReferenceCalls);
            Assert.Equal(1, ReferenceCalls.Count);
            Assert.Equal("lib.Rules.Types.String.Word_LastWord_Remove", ReferenceCalls[0]);
            Assert.Equal(2, complexity);
            Assert.Equal(0, maintainability);
            #endregion

            #region Test2:
            // ===========================================

            #endregion

        }

        /// <summary>
        /// Tests parser  of code line for method reference call.
        /// </summary>
        [Fact]
        [Test_Method("CodeLine_ReferenceCall()")]
        public static void MethodReferenceCall_Tests()
        {
            #region Test1: false.zWaitCursor();
            //      ================================================ 
            var value = MethodNTstats_Methods.CodeLine_ReferenceCall("false.zWaitCursor();");
            Assert.Equal(value, "zWaitCursor");
            #endregion


            #region Test2: if (dte.zClass_Info(out _Project, out projectItem, out Class, out function) == false) return;
            //      ===================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("if (dte.zClass_Info(out _Project, out projectItem, out Class, out function) == false) return;");
            Assert.Equal(value, "zClass_Info");
            #endregion


            #region Test3: if (dte.zCTI_Solution_Info(out solution, out projectItems2, out _Project, out prjFolder) == false) return;
            //      =================================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("if (dte.zCTI_Solution_Info(out solution, out projectItems2, out _Project, out prjFolder) == false) return;");
            Assert.Equal(value, "zCTI_Solution_Info");
            #endregion


            #region Test4: _form.input_Project.Field_Value = _Project.FullName;
            //      ============================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("_form.input_Project.Field_Value = _Project.FullName;");
            Assert.Equal(value, "");
            #endregion


            #region Test5: bpTools.lib.system.DTE.solution.Namespace.From_ClassAsStr(Class);
            //      =======================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("bpTools.lib.system.DTE.solution.Namespace.From_ClassAsStr(Class);");
            Assert.Equal(value, "lib.system.DTE.solution.Namespace.From_ClassAsStr");
            #endregion


            #region Test6: _projectfolder.Replace(@'\', '.');
            //      ==========================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("_projectfolder.Replace(@'\', '.');");
            Assert.Equal(value, "");
            #endregion


            #region Test7: projectPath = bp.lib.Rules.Types.String.Word_LastWord_Remove(projectPath, projectNameSpace);
            //      ====================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("projectPath = bp.lib.Rules.Types.String.Word_LastWord_Remove(projectPath, projectNameSpace);");
            Assert.Equal("lib.Rules.Types.String.Word_LastWord_Remove", value);
            #endregion


            #region Test8: ProjectItem[] projectItems = bpTools.lib.system.DTE.solution.project.ProjectItems.From_Project_Active(dte, false, false, false); // Return classes in the same namespace
            //      =========================================================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("ProjectItem[] projectItems = bpTools.lib.system.DTE.solution.project.ProjectItems.From_Project_Active(dte, false, false, false); // Return classes in the same namespace");
            Assert.Equal("lib.system.DTE.solution.project.ProjectItems.From_Project_Active", value);
            #endregion


            #region Test9: var projectItemsState = bpTools.lib.system.DTE.solution.project.ProjectItems.To_ProjectItem_State(projectItems);
            //      =======================================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("var projectItemsState = bpTools.lib.system.DTE.solution.project.ProjectItems.To_ProjectItem_State(projectItems);");
            Assert.Equal("lib.system.DTE.solution.project.ProjectItems.To_ProjectItem_State", value);
            #endregion


            #region Test10: _classNameList = projectItemsState.Select(x => x.Path.Replace(@'\', '.').Replace(projectPath, '')).ToList();
            //      ====================================================================================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("_classNameList = projectItemsState.Select(x => x.Path.Replace(@'\', '.').Replace(projectPath, '')).ToList();");
            Assert.Equal("", value);
            #endregion


            #region Test11: _classNameList.zTo_IList(_form.listBox_Classes.Items);
            //      ==============================================================
            value = MethodNTstats_Methods.CodeLine_ReferenceCall("_classNameList.zTo_IList(_form.listBox_Classes.Items);");
            Assert.Equal("zTo_IList", value);
            #endregion
        }

        [Fact]
        [Test_Method("MethodNTstats_()")]
        [BlueprintRule_Method(Ignore = true)]
        public static void MethodNTstats_Test()
        {
            int ii;
            #region Test1:
            //      ===========================================
            var source = new List<string>
            {
                "/// <summary>",
                "/// Return the specified chars from the right of the input string.",
                "/// </summary>",
                "/// <param name=\"inputStr\">The input string.</param>",
                "/// <param name=\"chars\">The chars.</param>",
                "/// <returns>string</returns>",
                "[BlueprintRule_Method(Ignore = true)]",
                "public string SubStr_Right(string inputStr, int chars)",
                "{",
                "  if (chars > inputStr.Length) return \"\";",
                "  int position = inputStr.Length - chars;",
                "  return inputStr.Substring(position, chars);",
                "}"
            };
            ii = 0;        
            var method = MethodNT_.Create(source, ref ii, 12);
            Assert.Equal(true, method.Attribute_Rule.Ignore);
            Assert.Equal(13, method.Statistics.MethodTotalLines);
            Assert.Equal(5, method.Statistics.MethodTotalBodyLines);
            Assert.Equal(2, method.Statistics.CodeComplexity);
            Assert.Equal(1, method.Statistics.CodeMaintainability);
            #endregion

            #region Test2:
            // ===========================================

            #endregion

        }

    }
}
