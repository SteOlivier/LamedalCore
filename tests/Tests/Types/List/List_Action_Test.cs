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
    public sealed class List_Action_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library


        [Fact]
        [Test_Method("AddRange()")]
        [Test_Method("Shuffle()")]
        public void AddRange_Test()
        {
            // AddRange
            var list = new List<int>{1,2};
            _lamed.Types.List.Action.AddRange(list, 5, 4, 8, 4, 2);
            Assert.Equal(new List<int> { 1, 2, 5, 4, 8, 4, 2 }, list);

            // Shuffle
            var listShuffle = _lamed.Types.List.Action.Shuffle(new List<int> { 1, 2, 5, 4, 8, 4, 2 }).ToList();
            Assert.True(_lamed.Types.List.Find.Contains(new List<int> { 1, 2, 5, 4, 8, 4, 2 }, listShuffle));
            Assert.NotEqual(new List<int> { 1, 2, 5, 4, 8, 4, 2 }, listShuffle);
            Assert.False(_lamed.Types.List.Find.Identical(new List<int> { 1, 2, 5, 4, 8, 4, 2 }, listShuffle));

            // Exception
            list = null;
            IEnumerable<int> result = _lamed.Types.List.Action.Shuffle(list);
            Assert.Throws<ArgumentNullException>(() => Assert.Equal(new List<int>(), result));
        }

        [Fact]
        [Test_Method("MoveElements()")]
        public void MoveElements_Test()
        {
            var items = new[] { "Item3", "Item2", "Duplicate1", "Item1", "Duplicate2" };

            _lamed.Types.List.Action.MoveElements(items, 2, 2);
            Assert.Equal(new[] { "Item3", "Item2", "Duplicate1", "Item1", "Duplicate2" }, items);

            _lamed.Types.List.Action.MoveElements(items, 2, 4);
            var moveResult = new[] { "Item3", "Item2", "Duplicate2", "Item1", "Duplicate1" };
            Assert.Equal(moveResult, items);

            _lamed.Types.List.Action.MoveElements(moveResult, 4, 2);
            var moveResult2 = new[] { "Item3", "Item2", "Duplicate1", "Item1", "Duplicate2" };
            Assert.Equal(moveResult2, moveResult);
        }

        [Fact]
        [Test_Method("Merge()")]
        public void Merge_Test()
        {
            var items = new[] { 4, 2, 5, 6, 4 }; // 4, 2, 5, 6, 4 
            var items2 = new[] { 2, 4, 5, 6 };  // 2, 4, 5, 6

            var mergedItems = _lamed.Types.List.Action.Merge(items.ToList(), items2.ToList());
            Assert.True(_lamed.Types.List.Find.Contains(new[] { 4, 2, 5, 6, 4, 2, 4, 5, 6 }, mergedItems));
        }

        [Fact]
        [Test_Method("Unique()")]
        [Test_Method("Sort()")]
        [Test_Method("ToLower()")]
        public void UniqueAndSort_Test()
        {
            #region Unique
            var items1 = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate", "item1" };
            Assert.Equal(new[] { "Item3", "Item2", "Duplicate", "Item1", "item1" }, _lamed.Types.List.Action.Unique(items1).ToArray());
            Assert.Equal(new[] { "Duplicate", "item1", "Item1", "Item2", "Item3" }, _lamed.Types.List.Action.Unique(items1, enSort.Ascending).ToArray());
            Assert.Equal(new[] { "Item3","Item2","Item1","Duplicate"}, _lamed.Types.List.Action.Unique(items1, enSort.Descending, true).ToArray());
            Assert.Equal(null, _lamed.Types.List.Action.Unique(null, enSort.Ascending, true));

            LamedalCore_[] itemsT = {_lamed, _lamed};
            Assert.Equal(new List<LamedalCore_> {_lamed}, _lamed.Types.List.Action.Unique(itemsT));
            itemsT = null;
            Assert.Equal(null, _lamed.Types.List.Action.Unique(itemsT));
            #endregion

            // Sort
            var items2 = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate", "Item1" };
            Assert.Equal(new[] { "Duplicate", "Duplicate", "Item1", "Item1", "Item2", "Item3" }, _lamed.Types.List.Action.Sort(items2).ToArray());

            // ToLower
            var items3 = new[] { "Item3", "Item2", "Duplicate", "Item1", "Duplicate" };
            Assert.Equal(new[] {"item3", "item2", "duplicate", "item1", "duplicate"} , _lamed.Types.List.String.ToLower(items3).ToArray());
        }

        [Fact]
        [Test_Method("Copy_To<T>()")]
        [Test_Method("Copy_From()")]
        [Test_Method("Copy_FromArray()")]
        public void Copy_Test()
        {
            List<string> nullList = null;
            var fromList = new List<string> { "A", "B", "C", "D" };

            #region Copy_To
            // Partial copy and do not clear toList
            // Full copy
            var toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList);
            Assert.Equal(new List<string> { "A", "B", "C", "D" }, toList);

            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false);
            Assert.Equal(new List<string> { "E", "A", "B", "C", "D" }, toList);
            #endregion

            #region Sub-list copy
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false, 1, 2);
            Assert.Equal(new List<string> {"E", "B", "C"}, toList);

            // End index too great
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false, 1, 12);
            Assert.Equal(new List<string> { "E", "B", "C","D" }, toList);

            // Start & end index too great
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false, 12, 12);
            Assert.Equal(new List<string> { "E", "D" }, toList);

            // Start index too small
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false, -5, 12);
            Assert.Equal(new List<string> { "E", "A", "B", "C","D" }, toList);

            // Start & end index too small
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, false, -5, -5);
            Assert.Equal(new List<string> { "E", "A", "B", "C", "D" }, toList);

            // Partial copy and clear toList
            toList = new List<string> { "E" };
            _lamed.Types.List.Action.Copy_To(fromList, toList, true, 1, 2);
            Assert.Equal(new List<string> { "B", "C"}, toList);
            #endregion

            // Exceptions
            Assert.Throws<ArgumentNullException>(()=>_lamed.Types.List.Action.Copy_To(fromList, nullList));
            Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Action.Copy_To(nullList, toList));

            #region Copy_From
            _lamed.Types.List.Action.Copy_From(toList, fromList);
            Assert.Equal(new List<string> { "A", "B", "C", "D" }, toList);

            _lamed.Types.List.Action.Copy_FromArray(toList, true, "A", "B");
            Assert.Equal(new List<string> { "A", "B" }, toList);
            #endregion
        }
    }
}
