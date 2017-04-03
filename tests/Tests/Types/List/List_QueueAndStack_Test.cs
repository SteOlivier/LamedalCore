using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types.List
{
    public sealed class List_QueueAndStack_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Enqueue()")]
        [Test_Method("Dequeue()")]
        [Test_Method("TryDequeue()")]
        public void Enqueue_Test()
        {
            var list = new List<string>();

            #region Enqueue & Dequeue
            _lamed.Types.List.Queue.Enqueue(list, "A");
            _lamed.Types.List.Queue.Enqueue(list, "B");
            _lamed.Types.List.Queue.Enqueue(list, "C");
            _lamed.Types.List.Queue.Enqueue(list, "D");
            Assert.Equal(new List<string> {"A", "B", "C", "D"}, list);

            // Dequeue
            Assert.Equal("A",_lamed.Types.List.Queue.Peek(list));
            Assert.Equal("A",_lamed.Types.List.Queue.Dequeue(list));
            Assert.Equal("B",_lamed.Types.List.Queue.Dequeue(list));
            Assert.Equal("C",_lamed.Types.List.Queue.Dequeue(list));
            Assert.Equal("D",_lamed.Types.List.Queue.Peek(list));
            Assert.Equal("D",_lamed.Types.List.Queue.Dequeue(list));

            Assert.Equal(null, _lamed.Types.List.Queue.Peek(list));
            Assert.Equal(null, _lamed.Types.List.Queue.Dequeue(list));
            #endregion

            #region Exceptions
            // Dequeue
            list = null;
            var ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Queue.Dequeue(list));
            var errorMsg = "Value cannot be null.".NL() + "Parameter name: list";
            Assert.Equal(errorMsg, ex.Message);

            // Enqueue
            ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Queue.Enqueue(list, "A"));
            Assert.Equal(errorMsg, ex.Message);

            // Peek
            ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Queue.Peek(list));
            Assert.Equal(errorMsg, ex.Message);
            #endregion
        }

        [Fact]
        [Test_Method("Push()")]
        [Test_Method("Pop()")]
        public void Push_Test()
        {
            var list = new List<string>();

            #region Push & Pop
            _lamed.Types.List.Stack.Push(list, "A");
            _lamed.Types.List.Stack.Push(list, "B");
            _lamed.Types.List.Stack.Push(list, "C");
            _lamed.Types.List.Stack.Push(list, "D");
            Assert.Equal(new List<string> { "A", "B", "C", "D" }, list);

            Assert.Equal("D", _lamed.Types.List.Stack.Peek(list));
            Assert.Equal("D", _lamed.Types.List.Stack.Pop(list));
            Assert.Equal("C", _lamed.Types.List.Stack.Pop(list));
            Assert.Equal("B", _lamed.Types.List.Stack.Pop(list));
            Assert.Equal("A", _lamed.Types.List.Stack.Peek(list));
            Assert.Equal("A", _lamed.Types.List.Stack.Pop(list));
            Assert.Equal(null, _lamed.Types.List.Stack.Pop(list));
            Assert.Equal(null, _lamed.Types.List.Stack.Peek(list));
            #endregion

            #region Exceptions
            list = null;
            // Push
            var ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Stack.Push(list, "A"));
            var errorMsg = "Value cannot be null.".NL() + "Parameter name: list";
            Assert.Equal(errorMsg, ex.Message);

            // Pop
            ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Stack.Pop(list));
            Assert.Equal(errorMsg, ex.Message);

            // Peek
            ex = Assert.Throws<ArgumentNullException>(() => _lamed.Types.List.Stack.Peek(list));
            Assert.Equal(errorMsg, ex.Message);

            #endregion
        }


    }
}
