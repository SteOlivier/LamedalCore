using System;
using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.Types.List;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types.List
{
    public sealed class List_Level_Test
    {
        private readonly List_Level _listLevel = LamedalCore_.Instance.Types.List.Level;

        /// <summary>
        /// List  query tests.
        /// </summary>
        [Fact]
        [Test_Method("Query")]
        public void Query_Tests()
        {
            // ======
            // Test data
            // ======
            var namesp = new List<string>
            {
                "bp.name1.classname1.cs",
                "bp.name1.classname2.cs",
                "bp.name1.classname3.cs",
                "bp.name2.classname4.cs",
                "bp.name2.classname5.cs",
                "bp.name3.classname6.cs"
            };

            #region Query tests
            var result1 = _listLevel.Query(namesp, 1, ".");
            Assert.True(result1[0] == "bp");

            var result2 = _listLevel.Query(namesp, 2, ".");
            Assert.True(result2[0] == "name1");
            Assert.True(result2[1] == "name2");
            Assert.True(result2[2] == "name3");

            var result3 = _listLevel.Query(namesp, 3, ".", ".name1.");
            Assert.True(result3[0] == "classname1");
            Assert.True(result3[1] == "classname2");
            Assert.True(result3[2] == "classname3");

            var result4 = _listLevel.Query(namesp, 4, ".", ".name1.");
            Assert.Equal("cs", result4[0]);
            Assert.Equal(1, result4.Count);
            #endregion

            #region Exceptions
            var result5 = _listLevel.Query(namesp, 5, ".", "bp");
            Assert.Equal(0, result5.Count);
            result5 = _listLevel.Query(namesp, 15, ".", "bp");
            Assert.Equal(0, result5.Count);

            var ex = Assert.Throws<InvalidOperationException>(() =>_listLevel.Query(namesp, 0, "."));
            Assert.Equal("Error: Level parameter must always be > 0", ex.Message);

            namesp = null;
            var ex2 = Assert.Throws<ArgumentNullException>(() => _listLevel.Query(namesp, 0, "."));
            var errorMsg = "Value cannot be null.".NL() + "Parameter name: strList";
            Assert.Equal(errorMsg, ex2.Message);

            #endregion
        }

        [Fact]
        [Test_Method("Levels()")]
        public void Level1and2_Tests()
        {
            string root = null;
            var tree = new List<string>
            {
                "Root.level1.level2a",
                "Root.level1.level2b",
                "Root.level1.level2c",
                "Root.level1.level2d.Level3"
            };

            #region Test1: Level1and2(tree, ".") == "Root.level1"
            //      ===========================================
            root = _listLevel.Level1and2(tree, ".");
            Assert.Equal("Root.level1", root);
            #endregion

            #region Test2: Levels(tree, 1, ".") == "Root"
            //      ===========================================
            root = _listLevel.Levels(tree, 1, ".");
            Assert.Equal("Root", root);
            #endregion

            #region Test3: Levels(tree, 2, ".") == "Root.level1"
            //      ===========================================
            root = _listLevel.Levels(tree, 2, ".");
            Assert.Equal("Root.level1", root);
            #endregion

            #region Test4: Levels(tree, 3, ".") == "Root.level1.level2a"
            //      ===========================================
            root = _listLevel.Levels(tree, 3, ".");
            Assert.Equal("Root.level1.level2a", root);
            #endregion

            // Exceptions
            Assert.Equal("", _listLevel.Levels(tree, 0, "."));
            tree = null;
            var ex = Assert.Throws<ArgumentNullException>(() => _listLevel.Levels(tree, 3, "."));
            var errorMsg = "Value cannot be null.".NL() + "Parameter name: strList";
            Assert.Equal(errorMsg, ex.Message);
        }


    }
}
