using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LamedalCore.domain.Attributes;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.IO
{
    public sealed class IO_Folder_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Path_Assembly()")]
        [Test_Method("Path_Temporary()")]
        public void Folder_test()
        {
            // Just execute methods that can not be tested
            #region Path_Assembly
            var assembly = _lamed.lib.About.Assembly();
            var assemblyFolder = _lamed.lib.IO.Folder.Path_Assembly(assembly.GetName());
            string error;
            Assert.True(IO_.IsGoodFolderOrFileFormat(assemblyFolder,out error), error);
            Assert.True(_lamed.lib.IO.Folder.Exists(assemblyFolder), assemblyFolder);
            #endregion

            #region Path_Temporary
            var tempFolder = _lamed.lib.IO.Folder.Path_Temporary();
            Assert.True(IO_.IsGoodFolderOrFileFormat(tempFolder, out error), error);
            Assert.True(_lamed.lib.IO.Folder.Exists(tempFolder), tempFolder);
            #endregion
        }
        
        [Fact]
        [Test_Method("Exists()")]
        public void Exists_test()
        {
            Assert.False(_lamed.lib.IO.Folder.Exists(""));
            Assert.False(_lamed.lib.IO.Folder.Exists("FolderNameThatNotExist"));
        }

        [Fact]
        [Test_Method("Create()")]
        [Test_Method("Exists()")]
        [Test_Method("Delete()")]
        [Test_Method("IsFolder()")]
        [Test_Method("Folders()")]
        [Test_Method("AbsoluteFolderPath()")]
        public void Folder_Test()
        {

            #region Setup ] --------------------------------------------------------------------------------
            var appFolder = _lamed.lib.IO.Folder.Path_Application();
            var testFolder = appFolder + "TestFolder/";
            Folder_Create(testFolder);
            #endregion

            // Test
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test1/"));
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test2/"));
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test3/"));
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test4/Sub1/"));
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test4/Sub1/Sub2/"));
            Assert.True(_lamed.lib.IO.Folder.Exists(testFolder + "test4/"));
            // Is Folder
            Assert.True(_lamed.lib.IO.Folder.IsFolder(testFolder));
            Assert.True(_lamed.lib.IO.Folder.IsFolder(testFolder + "test1/"));
            // Search
            IList<string> folders1 = _lamed.lib.IO.Search.Folders(testFolder);
            IList<string> folders2 = _lamed.lib.IO.Search.Folders(testFolder, searchOption: SearchOption.AllDirectories);
            Assert.Equal(5, folders1.Count());
            Assert.Equal(testFolder + "test3/", folders1[3]);
            Assert.Equal(8, folders2.Count());
            Assert.Equal(testFolder + "test4/Sub1/Sub2/", folders2[7]);

            // Move & test
            _lamed.lib.IO.Folder.Move(testFolder + "test1/", testFolder + "test5/");
            Assert.False(_lamed.lib.IO.Folder.Exists(testFolder + "test1/"));
            Assert.True(_lamed.lib.IO.Folder.IsFolder(testFolder + "test5/"));
            // Absolute path
            string relativePath = @"../bling.txt";
            string baseDirectory = @"C:/blah1/blah2/";
            string absolutePath = _lamed.lib.IO.Folder.Path_Absolute(baseDirectory, relativePath);
            Assert.Equal("C:/blah1/bling.txt", absolutePath);

            #region Cleanup -------------------------------------------------------------------------------
            Folder_Cleanup(testFolder);
            Assert.False(_lamed.lib.IO.Folder.Exists(testFolder));
            Assert.False(_lamed.lib.IO.Folder.Exists(testFolder + "test1/"));
            #endregion
        }

        public static void Folder_Cleanup(string testFolder)
        {
            var lamed = LamedalCore_.Instance;
            if (lamed.lib.IO.Folder.Exists(testFolder)) lamed.lib.IO.Folder.Delete(testFolder, true);
        }

        public static void Folder_Create(string testFolder)
        {
            var lamed = LamedalCore_.Instance;
            if (lamed.lib.IO.Folder.Exists(testFolder)) lamed.lib.IO.Folder.Delete(testFolder, true);
            Assert.False(lamed.lib.IO.Folder.Exists(testFolder), testFolder);

            // Create folders ]-----------------------------------------------------------------------
            lamed.lib.IO.Folder.Create(testFolder + "test1/");
            lamed.lib.IO.Folder.Create(testFolder + "test1/"); // Redo step and no error is expected
            lamed.lib.IO.Folder.Create(testFolder + "test2/");
            lamed.lib.IO.Folder.Create(testFolder + "test3");
            lamed.lib.IO.Folder.Create(testFolder + "test4/");
            lamed.lib.IO.Folder.Create(testFolder + "test4/Sub1/Sub2/");
            lamed.lib.IO.Folder.Create(testFolder + "folder\\folder2");
        }
    }
}
