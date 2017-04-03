using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.lib.IO
{
    public sealed class IO_Parts_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Folder()")]
        [Test_Method("Folder_Temporary()")]
        [Test_Method("FolderAndFile()")]
        [Test_Method("File_RemoveExtention()")]
        [Test_Method("Ext()")]
        public void FolderFileAndExt_Test()
        {
            var file = "c:/folder1/folder2/folder4/file1.txt";
            var folder1 = "c:/folder1/folder2/folder4/";
            var folder2 = "c:/folder1/folder2/folder4";

            #region Folder tests
            Assert.Equal("c:/folder1/folder2/folder4/", _lamed.lib.IO.Parts.Folder(file));
            Assert.Equal("c:/folder1/folder2/folder4/", _lamed.lib.IO.Parts.Folder(folder1));
            Assert.Equal("c:/folder1/folder2/folder4/", _lamed.lib.IO.Parts.Folder(folder2));
            #endregion

            #region File
            Assert.Equal("file1.txt", _lamed.lib.IO.Parts.File(file));
            Assert.Equal("", _lamed.lib.IO.Parts.File(folder1));
            Assert.Equal("", _lamed.lib.IO.Parts.File(folder2));   // All files must have extentions
            #endregion

            #region FolderAndFile
            Assert.Equal("c:/folder1/folder2/folder4/file1", _lamed.lib.IO.Parts.FolderAndFile(file));
            Assert.Equal("c:/folder1/folder2/folder4/", _lamed.lib.IO.Parts.FolderAndFile(folder1));
            Assert.Equal("c:/folder1/folder2/folder4/", _lamed.lib.IO.Parts.FolderAndFile(folder2));   // All files must have extentions
            #endregion

            #region File_RemoveExtention
            Assert.Equal("file1", _lamed.lib.IO.Parts.File_RemoveExtention(file));
            Assert.Equal("", _lamed.lib.IO.Parts.File_RemoveExtention(folder1));
            Assert.Equal("", _lamed.lib.IO.Parts.File_RemoveExtention(folder2));   // All files must have extentions
            #endregion

            #region Ext
            Assert.Equal(".txt", _lamed.lib.IO.Parts.Ext(file));
            Assert.Equal("", _lamed.lib.IO.Parts.Ext(folder1));
            Assert.Equal("", _lamed.lib.IO.Parts.Ext(folder2));   // All files must have extentions
            #endregion
        }

        [Fact]
        [Test_Method("_Format2Slash()")]
        public void _Format2Slash_Test()
        {
            Assert.Equal(@"c:/folder1/folder2/file1.txt", _lamed.lib.IO.Parts._Format2Slash(@"c:\folder1\folder2\file1.txt"));
            Assert.Equal(@"c:/folder1/folder2/", _lamed.lib.IO.Parts._Format2Slash(@"c:\folder1\folder2"));
            Assert.Equal(@"c:/folder1/folder2.extra/folder4/", _lamed.lib.IO.Parts._Format2Slash(@"c:\folder1\folder2.extra\folder4"));
            Assert.Equal(@"c:/folder1/folder2.extra/folder4/file.txt", _lamed.lib.IO.Parts._Format2Slash(@"c:\folder1\folder2.extra\folder4\file.txt"));
        }

        [Fact]
        [Test_Method("FolderFileAndExt()")]
        [Test_Method("Ext_Change()")]
        [Test_Method("File_Change()")]
        [Test_Method("File_Add2Name()")]
        public void Ext_Change_Test()
        {
            // FolderFileAndExt
            string folder, file, ext;
            _lamed.lib.IO.Parts.FolderFileAndExt("c:/folder1/folder2/file1.doc", out folder, out file, out ext);
            Assert.Equal("c:/folder1/folder2/", folder);
            Assert.Equal("file1", file);
            Assert.Equal(".doc", ext);

            // Ext_Change
            Assert.Equal("c:/folder1/folder2/file1.doc", _lamed.lib.IO.Parts.Ext_Change("c:/folder1/folder2/file1.txt","doc"));

            // File_Change
            Assert.Equal("c:/folder1/folder2/file222.doc", _lamed.lib.IO.Parts.File_Change("c:/folder1/folder2/file1.txt", "file222.doc"));
            
            // File_Add2Name
            Assert.Equal("c:/folder1/folder2/file1222.txt", _lamed.lib.IO.Parts.File_Add2Name("c:/folder1/folder2/file1.txt", "222"));

            
        }
    }
}
