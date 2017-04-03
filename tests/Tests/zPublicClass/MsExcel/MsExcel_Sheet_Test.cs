using System;
using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zPublicClass;
using LamedalCore.zPublicClass.ExcelData;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.zPublicClass.MsExcel
{
    public sealed class MsExcel_Sheet_Test: pcTest
    {
        public MsExcel_Sheet_Test(ITestOutputHelper debug = null) : base(debug) { }

        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("SheetDef_Parse()")]
        public void SheetDef_Parse_Test()
        {
            #region Test: {Sheet}->"Q22";{Data}->|A5|;|A10|->"Name or Nickname:";|A14|->"1";|A35|->"22";|K12|->"Total"
            // ======================================================================================
            var input = "{Sheet}->\"Q22\";{Data}->|A5|;|A10|->\"Name or Nickname:\";|A14|->\"1\";|A35|->\"22\";|K12|->\"Total\"";
            pcExcelDef_Sheet sheetDef = _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(input);
            Assert.False(sheetDef==null, "Class 'MsExcelDef_Sheet' is NULL.");

            Assert.Equal("Q22", sheetDef.SheetName);
            Assert.Equal("A5", sheetDef.DataCellAddress);
            Assert.Equal("A10", sheetDef.Cells[0].CellAddress);
            Assert.Equal("Name or Nickname:", sheetDef.Cells[0].CellValue);
            Assert.Equal("A14", sheetDef.Cells[1].CellAddress);
            Assert.Equal("1", sheetDef.Cells[1].CellValue);
            Assert.Equal("A35", sheetDef.Cells[2].CellAddress);
            Assert.Equal("22", sheetDef.Cells[2].CellValue);
            Assert.Equal("K12", sheetDef.Cells[3].CellAddress);
            Assert.Equal("Total", sheetDef.Cells[3].CellValue);
            #endregion
        }

        [Fact]
        [Test_Method("ExcelFile_LoadAsExcelData()")]
        public void ExcelFile_LoadAsExcelData_Test()
        {
            var excelInputFile = "Sheet_EqualTest.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Setup part
            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            var file_input = folderTestCases + excelInputFile;

            // Test if files exists
            Assert.True(_lamed.lib.IO.Folder.Exists(folderTestCases), folderTestCases);
            Assert.True(_lamed.lib.IO.File.Exists(file_input), file_input);

            // Valid sheets
            pcExcelData_ input1 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet1");
            pcExcelData_ input2 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet2");
            pcExcelData_ input3 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet3");

            // Invalid sheets
            var ex = Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet334"));
            Assert.Equal("Error! Worksheet with name 'Sheet334' was not found!", ex.Message);
        }

        [Fact]
        [Test_Method("CompareDataSheet()")]
        [Test_Method("ExcelFile_LoadAsExcelData()")]
        public void SheetsEquality_Test()
        {
            #region Input
            var excelInputFile = "Sheet_EqualTest.xlsx";
            var excelResultFile = "Sheet_EqualTest2.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Setup part
            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel/");
            var file_input = folderTestCases + excelInputFile;
            var file_result = folderTestCases + excelResultFile;

            // Test if files exists
            Assert.True(_lamed.lib.IO.Folder.Exists(folderTestCases), folderTestCases);
            Assert.True(_lamed.lib.IO.File.Exists(file_input), file_input);
            Assert.True(_lamed.lib.IO.File.Exists(file_result), file_result);

            // Read test cases
            pcExcelData_ input1 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet1");
            pcExcelData_ input2 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet2");
            pcExcelData_ input3 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_input, "Sheet3");

            pcExcelData_ result1 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_result, "Sheet1");
            pcExcelData_ result2 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_result, "Sheet2");
            pcExcelData_ result3 = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(file_result, "Sheet3");
            #endregion

            #region Compare results - Sheet1
            // ===========================================================
            DebugLog("Test Sheet1:",true);
            List<string> results = _lamed.lib.Excel.Data.CompareDataSheet(input1, result1);
            Assert.Equal(0, results.Count);
            #endregion

            #region Sheet2 ==================================================
            DebugLog("Test Sheet2:");
            // Addess test
            List<string> results2 = _lamed.lib.Excel.Data.CompareDataSheet(input2, result2);
            Assert.Equal(2, results2.Count);
            Assert.Equal(results2[0], "A1");
            Assert.Equal(results2[1], "B5");

            // Value test
            results2 = _lamed.lib.Excel.Data.CompareDataSheet(input2, result2, enExcel_FindReturnValue.CellValue);
            Assert.Equal(results2[0], "Value 'Field1' != 'Field1_'");
            Assert.Equal(results2[1], "Value 'f' != 'f_'");

            // Address & Value test
            results2 = _lamed.lib.Excel.Data.CompareDataSheet(input2, result2, enExcel_FindReturnValue.CellAddressAndValue);
            Assert.Equal(results2[0], "A1 -> Value 'Field1' != 'Field1_'");
            Assert.Equal(results2[1], "B5 -> Value 'f' != 'f_'");
            // ========================================================
            #endregion

            #region Sheet3 =================================================
            DebugLog("Test Sheet3:");
            // Addess test
            List<string> results3 = _lamed.lib.Excel.Data.CompareDataSheet(input3, result3);
            Assert.Equal(2, results3.Count);
            Assert.Equal(results3[0], "B1");
            Assert.Equal(results3[1], "C4");

            // Value test
            results3 = _lamed.lib.Excel.Data.CompareDataSheet(input3, result3, enExcel_FindReturnValue.CellValue);
            Assert.Equal(results3[0], "Value 'Field2' != 'Field2_'");
            Assert.Equal(results3[1], "Value 'h' != 'h_'");

            // Address & Value test
            results3 = _lamed.lib.Excel.Data.CompareDataSheet(input3, result3, enExcel_FindReturnValue.CellAddressAndValue);
            Assert.Equal(results3[0], "B1 -> Value 'Field2' != 'Field2_'");
            Assert.Equal(results3[1], "C4 -> Value 'h' != 'h_'");
            // ====================================================================
            #endregion
        }

        [Fact]
        public void ExcelCsv_Test()
        {
            // Get Result ===============================================
            List<string> aboutExcel_AsList = _lamed.lib.About.Excel.About_Excel_ToList();
            var dataResult = new pcExcelData_();
            dataResult.csvLoadFromLines(aboutExcel_AsList.ToArray());

            // Get Input ===============================================
            var file1 = "Excel_About_Test.csv";
            _lamed.lib.About.Excel.About_Excel(file1);
            var folder = _lamed.lib.IO.Folder.Path_Application();
            var file = folder + file1;

            Assert.True(_lamed.lib.IO.File.Exists(file), file);
            var dataInput = _lamed.lib.Excel.IO_Read.csvLoadFromFile(file);

            // Test
            var diffList = _lamed.lib.Excel.Data.CompareDataSheet(dataInput, dataResult, enExcel_FindReturnValue.CellAddressAndValue);
            Assert.Equal(0, diffList.Count);
        }
    }
}
