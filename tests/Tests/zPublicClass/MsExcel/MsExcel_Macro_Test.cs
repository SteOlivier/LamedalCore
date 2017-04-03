using System;
using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.Excel;
using LamedalCore.zPublicClass.ExcelData;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.zPublicClass.MsExcel
{
    public sealed class MsExcel_Macro_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        private readonly Excel_ _excel = LamedalCore_.Instance.lib.Excel;

        [Fact]
        [Test_Method("csvLoadFromFile")]
        public void csvLoadFile_Test()
        {
            // Load a sample set
            var data = new pcExcelData_();
            var lines = new List<string>();
            lines.Add("A1,B1,C1,D1");
            lines.Add("A2,B2,C2,D2");
            lines.Add("A3,B3,C3,D3");
            lines.Add("A4,B4,C4,D4");
            data.csvLoadFromLines(lines.ToArray());

            #region Test (1,1); A1
            // ====================
            string cell1 = data.Value_Get(1,1);
            Assert.Equal("A1",cell1);
            string cell2 = data.Value_Get("A1");
            Assert.Equal("A1",cell2);
            #endregion

            #region Test C2; D3, D4
            // ====================
            Assert.Equal("C2", data.Value_Get("C2"));
            Assert.Equal("D3", data.Value_Get("D3"));
            Assert.Equal("D4", data.Value_Get("D4"));
            #endregion
        }

        [Fact]
        [Test_Method("Compile_RowItem_GreaterAss()")]
        public void Compile_RowItem_Test()
        {
            // Input
            string value1, value2;
            List<string> row = "1,2,|>|,|>|,|<|,|<|,9,10".zConvert_Str_ToListStr(",");
            Assert.Equal("1", row[0]);
            Assert.Equal("2", row[1]);
            Assert.Equal("|>|", row[2]);
            Assert.Equal("|>|", row[3]);
            Assert.Equal("|<|", row[4]);
            Assert.Equal("|<|", row[5]);
            Assert.Equal("9", row[6]);
            Assert.Equal("10", row[7]);

            #region Test1: item 2 -> 1,2
            var result = _excel.Macro.MacroItem.ItemRow_GreaterThan(row, 2, out value1, out value2);
            Assert.True(result);
            Assert.Equal("1", value1);
            Assert.Equal("2", value2);
            #endregion

            #region Test1: item 5 -> 10,9
            result = _excel.Macro.MacroItem.ItemRow_LessThan(row, 5, out value1, out value2);
            Assert.True(result);
            Assert.Equal("10", value1);
            Assert.Equal("9", value2);
            #endregion
        }

        [Fact]
        [Test_Method("_IsValidMacroValue()")]
        public void _IsValidMacroValue_Test()
        {
            string macro, errorMsg;
            macro = "|A1|";
            Assert.Equal(true,_excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal("", errorMsg);

            macro = "|1|";
            Assert.Equal(true, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal("", errorMsg);

            macro = "|??|";
            Assert.Equal(false, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal("Error! ?? is undefined.", errorMsg);

            macro = "1";
            Assert.Equal(false, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal($"Error! '{macro}' is not a macro setting.", errorMsg);

            macro = "A1";
            Assert.Equal(false, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal($"Error! '{macro}' is not a macro setting.", errorMsg);

            macro = "A1|";
            Assert.Equal(false, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal($"Error! '{macro}' is not a macro setting.", errorMsg);

            macro = "|A1";
            Assert.Equal(false, _excel.Macro.MacroItem._IsValidMacroValue(ref macro, out errorMsg));
            Assert.Equal($"Error! '{macro}' is not a macro setting.", errorMsg);

        }

        [Fact]
        [Test_Method("Compile_Row()")]
        public void Macro_Compile_Row_Test()
        {
            #region Test1: 1,2,|>|,|>|,|<|,5,6 -> 1,2,3,4,4,5,6 
            List<string> row = "1,2,|>|,|>|,|<|,|<|,9,10".zConvert_Str_ToListStr(",");
            // ==========================================================
            string errorMsg;
            _excel.Macro.MacroItem.Compile_Row(row, out errorMsg);
            var rowResult = "1,2,3,4,7,8,9,10".zConvert_Str_ToListStr(",");
            Assert.Equal(rowResult, row);
            #endregion

            #region Test2: LeftSide,Test1,Test2,|>|,Middle,|>|,|>|,|>|,RightSide -> LeftSide,Test1,Test2,Test2,Middle,|??|,|??|,|??|,RightSide 
            row = "LeftSide,Test1,Test2,|>|,Middle,|>|,|>|,|>|,RightSide".zConvert_Str_ToListStr(",");
            // ==========================================================
            _excel.Macro.MacroItem.Compile_Row(row, out errorMsg);
            rowResult = "LeftSide,Test1,Test2,|??|,Middle,|??|,|??|,|??|,RightSide".zConvert_Str_ToListStr(",");
            Assert.Equal(rowResult, row);
            #endregion

            #region Test3: |<|,|<|,|<|,|<|,|<|,|<|,2,1 -> 8,7,6,5,4,3,2,1 
            row = "|<|,|<|,|<|,|<|,|<|,|<|,2,1".zConvert_Str_ToListStr(",");
            // ==========================================================
            _excel.Macro.MacroItem.Compile_Row(row, out errorMsg);
            rowResult = "8,7,6,5,4,3,2,1".zConvert_Str_ToListStr(",");
            Assert.Equal(rowResult, row);
            #endregion

            // Exceptions:   1, 2, |>,4
            row = "A1B1,A2B2,|>|,|>|".zConvert_Str_ToListStr(",");
            _excel.Macro.MacroItem.Compile_Row(row, out errorMsg);
            var rowR = "A1B1,A2B2,|??|,|??|".zConvert_Str_ToListStr(",");
            Assert.Equal(rowR, row);
        }

        [Fact]
        [Test_Method("DataIntegrity_Check()")]
        public void DataIntegrity_Check_Test()
        {
            #region Input
            string errorMsg;
            var data = new pcExcelData_();
            var lines = new List<string>();
            // ========================
            //  Test1
            //  1    ,  2   , |>|  , |>|
            // |A2|  , |C4| , |>|  , |>|, Test2
            // Test  , |B3| , |C3| , |>|
            // |<|   , |<|  ,  10  ,  15
            //       ,      , Test3
            // |A1|->Test1
            // |E3|->Test2
            // |C6|->Test3
            // ========================
            lines.Add("Test1");
            lines.Add("1,2,|>|,|>|");
            lines.Add("|A2|,|C4|,|>|,|>|,Test2");
            lines.Add("Test,|B3|,|C3|,|>|");
            lines.Add("|<|,|<|,10,15");
            lines.Add(",,Test3");
            lines.Add("|A1|->\"Test1\"");
            lines.Add("|E3|->\"Test2\"");
            lines.Add("|C6|->Test3");
            data.csvLoadFromLines(lines.ToArray());
            #endregion 

            // Test the ref points
            var reflines = _lamed.lib.Excel.Adress.Find(data, "|->", enExcel_Compare.Contains, enExcel_FindReturnValue.CellValue);
            Assert.Equal("|A1|->\"Test1\"", reflines[0]);
            Assert.Equal("|E3|->\"Test2\"", reflines[1]);
            Assert.Equal("|C6|->Test3", reflines[2]);
            string cellAddress, cellValue;
            _lamed.lib.Excel.Macro.MacroItem.DataIntegrity_CellParser("|A1|->\"Test1\"", out cellAddress, out cellValue);
            Assert.Equal("A1", cellAddress);
            Assert.Equal("Test1", cellValue);
            Assert.True(_lamed.lib.Excel.Macro.MacroItem.DataIntegrity_Check(data, "A1", "Test1", out errorMsg));
            Assert.False(_lamed.lib.Excel.Macro.MacroItem.DataIntegrity_Check(data, "A1", "Test2", out errorMsg));
            Assert.Equal("Reference check address: 'A1' == 'Test1'  (Error! Should be 'Test2').", errorMsg);

            // Test all integrity points
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errorMsg), errorMsg);

        }

        [Fact]
        [Test_Method("Compile()")]
        public void MacroCompile_LeftRightTest()
        {
            #region Input
            string errorMsg;
            var data = new pcExcelData_();
            var lines = new List<string>();
            // ========================
            //  Test1
            //  1    ,  2   , |>|  , |>|
            // |A2|  , |C4| , |>|  , |>|, Test2
            // Test  , |B3| , |C3| , |>|
            // |<|   , |<|  ,  10  ,  15
            //       ,      , Test3
            // |A1|->Test1
            // |E3|->Test2
            // |C6|->Test3
            // ========================
            lines.Add("Test1");
            lines.Add("1,2,|>|,|>|");
            lines.Add("|A2|,|C4|,|>|,|>|,Test2");
            lines.Add("Test,|B3|,|C3|,|>|");
            lines.Add("|<|,|<|,10,15");
            lines.Add(",,Test3");
            lines.Add("|A1|->Test1");
            lines.Add("|E3|->Test2");
            lines.Add("|C6|->Test3");
            data.csvLoadFromLines(lines.ToArray());
            
            // Test all integrity points
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errorMsg), errorMsg);

            #endregion

            #region Result
            var resultData = new pcExcelData_();
            var resultLine = new List<string>();
            // ========================
            //  Test1
            //  1  ,  2   ,   3  ,   4
            // |A2|, |C4| , |E6| , |G8|
            // Test, |B3| , |C3| , |D3|, Test2
            //  0  ,  5   ,  10 ,  15 
            //       ,      , Test3
            // |A1|->Test1
            // |E3|->Test2
            // |C6|->Test3
            // ========================
            resultLine.Add("Test1");
            resultLine.Add("1,2,3,4");
            resultLine.Add("|A2|,|C4|,|E6|,|G8|,Test2");
            resultLine.Add("Test,|B3|,|C3|,|D3|");
            resultLine.Add("0,5,10,15");
            resultLine.Add(",,Test3");
            resultLine.Add("|A1|->Test1");
            resultLine.Add("|E3|->Test2");
            resultLine.Add("|C6|->Test3");
            resultData.csvLoadFromLines(resultLine.ToArray());

            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(resultData, out errorMsg),errorMsg);
            #endregion

            _excel.Macro.Compile(data, out errorMsg);  // Do the conversion
            Assert.True(_lamed.lib.IO.Json.Object_IsEqual(resultData, data, out errorMsg), errorMsg);
        }

        [Fact]
        [Test_Method("DataIntegrity_CellParser()")]
        public void DataIntegrity_CellParser_Test()
        {
            string cellAddress, cellValue;
            _lamed.lib.Excel.Macro.MacroItem.DataIntegrity_CellParser("|A1|->Test1", out cellAddress, out cellValue);
            Assert.Equal("A1", cellAddress);
            Assert.Equal("Test1", cellValue);
            _lamed.lib.Excel.Macro.MacroItem.DataIntegrity_CellParser("|E3|->Test2", out cellAddress, out cellValue);
            Assert.Equal("E3", cellAddress);
            Assert.Equal("Test2", cellValue);
            _lamed.lib.Excel.Macro.MacroItem.DataIntegrity_CellParser("|C6|->Test3", out cellAddress, out cellValue);
            Assert.Equal("C6", cellAddress);
            Assert.Equal("Test3", cellValue);
        }

        [Fact]
        [Test_Method("Compile()")]
        public void MacroCompile_UpDownTest()
        {
            #region Input
            var data = new pcExcelData_();
            var lines = new List<string>();
            // ========================
            // |A1|, |^| 
            // |A2|, |^| 
            // |V| , |C3|, Test 
            // |V| , |D4|
            // |C3|->Test  
            // ========================
            lines.Add("|A1|,|^|,|^|");
            lines.Add("|A2|,|^|,|^|");
            lines.Add("|V|,|C3|,|B2|,Test");
            lines.Add("|V|,|D4|,|C3|");
            lines.Add("|D3|->Test");
            data.csvLoadFromLines(lines.ToArray());
            string errMsg;
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errMsg),errMsg);

            // Test the column values
            var cols = data.Columns;
            var col0 = "|A1|,|A2|,|V|,|V|,|D3|->Test".zConvert_Str_ToListStr(",");
            var col1 = "|^|,|^|,|C3|,|D4|,".zConvert_Str_ToListStr(",");
            var col2 = "|^|,|^|,|B2|,|C3|,".zConvert_Str_ToListStr(",");
            var col3 = ",,Test,,".zConvert_Str_ToListStr(",");
            Assert.Equal(col0, cols[0]);
            Assert.Equal(col1, cols[1]);
            Assert.Equal(col2, cols[2]);
            Assert.Equal(col3, cols[3]);
            #endregion

            #region Result
            var resultData = new pcExcelData_();
            var resultLine = new List<string>();
            // ========================
            // |A1|,|A1| 
            // |A2|,|B2| 
            // |A3|,|C3|, 100 
            // |A4|,|D4| 
            // |C3|->Test  
            // ========================
            resultLine.Add("|A1|,|A1|,|??|");
            resultLine.Add("|A2|,|B2|,|A1|");
            resultLine.Add("|A3|,|C3|,|B2|,Test");
            resultLine.Add("|A4|,|D4|,|C3|");
            resultLine.Add("|D3|->Test");
            resultData.csvLoadFromLines(resultLine.ToArray());
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errMsg),errMsg);
            #endregion

            string errorMsg2, errorMsg1;
            _excel.Macro.Compile(data, out errorMsg1);  // Do the conversion
            Assert.True(_lamed.lib.IO.Json.Object_IsEqual(resultData, data, out errorMsg2), errorMsg2);
        }

        [Fact]
        [Test_Method("Compile()")]
        public void MacroCompile_DownLeftTest()
        {
            #region Input
            string errorMsg;
            var data = new pcExcelData_();
            var lines = new List<string>();
            // ========================
            // |B17|, |AB17|, |AA17| 
            // |B27|, |AB27|, |AA27| 
            // |B30|, |V>|  , |V>| 
            // |B44|, |V>|  , |V>| 
            // ========================
            lines.Add("|B17|,|AB17|,|AA17|");
            lines.Add("|B27|,|AB27|,|AA27|");
            lines.Add("|B30|,|V>|,|V>|");
            lines.Add("|B44|,|V>|,|V>|");
            data.csvLoadFromLines(lines.ToArray());
            List<string> addressList = _lamed.lib.Excel.Adress.Find(data, "|V>|", enExcel_Compare.Equal, enExcel_FindReturnValue.CellAddress);
            var addressList_Test = "B3,C3,B4,C4".zConvert_Str_ToListStr(",");
            Assert.True(_lamed.lib.IO.Json.Object_IsEqual(addressList_Test, addressList, out errorMsg), errorMsg);
            //Compile_DownLeftCalculate(data, addressList);

            #endregion

            #region Result
            var resultData = new pcExcelData_();
            var resultLine = new List<string>();
            // ========================
            // |B17|,|AB17|,|AA17| 
            // |B27|,|AB27|,|AA27| 
            // |B30|,|AB30|,|AA30| 
            // |B44|,|AB44|,|AA44| 
            // ========================
            resultLine.Add("|B17|,|AB17|,|AA17|");
            resultLine.Add("|B27|,|AB27|,|AA27|");
            resultLine.Add("|B30|,|AB30|,|AA30|");
            resultLine.Add("|B44|,|AB44|,|AA44|");
            resultData.csvLoadFromLines(resultLine.ToArray());
            #endregion

            _excel.Macro.Compile(data, out errorMsg);  // Do the conversion
            Assert.True(_lamed.lib.IO.Json.Object_IsEqual(resultData, data, out errorMsg), errorMsg);
        }

        [Fact]
        [Test_Method("Execute()")]
        public void Macro_ExecuteTest()
        {
            // Load a sample set
            var data = new pcExcelData_();
            var lines = new List<string>();
            lines.Add("1,2,3,4");
            lines.Add("5,6,7,8");
            lines.Add("9,10,11,12");
            lines.Add("13,14,15,16");
            data.csvLoadFromLines(lines.ToArray());

            var dataConfig = new pcExcelData_();
            var configLine = new List<string>();
            configLine.Add("|A1|,|A2|");
            configLine.Add("|B1|,|B2|");
            configLine.Add("|C1|,|C2|");
            configLine.Add("|D1|,|D2|");
            dataConfig.csvLoadFromLines(configLine.ToArray());

            _excel.Macro.Execute_Data(data, dataConfig);  // Do the conversion

            var dataResult = new pcExcelData_();
            var resultLine = new List<string>();
            resultLine.Add("1,5");
            resultLine.Add("2,6");
            resultLine.Add("3,7");
            resultLine.Add("4,8");
            dataResult.csvLoadFromLines(resultLine.ToArray());

            string errorMsg;
            Assert.True(_lamed.lib.IO.Json.Object_IsEqual(dataResult, dataConfig, out errorMsg), errorMsg);
        }

        [Fact]
        [Test_Method("Compile_NextValue()")]
        public void Macro_NextValue_Test()
        {
            #region 4, 5 -> 6
            string errorMsg;
            var value = _excel.Macro.MacroItem.Compile_NextValue("4", "5", out errorMsg);
            Assert.Equal("6", value);
            #endregion

            #region 6, 9 -> 12
            value = _excel.Macro.MacroItem.Compile_NextValue("6", "9", out errorMsg);
            Assert.Equal("12", value);
            #endregion

            #region 10, 15 -> 20
            value = _excel.Macro.MacroItem.Compile_NextValue("10", "15", out errorMsg);
            Assert.Equal("20", value);
            #endregion

            #region |B3|, |C4| --> |D5|
            value = _excel.Macro.MacroItem.Compile_NextValue("|B3|", "|C4|", out errorMsg);
            Assert.Equal("|D5|", value);
            #endregion

            #region |AB3|, |BC5| --> |CD7|
            value = _excel.Macro.MacroItem.Compile_NextValue("|AB3|", "|BC5|", out errorMsg);
            Assert.Equal("|CD7|", value);
            #endregion

            #region AB3, BC5 --> CD7
            value = _excel.Macro.MacroItem.Compile_NextValue("AB3", "BC5", out errorMsg);
            Assert.Equal("|??|", value);
            #endregion

            #region |??|, |??| --> |??|
            value = _excel.Macro.MacroItem.Compile_NextValue("|??|", "|??|", out errorMsg);
            Assert.Equal("|??|", value);
            #endregion

            #region |C2|, |B1| --> |A?|
            value = _excel.Macro.MacroItem.Compile_NextValue("|C2|", "|B1|", out errorMsg);
            Assert.Equal("|A?|", value);
            #endregion

            #region |B1|, |A?| --> |??|
            value = _excel.Macro.MacroItem.Compile_NextValue("|B1|", "|A?|", out errorMsg);
            Assert.Equal("|??|", value);
            #endregion

            #region |A?|, |??| --> |??|
            value = _excel.Macro.MacroItem.Compile_NextValue("|B1|", "|A?|", out errorMsg);
            Assert.Equal("|??|", value);
            #endregion

        }

        [Fact]
        [Test_Method("Compile()")]
        public void MacroCompile_UpDown0_Test()
        {
            #region Input
            var data = new pcExcelData_();
            var lines = new List<string>();
            // ========================
            // |A1|, |^0|,|^0| 
            // |A2|, |^0|,|^0| 
            // |V0|, |C3|,|B2|, Test 
            // |V0|, |D4|,|C3|
            // |C3|->Test  
            // ========================
            lines.Add("|A1|,|^0|,|^0|");
            lines.Add("|A2|,|^0|,|^0|");
            lines.Add("|V0|,|C3|,|B2|,Test");
            lines.Add("|V0|,|D4|,|C3|");
            lines.Add("|D3|->Test");
            data.csvLoadFromLines(lines.ToArray());
            string errMsg;
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errMsg), errMsg);
            #endregion

            #region Result
            var resultData = new pcExcelData_();
            var resultLine = new List<string>();
            // ========================
            // |A1|,|A1|, |B2|
            // |A2|,|B2|, |B2|
            // |A2|,|C3|, |B2|, 100 
            // |A2|,|D4|, |C3| 
            // |C3|->Test  
            // ========================
            resultLine.Add("|A1|,|C3|,|B2|");
            resultLine.Add("|A2|,|C3|,|B2|");
            resultLine.Add("|A2|,|C3|,|B2|,Test");
            resultLine.Add("|A2|,|D4|,|C3|");
            resultLine.Add("|D3|->Test");
            resultData.csvLoadFromLines(resultLine.ToArray());
            Assert.True(_lamed.lib.Excel.Macro.DataIntegrity_Check(data, out errMsg), errMsg);
            #endregion

            string errorMsg2, errorMsg1;
            _excel.Macro.Compile(data, out errorMsg1);  // Do the conversion
            Assert.True(_lamed.lib.Test.ObjectsAreEqual(resultData, data, out errorMsg2), errorMsg2);
        }

        [Fact]
        public void Compile_CellMergeValues_Test()
        {
            #region |AB27|, |B30| --V> |AB30|
            Assert.Equal("|AB30|", _lamed.lib.Excel.Macro.MacroItem.Compile_CellMergeValues("|AB27|", "|B30|"));
            #endregion

            #region |AB30|, |B44| --V> |AB44|
            Assert.Equal("|AB44|", _lamed.lib.Excel.Macro.MacroItem.Compile_CellMergeValues("|AB30|", "|B44|"));
            #endregion

            //Exceptions
            var ex = Assert.Throws<ArgumentException>(() => _lamed.lib.Excel.Macro.MacroItem.Compile_CellMergeValues("AB27|", "|B30|"));
            var errorMsg = @"Error! Inconsistent parameters 'AB27|' and 'B30'.".NL() + "Error! 'AB27|' is not a macro setting.".NL();
            Assert.Equal(errorMsg, ex.Message);

        }

        [Fact]
        public void Normalize_Test()
        {
            #region Input
            string errorMsg;
            var lines = new List<string>();
            // ========================
            //  Test1
            //  1    ,  2   , |>|  , |>|
            // |A2|  , |C4| , |>|  , |>|, Test2
            // Test  , |B3| , |C3| , |>|
            // |<|   , |<|  ,  10  ,  15
            //       ,      , Test3
            // |A1|->Test1
            // |E3|->Test2
            // |C6|->Test3
            // ========================
            lines.Add("Test1");
            lines.Add("1,2,|>|,|>|");
            lines.Add("|A2|,|C4|,|>|,|>|,Test2");
            lines.Add("Test,|B3|,|C3|,|>|");
            lines.Add("|<|,|<|,10,15");
            lines.Add(",,Test3");
            lines.Add("|A1|->Test1");
            lines.Add("|E3|->Test2");
            lines.Add("|C6|->Test3");

            var data = new pcExcelData_();
            data.csvLoadFromLines(lines.ToArray());
            _lamed.lib.Excel.Data.Normalize(data.Rows);
            #endregion

            foreach (List<string> row in data.Rows)
            {
                Assert.Equal(5,row.Count);
            }
        }

    }
}
