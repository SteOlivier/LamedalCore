using System;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.Excel;
using LamedalCore.zPublicClass.ExcelData;
using Xunit;

namespace LamedalCore.Test.Tests.zPublicClass.MsExcel
{
    /// <summary>
    /// MS Excel methods
    /// </summary>
    public sealed class MsExcel_Test
    {
        private static readonly Excel_ _excel = LamedalCore_.Instance.lib.Excel;

        [Fact, Test_Method("CellAddress()")]
        public void CellName_Test()
        {
            Assert.Equal("A1", _excel.Adress.CellAddress(1, 1));
            Assert.Equal("B1", _excel.Adress.CellAddress(2, 1));
            Assert.Equal("A2", _excel.Adress.CellAddress(1, 2));
            Assert.Equal("B2", _excel.Adress.CellAddress(2, 2));
            Assert.Equal("Z1", _excel.Adress.CellAddress(26, 1));
            Assert.Equal("AA1", _excel.Adress.CellAddress(27, 1));
            Assert.Equal("BA1", _excel.Adress.CellAddress(53, 1));
            Assert.Equal("BZ1", _excel.Adress.CellAddress(78, 1));
            Assert.Equal("CA1", _excel.Adress.CellAddress(79, 1));
            Assert.Equal("IV1", _excel.Adress.CellAddress(256, 1));
            Assert.Equal("CA1", _excel.Adress.CellAddress(79, 1));
            Assert.Equal("ZZ1", _excel.Adress.CellAddress(702, 1));
            Assert.Equal("AAA1", _excel.Adress.CellAddress(703, 1));
            Assert.Equal("AAZ1", _excel.Adress.CellAddress(728, 1));
            Assert.Equal("ABA1", _excel.Adress.CellAddress(729, 1));
            Assert.Equal("XFD1", _excel.Adress.CellAddress(16384, 1));
            Assert.Equal("ABA1000", _excel.Adress.CellAddress(729, 1000));

            Assert.Equal("A2", _excel.Adress.CellAddress_NextRow("A1"));
            Assert.Equal("B1", _excel.Adress.CellAddress_NextCol("A1"));
        }

        [Fact, Test_Method("CellAddress()")]
        public void CellNameExceptions_Test()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _excel.Adress.CellAddress(0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _excel.Adress.CellAddress(1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => _excel.Adress.CellAddress(0, 0));
        }

        [Fact, Test_Method("ColName_FromColNumber()")]
        public static void ColName_FromColNumber_Test()
        {
            #region Test1: Cols 1, 4, 6, 27
            //      ===========================================
            Assert.Equal("A", _excel.Adress.ColName_FromColNumber(1));
            Assert.Equal("D", _excel.Adress.ColName_FromColNumber(4));
            Assert.Equal("F", _excel.Adress.ColName_FromColNumber(6));
            Assert.Equal("AA", _excel.Adress.ColName_FromColNumber(27));
            #endregion

            #region Test1: Cols 1, 4, 6, 27
            //      ===========================================
            Assert.Throws<ArgumentOutOfRangeException>(() => _excel.Adress.ColName_FromColNumber(0));

            #endregion
        }

        [Fact]
        [Test_Method("ColName_2Int()")]
        public static void ColName_2Int_Test()
        {
            #region Test1: A, D, F, AA
            //      ===========================================
            Assert.Equal(1, _excel.Adress.ColName_2Int("A"));
            Assert.Equal(4, _excel.Adress.ColName_2Int("D"));
            Assert.Equal(6, _excel.Adress.ColName_2Int("F"));
            Assert.Equal(27, _excel.Adress.ColName_2Int("AA"));
            #endregion

            #region Test2:  A5, D13
            // ===========================================
            Assert.Equal(1, _excel.Adress.ColName_2Int("A5"));
            Assert.Equal(4, _excel.Adress.ColName_2Int("D13"));
            #endregion

            #region Test2:  Name456
            // ===========================================
            Assert.Equal(269579, _excel.Adress.ColName_2Int("Name456"));
            #endregion

            #region Test3: Z1 .. ABA1000
            // ===========================================
            Assert.Equal(26, _excel.Adress.ColName_2Int("Z1"));
            Assert.Equal(27, _excel.Adress.ColName_2Int("AA1"));
            Assert.Equal(53, _excel.Adress.ColName_2Int("BA1"));
            Assert.Equal(78, _excel.Adress.ColName_2Int("BZ1"));
            Assert.Equal(79, _excel.Adress.ColName_2Int("CA1"));
            Assert.Equal(256, _excel.Adress.ColName_2Int("IV1"));
            Assert.Equal(79, _excel.Adress.ColName_2Int("CA1"));
            Assert.Equal(702, _excel.Adress.ColName_2Int("ZZ1"));
            Assert.Equal(703, _excel.Adress.ColName_2Int("AAA1"));
            Assert.Equal(728, _excel.Adress.ColName_2Int("AAZ1"));
            Assert.Equal(729, _excel.Adress.ColName_2Int("ABA1"));
            Assert.Equal(16384, _excel.Adress.ColName_2Int("XFD1"));
            Assert.Equal(729, _excel.Adress.ColName_2Int("ABA1000"));
            #endregion

        }

        [Fact]
        [Test_Method("ColRow_AsRefName()")]
        public static void ColRow_AsRefName_Test()
        {
            #region Test1: A1
            //      ===========================================
            string col;
            int row;
            _excel.Adress.ColRow_AsRefName(out col, out row, "A1");
            Assert.Equal("A", col);
            Assert.Equal(1, row);
            #endregion

            #region Test2: AZ35
            // ===========================================
            _excel.Adress.ColRow_AsRefName(out col, out row, "AZ35");
            Assert.Equal("AZ", col);
            Assert.Equal(35, row);
            #endregion

            #region Test3: D
            // ===========================================
            _excel.Adress.ColRow_AsRefName(out col, out row, "D");
            Assert.Equal("D", col);
            Assert.Equal(0, row);
            #endregion

            #region Test4: Name456
            // ===========================================
            _excel.Adress.ColRow_AsRefName(out col, out row, "Name456");
            Assert.Equal("Name", col);
            Assert.Equal(456, row);
            #endregion

            //A?
            #region Test5: A?
            // ===========================================
            _excel.Adress.ColRow_AsRefName(out col, out row, "A?");
            Assert.Equal("A", col);
            Assert.Equal(0, row);
            #endregion
        }

        [Fact]
        [Test_Method("Adress.Range()")]
        public static void Range_Calculate_Test()
        {
            #region Test1: A1 + 3, 3 -> C3
            //      ===========================================
            Assert.Equal("C3", _excel.Adress.Range("A1", 3,3));
            #endregion

            #region Test2:  A5 + 4,9 -> D13
            // ===========================================
            Assert.Equal("D13", _excel.Adress.Range("A5", 4,9));
            #endregion

            // Exceptions
            var ex = Assert.Throws<InvalidOperationException>(() =>_excel.Adress.Range("A1", 0, 3));
            Assert.Equal("Error! Columns must be 1 or greater!", ex.Message);

            ex = Assert.Throws<InvalidOperationException>(() => _excel.Adress.Range("A1", 3, 0));
            Assert.Equal("Error! Rows must be 1 or greater!", ex.Message);
        }

        [Fact]
        [Test_Method("Value_Set()")]
        [Test_Method("Value_Get()")]
        [Test_Method("Row()")]
        public void ValueAndRow_Test()
        {
            // Value_Set
            pcExcelData_ excelData = new pcExcelData_();
            excelData.Value_Set("A1", "5");
            excelData.Value_Set(2,1, "6");
            excelData.Value_Set("B3", "15");

            // Value_Get
            Assert.Equal(excelData.Value_Get("A1"), "5");
            Assert.Equal(excelData.Value_Get("B3"), "15");

            // Row
            var row = excelData.Row(1);
            Assert.Equal(new string[] {"5", "6"}, row);

            // Exceptions
            Assert.Throws<ArgumentOutOfRangeException>(() => excelData.Row(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => excelData.Row(10));
        }

    }
}
