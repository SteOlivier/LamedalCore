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
    public sealed class MsExcel_Dashboard_Test : pcTest
    {
        public MsExcel_Dashboard_Test(ITestOutputHelper debug = null) : base(debug) { }

        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Dashboard_FromSheets()")]
        [Test_Method("CompareDataSheet()")]
        public void Macro_Sheets_Test()
        {
            #region Get Input
            // ===================================
            var excelFile = "General_Input.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel_List/");
            var file = folderTestCases + excelFile;
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");
            #endregion

            _lamed.lib.Excel.Macro.Dashboard_FromSheets(folderTestCases, excelFile);

            #region Test compile results 
            // =================================================
            var compile = excelFile.Replace(".xlsx", "_Compile.xlsx");
            var resultExcel = folderTestCases + "TestResult_Compile.xlsx";
            List<string> result = _lamed.lib.Excel.IO_Read.CompareDataSheet(folderTestCases + compile, "",resultExcel, "");
            Assert.Equal(result.Count,0);
            #endregion

            #region Test end result
            // ================================================
            var dashboard = excelFile.Replace(".xlsx", "_Result.xlsx");
            resultExcel = folderTestCases + "TestResult_Result.xlsx";
            result = _lamed.lib.Excel.IO_Read.CompareDataSheet(folderTestCases + dashboard, "", resultExcel, "");
            Assert.Equal(result.Count, 0);
            #endregion

        }

        [Fact]
        [Test_Method("ExcelFile_LoadAsExcelData()")]
        [Test_Method("Find_First()")]
        [Test_Method("SheetDef_Parse()")]
        public void SheetDef_Test()
        {
            #region Get Input
            // ===================================
            var excelFile = "General_Input.xlsx";
            Config_Info.Config_File_Test(_Debug);

            // Test if file exists =====================================
            string folderTestCases = Config_Info.Config_File_Test(_Debug, @"Excel_List/");

            //string folderApplication;
            //string folderExcelTestCases;
            //pcTest_Configuration config;
            //_lamed.lib.Test.ConfigSettings(out folderApplication, out folderExcelTestCases, out config);

            var file = folderTestCases + excelFile;
            DebugLog("File: " + file, true);
            Assert.True(_lamed.lib.IO.File.Exists(file), $"File: '{file}' does not exist!");
            
            // =============================
            //var folder = folderExcelTestCases + @"Excel_list\";
            var fileCompile = folderTestCases + excelFile;
            pcExcelData_ excelData = _lamed.lib.Excel.IO_Read.ExcelFile_LoadAsExcelData(fileCompile);
            #endregion

            #region Test Sheet definition string
            // ===================================================
            string sheetDefStr, sheetDefStr2;
            if (excelData.Find_First(out sheetDefStr, "{Sheet}->") == false) "Error! Unable to find '{Sheet}->' in Excel sheet".zException_Show();
            Assert.Equal("{Sheet}->\"Q22\";{Data}->|A5|;|A10|->\"Name or Nickname:\";|A14|->\"1\";|A35|->\"22\";|K12|->\"Total\"", sheetDefStr);
            Assert.Equal(false, excelData.Find_First(out sheetDefStr2, "{Invalid search}->"));
            #endregion

            #region Test sheet definition
            // ================================================
            pcExcelDef_Sheet sheetDef = _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefStr);
            Assert.Equal(sheetDef.SheetName, "Q22");
            Assert.Equal(sheetDef.DataCellAddress, "A5");
            Assert.Equal(sheetDef.Cells[0].CellAddress, "A10");
            Assert.Equal(sheetDef.Cells[0].CellValue, "Name or Nickname:");
            Assert.Equal(sheetDef.Cells[1].CellAddress, "A14");
            Assert.Equal(sheetDef.Cells[1].CellValue, "1");
            Assert.Equal(sheetDef.Cells[2].CellAddress, "A35");
            Assert.Equal(sheetDef.Cells[2].CellValue, "22");
            Assert.Equal(sheetDef.Cells[3].CellAddress, "K12");
            Assert.Equal(sheetDef.Cells[3].CellValue, "Total");
            #endregion

            #region Test found Excel files
            // ======================================
            List<string> filesGood = _lamed.lib.Excel.Macro.Dashbaord_FindSheetFiles(folderTestCases, sheetDef);
            Assert.Equal(filesGood.Count, 8);
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[0]), "Q22_Bruce.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[1]), "Q22_Charles.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[2]), "Q22_Danie.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[3]), "Q22_Erik.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[4]), "Q22_Henk.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[5]), "Q22_Jerrie.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[6]), "Q22_Joe.xlsx");
            Assert.Equal(_lamed.lib.IO.Parts.File(filesGood[7]), "Q22_Leon.xlsx");
            #endregion

            #region Exceptions
            // Sheet
            var sheetDefError1 = "{Sheet}-->\"Q22\";{Data}->|A5|;|A10|->\"Name or Nickname:\";|A14|->\"1\";|A35|->\"22\";|K12|->\"Total\"";
            var ex = Assert.Throws<InvalidOperationException>(() =>_lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefError1));
            Assert.Equal("Error! '{Sheet}->' was not found.", ex.Message);

            // Data
            var sheetDefError2 = "{Sheet}->\"Q22\";{Data}-->|A5|;|A10|->\"Name or Nickname:\";|A14|->\"1\";|A35|->\"22\";|K12|->\"Total\"";
            ex = Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefError2));
            Assert.Equal("Error! ';{Data}->' was not found.", ex.Message);

            // '|->'
            var sheetDefError3 = "{Sheet}->\"Q22\";{Data}->|A5|;|A10->\"Name or Nickname:\";|A14->\"1\";|A35->\"22\";|K12->\"Total\"";
            ex = Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefError3));
            Assert.Equal("Error! '|->' cell reference was not found.", ex.Message);

            // ';|'
            var sheetDefError4 = "{Sheet}->\"Q22\";{Data}->|A5|; |A10|->\"Name or Nickname:\"; |A14|->\"1\"; |A35|->\"22\";|K12|->\"Total\"";
            ex = Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefError4));
            Assert.Equal("Error! No cell references checks defined.", ex.Message);

            // Sheet name has not quotes
            var sheetDefError5 = "{Sheet}->Q22;{Data}->|A5|;|A10|->\"Name or Nickname:\";|A14|->\"1\";|A35|->\"22\";|K12|->\"Total\"";
            ex = Assert.Throws<InvalidOperationException>(() => _lamed.lib.Excel.Macro.MacroItem.SheetDef_Parse(sheetDefError5));
            Assert.Equal("Error! Sheet ref 'Q22' does not contain quotes.", ex.Message);

            #endregion
        }
    }
}
