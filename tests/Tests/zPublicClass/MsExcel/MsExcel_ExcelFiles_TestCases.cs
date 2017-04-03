using System;
using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.zPublicClass;
using LamedalCore.zPublicClass.ExcelData;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.zPublicClass.MsExcel
{
    public sealed class MsExcel_ExcelFiles_TestCases : pcTest
    {
        public MsExcel_ExcelFiles_TestCases(ITestOutputHelper debug = null) : base(debug) { }

        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Compile()")]
        public void ArrowRight_Test()
        {
            #region Get Input
            // ===================================
            var excelFile = "TestArrowRight.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");

            //string folderApplication;
            //string folderTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config);

            var file = folderTestCases + excelFile;            
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");
            #endregion

            // Load the Test ==========================================
            //string file = @"D:\Dev\LaMedal\trunk\LamedalCore\Apps\22_Core_ExcelDashboard\Test\TestArrowRight.xlsx";
            pcExcelData_ input = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Input");
            pcExcelData_ result = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Result");
            // =========================================================

            // Test sheet integrity ====================================
            string resultMsg1, resultMsg2;
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(input, out resultMsg1) == false) throw new Exception(resultMsg1);
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(result, out resultMsg2) == false) throw new Exception(resultMsg2);
            var msg = "Input:".NL() + resultMsg1.NL(2) + "Result:".NL() + resultMsg2;
            DebugLog(msg);
            // ============================================================

            // Execute macro and test the results =========================
            string resultMsg;
            _lamed.lib.Excel.Macro.Compile(input, out resultMsg);
            if (_lamed.lib.IO.Json.Object_IsEqual(result.Normalize(), input.Normalize(), out resultMsg1) == false)
                resultMsg1.zException_Show();
            // ============================================================
        }

        [Fact]
        [Test_Method("Compile()")]
        public void ArrowLeft_Test()
        {
            var excelFile = "TestArrowLeft.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            //string folderApplication;
            //string folderTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config);

            var file = folderTestCases + excelFile;            
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");

            // Load the Test ==========================================
            pcExcelData_ input = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Input");
            pcExcelData_ result = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Result");
            // =========================================================

            // Test sheet integrity ====================================
            string resultMsg1, resultMsg2;
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(input, out resultMsg1) == false) throw new Exception(resultMsg1);
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(result, out resultMsg2) == false) throw new Exception(resultMsg2);
            var msg = "Input:".NL() + resultMsg1.NL(2) + "Result:".NL() + resultMsg2;
            DebugLog(msg);
            // ============================================================

            // Execute macro and test the results =========================
            string resultMsg;
            _lamed.lib.Excel.Macro.Compile(input, out resultMsg);
            if (_lamed.lib.IO.Json.Object_IsEqual(result.Normalize(), input.Normalize(), out resultMsg) == false)
                resultMsg.zException_Show();
            // ============================================================
        }

        [Fact]
        [Test_Method("Compile()")]
        public void ArrowDown_Test()
        {
            var excelFile = "TestArrowDown.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            //string folderApplication;
            //string folderTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config);
            
            var file = folderTestCases + excelFile;
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");

            // Load the Test ==========================================
            pcExcelData_ input = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Input");
            pcExcelData_ result = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Result");
            // =========================================================

            // Test sheet integrity ====================================
            string resultMsg1, resultMsg2;
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(input, out resultMsg1) == false) throw new Exception(resultMsg1);
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(result, out resultMsg2) == false) throw new Exception(resultMsg2);
            var msg = "Input:".NL() + resultMsg1.NL(2) + "Result:".NL() + resultMsg2;
            DebugLog(msg);
            // ============================================================

            // Execute macro and test the results =========================
            string resultMsg;
            _lamed.lib.Excel.Macro.Compile(input, out resultMsg);
            if (_lamed.lib.IO.Json.Object_IsEqual(result.Normalize(), input.Normalize(), out resultMsg) == false)
                resultMsg.zException_Show();
            // ============================================================
        }

        [Fact]
        [Test_Method("Compile()")]
        public void ArrowUp_Test()
        {
            var excelFile = "TestArrowUp.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            //string folderApplication;
            //string folderTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config);

            var file = folderTestCases + excelFile;            
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");

            // Load the Test ==========================================
            pcExcelData_ input = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Input");
            pcExcelData_ result = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Result");
            // =========================================================

            // Test sheet integrity ====================================
            string resultMsg1, resultMsg2;
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(input, out resultMsg1) == false) throw new Exception(resultMsg1);
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(result, out resultMsg2) == false) throw new Exception(resultMsg2);
            var msg = "Input:".NL() + resultMsg1.NL(2) + "Result:".NL() + resultMsg2;
            DebugLog(msg);
            // ============================================================

            // Execute macro and test the results =========================
            string resultMsg;
            _lamed.lib.Excel.Macro.Compile(input, out resultMsg);
            if (_lamed.lib.IO.Json.Object_IsEqual(result.Normalize(), input.Normalize(), out resultMsg) == false)
                resultMsg.zException_Show();
            // ============================================================
        }

        [Fact]
        [Test_Method("Compile()")]
        public void ArrowDownLeft_Test()
        {
            var excelFile = "TestArrow_DownLeft.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            //string folderApplication;
            //string folderTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config);

            var file = folderTestCases + excelFile;
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");

            // Load the Test ==========================================
            pcExcelData_ input = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Input");
            pcExcelData_ result = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file, "Result");
            // =========================================================

            // Test sheet integrity ====================================
            string resultMsg1, resultMsg2;
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(input, out resultMsg1) == false) throw new Exception(resultMsg1);
            if (_lamed.lib.Excel.Macro.DataIntegrity_Check(result, out resultMsg2) == false) throw new Exception(resultMsg2);
            var msg = "Input:".NL() + resultMsg1.NL(2) + "Result:".NL() + resultMsg2;
            DebugLog(msg);
            // ============================================================

            // Execute macro and test the results =========================
            string resultMsg;
            _lamed.lib.Excel.Macro.Compile(input, out resultMsg);
            if (_lamed.lib.IO.Json.Object_IsEqual(result.Normalize(), input.Normalize(), out resultMsg) == false)
                resultMsg.zException_Show();
            // ============================================================
        }

        [Fact]
        [Test_Method("Execute_ExcelMacro()")]
        [Test_Method("CompareDataSheet()")]
        public void ExecRightArrow_Test()
        {
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");

            #region Load the input ==========================================
            string fileInput = folderTestCases + "Budget_Input1Data.xlsx";
            string fileConfig = folderTestCases + "Budget_Input2Config.xlsx";
            string fileResult = folderTestCases + "Budget_Output";   // The .xlsx will be added automatically
            string fileConfigCompile_Test = folderTestCases + "Budget_Result_Config_Compile.xlsx";
            string fileResult_Test = folderTestCases + "Budget_Result_Output.xlsx";

            Assert.True(_lamed.lib.IO.Folder.Exists(folderTestCases), $"Folder: '{folderTestCases}' does not exist!");
            Assert.True(_lamed.lib.IO.File.Exists(fileInput), $"File: '{fileInput}' does not exist!");
            Assert.True(_lamed.lib.IO.File.Exists(fileConfig), $"File: '{fileConfig}' does not exist!");
            Assert.True(_lamed.lib.IO.File.Exists(fileConfigCompile_Test), $"File: '{fileConfigCompile_Test}' does not exist!");
            Assert.True(_lamed.lib.IO.File.Exists(fileResult_Test), $"File: '{fileResult_Test}' does not exist!");
            #endregion

            #region TestCase
            string errorMsg;
            _lamed.lib.Excel.Macro.Execute_ExcelMacro(fileInput, fileConfig, fileResult, out errorMsg);
            string fileConfigCompile = fileConfig.Replace(".xlsx", "_Compile.xlsx");
            Assert.True(_lamed.lib.IO.File.Exists(fileResult+ ".xlsx"), $"File: '{fileResult}' does not exist!");
            Assert.True(_lamed.lib.IO.File.Exists(fileConfigCompile), $"File: '{fileConfigCompile}' does not exist!");

            // Test the results
            // Budget_Input2Config_Compile <-> Budget_Result_Config_Compile
            var msg = $"Test Sheets: '{_lamed.lib.IO.Parts.File(fileConfigCompile)}' and '{_lamed.lib.IO.Parts.File(fileConfigCompile_Test)}'";
            DebugLog(msg, true);
            List<string> results = _lamed.lib.Excel.IO_Read.CompareDataSheet(fileConfigCompile, "", fileConfigCompile_Test, "");
            Assert.Equal(0, results.Count); // Test that there is no differences

            // Budget_Output <-> Budget_Result_Output
            msg = $"Test Sheets: '{_lamed.lib.IO.Parts.File(fileResult)}' and '{_lamed.lib.IO.Parts.File(fileResult_Test)}'";
            DebugLog(msg);
            results = _lamed.lib.Excel.IO_Read.CompareDataSheet(fileResult+".xlsx", "", fileResult_Test, "");
            Assert.Equal(0, results.Count); // Test that there is no differences
            #endregion

            #region Exceptions
            string fileInput2 = folderTestCases + "Budget_Input1Data123.xlsx";
            string fileConfig2 = folderTestCases + "Budget_Input2Config123.xlsx";
            Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.Execute_ExcelMacro(fileInput2, fileConfig, fileResult, out errorMsg));
            Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.Execute_ExcelMacro(fileInput, fileConfig2, fileResult, out errorMsg));

            #endregion
        }

    }
}
