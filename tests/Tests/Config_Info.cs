using LamedalCore.zPublicClass;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests
{
    /// <summary>
    /// Create simple test hookup class
    /// </summary>
    public static class Config_Info
    {
        private static bool   _FirstTime = true;
        private static string _folderTestCases = "";
        private static string _folderApplication;
        private static pcTest_Configuration _config;
        private static ITestOutputHelper _Debug;

        /// <summary>Make sure the tests data folders are configured correctly.</summary>
        /// <param name="debug">The debug.</param>
        /// <param name="add2Path">The add2 path.</param>
        /// <returns>The test folder where the test data is located</returns>
        public static string Config_File_Test(ITestOutputHelper debug, string add2Path = "")
        {
            if (_FirstTime == false) return _folderTestCases + add2Path;  // Ensure that this method is only run once

            _Debug = debug;
            var test = new Config_Test(debug);
            _folderTestCases = test.Config_File_Test(out _config, out _folderApplication);
            _FirstTime = false;

            var result = _folderTestCases + add2Path;
            LamedalCore_.Instance.lib.IO.Folder.Create(result);
            return result;
        }

        
    }
}