using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.zz;

namespace LamedalCore.Test.Tests.lib.IO
{
    public static class IO_
    {
        /// <summary>Determines whether [is good folderOrFile format] [the specified folderOrFile].</summary>
        /// <param name="folderOrFile">The folderOrFile.</param>
        /// <param name="errorMsg">The error MSG.</param>
        /// <param name="testIfExist">if set to <c>true</c> [test if exist].</param>
        /// <returns></returns>
        public static bool IsGoodFolderOrFileFormat(string folderOrFile, out string errorMsg, bool testIfExist = true)
        {
            // Test for '\'
            errorMsg = "";
            if (folderOrFile.Contains(@"\"))
            {
                errorMsg = @"Error: Folder contains '\' characters. Folders should be of format '/'";
                return false;
            }

            // This is a file
            if (folderOrFile.Contains("."))
            {
                if (testIfExist && LamedalCore_.Instance.lib.IO.File.Exists(folderOrFile) == false)
                {
                    errorMsg = $"Error: '{folderOrFile}' does not exist!";
                    return false;
                }
                return true; // This is a file, no more tests required
            }

            // This is a folder
            if (folderOrFile.zSubStr_Right(1) != "/")
            {
                errorMsg = @"Error: Folder does not end with '/'";
                return false;
            }

            if (testIfExist && LamedalCore_.Instance.lib.IO.Folder.Exists(folderOrFile) == false)
            {
                errorMsg = $"Error: '{folderOrFile}' does not exist!";
                return false;
            }

            return true;
        }
    }
}
