using System.Collections.Generic;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.IO.IO_StateInfo;
using LamedalCore.Test.Tests.Types.Class;
using Xunit;

namespace LamedalCore.Test.Tests.lib.IO
{
    public sealed class IO_StateInfo_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        private static readonly object _Lock = new object();
        private static bool _firstTime = true;


        [Fact]
        public void IOStateInfoLevel1_SetupTest()
        {
            lock (_Lock)
            {
                if (_firstTime)
                {
                    _lamed.lib.IO.StateInfo.Reset(); // Cleanup state information once
                    _firstTime = false;
                }
            }

            // Setup the StateInfo if not exits
            var person = new IO_StateInfo_Data();
            IO_StateInfo_RW1 _infoPerson = _lamed.lib.IO.StateInfo.Level1;
            _infoPerson = _lamed.lib.IO.StateInfo.Level1; // Reload all

            _infoPerson.Data_Load("Person", person); // Load the data
            person.Name = "Cobus";
            person.Surname = "Olivier";

            // Save the data =======================================================
            _infoPerson.Data_Save("Person", person, true);
            // =====================================================================

            var folder = _lamed.lib.IO.Folder.Path_Application();
            var file = folder + "StateInfo_lvl1.json";
            Assert.True(_lamed.lib.IO.File.Exists(file));
        }


        [Fact]
        [Test_Method("Data_Load()")]
        [Test_Method("Data_Get()")]
        public void IOStateInfoLevel1_LoadTest()
        {
            IOStateInfoLevel1_SetupTest();

            #region Test1: Data_Load
            // =======================================
            var person = new IO_StateInfo_Data();
            var _infoPerson = _lamed.lib.IO.StateInfo.Level1;
            _infoPerson.Data_Load("Person", person);  // Load the data
            Assert.Equal(person.Name, "Cobus");
            Assert.Equal(person.Surname, "Olivier");
            #endregion

            #region Test2: Json.Object_Set
            // =======================================
            var person2 = new IO_StateInfo_Data();
            Assert.Equal(person2.Name, null);
            Assert.Equal(person2.Surname, null);
            var personStr = _infoPerson.State.Data_Get("Person");
            if (personStr != "") _lamed.lib.IO.Json.Object_Set(person2, personStr);
            Assert.Equal(person2.Name, "Cobus");
            Assert.Equal(person2.Surname, "Olivier");
            #endregion

            _infoPerson.State.Data_Remove("Test");
            Assert.Equal("",_infoPerson.State.Data_Get("Test"));

            _infoPerson.State.Data_Add("Test", "jsonStr");
            Assert.Equal("jsonStr", _infoPerson.State.Data_Get("Test"));
            _infoPerson.State.Data_Remove("Test");
            Assert.Equal("", _infoPerson.State.Data_Get("Test"));
        }

        [Fact]
        [Test_Method("Data_Load()")]
        [Test_Method("Data_Save()")]
        public void IOStateInfoLevel2_SetupTest()
        {
            IOStateInfoLevel1_SetupTest(); // Setup the StateInfo if not exits

            #region Test: Load the data =================================
            var person = new IO_StateInfo_Data();
            var _infoPerson1 = _lamed.lib.IO.StateInfo.Level1;
            _infoPerson1.Data_Load("Person", person);  // Load the data
            Assert.Equal(person.Name, "Cobus");
            Assert.Equal(person.Surname, "Olivier");

            var person2 = new IO_StateInfo_Data();
            Assert.Null(person2.Name);
            Assert.Null(person2.Surname);
            person2.Name = "Cobus";
            person2.Surname = "Olivier";

            var person2a = new IO_StateInfo_Data();
            var _infoPerson2 = _lamed.lib.IO.StateInfo.Level2;
            _infoPerson2.Data_Load("Level1", "Person", person2a);  // Load the data class & set values if not exist
            Assert.NotEqual(person2, person2a);

            var person3 = new IO_StateInfo_Data();
            _infoPerson2.Data_Load("Level2", "Person", person3);  // Load new data class & set values if not exist
            person3.Name = "Piet";
            person3.Surname = "Pompies";

            //var lassie = new IO_StateInfo_Data();
            var lassie = new Types_ClassInfo_Dog_Bulldog(10);
            _infoPerson2.Data_Load("Level2", "Dog", lassie);  // Load the data from another object
            lassie.Health = 5;
            lassie.OwnderName = "Jan";

            // Add a few simple tests
            _infoPerson2.State.Data_Add("Heading123", "Level123", "jsonString");
            Assert.Equal("jsonString",_infoPerson2.State.Data_Get("Heading123", "Level123"));

            var names = _infoPerson2.State.lvl1_Names("Level2");
            List<string> namesResult = new List<string>() {"Person", "Dog"};
            Assert.Equal(namesResult, names);
            #endregion

            #region Test: Save the data =======================================================
            _infoPerson1.Data_Save("Person", person, true);
            _infoPerson2.Data_Save("Level1", "Person", person, true);
            _infoPerson2.Data_Save("Level2", "Person", person3, true);
            _infoPerson2.Data_Save("Level2", "Dog", lassie, true);
            // =====================================================================

            var folder = _lamed.lib.IO.Folder.Path_Application();
            var file = folder + "StateInfo_lvl2.json";
            Assert.True(_lamed.lib.IO.File.Exists(file));
            #endregion
        }
    }
}
