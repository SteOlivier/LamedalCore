using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.Types;
using LamedalCore.Types.List;
using Xunit;

namespace LamedalCore.Test.Tests.Types.List
{
    public sealed class List_Convert_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Int_ToStrRanges()")]
        public void Int_ToStrRanges_Test()
        {
            Assert.Equal(new List<string> { "1-3", "7", "10-12" }, _lamed.Types.List.Convert.Int_ToStrRanges(1,2,3,7,10,11,12));
            Assert.Equal(new List<string> { "1", "5", "7" }, _lamed.Types.List.Convert.Int_ToStrRanges(1,5,7));
            Assert.Equal(new List<string>(), _lamed.Types.List.Convert.Int_ToStrRanges());
        }

        [Fact]
        [Test_Method("IList_2IListT()")]
        [Test_Method("IListT_2ListT()")]
        public void IList_2IListT_Test()
        {
            IList list = new List<string>() {"A", "B", "C"};
            IList<string> list2 = _lamed.Types.List.Convert.IList_2IListT<string>(list);
            List<string> list3 = _lamed.Types.List.Convert.IListT_2ListT<string>(list2);
            List<string> list4 = _lamed.Types.List.Convert.IList_2ListT<string>(list);

            IList list_Test = new List<string>() { "A", "B", "C" };
            IList<string> list_Test2 = new List<string> { "A", "B", "C" };
            List<string> list_Test3 = new List<string> { "A", "B", "C" };

            Assert.Equal(list_Test, list);
            Assert.Equal(list_Test2, list2);
            Assert.Equal(list_Test3, list3);
            Assert.Equal(list_Test3, list4);
        }

        [Fact]
        [Test_Method("IListObject_2IListT()")]
        public void IListObject_2IListT_Test()
        {
            IList<object> list_object = new List<object>() { "A", "B", "C" };
            IList<string> list_String = _lamed.Types.List.Convert.IListObject_2IListT<string>(list_object);
            Assert.Equal(new List<string> { "A", "B", "C" }, list_String);
        }

        [Fact]
        [Test_Method("Array_2TwoDimensionArray()")]
        public void Array_2TwoDimensionArray_Test()
        {
            string[] list = {"A", "B", "C", "D"};
            string[,] list2D = _lamed.Types.List.Convert.Array_2TwoDimensionArray(list, 2);
            string[,] list2D_ = new string[2,2];
            list2D_[0, 0] = "A";
            list2D_[0, 1] = "B";
            list2D_[1, 0] = "C";
            list2D_[1, 1] = "D";

            Assert.Equal(list2D_, list2D);
        }

        [Fact]
        [Test_Method("IList_FromEnum()")]
        [Test_Method("enum_2IList()")]
        [Test_Method("Identical()")]
        public void To_IList_Tests()
        {
            var testList1 = new List<string>();
            _lamed.Types.List.Convert.IList_FromEnum(testList1, typeof(List_Convert_Data));
            var testList2 = new List<string>() {"Test1", "Test_Value"};

            Assert.Equal(true, _lamed.Types.List.Find.Identical(testList1, testList2));
            Assert.Equal(testList2, testList1);

            // Exceptions
            testList1 = null;
            Assert.Throws<ArgumentNullException>(()=>_lamed.Types.List.Convert.IList_FromEnum(testList1, typeof(List_Convert_Data)));
        }
    }
}
