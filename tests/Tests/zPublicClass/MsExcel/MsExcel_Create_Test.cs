using System;
using System.Drawing;
using LamedalCore.domain.Enumerals;
using LamedalCore.zPublicClass.ExcelData;
using Xunit;

namespace LamedalCore.Test.Tests.zPublicClass.MsExcel
{
    public sealed class MsExcel_Create_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        public void SimpleTest()
        {
            var data = new pcExcelData_();
            data.WorkSheet_New("TestSheet", enExcel_Orientation.Landscape, "The Author", "Workbook Title");
            //sheet.PageSetup.Orientation = Orientation.Landscape;
            //sheet.PageSetup.PrintRepeatRows = 2;
            //sheet.PageSetup.PrintRepeatColumns = 2;

            data.WorkSheet_ColumnWidth(1, 24.6);
            data.WorkSheet_ColumnWidth("A", 24.6);
            //_lamed.lib.Excel.WorkSheet.WorkSheet_ColumnWidth();
            //sheet.ColumnWidths[0] = 24.6;

            data.WorkSheet_CellSet("A1", "Test", fontName: "Arial Black");
            //sheet.Cells["A1"] = "Test";
            //sheet.Cells["A1"].FontName = "Arial Black";

            data.WorkSheet_CellSet(2,1, "Another Test", border: enExcel_CellBorder.Bottom | enExcel_CellBorder.Right);
            //sheet.Cells[0, 1] = "Another Test";
            //sheet.Cells[0, 1].Border = CellBorder.Bottom | CellBorder.Right;

            data.WorkSheet_CellSet(2,1, "Bold Red", bold: true, textColor: Color.Red);
            //sheet.Cells[0, 2] = "Bold Red";
            //sheet.Cells[0, 2].Bold = true;
            //sheet.Cells[0, 2].TextColor = Color.Red;

            data.WorkSheet_CellSet(3,1, "BIU Big Blue", bold:true, underline:true, italic:true, 
                textColor:Color.Blue, fontSize:18, webLink: "https://github.com/mstum/Simplexcel", columnWidth:40);
            data.WorkSheet_ColumnWidth(3,40);

            data.WorkSheet_CellSet(4, 1, 13);
            data.WorkSheet_CellSet(5, 1, 13.58);

            data.WorkSheet_CellSet(2, 1, "Orange", bold: true, italic: true, textColor: Color.Orange, fontSize: 18);

            data.Workbook_Save(@"testCompressed.xlsx");
            //_lamed.lib.Command.Execute_Explorer();

            // Exceptions
            data.Workbook_Close();
            Assert.Throws<InvalidOperationException>(() => data.WorkSheet_CellSet(4, 1, 13));
        }
    }
}
