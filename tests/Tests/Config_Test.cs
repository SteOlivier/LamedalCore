using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.zPublicClass;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests
{
    public sealed class Config_Test : pcTest 
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        public Config_Test(ITestOutputHelper debug = null) : base(debug) { }

        [Fact]
        [Test_Method("SampleMethod()")]
        public void SampleMethod_Test()
        {
            #region Test1: 
            // =================================================

            #endregion
        }

        [Theory]
        [InlineData(0), InlineData(1)]
        [Trait("Ticket", "723")]
        public void Test_Sample(int ii)
        {
            Assert.True(ii == 0 || ii == 1);
        }

        /// <summary>This method is called from other test methods.</summary>
        /// <param name="config">The configuration.</param>
        /// <param name="folderApplication">The folder application.</param>
        /// <returns></returns>
        [Test_Method("Test.ConfigSettings()")]
        [Test_Method("IO.Folder.Exists()")]
        public string Config_File_Test(out pcTest_Configuration config, out string folderApplication)
        {
            string folderTestCases;
            string configFile;
            var result = _lamed.lib.Test.ConfigSettings(out folderApplication, out folderTestCases, out config, out configFile);
            if (result == false)
            {
                DebugLog("Error with unit test folder settings!");
                DebugLog(" Please correct. (Opening the running folder and the 'Config.json' file).");
                DebugLog($"  + Excel test case folder: '{folderTestCases}'");
                DebugLog($"  + Application Folder    : '{folderApplication}'");
            }
            // Following will test if the config file can be loaded
            // ===========================================================
            Assert.True(_lamed.lib.IO.Folder.Exists(folderApplication), folderApplication);
            Assert.NotEqual("", folderTestCases);
            Assert.True(_lamed.lib.IO.Folder.Exists(folderTestCases), $"Error! Folder: '{folderTestCases}' does not exists.");

            return folderTestCases;
        }

        [Fact]
        [Test_Method("Execute_Explorer()")]
        public void ExploreToResult_Test()
        {
            _lamed.lib.Command.Execute_Explorer();  // Open the output forlder
        }
    }
}