using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.lib.Console1;
using LamedalCore.zPublicClass;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.lib
{
    public sealed class lib_Console_IO_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        public lib_Console_IO_Test(ITestOutputHelper debug = null) : base(debug) { } 

        [Fact]
        [Test_Method("Table_FormatStr()")]
        public void Table_FormatStr_Test()
        {
            var line = "| test1 | test2 | test3 | test4 |\r\n---------------------------------";
            var header = _lamed.lib.Console.IO.Table_FormatStr(true, "test1", "test2", "test3", "test4");
            Assert.Equal(line, header);
            Assert.Equal("|test1  |test2  |test3  |test4  |",_lamed.lib.Console.IO.Table_FormatStr(false, "test1", "test2", "test3", "test4"));
        }

        [Fact]
        [Test_Method("()")]
        public void AboutMesagges_Test()
        {
            var about = _lamed.HelloWorld_();
            Assert.Equal("Hello, I am LamedaL. I am your helper library for open source cross platform applications.", about);

            // Lets just call these methods
            _lamed.HelloWorld_WriteLine();
            _lamed.About_();
            _lamed.About_WriteLine();
            _lamed.lib.About.Console_About();
            _lamed.lib.About.HelloWorld();
            _lamed.lib.About.MachineName();
            _lamed.lib.About.StackTrace();
            _lamed.lib.About.TickCount();
            _lamed.lib.About.Excel.About_Excel();
            _lamed.lib.About.ProcessorCount();
        }

        [Fact]
        [Test_Method("Menu_ReadKey()")]
        [Test_Method("WriteLine_HighLight()")]
        public void Menu_ReadKey_Test()
        {
            int selectedItem = 0;
            bool loopComplete = false;

            // Menu_ReadKey
            var key = new ConsoleKeyInfo('S', ConsoleKey.DownArrow, false, false, false);
            _lamed.lib.Console.IO.Menu_ReadKey1(key, 5, ref selectedItem,ref loopComplete);
            Assert.Equal(false, loopComplete);
            Assert.Equal(1, selectedItem);
            key = new ConsoleKeyInfo('W', ConsoleKey.UpArrow, false, false, false);
            _lamed.lib.Console.IO.Menu_ReadKey1(key, 5, ref selectedItem, ref loopComplete);
            Assert.Equal(false, loopComplete);
            Assert.Equal(0, selectedItem);
            key = new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false);
            _lamed.lib.Console.IO.Menu_ReadKey1(key, 5, ref selectedItem, ref loopComplete);
            Assert.Equal(true, loopComplete);
            Assert.Equal(0, selectedItem);

            // WriteLine_HighLight
            _lamed.lib.Console.IO.WriteLine_HighLight("Line1");
            _lamed.lib.Console.IO.WriteLine_HighLight("Line2", true);

        }

        [Fact]
        [Test_Method("Menu_Interactive()")]
        public void Menu_Interactive_Test()
        {
            Console_IO.TestCode1 = true;
            var item = _lamed.lib.Console.IO.Menu_Interactive("Item1", "Item2", "Item3", "Item4");
            Assert.Equal(1,item);
        }

 
    }
}
