using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttribute.ClassNTBlueprintRule;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTAttributes;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTComment;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTComment.MethodNTComment_Parameter;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTHeader;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTHeader.MethodNTHeader_Parameter;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTBody.MethodNT.MethodNTstats;
using LamedalCore.lib.SolutionNT.ClassNT.ClassNTStats;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.ClassNT
{
    public sealed class ClassNTMethod_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Parameter_FromXML()")]
        public static void Parameter_FromXML_Test()
        {
            // Test1: <param name="convertToValidXML">Convert to valid XML indicator. Default value = false.</param>
            string paramLine = "<param name=\"convertToValidXML\">Convert to valid XML indicator. Default value = false.</param>";
            string nameValue, commentValue;
            MethodNTComment_Parameter_Methods.Parameter_FromXML(paramLine, out nameValue, out commentValue);
            Assert.Equal("convertToValidXML", nameValue);
            Assert.Equal("Convert to valid XML indicator. Default value = false.", commentValue);
        }

        [Fact]
        [Test_Method("Parameter_ToXML()")]
        public static void Parameter_ToXML_Test()
        {
            string parameterXML = MethodNTComment_Parameter_Methods.Parameter_ToXML("convertToValidXML", "Convert to valid XML indicator. Default value = false.");
            Assert.Equal("        <param name=\"convertToValidXML\">Convert to valid XML indicator. Default value = false.</param>".NL(), parameterXML);
        }

        [Fact]
        [Test_Method("MethodNTComment_()")]
        public void MethodNTComment_Test1()
        {
            List<string> Attribute_Lines;
            var source = new List<string>();
            source.Add("        /// <summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>");
            source.Add("        /// <param name=\"inputStr\">The input string.</param>");
            source.Add("        /// <param name=\"searchValue\">The search value.</param>");
            source.Add("        /// <returns>string</returns>");
            source.Add("        [Test]");
            source.Add("        public int ContainsIndex(string inputStr, string searchValue)");
            source.Add("       {");
            source.Add("            int found = -1;");
            source.Add("            int termIndex = 0;");
            source.Add("        }");
            var ii = 0;
            var comment = MethodNTComment_.Create(source, ref ii, out Attribute_Lines);
            Assert.Equal("Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()", comment.CommentSummary);
            Assert.Equal("inputStr", comment.CommentParameters[0].ParameterName);
            Assert.Equal("The input string.", comment.CommentParameters[0].ParameterComment);
            Assert.Equal(2, comment.CommentParameters.Count);
            Assert.Equal("string", comment.CommentReturn);
            Assert.Equal("[Test]", Attribute_Lines[0]);

            #region XML Test
            // =================================================================
            var XML = comment.ToXML(false);
            var resultXML =
@"        <summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>
        <param name=""inputStr"">The input string.</param>
        <param name=""searchValue"">The search value.</param>";
            Assert.Equal(resultXML, XML);

            var resultXML2 =
@"        /// <summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>
        /// <param name=""inputStr"">The input string.</param>
        /// <param name=""searchValue"">The search value.</param>";
            var XML2 = comment.ToXML(true);
            Assert.Equal(resultXML2, XML2);
            #endregion
        }

        [Fact]
        [Test_Method("Summary_ToXML()")]
        [Test_Method("Summary_FromXML()")]
        public static void Summary_FromXML_Test()
        {
            // Summary_ToXML
            Assert.Equal("        <summary>string</summary>".NL(), MethodNTComment_Methods.Summary_ToXML("string", true));
            Assert.Equal("        /// <summary>string</summary>".NL(), MethodNTComment_Methods.Summary_ToXML("string", true,true));
            Assert.Equal("        <summary></summary>".NL(), MethodNTComment_Methods.Summary_ToXML("", true));
            Assert.Equal("        /// <summary></summary>".NL(), MethodNTComment_Methods.Summary_ToXML("", true, true));

            // Summary_FromXML
            Assert.Equal("string", MethodNTComment_Methods.Summary_FromXML("<summary>string</summary>"));
            Assert.Equal("", MethodNTComment_Methods.Summary_FromXML("<summary></summary>"));
            Assert.Equal("", MethodNTComment_Methods.Summary_FromXML(""));

        }

        [Fact]
        [Test_Method("Return_FromXML()")]
        [Test_Method("Return_ToXML()")]
        public static void Return_FromXML_Test()
        {
            // Return_FromXML
            Assert.Equal("string", MethodNTComment_Methods.Return_FromXML("<returns>string</returns>"));
            Assert.Equal("", MethodNTComment_Methods.Return_FromXML(""));

            // Return_ToXML
            Assert.Equal("        <returns>string</returns>", MethodNTComment_Methods.Return_ToXML("string"));
            Assert.Equal("        <returns></returns>", MethodNTComment_Methods.Return_ToXML(""));
            Assert.Equal("        <returns>string</returns>", MethodNTComment_Methods.Return_ToXML("string", true));
        }

        [Fact]
        [Test_Method("Comment_Parts()")]
        public static void Comment_Parts_Test()
        {
            // Declarations =================================
            List<string> source;
            int ii;
            List<string> parameterLines, attributeLines;
            string returnLine, documentationSummary, ctiCodeLine;
            // ==============================================

            #region Test 1
            // =======================================
            source = new List<string>();
            source.Add("        /// <summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>");
            source.Add("        /// <param name=\"inputStr\">The input string.</param>");
            source.Add("        /// <param name=\"searchValue\">The search value.</param>");
            source.Add("        /// <returns>string</returns>");
            source.Add("        [Test]");
            source.Add("        public int ContainsIndex(string inputStr, string searchValue)");
            source.Add("       {");
            source.Add("            int found = -1;");
            source.Add("            int termIndex = 0;");
            source.Add("        }");
            ii = 0;
            MethodNTComment_Methods.Comment_Parts(source, ref ii, out documentationSummary, out parameterLines, out attributeLines, out returnLine, out ctiCodeLine);

            // Test the result that is expected
            Assert.Equal("Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()", documentationSummary);

            Assert.Equal("<param name=\"inputStr\">The input string.</param>", parameterLines[0]);
            Assert.Equal("<param name=\"searchValue\">The search value.</param>", parameterLines[1]);
            Assert.Equal(2, parameterLines.Count);

            Assert.Equal("string", returnLine);
            Assert.Equal("", ctiCodeLine);

            Assert.Equal("[Test]", attributeLines[0]);
            Assert.Equal(1, attributeLines.Count);
            #endregion

            #region Test 2
            // ===============================================
            source = new List<string>();
            source.Add("        /// <summary>");
            source.Add("        /// Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()");
            source.Add("        /// </summary>");
            source.Add("        /// <param name=\"inputStr\">The input string.</param>");
            source.Add("        /// <param name=\"searchValue\">The search value.</param>");
            source.Add("        /// <returns>");
            source.Add("        /// the ");
            source.Add("        /// string");
            source.Add("        /// </returns>");
            source.Add("        [Test]");
            source.Add("        [Pure]");
            source.Add("        public int ContainsIndex(string inputStr, string searchValue)");
            source.Add("       {");
            source.Add("            int found = -1;");
            source.Add("            int termIndex = 0;");
            source.Add("        }");
            ii = 0;
            MethodNTComment_Methods.Comment_Parts(source, ref ii, out documentationSummary, out parameterLines, out attributeLines, out returnLine, out ctiCodeLine);

            // Test the result that is expected
            Assert.Equal("Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()", documentationSummary);
            Assert.Equal("<param name=\"inputStr\">The input string.</param>", parameterLines[0]);
            Assert.Equal("<param name=\"searchValue\">The search value.</param>", parameterLines[1]);
            Assert.Equal(2, parameterLines.Count);

            Assert.Equal("the string", returnLine);

            Assert.Equal("[Test]", attributeLines[0]);
            Assert.Equal("[Pure]", attributeLines[1]);
            Assert.Equal(2, attributeLines.Count);

            #endregion
        }

        /// <summary>Comments parts test2.</summary>
        /// <code>
        /// Sample test code.
        /// </code>
        [Fact]
        [Test_Method("Comment_Parts()")]
        public static void Comment_Parts_Test2()
        {
            #region Test data
            var source = new List<string>
            {
                "        /// <summary>",
                "        /// Return the specified chars from the right of the input string.",
                "        /// </summary>",
                "        /// <param name=\"inputStr\">The input string.</param>",
                "        /// <param name=\"chars\">The chars.</param>",
                "        /// <returns>string</returns>",
                "        /// <code>",
                "        /// Sample test code.",
                "        /// </code>",
                "        [Pure]",
                "        [Category(\"Appearance\")]",
                "        [Description(\"Set Cancel Visible property\")]",
                "        [Test, TestedMethod(\"Key_Add()\"), TestedMethod(\"Key_Find()\"), TestedMethod(\"Key_Remove()\")]",
                "        public string SubStr_Right(string inputStr, int chars)",
                "        {",
                "            if (chars > inputStr.Length) return \"\";",
                "            int position = inputStr.Length - chars;",
                "            return inputStr.Substring(position, chars);",
                "        }"
            };
            #endregion

            int ii = 0;
            List<string> documentationLines, attributeLines;
            string returnLine, im_DocumentationSummary, ctiCodeLine;
            MethodNTComment_Methods.Comment_Parts(source, ref ii, out im_DocumentationSummary, out documentationLines, out attributeLines, out returnLine, out ctiCodeLine);

            // Test the results
            Assert.Equal("string", returnLine);
            Assert.Equal("Return the specified chars from the right of the input string.", im_DocumentationSummary);
            Assert.Equal("Sample test code.", ctiCodeLine);

            #region Attributes
            var attributeLinesResult = new List<string>
            {
                "[Pure]", "[Category(\"Appearance\")]", "[Description(\"Set Cancel Visible property\")]",
                "[Test]","[TestedMethod(\"Key_Add()\")]","[TestedMethod(\"Key_Find()\")]","[TestedMethod(\"Key_Remove()\")]"
            };
            Assert.Equal(attributeLinesResult, attributeLines);
            #endregion

            #region Parameters
            var documentationLinesResult = new List<string>
            {
                "<param name=\"inputStr\">The input string.</param>",
                "<param name=\"chars\">The chars.</param>"
            };
            Assert.Equal(documentationLinesResult, documentationLines);
            #endregion
        }

        [Fact]
        [Test_Method("Documentation_Parts()")]
        public static void Documentation_Parts_Test()
        {
            // Declarations =================================
            List<string> source;
            int ii;
            List<string> documentationLines, attributeLines;
            string returnLine, im_DocumentationSummary, ctiCodeLine;
            var stats = new ClassNTStats_();
            // ==============================================

            #region Test1: public int ContainsIndex(string inputStr, string searchValue)
            //      =========================================================
            source = new List<string>();
            source.Add("        /// <summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>");
            source.Add("        /// <param name=\"inputStr\">The input string.</param>");
            source.Add("        /// <param name=\"searchValue\">The search value.</param>");
            source.Add("        /// <returns>string</returns>");
            source.Add("        [Test]");
            source.Add("        [Pure]");
            source.Add("        public int ContainsIndex(string inputStr, string searchValue)");
            source.Add("       {");
            source.Add("            int found = -1;");
            source.Add("            int termIndex = 0;");
            source.Add("        }");
            ii = 0;
            MethodNTComment_Methods.Documentation_Parts(source, ref ii, stats, out documentationLines, out im_DocumentationSummary, out attributeLines, out returnLine, out ctiCodeLine);

            // Test the result that is expected
            Assert.Equal("<summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>", documentationLines[0]);
            Assert.Equal("<param name=\"inputStr\">The input string.</param>", documentationLines[1]);
            Assert.Equal("<param name=\"searchValue\">The search value.</param>", documentationLines[2]);
            Assert.Equal("<returns>string</returns>", documentationLines[3]);
            Assert.Equal(4, documentationLines.Count);

            Assert.Equal(2, attributeLines.Count);
            Assert.Equal("[Test]", attributeLines[0]);
            Assert.Equal("[Pure]", attributeLines[1]);
            Assert.Equal("<returns>string</returns>", returnLine);
            #endregion

            #region Test2: public int ContainsIndex(string inputStr, string searchValue)
            //      ===============================================
            source = new List<string>
            {
                "        /// <summary>",
                "        /// Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()",
                "        /// </summary>",
                "        /// <param name=\"inputStr\">The input string.</param>",
                "        /// <param name=\"searchValue\">The search value.</param>",
                "        /// <returns>",
                "        /// string",
                "        /// </returns>",
                "        [Test]",
                "        [Pure]",
                "        [BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]",
                "        public int ContainsIndex(string inputStr, string searchValue)",
                "        {",
                "            int found = -1;",
                "            int termIndex = 0;",
                "        }"
            };
            ii = 0;
            MethodNTComment_Methods.Documentation_Parts(source, ref ii, stats, out documentationLines, out im_DocumentationSummary, out attributeLines, out returnLine, out ctiCodeLine);

            // Test the result that is expected
            Assert.Equal("<summary>Determines whether the specified input string contains index. This is supposed to be faster than IndexOf()</summary>", documentationLines[0]);
            Assert.Equal("<param name=\"inputStr\">The input string.</param>", documentationLines[1]);
            Assert.Equal("<param name=\"searchValue\">The search value.</param>", documentationLines[2]);
            Assert.Equal("<returns>string</returns>", documentationLines[3]);
            Assert.Equal(4, documentationLines.Count);

            Assert.Equal(3, attributeLines.Count);
            Assert.Equal("[Test]", attributeLines[0]);
            Assert.Equal("[Pure]", attributeLines[1]);
            Assert.Equal("[BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]", attributeLines[2]);
            Assert.Equal("<returns>string</returns>", returnLine);
            #endregion

            #region Test3: public string SubStr_Right(string inputStr, int chars)
            //      =============================================================
            source = new List<string>();
            source.Add("        /// <summary>");
            source.Add("        /// Return the specified chars from the right of the input string.");
            source.Add("        /// </summary>");
            source.Add("        /// <param name=\"inputStr\">The input string.</param>");
            source.Add("        /// <param name=\"chars\">The chars.</param>");
            source.Add("        /// <returns>string</returns>");
            source.Add("        [Pure]");
            source.Add("        [Category(\"Appearance\")]");
            source.Add("        [Description(\"Set Cancel Visible property\")]");
            source.Add("        [Test, TestedMethod(\"Key_Add()\"), TestedMethod(\"Key_Find()\"), TestedMethod(\"Key_Remove()\")]");
            source.Add("        public string SubStr_Right(string inputStr, int chars)");
            source.Add("        {");
            source.Add("            if (chars > inputStr.Length) return \"\";");
            source.Add("            int position = inputStr.Length - chars;");
            source.Add("            return inputStr.Substring(position, chars);");
            source.Add("        }");
            ii = 0;
            MethodNTComment_Methods.Documentation_Parts(source, ref ii, stats, out documentationLines, out im_DocumentationSummary, out attributeLines, out returnLine, out ctiCodeLine);

            // Get the memorandum
            var documentationLines_Memo = new List<string>();
            documentationLines_Memo.Add("<summary>Return the specified chars from the right of the input string.</summary>");
            documentationLines_Memo.Add("<param name=\"inputStr\">The input string.</param>");
            documentationLines_Memo.Add("<param name=\"chars\">The chars.</param>");
            documentationLines_Memo.Add("<returns>string</returns>");
            Assert.Equal(documentationLines_Memo, documentationLines);

            var attributeLines_Memo = new List<string>();
            attributeLines_Memo.Add("[Pure]");
            attributeLines_Memo.Add("[Category(\"Appearance\")]");
            attributeLines_Memo.Add("[Description(\"Set Cancel Visible property\")]");
            attributeLines_Memo.Add("[Test]");
            attributeLines_Memo.Add("[TestedMethod(\"Key_Add()\")]");
            attributeLines_Memo.Add("[TestedMethod(\"Key_Find()\")]");
            attributeLines_Memo.Add("[TestedMethod(\"Key_Remove()\")]");
            Assert.Equal(attributeLines_Memo, attributeLines);

            Assert.Equal("<returns>string</returns>", returnLine);
            #endregion
        }

        [Fact]
        [Test_Method("Parameter_Parts()")]
        public static void Parameter_Parts_Test()
        {
            string paramLine;
            string typeName, name, optionalValue;
            enParameterRefType refType;
            bool isThis;

            #region Test1: List<string> sourceLines
            //      =======================================
            paramLine = "this List<string> sourceLines";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(true, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("List<string>", typeName);
            Assert.Equal("sourceLines", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test2: ref int ii
            //      ===========================================
            paramLine = "ref int ii";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.ByReference, refType);
            Assert.Equal("int", typeName);
            Assert.Equal("ii", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test3: List<string> sourceLines
            //      ===========================================
            paramLine = "List<string> sourceLines";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("List<string>", typeName);
            Assert.Equal("sourceLines", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test4: out string methodName
            //      ===========================================
            paramLine = "out string methodName";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.Output, refType);
            Assert.Equal("string", typeName);
            Assert.Equal("methodName", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test5: bool isGeneric = false
            //      ===========================================
            paramLine = "this bool isGeneric = false";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(true, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("bool", typeName);
            Assert.Equal("isGeneric", name);
            Assert.Equal("false", optionalValue);
            #endregion

            #region Test6: Dictionary<string, int> array
            //      ===========================================
            paramLine = "Dictionary<string, int> array";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("Dictionary<string, int>", typeName);
            Assert.Equal("array", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test7: Func<T, RT> func
            //      ===========================================
            paramLine = "Func<T, RT> func";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("Func<T, RT>", typeName);
            Assert.Equal("func", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test8: List<Train_Food> trainFoods
            //      ===========================================
            paramLine = "List<Train_Food> trainFoods";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(false, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("List<Train_Food>", typeName);
            Assert.Equal("trainFoods", name);
            Assert.Equal(null, optionalValue);
            #endregion

            #region Test9: IEnumerable<T> array
            //      ===========================================
            paramLine = "this IEnumerable<T> array";
            MethodNTHeader_Parameter_Methods.Parameter_Parts(ref paramLine, out isThis, out refType, out typeName, out name, out optionalValue);
            Assert.Equal(true, isThis);
            Assert.Equal(enParameterRefType.ByValue, refType);
            Assert.Equal("IEnumerable<T>", typeName);
            Assert.Equal("array", name);
            Assert.Equal(null, optionalValue);
            #endregion
        }

        [Fact]
        [Test_Method("Parameter_Parse2StrList()")]
        public void Parameter_Parse2StrList_Test()
        {
            #region Test1: List<string> sourceLines, ref int ii
            //      ================================================
            List<string> Lines = MethodNTHeader_Parameter_Methods.Parameter_Parse2StrList("List<string> sourceLines, ref int ii");
            Assert.Equal("List<string> sourceLines", Lines[0]);
            Assert.Equal("ref int ii", Lines[1]);

            #endregion


            #region Test2: List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, out string returnType, out DTE_MethodKind kind, bool isGeneric = false
            //      ==================================================================================================================================================
            Lines = MethodNTHeader_Parameter_Methods.Parameter_Parse2StrList("this List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, out string returnType, out DTE_MethodKind kind, bool isGeneric = false");
            Assert.Equal("this List<string> sourceLines", Lines[0]);
            Assert.Equal("ref int ii", Lines[1]);
            Assert.Equal("out string methodName", Lines[2]);
            Assert.Equal("out DTE_MethodScope scope", Lines[3]);
            Assert.Equal("out string returnType", Lines[4]);
            Assert.Equal("out DTE_MethodKind kind", Lines[5]);
            Assert.Equal("bool isGeneric = false", Lines[6]);

            #endregion
        }

        [Fact]
        [Test_Method("Parameters_Parse()")]
        public static void Parameters_Parse_Test()
        {
            #region Test1: public void Setup(List<string> sourceLines, ref int ii)
            //      ==============================================================
            var header = "public void Setup(List<string> sourceLines, ref int ii)";
            List<string> parametersLines;
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("List<string> sourceLines", parametersLines[0]);
            Assert.Equal("ref int ii", parametersLines[1]);
            Assert.Equal(2, parametersLines.Count);

            #endregion

            #region Test2: public static string Parse(List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, out string returnType, out DTE_MethodKind kind, bool isGeneric = false)
            // ======================================================================
            header = "public static string Parse(List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, out string returnType, out DTE_MethodKind kind, bool isGeneric = false)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("List<string> sourceLines", parametersLines[0]);
            Assert.Equal("ref int ii", parametersLines[1]);
            Assert.Equal("out string methodName", parametersLines[2]);
            Assert.Equal("out DTE_MethodScope scope", parametersLines[3]);
            Assert.Equal("out string returnType", parametersLines[4]);
            Assert.Equal("out DTE_MethodKind kind", parametersLines[5]);
            Assert.Equal("bool isGeneric = false", parametersLines[6]);
            Assert.Equal(7, parametersLines.Count);
            #endregion


            #region Test3: public static List<Rectangle> Shape_Move(List<Rectangle> shape, int x, int y, int recSize)
            // =================================================================================================
            header = "public static List<Rectangle> Shape_Move(List<Rectangle> shape, int x, int y, int recSize)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("List<Rectangle> shape", parametersLines[0]);
            Assert.Equal("int x", parametersLines[1]);
            Assert.Equal("int y", parametersLines[2]);
            Assert.Equal("int recSize", parametersLines[3]);
            Assert.Equal(4, parametersLines.Count);

            #endregion


            #region Test4: public static SortedList<int, Train_PathInfo> AI_FoodSort(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)
            // ================================================================================================================================
            header = "public static SortedList<int, Train_PathInfo> AI_FoodSort(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("Train_ train", parametersLines[0]);
            Assert.Equal("ref bool foodChange", parametersLines[1]);
            Assert.Equal("List<Train_Food> trainFoods", parametersLines[2]);
            Assert.Equal(3, parametersLines.Count);

            #endregion


            #region Test5: public static IEnumerable<RT> ForEach<T, RT>(IEnumerable<T> array, Func<T, RT> func)
            // ===========================================================================================
            header = "public static IEnumerable<RT> ForEach<T, RT>(IEnumerable<T> array, Func<T, RT> func)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("IEnumerable<T> array", parametersLines[0]);
            Assert.Equal("Func<T, RT> func", parametersLines[1]);
            Assert.Equal(2, parametersLines.Count);

            #endregion


            #region Test6: public void TestMethod(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)
            // =============================================================================================
            header = "public void TestMethod(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("Train_ train", parametersLines[0]);
            Assert.Equal("ref bool foodChange", parametersLines[1]);
            Assert.Equal("List<Train_Food> trainFoods", parametersLines[2]);
            Assert.Equal(3, parametersLines.Count);

            #endregion

            #region Test7: public void TestMethod(Dictionary<string, int> array, Func<T, RT> func)
            // ==============================================================================
            header = "public void TestMethod(Dictionary<string, int> array, Func<T, RT> func)";
            MethodNTHeader_Parameter_Methods.Parameters_Parse(header, out parametersLines);
            Assert.Equal("Dictionary<string, int> array", parametersLines[0]);
            Assert.Equal("Func<T, RT> func", parametersLines[1]);
            Assert.Equal(2, parametersLines.Count);
            #endregion
        }

        [Fact]
        [Test_Method("MethodNTHeader_Methods.Str2StrList()")]
        [Test_Method("MethodNTHeader_Methods.Parse()")]
        public static void Parse_Simple_Test()
        {
            List<string> source;
            int ii = 0;
            string methodName;
            enCode_Scope scope;
            string returnType;
            enMethod_Kind kind;
            enCode_Specialty specialty;

            #region Test1: public void Setup(List<string> sourceLines, ref int ii)
            //      ==============================================================
            source = MethodNTHeader_Methods.Str2StrList("public void Setup(List<string> sourceLines, ref int ii)");
            string header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(0, ii);
            Assert.Equal("Setup", methodName);
            Assert.Equal(enCode_Scope._public, scope);
            Assert.Equal("void", returnType);
            Assert.Equal(enMethod_Kind.IsVoid, kind);
            Assert.Equal(enCode_Specialty.IsNormal, specialty);
            Assert.Equal("public void Setup(List<string> sourceLines, ref int ii)", header);
            #endregion

            #region Test2: public static string Parse(List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, out string returnType, out DTE_MethodKind kind, bool isGeneric = false)
            //      ===================================================================
            ii = 0;
            source = MethodNTHeader_Methods.Str2StrList("public static string Parse(List<string> sourceLines, ref int ii, out string methodName, out DTE_MethodScope scope, ".NL() +
                                 "          out string returnType, out DTE_MethodKind kind, bool isGeneric = false)");
            header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(1, ii);
            Assert.Equal("Parse", methodName);
            Assert.Equal(enCode_Scope._public, scope);
            Assert.Equal("string", returnType);
            Assert.Equal(enMethod_Kind.IsFunction, kind);
            Assert.Equal(enCode_Specialty.IsStatic, specialty);
            #endregion

            #region Test3: public static List<Rectangle> Shape_Move(List<Rectangle> shape, int x, int y, int recSize)
            //      =================================================================================================
            ii = 0;
            source = MethodNTHeader_Methods.Str2StrList("public static List<Rectangle> Shape_Move(List<Rectangle> shape, int x, int y, int recSize)");
            header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(0, ii);
            Assert.Equal("Shape_Move", methodName);
            Assert.Equal(enCode_Scope._public, scope);
            Assert.Equal("List<Rectangle>", returnType);
            Assert.Equal(enMethod_Kind.IsFunction, kind);
            Assert.Equal(enCode_Specialty.IsStatic, specialty);
            #endregion

            #region Test4: public Dictionary<int, string> CreateDictionary()
            //      =================================================================
            source = MethodNTHeader_Methods.Str2StrList("public Dictionary<int, string> CreateDictionary()");
            header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(0, ii);
            Assert.Equal("CreateDictionary", methodName);
            #endregion

            #region Test5: public static SortedList<int, Train_PathInfo> AI_FoodSort(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)
            //      ================================================================================================================================
            source = MethodNTHeader_Methods.Str2StrList("public static SortedList<int, Train_PathInfo> AI_FoodSort(Train_ train, ref bool foodChange, List<Train_Food> trainFoods)");
            header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(0, ii);
            Assert.Equal("AI_FoodSort", methodName);
            Assert.Equal(enCode_Scope._public, scope);
            Assert.Equal("SortedList<int, Train_PathInfo>", returnType);
            Assert.Equal(enMethod_Kind.IsFunction, kind);
            Assert.Equal(enCode_Specialty.IsStatic, specialty);
            #endregion

            #region Test6: public static IEnumerable<RT> ForEach<T, RT>(IEnumerable<T> array, Func<T, RT> func)
            //      ===========================================================================================
            source = MethodNTHeader_Methods.Str2StrList("public static IEnumerable<RT> ForEach<T, RT>(IEnumerable<T> array, Func<T, RT> func)");
            header = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(0, ii);
            Assert.Equal("ForEach", methodName);
            Assert.Equal(enCode_Scope._public, scope);
            Assert.Equal("IEnumerable<RT>", returnType);
            Assert.Equal(enMethod_Kind.IsFunction, kind);
            Assert.True(specialty.zFlag_IsSet(false, enCode_Specialty.IsStatic, enCode_Specialty.IsGeneric));
            #endregion

            #region Test7: public string PropetyName !!
            //      ==============================================================
            ii = 0;
            source = MethodNTHeader_Methods.Str2StrList("public string PropetyName !!");
            var header7 = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(enMethod_Kind.IsProperty, kind);

            source = MethodNTHeader_Methods.Str2StrList("public string PropetyName !!+");
            var header8 = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(enMethod_Kind.IsSetter, kind);
            #endregion

            #region Test8: public string PropetyName !!+
            //      ==============================================================
            ii = 0;
            source = MethodNTHeader_Methods.Str2StrList("public ConstructorName()");
            var header9 = MethodNTHeader_Methods.Parse(source, ref ii, out methodName, out scope, out returnType, out kind, out specialty);
            Assert.Equal(enMethod_Kind.IsConstructor, kind);
            #endregion
        }

        [Fact]
        [Test_Method("MethodNTHeader_")]
        public void MethodNTHeader_Test()
        {
            #region Test1: public void Setup1(List<string> sourceLines, ref int ii)
            //      ==============================================================
            int ii = 0;
            List<string> source = MethodNTHeader_Methods.Str2StrList("public void Setup1(List<string> sourceLines, ref int ii)");
            var method = MethodNTHeader_.Create(source, ref ii);
            Assert.Equal(0, ii);
            Assert.Equal("Setup1", method.Header_Name);
            Assert.Equal(enCode_Scope._public, method.Header_Scope);
            Assert.Equal("void", method.Header_ReturnType);
            Assert.Equal(enMethod_Kind.IsVoid, method.Header_Kind);
            Assert.Equal(enCode_Specialty.IsNormal, method.Header_Specialty);
            Assert.Equal("public void Setup1(List<string> sourceLines, ref int ii)", method.Method_HeaderLine);
            Assert.Equal("Setup1(List<string>, ref int)", method.Method_Signature);
            #endregion

        }


        [Fact]
        [Test_Method("MethodNT_")]
        public void MethodNT_Test()
        {
            int ii = 0, iiEnd = 0;

            #region Test1: Simple method
            //      ==========================================
            var source = new List<string>
            {
                "/// <summary>",
                "/// Return the specified chars from the right of the input string.",
                "/// </summary>",
                "/// <param name=\"inputStr\">The input string.</param>",
                "/// <param name=\"chars\">The chars.</param>",
                "/// <returns>string</returns>",
                "[Pure]",
                "[Category(\"Appearance\")]",
                "[Description(\"Set Cancel Visible property\")]",
                "[Test, TestedMethod(\"Key_Add()\"), TestedMethod(\"Key_Find()\"), TestedMethod(\"Key_Remove()\")]",
                "public string SubStr_Right(string inputStr, int chars)",
                "{",
                "  if (chars > inputStr.Length) return \"\";",
                "  int position = inputStr.Length - chars;",
                "  return inputStr.Substring(position, chars);",
                "}"
            };

            var method = MethodNT_.Create(source, ref ii, iiEnd, parentClassName: "ParentClassName");
            #region Stats

            var statsClass = ClassNTStats_.Create();
            ClassNTStats_Methods.zUpdate(statsClass, method);
            MethodNTstats_ statsMethod = method.Statistics;
            Assert.Equal(16, statsMethod.MethodTotalLines);
            Assert.Equal(2, statsMethod.MethodTotalBodyLines);
            Assert.Equal(4, statsMethod.MethodTotalCommentLines);
            Assert.Equal(6, statsMethod.SourceLines.Count);
            Assert.Equal(0, statsMethod.ReferenceCalls.Count);
            Assert.Equal(2, statsMethod.CodeComplexity);
            Assert.Equal(1, statsMethod.CodeMaintainability);
            #endregion

            #region Comment
            // =========================================================
            Assert.Equal("Return the specified chars from the right of the input string.", method.Comment.CommentSummary);
            Assert.Equal("string", method.Comment.CommentReturn);
            Assert.Equal("inputStr", method.Comment.CommentParameters[0].ParameterName);
            Assert.Equal("The input string.", method.Comment.CommentParameters[0].ParameterComment);
            #endregion

            #region Attributes
            // ==========================================================
            Assert.Equal("Pure", method.Attribute_Breakdown.Items[0].AttributeName);
            Assert.Equal("Category", method.Attribute_Breakdown.Items[1].AttributeName);
            Assert.Equal("\"Appearance\"", method.Attribute_Breakdown.Items[1].Parameters[0].Value);
            Assert.Equal("Description", method.Attribute_Breakdown.Items[2].Parameters[0].Name);
            Assert.Equal("\"Set Cancel Visible property\"", method.Attribute_Breakdown.Items[2].Parameters[0].Value);
            Assert.Equal("Test", method.Attribute_Breakdown.Items[3].AttributeName);
            Assert.Equal("TestedMethod", method.Attribute_Breakdown.Items[4].AttributeName);
            Assert.Equal("\"Key_Add()\"", method.Attribute_Breakdown.Items[4].Parameters[0].Value);
            Assert.Equal("TestedMethod", method.Attribute_Breakdown.Items[5].AttributeName);
            Assert.Equal("\"Key_Find()\"", method.Attribute_Breakdown.Items[5].Parameters[0].Value);
            Assert.Equal("TestedMethod", method.Attribute_Breakdown.Items[6].AttributeName);
            Assert.Equal("\"Key_Remove()\"", method.Attribute_Breakdown.Items[6].Parameters[0].Value);
            #endregion

            #region Method header
            // =============================================
            Assert.Equal("public string SubStr_Right(string inputStr, int chars)", method.Header.Method_HeaderLine);
            Assert.Equal("SubStr_Right(string,int) : string", method.Header.Method_Signature);
            Assert.Equal(enMethod_Kind.IsFunction, method.Header.Header_Kind);
            Assert.Equal(enCode_Scope._public, method.Header.Header_Scope);
            Assert.Equal("string", method.Header.Header_ReturnType);
            Assert.Equal("SubStr_Right", method.Header.Header_Name);
            Assert.Equal(enCode_Specialty.IsNormal, method.Header.Header_Specialty);
            #endregion

            #region Parameters
            // ===========================
            Assert.Equal("inputStr", method.Header.Header_Parameters[0].ParameterName);
            Assert.Equal("string", method.Header.Header_Parameters[0].ParameterTypeName);
            Assert.Equal(null, method.Header.Header_Parameters[0].ParameterValue);
            Assert.Equal(enParameterRefType.ByValue, method.Header.Header_Parameters[0].ParameterRefType);
            Assert.Equal("chars", method.Header.Header_Parameters[1].ParameterName);
            Assert.Equal("int", method.Header.Header_Parameters[1].ParameterTypeName);
            Assert.Equal(2, method.Header.Header_Parameters.Count);
            #endregion

            #endregion

            #region Test2: MethodNT_.Create(source);
            //      =====================================================
            var sourceLine = source.zTo_Str("".NL());
            var method2 = MethodNT_.Create(sourceLine, "ParentClassName");
            string error;
            Assert.True(_lamed.Types.Object.IsEqual(method, method2, out error),error);

            #endregion
        }

        [Fact]
        [Test_Method("Method_Parse()")]
        [Test_Method("SyncParametersWithComments()")]
        [BlueprintRule_Method(Ignore = true)]
        [BlueprintRule_MethodAliasDef(MirrorClass = typeof(ClassNTMethod_Test), MirrorMethodName = "MethodName", MirrorParameter1 = "parmeter1")]
        public static void Method_Parse_Test()
        {
            int ii = 0, iiEnd = 0;
            MethodNTComment_ comment;
            MethodNTHeader_ header;
            MethodNTstats_ body;
            ClassNTBlueprintMethodRule_ blueprintMethodRule;
            ClassNTBlueprintMethodRuleAliasDef_ aliasDef;
            string methodName;
            List<string> sourceCode;
            List<string> attribute_Lines;
            ClassNTAttributes_ Attribute_Breakdown;

            #region Test1:
            //      ==========================================
            var source = new List<string>
            {
                "/// <summary>",
                "/// Return the specified chars from the right of the input string.",
                "/// </summary>",
                "/// <param name=\"inputStr\">The input string.</param>",
                "/// <param name=\"chars\">The chars.</param>",
                "/// <returns>string</returns>",
                "[Pure]",
                "[Category(\"Appearance\")]",
                "[Description(\"Set Cancel Visible property\")]",
                "[BlueprintRule_Method(Ignore = true)]",
                "[Fact, Test_Method(\"Key_Add()\"), Test_Method(\"Key_Find()\")]" ,
                "[Test_Method(\"Key_Remove()\")]",
                "[BlueprintRule_MethodAliasDef(MirrorClass = typeof(ClassNTMethod_Test), MirrorMethodName = \"MethodName\", MirrorParameter1 = \"parmeter1\")]",
                "public string SubStr_Right(string inputStr, int chars)",
                "{",
                "  if (chars > inputStr.Length) return \"\";",
                "  int position = inputStr.Length - chars;",
                "  return inputStr.Substring(position, chars);",
                "}"
            };

            MethodNT_Methods.Method_Parse(source, ref ii, iiEnd, out methodName, out sourceCode, out comment, out attribute_Lines,
                out Attribute_Breakdown, out blueprintMethodRule, out aliasDef, out header, out body);
            MethodNT_Methods.SyncParametersWithComments(header, comment);

            #region Comment
            // =========================================================
            Assert.Equal("Return the specified chars from the right of the input string.", comment.CommentSummary);
            Assert.Equal("string", comment.CommentReturn);
            Assert.Equal("inputStr", comment.CommentParameters[0].ParameterName);
            Assert.Equal("The input string.", comment.CommentParameters[0].ParameterComment);
            #endregion

            #region Attributes
            // ==========================================================
            // Pure
            Assert.Equal("Pure", Attribute_Breakdown.Items[0].AttributeName);
            // Category
            Assert.Equal("Category", Attribute_Breakdown.Items[1].AttributeName);
            Assert.Equal("\"Appearance\"", Attribute_Breakdown.Items[1].Parameters[0].Value);
            // Description
            Assert.Equal("Description", Attribute_Breakdown.Items[2].Parameters[0].Name);
            Assert.Equal("\"Set Cancel Visible property\"", Attribute_Breakdown.Items[2].Parameters[0].Value);
            // BlueprintRule_Method
            Assert.Equal("BlueprintRule_Method", Attribute_Breakdown.Items[3].AttributeName);
            // Fact
            Assert.Equal("Fact", Attribute_Breakdown.Items[4].AttributeName);
            // Test_Method
            Assert.Equal("Test_Method", Attribute_Breakdown.Items[5].AttributeName);
            Assert.Equal("\"Key_Add()\"", Attribute_Breakdown.Items[5].Parameters[0].Value);
            Assert.Equal("Test_Method", Attribute_Breakdown.Items[6].AttributeName);
            Assert.Equal("\"Key_Find()\"", Attribute_Breakdown.Items[6].Parameters[0].Value);
            Assert.Equal("Test_Method", Attribute_Breakdown.Items[7].AttributeName);
            Assert.Equal("\"Key_Remove()\"", Attribute_Breakdown.Items[7].Parameters[0].Value);
            Assert.Equal(true, blueprintMethodRule.Ignore);

            #endregion

            #region Method header
            // =============================================
            Assert.Equal("public string SubStr_Right(string inputStr, int chars)", header.Method_HeaderLine);
            Assert.Equal("SubStr_Right(string,int) : string", header.Method_Signature);
            Assert.Equal(enMethod_Kind.IsFunction, header.Header_Kind);
            Assert.Equal(enCode_Scope._public, header.Header_Scope);
            Assert.Equal("string", header.Header_ReturnType);
            Assert.Equal("SubStr_Right", header.Header_Name);
            Assert.Equal(enCode_Specialty.IsNormal, header.Header_Specialty);
            #endregion

            #region Parameters
            // ===========================
            Assert.Equal("inputStr", header.Header_Parameters[0].ParameterName);
            Assert.Equal("string", header.Header_Parameters[0].ParameterTypeName);
            Assert.Equal("The input string.", header.Header_Parameters[0].ParameterComment);
            Assert.Equal(null, header.Header_Parameters[0].ParameterValue);
            Assert.Equal(enParameterRefType.ByValue, header.Header_Parameters[0].ParameterRefType);
            Assert.Equal("chars", header.Header_Parameters[1].ParameterName);
            Assert.Equal("int", header.Header_Parameters[1].ParameterTypeName);
            Assert.Equal("The chars.", header.Header_Parameters[1].ParameterComment);
            Assert.Equal(2, header.Header_Parameters.Count);
            #endregion

            #endregion

        }
    }
}
