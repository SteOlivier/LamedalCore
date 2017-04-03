using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.Types;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.IO
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.XUnitTestMethods)]
    public sealed class IO_File_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("File_Write()")]
        [Test_Method("File_Read2Str()")]
        [Test_Method("Delete()")]
        [Test_Method("DeleteFiles()")]
        [Test_Method("Copy()")]
        [Test_Method("File_Read2Str()")]
        [Test_Method("Files()")]
        [Test_Method("FileInSubFolders()")]
        [Test_Method("IsFolder()")]
        [Test_Method("IsFile()")]
        public void File_Write_Test()
        {
            // Although here are many methods tested, it is better because the testcases are also created by these methods
            // Get the path
            var appFolder = _lamed.lib.IO.Folder.Path_Application();
            var testFolder = appFolder + "TestFolderIO/";
            IO_Folder_Test.Folder_Cleanup(testFolder);  // Make sure folder is clean
            IO_Folder_Test.Folder_Create(testFolder);

            #region File_Write
            var file = testFolder + "testFile.txt";
            //var ex1 =Assert.Throws<InvalidOperationException>(() => _lamed.lib.IO.RW.File_Write(file, "This is test line0", true, true));
            //Assert.Equal("Error! It is not possible to append and overwrite the file at the same time.", ex1.Message);

            // Overwrite twice
            _lamed.lib.IO.RW.File_Write(file, "This is test line1", false);
            Assert.Equal("This is test line1", _lamed.lib.IO.RW.File_Read2Str(file));
            Assert.Equal("6a3b0ffb40384ac41dd702307f16e1e2", _lamed.lib.IO.File.Hash_ToStr(file));

            _lamed.lib.IO.RW.File_Write(file, "This is test line2", true);
            Assert.Equal("This is test line2", _lamed.lib.IO.RW.File_Read2Str(file));
            Assert.Equal("a14d3fe730b2f308931327bede1cc479", _lamed.lib.IO.File.Hash_ToStr(file));
            Assert.Equal(true, _lamed.lib.IO.Folder.IsFolder(testFolder));
            Assert.Equal(false, _lamed.lib.IO.Folder.IsFolder(file));
            Assert.Equal(false, _lamed.lib.IO.File.IsFile(testFolder));
            Assert.Equal(true, _lamed.lib.IO.File.IsFile(file));
            #endregion

            #region FileAttributes
            _lamed.lib.IO.File.Attributes_Set(file, FileAttributes.Hidden);
            Assert.Equal(true,_lamed.lib.IO.File.Attributes_Check(file, FileAttributes.Hidden));
            #endregion

            #region Append to file
            _lamed.lib.IO.RW.File_Write(file, "This is test line3", enIOWriteAction.AppendFile);
            Assert.Equal("This is test line2".NL() + "This is test line3", _lamed.lib.IO.RW.File_Read2Str(file));
            #endregion

            #region Write to existing file
            var ex = Assert.Throws<InvalidOperationException>(()=>_lamed.lib.IO.RW.File_Write(file, "This is a test", enIOWriteAction.WriteFile));
            Assert.Equal("Error! Can not write to file because it already exists.", ex.Message);
            #endregion

            #region Copy & move
            // Copy & move
            _lamed.lib.IO.File.Copy(file, testFolder + "file2.txt");
            _lamed.lib.IO.File.Copy(file, testFolder + "file2.txt", true);
            var ex3 = Assert.Throws<IOException>(() =>_lamed.lib.IO.File.Copy(file, testFolder + "file2.txt"));
            Assert.Equal("The file 'D:/Dev/LaMedal/trunk/LamedalCore/LamedalCore.Test/bin/Debug/TestFolderIO/file2.txt' already exists.", ex3.Message);
            _lamed.lib.IO.File.Copy(file, testFolder + "file3.txt");
            _lamed.lib.IO.File.Copy(file, testFolder + "file4.txt");
            Assert.True(_lamed.lib.IO.File.Exists(testFolder + "file4.txt"));
            _lamed.lib.IO.File.Move(testFolder + "file4.txt", testFolder + "file5.txt");
            Assert.False(_lamed.lib.IO.File.Exists(testFolder + "file4.txt"));
            Assert.True(_lamed.lib.IO.File.Exists(testFolder + "file5.txt"));
            _lamed.lib.IO.File.Move(testFolder + "file5.txt", testFolder + "file5.txt");
            Assert.True(_lamed.lib.IO.File.Exists(testFolder + "file5.txt"));
            #endregion

            #region Find files
            // Files
            IList<string> files = _lamed.lib.IO.Search.Files(testFolder);
            Assert.Equal(4, files.Count);
            Assert.Equal(testFolder +"file2.txt", files[0]);

            // FileInSubFolders
            var testFolderList = new List<string> {testFolder};
            string folderAndFileFound;
            bool result = _lamed.lib.IO.Search.FileInSubFolders(testFolderList, "file2.txt", out folderAndFileFound);
            Assert.Equal(true, result);
            Assert.Equal("D:/Dev/LaMedal/trunk/LamedalCore/LamedalCore.Test/bin/Debug/TestFolderIO/file2.txt", folderAndFileFound);

            bool result2 = _lamed.lib.IO.Search.FileInSubFolders(testFolderList, "fileThatWillNotBeFound.txt", out folderAndFileFound);
            Assert.Equal(false, result2);
            Assert.Equal("", folderAndFileFound);

            // Empty file
            Assert.Throws<ArgumentNullException>(()=> _lamed.lib.IO.Search.FileInSubFolders(testFolderList, "", out folderAndFileFound));

            #endregion

            #region Delete the file twice
            Assert.Equal(true, _lamed.lib.IO.File.Exists(file));
            Assert.Equal(true, _lamed.lib.IO.File.Delete(file));
            Assert.Equal(false, _lamed.lib.IO.File.Exists(file));

            // Read from file that does not exist
            Assert.Equal("", _lamed.lib.IO.RW.File_Read2Str(file, false));
            var ex2 = Assert.Throws<FileNotFoundException>(() => _lamed.lib.IO.RW.File_Read2Str(file));
            Assert.Equal($"Could not find file '{file}'.", ex2.Message);

            // Delete more files
            var iTotal1 = _lamed.lib.IO.File.DeleteFiles(files);
            var iTotal2 = _lamed.lib.IO.File.DeleteFiles(testFolder);  
            Assert.Equal(3, iTotal1);
            Assert.Equal(0, iTotal2);
            #endregion

            // Cleanup
            IO_Folder_Test.Folder_Cleanup(testFolder);
            Assert.Equal(false, _lamed.lib.IO.File.Delete(file));

        }

        [Fact]
        [Test_Method("FilePath_Temporary()")]
        public void FilePath_Temporary_Test()
        {
            var file = _lamed.lib.IO.File.FilePath_Temporary();
            string error;
            Assert.True(IO_.IsGoodFolderOrFileFormat(file, out error),error);
        }

        [Fact]
        [Test_Method("Exists_GetNextNo()")]
        public void Exists_GetNextNo_Test()
        {
            var showTestFolder = false;   // If true, the folder where the files were created will be shown; else it will be removed after test.

            // Create testcases
            var appFolder = _lamed.lib.IO.Folder.Path_Application();
            var testFolder = appFolder + "TestFolderIO2/";
            IO_Folder_Test.Folder_Cleanup(testFolder);
            IO_Folder_Test.Folder_Create(testFolder);

            Assert.Equal(testFolder+"testFileThatIsTotallyUnuque.txt", _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFileThatIsTotallyUnuque.txt"));
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0001.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0002.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0003.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0005.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0007.txt", "This is a test", true);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0009.txt", "This is a test", true);

            Assert.Equal(testFolder + "testFile_0004.txt", _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt"));
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0004.txt", "This is a test", true);
            Assert.Equal(testFolder + "testFile_0006.txt", _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt"));
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0006.txt", "This is a test", true);
            Assert.Equal(testFolder + "testFile_0008.txt", _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt"));
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile_0008.txt", "This is a test", true);
            for (int ii = 0; ii < 100; ii++)
            {
                var file = _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt");
                _lamed.lib.IO.RW.File_Write(file, "This is a test", true);
            }
            Assert.Equal(testFolder + "testFile_0110.txt", _lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt"));

            Assert.Throws<ArgumentException>(()=>_lamed.lib.IO.File.Exists_GetNextNo(testFolder + "testFile.txt",jumpFactor:1));

            if (showTestFolder)
                 _lamed.lib.Command.Execute_Explorer(testFolder);
            else IO_Folder_Test.Folder_Cleanup(testFolder);

        }

        [Fact]
        [Test_Method("FormatNo()")]
        public void FormatCounter_Test()
        {
            Assert.Equal("c:/folder1/folder2/file_0001.txt", _lamed.lib.IO.File._FormatNo("c:/folder1/folder2/file.txt",1));
            Assert.Equal("c:/folder1/folder2/file_0015.txt", _lamed.lib.IO.File._FormatNo("c:/folder1/folder2/file.txt",15));
        }

        private Types_DateTime _time = LamedalCore_.Instance.Types.DateTime;

        [Fact]
        [Test_Method("Time()")]
        [Test_Method("IsFile()")]
        public void Time_Test()
        {
            var appFolder = _lamed.lib.IO.Folder.Path_Application();
            var testFolder = appFolder + "TestTime/";
            IO_Folder_Test.Folder_Cleanup(testFolder);
            IO_Folder_Test.Folder_Create(testFolder);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile.txt", "This is a test", true);

            #region time
            var time1 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.Creation);
            var time2 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.LastAccess);
            var time3 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.LastWrite);
            Assert.Equal(time1, time2);
            Assert.Equal(time2,time3);

            _lamed.lib.Command.Sleep(2100);
            _lamed.lib.IO.RW.File_Write(testFolder + "testFile.txt", "This is a new line", enIOWriteAction.AppendFile);
            var time4 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.Creation);
            var time5 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.LastAccess);
            var time6 = _lamed.lib.IO.File.Time(testFolder + "testFile.txt", enIOFileActionTime.LastWrite);
            Assert.Equal(time1, time4);
            Assert.Equal(time2, time5);
            Assert.NotEqual(time1, time6);
            #endregion

            // IsFile
            Assert.True(_lamed.lib.IO.File.IsFile(testFolder + "testFile.txt"));

            // Drive & FileInfo
            Assert.Equal("D:/", _lamed.lib.IO.Parts.Drive(testFolder + "testFile.txt"));


            IO_Folder_Test.Folder_Cleanup(testFolder);
        }

        [Fact]
        [Test_Method("Config_Load()")]
        public void Config_Load_Test()
        {
            var configTest = _lamed.lib.IO.File.Config_Load< IO_StateInfo_Data>("uniqueConfig", false);
            var configResult = _lamed.Types.Object.Create<IO_StateInfo_Data>();
            string error;
            Assert.True(_lamed.lib.Test.ObjectsAreEqual(configResult, configTest, out error), error);
        }
    }
}
