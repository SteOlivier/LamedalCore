using System;
using System.Collections.Generic;
using System.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.Types;
using LamedalCore.Types.List;
using LamedalCore.zPublicClass;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.Types.List
{
    public enum List_Find_Data
    {
        Test1,
        Test_Value,
        Test2,
        Test3
    }

    public sealed class List_Find_Test: pcTest
    {
        public List_Find_Test(ITestOutputHelper debug = null) : base(debug) { }

        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library
        

        [Fact]
        [Test_Method("IsNullOrEmpty()")]
        public void IsNullOrEmpty_Test()
        {
            List<string> list1 = new List<string>();
            List<string> list2 = null;

            Assert.True(_lamed.Types.List.Find.IsNullOrEmpty(list1));
            Assert.True(_lamed.Types.List.Find.IsNullOrEmpty(list2));
        }
        

        [Fact]
        [Test_Method("In()")]
        public void Find_In_Test()
        {
            // In
            Assert.True(_lamed.Types.List.Find.In(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, "duplicate", true));
            Assert.False(_lamed.Types.List.Find.In(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, "duplicate", false));
            Assert.True(_lamed.Types.List.Find.In(new[] { 4, 2, 5, 6, 4 }, 5));
            Assert.False(_lamed.Types.List.Find.In(new[] { 4, 2, 5, 6, 4 }, 15, 20));
            Assert.True(_lamed.Types.List.Find.In(5, false, 4, 2, 5, 6, 4, 15));
            Assert.True(_lamed.Types.List.Find.In(5, false, 4, 2, 5, 6, 4, 15));

            Assert.True(_lamed.Types.List.Find.In("string1", false, "string1", "string2", "string3"));

            List_Find_Data test = List_Find_Data.Test3;
            Assert.True(_lamed.Types.List.Find.In(test, false, List_Find_Data.Test1, List_Find_Data.Test3));

            string testStr = null;
            Assert.Throws<ArgumentNullException>(() =>_lamed.Types.List.Find.In(testStr, false, "test1", "test2"));

            string[] testStrArray = null;
            testStr = "test";
            Assert.Throws<NullReferenceException>(()=>_lamed.Types.List.Find.In(testStr, false, testStrArray));

        }

        [Fact]
        [Test_Method("Contains()")]
        public void Contains_Test()
        {
            var toList = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }.ToList();
            Assert.True(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, toList.ToArray()));

            // List equal
            string errorMsg;
            Assert.True(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }.ToList(), toList, out errorMsg));
            Assert.True(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item2", "Duplicate", "Duplicate", "Item1", "new" }.ToList(), toList, out errorMsg));
            Assert.True(_lamed.Types.List.Find.Contains(new[] { "Item3_added", "Item3", "new2", "Item2", "Duplicate", "Duplicate", "Item1", "new" }.ToList(), toList, out errorMsg));

            // List not equal
            DebugLog("Contains() -> false tests:", true);
            Assert.False(_lamed.Types.List.Find.Contains(new[] { "Item3_added", "new2", "Item2", "Duplicate", "Duplicate", "Item1", "new" }.ToList(), toList, out errorMsg));
            DebugLog(errorMsg, true);
            Assert.True(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item2", "Duplicate", "Duplicate", "Item1", "new" }.ToList(), toList, out errorMsg));
            Assert.False(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item2", "Duplicate", "Duplicate", "Item_", "new" }.ToList(), toList, out errorMsg,true));
            DebugLog(errorMsg, true);
            Assert.False(_lamed.Types.List.Find.Contains(new[] { "Item3", "Item2", "Duplicate", "Itemnew1" }.ToList(), toList, out errorMsg));
            DebugLog(errorMsg,true);
        }

        [Fact]
        [Test_Method("Contains()")]
        public void Contains_Test2()
        {
            var items = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" };
            string[] n1 = null;
            string[] n2 = null;
            Assert.True(_lamed.Types.List.Find.Contains(n1, n2));
            Assert.False(_lamed.Types.List.Find.Contains(n1, items));
            Assert.False(_lamed.Types.List.Find.Contains(items, n2));
        }

        [Fact]
        [Test_Method("Identical()")]
        public void Identical_Test()
        {
            // Array equal
            var toList = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }.ToList();

            // List equal
            string errorMsg;
            Assert.Equal(true,_lamed.Types.List.Find.Identical(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }.ToList(), toList, out errorMsg));

            DebugLog("Identical() -> false tests", true);
            Assert.Equal(false,_lamed.Types.List.Find.Identical(new[] { "Item3", "Item2", "Duplicate", "Duplicate", "Item1" }.ToList(), toList, out errorMsg));
            DebugLog(errorMsg, true);
            Assert.Equal(false, _lamed.Types.List.Find.Identical(new[] { "Item3", "Item2", "Duplicate", "Item1" }.ToList(), toList, out errorMsg));
            DebugLog(errorMsg, true);

            // Exceptions
            Assert.Equal(false, _lamed.Types.List.Find.Identical(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }.ToList(), null, out errorMsg));
            Assert.Equal(false, _lamed.Types.List.Find.Identical(null, toList, out errorMsg));
            toList = null;
            Assert.Equal(true, _lamed.Types.List.Find.Identical(null, toList, out errorMsg));
        }

        [Fact]
        [Test_Method("Index_OfValue()")]
        public void Index_OfValue_Test()
        {
            int index;
            Assert.Equal(true, _lamed.Types.List.Find.Index_OfValue(new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" }, "Item2", out index));
            Assert.Equal(1, index);
        }
    }
}
