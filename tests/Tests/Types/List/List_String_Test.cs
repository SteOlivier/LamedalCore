using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using Xunit;

namespace LamedalCore.Test.Tests.Types.List
{
    public sealed class List_String_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Find_First()")]
        [Test_Method("Find_Index()")]
        [Test_Method("Find_All()")]
        public void Find_First_Test()
        {
            string[] items = { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" };
            string[] array = {"hello", "hi", "bye", "welcome", "hell"};

            #region Find_First
            Assert.Equal(true, _lamed.Types.List.String.Find_First(items, "duplicate"));
            Assert.Equal(false, _lamed.Types.List.String.Find_First(items, "duplicate2"));
            #endregion

            #region Find_Index
            Assert.Equal(2, _lamed.Types.List.String.Find_Index(items, "duplicate"));
            Assert.Equal(-1, _lamed.Types.List.String.Find_Index(items, "dup", false));
            Assert.Equal(2, _lamed.Types.List.String.Find_Index(items, "dup", true));
            Assert.Equal(-1, _lamed.Types.List.String.Find_Index(items, "dup",true, false));

            Assert.Equal(0, _lamed.Types.List.String.Find_Index(array, "hello"));
            Assert.Equal(-1, _lamed.Types.List.String.Find_Index(array, "ell"));
            Assert.Equal(4, _lamed.Types.List.String.Find_Index(array, "hell"));

            Assert.Equal(0, _lamed.Types.List.String.Find_Index(array, "hello",true));
            Assert.Equal(0, _lamed.Types.List.String.Find_Index(array, "ell", true));
            Assert.Equal(0, _lamed.Types.List.String.Find_Index(array, "hell",true));
            #endregion

            #region Find_All
            List<string> found = _lamed.Types.List.String.Find_All(array, "hell");
            List<string> notFound = _lamed.Types.List.String.Find_All(array, "hell",false);
            Assert.Equal(new List<string> {"hello", "hell"}, found);
            Assert.Equal(new List<string> { "hi", "bye", "welcome" }, notFound);
            #endregion

            // Exceptions
            string[] array2 = {};
            string[] array3 = null;
            Assert.Equal(null,_lamed.Types.List.String.Find_All(array2, "hell"));
            Assert.Equal(null, _lamed.Types.List.String.Find_All(array3, "hell"));
        }

        [Fact]
        [Test_Method("ToString()")]
        [Test_Method("ToListStr()")]
        [Test_Method("Identical()")]
        public void ToString_Test()
        {
            // ToString
            // Null
            List<string> n1 = null;
            Assert.True(_lamed.Types.List.String.ToString(n1) == "");

            Assert.Equal(",,test", _lamed.Types.List.String.ToString(new[] { "", "", "test" }, ","));
            Assert.Equal("Item3~Item2~Duplicate~Item1~Duplicate", _lamed.Types.List.String.ToString(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, "~"));

            // ToListStr
            var items2 = _lamed.Types.List.String.ToListStr("Item3~Item2~Duplicate~Item1~Duplicate", "~");
            Assert.True(_lamed.Types.List.Find.Identical(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, items2));
        }

        [Fact]
        [Test_Method("TrimLeftRegion()")]
        [Test_Method("Replace()")]
        public void Replace_Test()
        {
            // TrimLeftRegion
            var items2Trim = new[] { "  Item3", " ", "    Item2", "    Duplicate", "    Item1", "  Duplicate" };
            var itemsTrim = new[] { "Item3", "", "  Item2", "  Duplicate", "  Item1", "Duplicate" };
            Assert.Equal(itemsTrim, _lamed.Types.List.String.TrimLeftRegion(items2Trim));
            Assert.Equal(new[] {"line1"}, _lamed.Types.List.String.TrimLeftRegion("line1"));

            // Replace
            var items2Replace = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" };
            var replaceItems = _lamed.Types.List.String.Replace(items2Replace, "tem", "10");
            Assert.True(_lamed.Types.List.Find.Contains(new[] {"I103","I102","Duplicate","I101","Duplicate"}, replaceItems));
        }

        [Fact]
        [Test_Method("SortByStrLength()")]
        public void SortByStrLength_Test()
        {
            var items2Sort = new[] { "zzzzz", "zzzz", "zz", "z", "aa"};
            Assert.Equal(new[] { "z", "zz", "aa", "zzzz", "zzzzz" }, _lamed.Types.List.String.SortByStrLength(items2Sort));
            Assert.Equal(new[] { "zzzzz", "zzzz", "zz", "aa", "z" }, _lamed.Types.List.String.SortByStrLength(items2Sort,enSort.Descending));
            Assert.Equal(items2Sort, _lamed.Types.List.String.SortByStrLength(items2Sort,enSort.NoSort));
        }

        [Fact]
        [Test_Method("Find_FirstStr()")]
        [Test_Method("Find_All()")]
        public void Find_FirstStr_Test()
        {
            var items = new[] {"Item3", "Item2", "Duplicate", "Item1", "Duplicate"};

            // Find_FirstStr
            Assert.Equal("Item1",_lamed.Types.List.String.Find_FirstStr(items, "Item1"));
            Assert.Equal("Item1",_lamed.Types.List.String.Find_FirstStr(items, "item1"));
            Assert.Equal("Item3",_lamed.Types.List.String.Find_FirstStr(items, "item"));
            Assert.Equal("Item3",_lamed.Types.List.String.Find_FirstStr(items, "tem"));
            Assert.Equal(null,_lamed.Types.List.String.Find_FirstStr(items, "tem1233"));
            Assert.Equal(null,_lamed.Types.List.String.Find_FirstStr(items, ""));
            Assert.Equal(null,_lamed.Types.List.String.Find_FirstStr(items, null));
            Assert.Equal(null,_lamed.Types.List.String.Find_FirstStr(null, null));

            // Find_All
            List<string> itemsFound = new List<string> {"Item3", "Item2", "Item1"};
            Assert.Equal(itemsFound, _lamed.Types.List.String.Find_All(items, "tem"));

            List<string> itemsFound2;
            Assert.Equal(true, _lamed.Types.List.String.Find_All(items, "tem", out itemsFound2));
            Assert.Equal(itemsFound, itemsFound2);

            Assert.Equal(false, _lamed.Types.List.String.Find_All(items, null, out itemsFound2));
            Assert.Equal(null, itemsFound2);
        }
    }
}
