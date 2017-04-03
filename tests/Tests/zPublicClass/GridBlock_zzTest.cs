using System;
using System.Collections.Generic;
using System.Drawing;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zPublicClass.GridBlock;
using LamedalCore.zPublicClass.GridBlock.GridInterface;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.zPublicClass
{
    public sealed class GridBlock_zzTest   // Parent need to be of type IGridblock_Base
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Theory]
        [InlineData("1_1", "1_1", "1_1")]
        [InlineData("2_1", "1_2", "1_3")]
        [InlineData("2_1", "2_2", "2_3")]
        [Test_Method("Child_GridBlockGet()")]
        [Test_Method("Child_GridBlockSub()")]
        [Test_Method("Child_GridBlockMicro()")]
        public void GridBlock_4MacroSetup_Test(string addressMacro, string addressSub, string addressMicro)
        {
            int macroY, macroX;
            GridControl_Settings.Address_ToXY(addressMacro, out macroY, out macroX);
            int subY, subX;
            GridControl_Settings.Address_ToXY(addressSub, out subY, out subX);
            int microY, microX;
            GridControl_Settings.Address_ToXY(addressMicro, out microY, out microX);

            var settings = new GridControl_Settings(macroRows: 5, macroCols: 1, subRows: 5, subCols: 6, microRows: 6, microCols: 6);
            var gridSetup = new GridBlock_5Setup(null, settings);

            #region Cuboid

            var gridCubiod = gridSetup.GridCuboid;
            Assert.Equal(5*6*5*6*6, gridCubiod.Child_Count);
            Assert.Equal(enGrid_BlockType.MacroBlock, gridCubiod.Child_BlockType);
            Assert.Equal(enGrid_BlockDisplayType.Name, gridCubiod.Child_DisplayType);
            gridCubiod.State_Setup(0,0,Color.Red);  // There is not state

            #endregion

            #region Macro block
            var gridMacro = gridSetup.GetChild_MacroGridBlock(addressMacro) as GridBlock_3Macro;
            Assert.NotEqual(null, gridMacro);
            Assert.Equal(enGrid_BlockEditState.Undefined, gridMacro.State_EditState);  // First time state is undefined
            Assert.Equal(double.NaN, gridMacro.State_ValueDouble);
            Assert.Equal(null, gridMacro.zGridControl);
            Assert.Equal(true, gridMacro.Name_Caption.Contains("."));

            gridMacro.State_Setup(123.55, 2, Color.Red);  // Setup method makes state ValueSet
            Assert.Equal(123.55, gridMacro.State_ValueDouble);
            Assert.Equal(2, gridMacro.State_Id);
            Assert.Equal(Color.Red,gridMacro.State_Color);
            Assert.Equal(enGrid_BlockEditState.ValueSet, gridMacro.State_EditState);
            gridMacro.State_Id = 1;               // Setting values makes the state Changed
            gridMacro.State_ValueDouble = 2.5;
            Assert.Equal(enGrid_BlockEditState.Changed, gridMacro.State_EditState);
            Assert.Equal(addressMacro, gridMacro.Name_Address);
            Assert.Equal(6*5*6*6, gridMacro.Child_Count);
            Assert.Equal(enGrid_BlockType.SubBlock, gridMacro.Child_BlockType);
            Assert.Equal(enGrid_BlockDisplayType.Name, gridMacro.Child_DisplayType);
            Assert.Equal(macroY, gridMacro.State_Row);
            Assert.Equal(macroX, gridMacro.State_Col);

            #endregion

            #region Sub block
            var gridSub = gridMacro.GetChild_GridBlock(addressSub) as GridBlock_2Sub;
            Assert.NotEqual(null, gridSub);
            Assert.Equal(Color.Black, gridSub.State_Color);
            Assert.Equal(enGrid_BlockEditState.Undefined, gridSub.State_EditState);  // First time state is undefined
            Assert.Equal(double.NaN, gridSub.State_ValueDouble);

            gridSub.State_Setup(123.55, 2, Color.Red);  // Setup method makes state ValueSet
            Assert.Equal(123.55, gridSub.State_ValueDouble);
            Assert.Equal(2, gridSub.State_Id);
            Assert.Equal(Color.Red, gridSub.State_Color);
            Assert.Equal(enGrid_BlockEditState.ValueSet, gridSub.State_EditState);

            Assert.Equal(addressSub, gridSub.Name_Address);
            Assert.Equal(6 * 6, gridSub.Child_Count);
            Assert.Equal(gridMacro, gridSub._Parent);
            Assert.Equal(enGrid_BlockType.MicroBlock, gridSub.Child_BlockType);
            Assert.Equal(enGrid_BlockDisplayType.Name, gridSub.Child_DisplayType);
            Assert.Equal(subY, gridSub.State_Row);
            Assert.Equal(subX, gridSub.State_Col);

            // Child_GridBlockSub
            var gridSub2 = gridSetup.GetChild_SubGridBlock(addressMacro, addressSub);
            Assert.Equal(gridSub, gridSub2);

            #endregion

            #region Micro block
            var gridMicro = gridSub.GetChild_GridBlock(addressMicro) as GridBlock_1Micro;
            Assert.NotEqual(null, gridMicro);
            Assert.Equal(Color.Black, gridMicro.State_Color);
            Assert.Equal(double.NaN, gridMicro.State_ValueDouble);
            Assert.Equal(enGrid_BlockEditState.Undefined, gridMicro.State_EditState);  // First time state is undefined

            gridMicro.State_Setup(123.55, 2, Color.Red);  // Setup method makes state ValueSet
            Assert.Equal(123.55, gridMicro.State_ValueDouble);
            Assert.Equal(2, gridMicro.State_Id);
            Assert.Equal(Color.Red, gridMicro.State_Color);
            Assert.Equal(enGrid_BlockEditState.ValueSet, gridMicro.State_EditState);

            Assert.Equal(addressMicro, gridMicro.Name_Address);
            Assert.Equal(gridSub, gridMicro._Parent);
            Assert.Equal(microY, gridMicro.State_Row);
            Assert.Equal(microX, gridMicro.State_Col);
                

            // Child_GridBlockMicro
            var gridMicro2 = gridSetup.GetChild_MicroGridBlock(addressMacro, addressSub, addressMicro);
            Assert.Equal(gridMicro, gridMicro2);
            #endregion
        }

        [Theory]
        [InlineData("2_2", "6_2", "2_7")]
        [InlineData("1_6", "3_7", "7_3")]
        [Test_Method("Child_GridBlockGet()")]
        [Test_Method("Child_GridBlockSub()")]
        [Test_Method("Child_GridBlockMicro()")]
        public void GridBlock_4MacroSetup_Fail(string addressMacro, string addressSub, string addressMicro)
        {
            var settings = new GridControl_Settings(macroRows: 5, macroCols: 1, subRows: 5, subCols: 6,
                microRows: 6, microCols: 6);
            var gridSetup = new GridBlock_5Setup(null, settings);
            Assert.Throws<ArgumentException>(() => gridSetup.GetChild_MacroGridBlock(addressMacro) as GridBlock_3Macro);
            Assert.Throws<ArgumentException>(() => gridSetup.GetChild_SubGridBlock("1.1", addressSub));
            Assert.Throws<ArgumentException>(() => gridSetup.GetChild_MicroGridBlock("1.1", "1.1", addressMicro));
        }

        [Fact]
        [Test_Method("GridBlock_4MacroSetup(1_1,1_1,1_1)")]
        public void GridBlock_Frontend_Test1()
        {
            var settings = new GridControl_Settings(1, 1, 1, 1, 1, 1);
            var gridCuboid2 = new GridBlock_5Setup(OnCreateGridControl1, settings);

            #region Result: Tree
            var treeResult =
@"R1
R1cub1_1
R1cub1_1R1
R1cub1_1R1mac1_1
R1cub1_1R1mac1_1R1
R1cub1_1R1mac1_1R1sub1_1
R1cub1_1R1mac1_1R1sub1_1R1
R1cub1_1R1mac1_1R1sub1_1R1mic1_1";

            #endregion

            #region Result: TreeControls
            var treeControlsStr =
@"Row//?//R1//CuboidGrid
Grid//R1//R1cub1_1//CuboidGrid
Row//R1cub1_1//R1cub1_1R1//MacroBlock
Grid//R1cub1_1R1//R1cub1_1R1mac1_1//MacroBlock
Row//R1cub1_1R1mac1_1//R1cub1_1R1mac1_1R1//SubBlock
Grid//R1cub1_1R1mac1_1R1//R1cub1_1R1mac1_1R1sub1_1//SubBlock
Row//R1cub1_1R1mac1_1R1sub1_1//R1cub1_1R1mac1_1R1sub1_1R1//MicroBlock
Grid//R1cub1_1R1mac1_1R1sub1_1R1//R1cub1_1R1mac1_1R1sub1_1R1mic1_1//MicroBlock";

            #endregion

            var treeStr2 = gridCuboid2.TreeNameList().zTo_Str("".NL());
            Assert.Equal(treeResult, treeStr2);

            var treeControls = _ListTest1.zTo_Str("".NL());
            Assert.Equal(treeControlsStr, treeControls);

            Assert.Equal(treeControlsStr, gridCuboid2.TreeControlList().zTo_Str("".NL()));

        }

        private List<string> _ListTest1 = new List<string>();
        private void OnCreateGridControl1(IGridBlock_Base sender, enGrid_ControlType gridcontroltype, string parentname, string childname, 
            enGrid_BlockType blocktype, ref IGridControl gridcontrol)
        {
            //gridcontrol = new object() as IGridControl;
            _ListTest1.Add(ControlToStr(gridcontroltype, parentname, childname, blocktype));
        }

        private string ControlToStr(enGrid_ControlType gridcontroltype, string parentname, string childname, enGrid_BlockType blocktype)
        {
            var type = gridcontroltype.ToString();
            return type + "//" + parentname + "//" + childname + "//" + blocktype;
        }

        [Fact]
        [Test_Method("GridBlock_4MacroSetup(1_1,1_1,5_5)")]
        public void GridBlock_Frontend_Test2()
        {
            var settings = new GridControl_Settings(1, 1, 1, 1, 5, 5);
            var gridCuboid = new GridBlock_5Setup(null, settings);
            var treeStr = gridCuboid.TreeNameList().zTo_Str("".NL());

            #region result
            var treeResult =
@"R1
R1cub1_1
R1cub1_1R1
R1cub1_1R1mac1_1
R1cub1_1R1mac1_1R1
R1cub1_1R1mac1_1R1sub1_1
R1cub1_1R1mac1_1R1sub1_1R1
R1cub1_1R1mac1_1R1sub1_1R1mic1_1
R1cub1_1R1mac1_1R1sub1_1R1mic1_2
R1cub1_1R1mac1_1R1sub1_1R1mic1_3
R1cub1_1R1mac1_1R1sub1_1R1mic1_4
R1cub1_1R1mac1_1R1sub1_1R1mic1_5
R1cub1_1R1mac1_1R1sub1_1R2
R1cub1_1R1mac1_1R1sub1_1R2mic2_1
R1cub1_1R1mac1_1R1sub1_1R2mic2_2
R1cub1_1R1mac1_1R1sub1_1R2mic2_3
R1cub1_1R1mac1_1R1sub1_1R2mic2_4
R1cub1_1R1mac1_1R1sub1_1R2mic2_5
R1cub1_1R1mac1_1R1sub1_1R3
R1cub1_1R1mac1_1R1sub1_1R3mic3_1
R1cub1_1R1mac1_1R1sub1_1R3mic3_2
R1cub1_1R1mac1_1R1sub1_1R3mic3_3
R1cub1_1R1mac1_1R1sub1_1R3mic3_4
R1cub1_1R1mac1_1R1sub1_1R3mic3_5
R1cub1_1R1mac1_1R1sub1_1R4
R1cub1_1R1mac1_1R1sub1_1R4mic4_1
R1cub1_1R1mac1_1R1sub1_1R4mic4_2
R1cub1_1R1mac1_1R1sub1_1R4mic4_3
R1cub1_1R1mac1_1R1sub1_1R4mic4_4
R1cub1_1R1mac1_1R1sub1_1R4mic4_5
R1cub1_1R1mac1_1R1sub1_1R5
R1cub1_1R1mac1_1R1sub1_1R5mic5_1
R1cub1_1R1mac1_1R1sub1_1R5mic5_2
R1cub1_1R1mac1_1R1sub1_1R5mic5_3
R1cub1_1R1mac1_1R1sub1_1R5mic5_4
R1cub1_1R1mac1_1R1sub1_1R5mic5_5";
            #endregion

            Assert.Equal(treeResult, treeStr);
        }

        [Fact]
        [Test_Method("GridBlock_4MacroSetup(2_2,3_3,3_3)")]
        public void GridBlock_Frontend_Test3()
        {
            var settings = new GridControl_Settings(2, 2, 2, 2, 2, 2);
            var gridCuboid = new GridBlock_5Setup(null, settings);
            var treeStr = gridCuboid.TreeNameList().zTo_Str("".NL());

            #region result
            var treeResult =
@"R1
R1cub1_1
R1cub1_1R1
R1cub1_1R1mac1_1
R1cub1_1R1mac1_1R1
R1cub1_1R1mac1_1R1sub1_1
R1cub1_1R1mac1_1R1sub1_1R1
R1cub1_1R1mac1_1R1sub1_1R1mic1_1
R1cub1_1R1mac1_1R1sub1_1R1mic1_2
R1cub1_1R1mac1_1R1sub1_1R2
R1cub1_1R1mac1_1R1sub1_1R2mic2_1
R1cub1_1R1mac1_1R1sub1_1R2mic2_2
R1cub1_1R1mac1_1R1sub1_2
R1cub1_1R1mac1_1R1sub1_2R1
R1cub1_1R1mac1_1R1sub1_2R1mic1_1
R1cub1_1R1mac1_1R1sub1_2R1mic1_2
R1cub1_1R1mac1_1R1sub1_2R2
R1cub1_1R1mac1_1R1sub1_2R2mic2_1
R1cub1_1R1mac1_1R1sub1_2R2mic2_2
R1cub1_1R1mac1_1R2
R1cub1_1R1mac1_1R2sub2_1
R1cub1_1R1mac1_1R2sub2_1R1
R1cub1_1R1mac1_1R2sub2_1R1mic1_1
R1cub1_1R1mac1_1R2sub2_1R1mic1_2
R1cub1_1R1mac1_1R2sub2_1R2
R1cub1_1R1mac1_1R2sub2_1R2mic2_1
R1cub1_1R1mac1_1R2sub2_1R2mic2_2
R1cub1_1R1mac1_1R2sub2_2
R1cub1_1R1mac1_1R2sub2_2R1
R1cub1_1R1mac1_1R2sub2_2R1mic1_1
R1cub1_1R1mac1_1R2sub2_2R1mic1_2
R1cub1_1R1mac1_1R2sub2_2R2
R1cub1_1R1mac1_1R2sub2_2R2mic2_1
R1cub1_1R1mac1_1R2sub2_2R2mic2_2
R1cub1_1R1mac1_2
R1cub1_1R1mac1_2R1
R1cub1_1R1mac1_2R1sub1_1
R1cub1_1R1mac1_2R1sub1_1R1
R1cub1_1R1mac1_2R1sub1_1R1mic1_1
R1cub1_1R1mac1_2R1sub1_1R1mic1_2
R1cub1_1R1mac1_2R1sub1_1R2
R1cub1_1R1mac1_2R1sub1_1R2mic2_1
R1cub1_1R1mac1_2R1sub1_1R2mic2_2
R1cub1_1R1mac1_2R1sub1_2
R1cub1_1R1mac1_2R1sub1_2R1
R1cub1_1R1mac1_2R1sub1_2R1mic1_1
R1cub1_1R1mac1_2R1sub1_2R1mic1_2
R1cub1_1R1mac1_2R1sub1_2R2
R1cub1_1R1mac1_2R1sub1_2R2mic2_1
R1cub1_1R1mac1_2R1sub1_2R2mic2_2
R1cub1_1R1mac1_2R2
R1cub1_1R1mac1_2R2sub2_1
R1cub1_1R1mac1_2R2sub2_1R1
R1cub1_1R1mac1_2R2sub2_1R1mic1_1
R1cub1_1R1mac1_2R2sub2_1R1mic1_2
R1cub1_1R1mac1_2R2sub2_1R2
R1cub1_1R1mac1_2R2sub2_1R2mic2_1
R1cub1_1R1mac1_2R2sub2_1R2mic2_2
R1cub1_1R1mac1_2R2sub2_2
R1cub1_1R1mac1_2R2sub2_2R1
R1cub1_1R1mac1_2R2sub2_2R1mic1_1
R1cub1_1R1mac1_2R2sub2_2R1mic1_2
R1cub1_1R1mac1_2R2sub2_2R2
R1cub1_1R1mac1_2R2sub2_2R2mic2_1
R1cub1_1R1mac1_2R2sub2_2R2mic2_2
R1cub1_1R2
R1cub1_1R2mac2_1
R1cub1_1R2mac2_1R1
R1cub1_1R2mac2_1R1sub1_1
R1cub1_1R2mac2_1R1sub1_1R1
R1cub1_1R2mac2_1R1sub1_1R1mic1_1
R1cub1_1R2mac2_1R1sub1_1R1mic1_2
R1cub1_1R2mac2_1R1sub1_1R2
R1cub1_1R2mac2_1R1sub1_1R2mic2_1
R1cub1_1R2mac2_1R1sub1_1R2mic2_2
R1cub1_1R2mac2_1R1sub1_2
R1cub1_1R2mac2_1R1sub1_2R1
R1cub1_1R2mac2_1R1sub1_2R1mic1_1
R1cub1_1R2mac2_1R1sub1_2R1mic1_2
R1cub1_1R2mac2_1R1sub1_2R2
R1cub1_1R2mac2_1R1sub1_2R2mic2_1
R1cub1_1R2mac2_1R1sub1_2R2mic2_2
R1cub1_1R2mac2_1R2
R1cub1_1R2mac2_1R2sub2_1
R1cub1_1R2mac2_1R2sub2_1R1
R1cub1_1R2mac2_1R2sub2_1R1mic1_1
R1cub1_1R2mac2_1R2sub2_1R1mic1_2
R1cub1_1R2mac2_1R2sub2_1R2
R1cub1_1R2mac2_1R2sub2_1R2mic2_1
R1cub1_1R2mac2_1R2sub2_1R2mic2_2
R1cub1_1R2mac2_1R2sub2_2
R1cub1_1R2mac2_1R2sub2_2R1
R1cub1_1R2mac2_1R2sub2_2R1mic1_1
R1cub1_1R2mac2_1R2sub2_2R1mic1_2
R1cub1_1R2mac2_1R2sub2_2R2
R1cub1_1R2mac2_1R2sub2_2R2mic2_1
R1cub1_1R2mac2_1R2sub2_2R2mic2_2
R1cub1_1R2mac2_2
R1cub1_1R2mac2_2R1
R1cub1_1R2mac2_2R1sub1_1
R1cub1_1R2mac2_2R1sub1_1R1
R1cub1_1R2mac2_2R1sub1_1R1mic1_1
R1cub1_1R2mac2_2R1sub1_1R1mic1_2
R1cub1_1R2mac2_2R1sub1_1R2
R1cub1_1R2mac2_2R1sub1_1R2mic2_1
R1cub1_1R2mac2_2R1sub1_1R2mic2_2
R1cub1_1R2mac2_2R1sub1_2
R1cub1_1R2mac2_2R1sub1_2R1
R1cub1_1R2mac2_2R1sub1_2R1mic1_1
R1cub1_1R2mac2_2R1sub1_2R1mic1_2
R1cub1_1R2mac2_2R1sub1_2R2
R1cub1_1R2mac2_2R1sub1_2R2mic2_1
R1cub1_1R2mac2_2R1sub1_2R2mic2_2
R1cub1_1R2mac2_2R2
R1cub1_1R2mac2_2R2sub2_1
R1cub1_1R2mac2_2R2sub2_1R1
R1cub1_1R2mac2_2R2sub2_1R1mic1_1
R1cub1_1R2mac2_2R2sub2_1R1mic1_2
R1cub1_1R2mac2_2R2sub2_1R2
R1cub1_1R2mac2_2R2sub2_1R2mic2_1
R1cub1_1R2mac2_2R2sub2_1R2mic2_2
R1cub1_1R2mac2_2R2sub2_2
R1cub1_1R2mac2_2R2sub2_2R1
R1cub1_1R2mac2_2R2sub2_2R1mic1_1
R1cub1_1R2mac2_2R2sub2_2R1mic1_2
R1cub1_1R2mac2_2R2sub2_2R2
R1cub1_1R2mac2_2R2sub2_2R2mic2_1
R1cub1_1R2mac2_2R2sub2_2R2mic2_2";
            #endregion

            Assert.Equal(treeResult, treeStr);
        }

        [Fact]
        [Test_Method("Address_ToXY()")]
        public void Address_ToXY_Test()
        {
            int y, x;
            GridControl_Settings.Address_ToXY("1_1",out y, out x);
            Assert.Equal(1, y);
            Assert.Equal(1, x);

            GridControl_Settings.Address_ToXY("1_3", out y, out x);
            Assert.Equal(1, y);
            Assert.Equal(3, x);

            GridControl_Settings.Address_ToXY("7_3", out y, out x);
            Assert.Equal(7, y);
            Assert.Equal(3, x);
        }

     
    }
}
