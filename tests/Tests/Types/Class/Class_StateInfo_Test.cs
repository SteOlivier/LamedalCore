using System;
using System.Diagnostics;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zPublicClass;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.Types.Class
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.XUnitTestData)]
    public sealed class Class_StateInfo_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        public Class_StateInfo_Test(ITestOutputHelper debug = null) : base(debug) { }

        private object x1 = null;
        private object x2 = null;
        private Class_StateInfo_Data[] xObjects = new Class_StateInfo_Data[1000];
        private Class_StateInfo_Data[] yObjects = new Class_StateInfo_Data[1000];

        [Fact]
        public void State_Simple1()
        {
            // Init the state and test it
            #region Test1
            DebugLog("Test1");
            x1 = new object();
            x1.zObject().State_Set("hello");
            GarbageCollectAll();

            var value = x1.zObject().State_Get<string>();
            Assert.Equal("hello", value);
            #endregion

            #region Test2
            DebugLog("Test2");
            x2 = new Class_StateInfo_Data("Name", "Description");
            x2.zObject().State_Set(new Class_StateInfo_Data("Pieter", "Person name"));
            GarbageCollectAll();

            var state = x2.zObject().State_Get<Class_StateInfo_Data>();
            Assert.Equal("Pieter", state.Name);
            Assert.Equal("Person name", state.Description);
            #endregion
        }

        [Fact]
        public void State_Simple2()
        {
            State_Simple1();
            Assert.Equal("hello", x1.zObject().State_Get<string>());

            var wr2 = new WeakReference(x2);
            x2 = null;
            DebugLog("x2 has target? (true) = " + (wr2.Target != null));

            GarbageCollectAll();
            DebugLog("x2 has target? (false) = " + (wr2.Target != null));
        }

        [Fact]
        public void State_ManyTest()
        {
            #region Create objects
            DebugLog("Create 1000 objects");
            for (int ii = 0; ii < 1000; ii++)
            {
                xObjects[ii] = new Class_StateInfo_Data("Name"+ii,"Description"+ii);
                yObjects[ii] = new Class_StateInfo_Data("Property" + ii, "Value" + ii);
            }
            #endregion

            #region Assign objects
            var sw = new Stopwatch();
            sw.Start();
            for (int ii = 0; ii < 1000; ii++) xObjects[ii].zObject().State_Set(yObjects[ii]);
            sw.Stop();
            var time1 = sw.ElapsedTicks;
            DebugLog("Assignment of objects = " + time1);
            GarbageCollectAll();
            #endregion

            #region Normal check time
            sw = new Stopwatch();
            sw.Start();
            var r = new Random();
            for (int ii = 0; ii < 1000; ii++)
            {
                var jj = r.Next(1, 1000);
                Assert.Equal("Property" + jj, yObjects[jj].Name);
                Assert.Equal("Value" + jj, yObjects[jj].Description);
            }
            sw.Stop();
            var time2 = sw.ElapsedTicks;
            DebugLog("Normal check = " + time2);
            #endregion

            #region Assignment checks
            sw = new Stopwatch();
            sw.Start();
            for (int ii = 0; ii < 1000; ii++)
            {
                var jj = r.Next(1, 1000);
                var yObject = xObjects[jj].zObject().State_Get<Class_StateInfo_Data>();
                Assert.Equal("Property" + jj, yObject.Name);
                Assert.Equal("Value" + jj, yObject.Description);
            }
            sw.Stop();
            var time3 = sw.ElapsedTicks;
            DebugLog("Assignment checks = " + time3);
            #endregion

        }

        private void GarbageCollectAll()
        {
            DebugLog("--GC--");
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [Fact]
        public void StateComplete_Test()
        {
            #region Testdata
            // Assign new state to object and assign result values
            var y1 = new object();
            var y1_result = new Class_StateInfo_Data("Name1", "Description1");
            var y1_state = _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Data>(y1);
            y1_state.Name = "Name1";
            y1_state.Description = "Description1";

            var y2 = new object();
            var y2_result = new Class_StateInfo_Data("Name2", "Description2");
            var y2_state = _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Data>(y2, false);  // Note the false parameter - do not save the state
            Assert.Equal(null, y2_state);
            #endregion

            // Clear the memory
            y1_state = null;
            y2_state = null;
            GarbageCollectAll();

            // Test results for y1
            Class_StateInfo_Data y1_stateGet1 = _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Data>(y1);
            object y1_stateGet2 = _lamed.Types.Class.StateInfo.Key_Get(y1);
            string errorMsg;
            Assert.True(_lamed.lib.Test.ObjectsAreEqual(y1_stateGet1, y1_stateGet2, out errorMsg),errorMsg);  // state01_new1 == state01_new2
            Assert.True(_lamed.lib.Test.ObjectsAreEqual(y1_result, y1_stateGet1, out errorMsg),errorMsg);     // y1_result == y1_stateGet1
            Assert.True(_lamed.lib.Test.ObjectsAreEqual(y1_result, y1_stateGet2, out errorMsg),errorMsg);     // y1_result == y1_stateGet2

            // Test results for y2
            Class_StateInfo_Data y2_StateGet1 = _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Data>(y2);
            Assert.False(_lamed.lib.Test.ObjectsAreEqual(y2_result, y2_StateGet1, out errorMsg), errorMsg);  // y2_result != y2_StateGet1  [false parameter]

            #region Error condition
            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Test>(y1));

            // Re-assign property & retest error condition
            _lamed.Types.Class.StateInfo.Key_Set(y1, this);
            _lamed.Types.Class.StateInfo.Key_Get<Class_StateInfo_Test>(y1);  // No exception is thrown
            #endregion
        }
    }
}
