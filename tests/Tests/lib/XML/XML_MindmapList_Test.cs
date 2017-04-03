using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zPublicClass;
using LamedalCore.zz;
using Xunit;
using Xunit.Abstractions;

namespace LamedalCore.Test.Tests.lib.XML
{
    [BlueprintRule_Class(enBlueprintClassNetworkType.XUnitTestMethods)]
    public sealed class XML_MindmapList_Test : pcTest
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        public XML_MindmapList_Test(ITestOutputHelper debug = null) : base(debug) { } 

        [Fact]
        [Test_Method("TreeStrList_FromXML()")]
        public void XML_ToTreeStringList_Test1()
        {
            // Get test xml data
            string folder = Config_Info.Config_File_Test(_Debug, @"Text/mm/");
            string xml = _lamed.lib.IO.RW.File_Read2Str(folder + "test1.mm");

            #region Result expected
            var treeResult1 =
@"trunk
trunk:LaMedal
trunk:LaMedal:LaMedal.Access2System.csproj
trunk:LaMedal:LaMedal.Access2System.csproj:domain
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType):- 
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value):- Function to get the value of the enumerator.
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description):- Function to get the value of the enumerator.
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals:enClassNetworkType.cs";

            var treeResult2 =
@"1:trunk
2:trunk:LaMedal
3:trunk:LaMedal:LaMedal.Access2System.csproj
5:trunk:LaMedal:LaMedal.Access2System.csproj:domain
6:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes
9:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs
10:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType)
11:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType):- 
13:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs
14:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value)
15:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value):- Function to get the value of the enumerator.
16:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description)
17:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description):- Function to get the value of the enumerator.
23:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals
24:trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals:enClassNetworkType.cs";
            #endregion

            var nodeList1 = _lamed.lib.XML.Mindmap.TreeStrList_FromXML(xml);
            var nodeList2 = _lamed.lib.XML.Mindmap.TreeStrList_FromXML(xml, true);
            var nodeStr1 = nodeList1.zTo_Str("".NL());
            var nodeStr2 = nodeList2.zTo_Str("".NL());

            Assert.Equal(treeResult1, nodeStr1);
            Assert.Equal(treeResult2, nodeStr2);

            // This code was broken by the current changes to XML mindmap by adding the comment: To view this file, download...
            //XDocument xDoc = _lamed.lib.XML.Mindmap.TreeStrList_2XmlDocument(nodeList1);
            //var xml2 = xDoc.ToString();
            //Assert.Equal(xml, xml2);
        }

        [Fact]
        [Test_Method("TreeStrList_FromXML()")]
        public void XML_ToTreeStringList_Test2()
        {
            // Get test xml data
            string folder = Config_Info.Config_File_Test(_Debug, @"Text/mm/");
            string xml = _lamed.lib.IO.RW.File_Read2Str(folder + "test2.mm");

            #region Result expected
            var treeResult =
@"trunk
trunk:LaMedal
trunk:LaMedal:LaMedal.Core.csproj
trunk:LaMedal:LaMedal.Core.csproj:Blueprint_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:ActiveTemplate_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:ActiveTemplateEnd_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:BlueprintCodeInjection_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:CTIstate_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:CTIstateGlobal_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:enumValue_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:enumValue_Controller.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:TestedClass_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:TestedMethod_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Config
trunk:LaMedal:LaMedal.Core.csproj:domain:Config:Config_BlueprintCodeInjection.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Action.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Method.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enAttributeLocation.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_Day.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_DayShort.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_Month.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_MonthShort.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBCache_FilterType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBColumnType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBDataTableType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDTE_ClassScope.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enEnumerals_AsDataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enFreemindIcon.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enIconShell32Sizes.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enIOFileActionTime.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enResizeModifiers.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enRiskModal.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIstate_DialogChoices_EventArgs.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIState_DialogChoicesEventHandler.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIstate_ExitEventArgs.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIState_ExitEventHandler.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evDataTable_Commit_PostError.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evDataTable_Commit_Pre.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces:IBucket_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces:IStateEngineTransition.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:LookupWords_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:LookupWords_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Abbreviations_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Abbreviations_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Acronyms_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Acronyms_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Common_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Common_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_Objects.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_Verb.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:NewPart_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:NewPart_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Nonebreaking_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Nonebreaking_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Objects_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Objects_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Prefix_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Simple_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Simple_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Verbs_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Verbs_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Words_Other.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:State
trunk:LaMedal:LaMedal.Core.csproj:domain:State:stateCache_DataTableFileSetup.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:State:stateCache_DataTableFileSetup_Controller.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_ClassSimple.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_ClassState.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_enum.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_StructSimple.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:TypeExtensions
trunk:LaMedal:LaMedal.Core.csproj:domain:TypeExtensions:Dictionary_Serializer.cs
trunk:LaMedal:LaMedal.Core.csproj:lib
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_IPC.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_Mail.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_String.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_Zip.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Compress.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_File.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Folder.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Msg.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Parts.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_RW.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_RW_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Tests
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Tests:Abstract_RemoteDataObject_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_Mindmap.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_xDoc.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_DataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_Open.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_Rules.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_String.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_Build.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_Filter.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_FilterFields.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_GroupBy.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_Search.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_zShortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_Col.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_Key.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_DataRows.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_DataSet.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_IO.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_Relation.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_Hits.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_ValueDataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:BucketKeyType.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_Bucket.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords_Setup.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTable_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTableFile.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTableFile_Setup.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:CacheTest
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:CacheTest:Test_CacheData.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Old
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Old:State_Datatable_Connection.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_Datatable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_Datatable_State.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:lib2_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_ApplicationTools.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_Environment.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_Image.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Array.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Array_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Convert_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Enum.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Enum_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Generic.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Level.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Queue.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Shift.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Stack.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:Test
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:Test:Test_Data.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String__Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String_XML.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Color.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_DateTime.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Dictionary.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Enum.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Enum_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_EnumOfMan_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Generic.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Is.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Is_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Money.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Object.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_TimeSpan.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_To.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words:Words_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words:Words_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Check.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Contains.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Distance.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Move.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Other.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g3D_Trigometry.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_Ellipse.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_Line.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Arc.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Circle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Polygon.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Rectangle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Line.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Point.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Polygon.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Rectangle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:Spatial_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Assembly.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_ClassAttribute.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_ClassMethodMessage.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Resources.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Object2Str.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Test2.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:stateAssemblyFilters.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:system_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:shell32.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:shell32_FileInfo.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:user32.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.IO.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.List.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.Types.cs
trunk:LaMedal:LaMedal.Core.csproj:parts
trunk:LaMedal:LaMedal.Core.csproj:parts:AI
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine:AI_StateEngine_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine:AI_StateEngine_Activate_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBase_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBase_Cache.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBSetup_DataRows_Comparer.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBSetup_TableShredder.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:Dispose_BaseClass.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_BlueprintLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_BlueprintToolsLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_Default.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_MethodCallStackLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Injection_Config.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Injection_HackThisClass.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextAttribute.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextBoundObject.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextProperty.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_MessageSink.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:Factory_.cs
trunk:LaMedal:LaMedal.Core.csproj:zz
trunk:LaMedal:LaMedal.Core.csproj:zz:ArrayList_Blueprint.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Assembly_Blueprint.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:bool_Shortcut.cs.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:char_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataColumn_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRow_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRow_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRowCollection_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataSet_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataTable_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DateTime_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DB.DB_Search.Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Dictionary_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Enum_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Guid_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IEnumerable_string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IEnumerable_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_object_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:int_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:List_string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:List_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_XML_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:T_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Type_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z
trunk:LaMedal:LaMedal.Core.csproj:zz:z:DB.DBSetup.DataColumns.DataColumns_Key_z.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:string_zShortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zObjects.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zObjects_Extender.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zTypes_Extender.cs";
            #endregion

            var nodeList = _lamed.lib.XML.Mindmap.TreeStrList_FromXML(xml);
            var nodeStr = nodeList.zTo_Str("".NL());
            Assert.Equal(treeResult, nodeStr);

        }

        [Fact]
        [Test_Method("TreeStrList_2XmlDocument()")]
        public void XDoc_FromNodeStringList_Test1()
        {
            var nodeStr =
@"trunk
trunk:LaMedal
trunk:LaMedal:LaMedal.Access2System.csproj
trunk:LaMedal:LaMedal.Access2System.csproj:domain
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:BlueprintRule_Attribute.cs:public BlueprintRule_Attribute(enClassNetworkType classType):- 
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value):- Function to get the value of the enumerator.
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description)
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Attributes:Description_Controller.cs:public static bool zDescription(this Enum enumValue, out string Name, out string Description):- Function to get the value of the enumerator.
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals
trunk:LaMedal:LaMedal.Access2System.csproj:domain:Enumerals:enClassNetworkType.cs";

            List<string> nodeList = nodeStr.zConvert_Array_FromStr("".NL()).ToList();
            var XML1 = _lamed.lib.XML.Mindmap.TreeStrList_2XmlDocument(nodeList).ToString();
            var XML2 = _lamed.lib.XML.Mindmap.TreeStrList_2XmlDocument(nodeList,true).ToString();

            #region result
            var xml1Result=
@"<map version=""1.0.1"">
  <node ID=""ID_1"" TEXT=""trunk"" STYLE=""bubble"">
    <font NAME=""SansSerif"" BOLD=""true"" SIZE=""20"" />
    <icon BUILTIN=""gohome"" />
    <node ID=""ID_2"" TEXT=""LaMedal"">
      <icon BUILTIN=""folder"" />
      <node ID=""ID_3"" FOLDED=""true"" TEXT=""LaMedal.Access2System.csproj"">
        <font NAME=""SansSerif"" BOLD=""true"" SIZE=""16"" />
        <icon BUILTIN=""launch"" />
        <node ID=""ID_4"" FOLDED=""true"" TEXT=""domain"">
          <icon BUILTIN=""folder"" />
          <node ID=""ID_5"" TEXT=""Attributes"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_6"" FOLDED=""true"" TEXT=""BlueprintRule_Attribute.cs"">
              <icon BUILTIN=""idea"" />
              <node ID=""ID_7"" TEXT=""public BlueprintRule_Attribute(enClassNetworkType classType)"">
                <icon BUILTIN=""xmag"" />
                <node ID=""ID_8"" TEXT=""- "">
                  <icon BUILTIN=""help"" />
                </node>
              </node>
            </node>
            <node ID=""ID_9"" FOLDED=""true"" TEXT=""Description_Controller.cs"">
              <icon BUILTIN=""idea"" />
              <node ID=""ID_10"" TEXT=""public static bool zDescription(this Enum enumValue, out string Name, out string Description, out int value)"">
                <icon BUILTIN=""xmag"" />
                <node ID=""ID_11"" TEXT=""- Function to get the value of the enumerator."">
                  <icon BUILTIN=""help"" />
                </node>
              </node>
              <node ID=""ID_12"" TEXT=""public static bool zDescription(this Enum enumValue, out string Name, out string Description)"">
                <icon BUILTIN=""xmag"" />
                <node ID=""ID_13"" TEXT=""- Function to get the value of the enumerator."">
                  <icon BUILTIN=""help"" />
                </node>
              </node>
            </node>
          </node>
          <node ID=""ID_14"" TEXT=""Enumerals"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_15"" FOLDED=""true"" TEXT=""enClassNetworkType.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
        </node>
      </node>
    </node>
  </node>
</map>";
            #endregion

            Assert.Equal(xml1Result, XML1);
            Assert.Equal(xml1Result, XML2);

        }

        [Fact]
        [Test_Method("TreeStrList_2XmlDocument()")]
        public void XDoc_FromNodeStringList_Test2()
        {
            var nodeStr =
@"trunk
trunk:LaMedal
trunk:LaMedal:LaMedal.Core.csproj
trunk:LaMedal:LaMedal.Core.csproj:Blueprint_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:ActiveTemplate_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:ActiveTemplateEnd_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:BlueprintCodeInjection_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:CTIstate_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:CTIstateGlobal_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:enumValue_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:enumValue_Controller.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:TestedClass_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Attributes:TestedMethod_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Config
trunk:LaMedal:LaMedal.Core.csproj:domain:Config:Config_BlueprintCodeInjection.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Action.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Method.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enATemplate_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enAttributeLocation.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_Day.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_DayShort.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_Month.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDate_MonthShort.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBCache_FilterType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBColumnType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDBDataTableType.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enDTE_ClassScope.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enEnumerals_AsDataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enFreemindIcon.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enIconShell32Sizes.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enIOFileActionTime.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enResizeModifiers.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Enumerals:enRiskModal.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIstate_DialogChoices_EventArgs.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIState_DialogChoicesEventHandler.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIstate_ExitEventArgs.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evAIState_ExitEventHandler.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evDataTable_Commit_PostError.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Events:evDataTable_Commit_Pre.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces:IBucket_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Interfaces:IStateEngineTransition.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:LookupWords_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:LookupWords_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Abbreviations_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Abbreviations_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Acronyms_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Acronyms_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Common_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Common_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_Objects.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Modifiers_Verb.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:NewPart_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:NewPart_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Nonebreaking_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Nonebreaking_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Objects_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Objects_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Prefix_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Simple_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Simple_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Verbs_.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Verbs_.List.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Lookup:Words:Words_Other.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:State
trunk:LaMedal:LaMedal.Core.csproj:domain:State:stateCache_DataTableFileSetup.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:State:stateCache_DataTableFileSetup_Controller.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_ClassSimple.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_ClassState.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_enum.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:Test:Test_StructSimple.cs
trunk:LaMedal:LaMedal.Core.csproj:domain:TypeExtensions
trunk:LaMedal:LaMedal.Core.csproj:domain:TypeExtensions:Dictionary_Serializer.cs
trunk:LaMedal:LaMedal.Core.csproj:lib
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_IPC.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_Mail.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_String.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Comms_Zip.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Compress.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_File.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Folder.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Msg.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_Parts.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_RW.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:IO:IO_RW_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Tests
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:Tests:Abstract_RemoteDataObject_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_Mindmap.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Comms:XML:XML_xDoc.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_DataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_Open.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_Rules.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:Connection_String.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_Build.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_Filter.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_FilterFields.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Connection:SQL:SQL_GroupBy.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_Search.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:DB_zShortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_Col.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:Datacolumn:Datacolumn_Key.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_DataRows.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_DataSet.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_IO.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Setup:DBSetup_Relation.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_Hits.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_Value.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:Bucket_ValueDataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Bucket:BucketKeyType.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_Bucket.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords_Setup.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_BucketWords_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTable_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTableFile.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:Cache_DataTableFile_Setup.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:CacheTest
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Cache:CacheTest:Test_CacheData.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Old
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Old:State_Datatable_Connection.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_Datatable.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:DB:Table:Table_Datatable_State.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:lib2_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_ApplicationTools.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_Environment.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Rules_Image.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Array.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Array_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Convert_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Enum.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Enum_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Generic.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Level.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Queue.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Shift.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:List_Stack.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:Test
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:List:Test:Test_Data.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String__Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:String:String_XML.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Color.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_DateTime.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Dictionary.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Enum.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Enum_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_EnumOfMan_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Generic.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Is.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Is_Tests.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Money.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_Object.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_TimeSpan.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Types:Types_To.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words:Words_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Rules:Words:Words_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Check.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Contains.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Convert.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Distance.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Move.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g2D_Other.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:g3D_Trigometry.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_Ellipse.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersect_Line.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Arc.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Circle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Polygon.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Intersects:Intersects_Rectangle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Line.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Point.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Polygon.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:_2D:Shape:Shape_Rectangle.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:Spacial:Spatial_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Assembly.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_ClassAttribute.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_ClassMethodMessage.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Resources.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Object2Str.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:dotNet_Stream_Test2.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:dotNet:stateAssemblyFilters.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:system_.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:shell32.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:shell32_FileInfo.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:system:Win32:user32.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.IO.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.List.cs
trunk:LaMedal:LaMedal.Core.csproj:lib:zz.Types.cs
trunk:LaMedal:LaMedal.Core.csproj:parts
trunk:LaMedal:LaMedal.Core.csproj:parts:AI
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine:AI_StateEngine_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:AI:StateEngine:AI_StateEngine_Activate_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBase_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBase_Cache.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBSetup_DataRows_Comparer.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:DBase:DBSetup_TableShredder.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:Dispose_BaseClass.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_Attribute.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:ClassState:ClassState_Test.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_BlueprintLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_BlueprintToolsLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_Default.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Controller:Controller_MethodCallStackLogger.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Injection_Config.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:Injection_HackThisClass.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextAttribute.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextBoundObject.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_ContextProperty.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:dotNet:InjectionService:InjectionService_MessageSink.cs
trunk:LaMedal:LaMedal.Core.csproj:parts:Factory_.cs
trunk:LaMedal:LaMedal.Core.csproj:zz
trunk:LaMedal:LaMedal.Core.csproj:zz:ArrayList_Blueprint.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Assembly_Blueprint.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:bool_Shortcut.cs.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:char_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataColumn_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRow_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRow_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataRowCollection_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataSet_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DataTable_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DateTime_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:DB.DB_Search.Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Dictionary_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Enum_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Guid_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IEnumerable_string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IEnumerable_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_object_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:IList_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:int_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:List_string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:List_T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:string_XML_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:T_Array_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:T_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:Type_Shortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z
trunk:LaMedal:LaMedal.Core.csproj:zz:z:DB.DBSetup.DataColumns.DataColumns_Key_z.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:string_zShortcut.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zObjects.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zObjects_Extender.cs
trunk:LaMedal:LaMedal.Core.csproj:zz:z:zTypes_Extender.cs";

            List<string> nodeList = nodeStr.zConvert_Array_FromStr("".NL()).ToList();
            var XML1 = _lamed.lib.XML.Mindmap.TreeStrList_2XmlDocument(nodeList).ToString();
            //var XML2 = _lamed.lib.XML.Mindmap.TreeStrList_2XmlDocument(nodeList,true).ToString();

            var xml1Result =
@"<map version=""1.0.1"">
  <node ID=""ID_1"" TEXT=""trunk"" STYLE=""bubble"">
    <font NAME=""SansSerif"" BOLD=""true"" SIZE=""20"" />
    <icon BUILTIN=""gohome"" />
    <node ID=""ID_2"" TEXT=""LaMedal"">
      <icon BUILTIN=""folder"" />
      <node ID=""ID_3"" FOLDED=""true"" TEXT=""LaMedal.Core.csproj"">
        <font NAME=""SansSerif"" BOLD=""true"" SIZE=""16"" />
        <icon BUILTIN=""launch"" />
        <node ID=""ID_4"" FOLDED=""true"" TEXT=""Blueprint_.cs"">
          <icon BUILTIN=""idea"" />
        </node>
        <node ID=""ID_5"" FOLDED=""true"" TEXT=""domain"">
          <icon BUILTIN=""folder"" />
          <node ID=""ID_6"" TEXT=""Attributes"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_7"" FOLDED=""true"" TEXT=""ActiveTemplate_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_8"" FOLDED=""true"" TEXT=""ActiveTemplateEnd_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_9"" FOLDED=""true"" TEXT=""BlueprintCodeInjection_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_10"" FOLDED=""true"" TEXT=""CTIstate_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_11"" FOLDED=""true"" TEXT=""CTIstateGlobal_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_12"" FOLDED=""true"" TEXT=""enumValue_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_13"" FOLDED=""true"" TEXT=""enumValue_Controller.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_14"" FOLDED=""true"" TEXT=""TestedClass_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_15"" FOLDED=""true"" TEXT=""TestedMethod_Attribute.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_16"" TEXT=""Config"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_17"" FOLDED=""true"" TEXT=""Config_BlueprintCodeInjection.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_18"" TEXT=""Enumerals"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_19"" FOLDED=""true"" TEXT=""enATemplate_Action.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_20"" FOLDED=""true"" TEXT=""enATemplate_Method.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_21"" FOLDED=""true"" TEXT=""enATemplate_Value.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_22"" FOLDED=""true"" TEXT=""enAttributeLocation.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_23"" FOLDED=""true"" TEXT=""enDate_Day.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_24"" FOLDED=""true"" TEXT=""enDate_DayShort.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_25"" FOLDED=""true"" TEXT=""enDate_Month.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_26"" FOLDED=""true"" TEXT=""enDate_MonthShort.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_27"" FOLDED=""true"" TEXT=""enDBCache_FilterType.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_28"" FOLDED=""true"" TEXT=""enDBColumnType.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_29"" FOLDED=""true"" TEXT=""enDBDataTableType.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_30"" FOLDED=""true"" TEXT=""enDTE_ClassScope.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_31"" FOLDED=""true"" TEXT=""enEnumerals_AsDataTable.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_32"" FOLDED=""true"" TEXT=""enFreemindIcon.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_33"" FOLDED=""true"" TEXT=""enIconShell32Sizes.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_34"" FOLDED=""true"" TEXT=""enIOFileActionTime.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_35"" FOLDED=""true"" TEXT=""enResizeModifiers.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_36"" FOLDED=""true"" TEXT=""enRiskModal.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_37"" TEXT=""Events"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_38"" FOLDED=""true"" TEXT=""evAIstate_DialogChoices_EventArgs.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_39"" FOLDED=""true"" TEXT=""evAIState_DialogChoicesEventHandler.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_40"" FOLDED=""true"" TEXT=""evAIstate_ExitEventArgs.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_41"" FOLDED=""true"" TEXT=""evAIState_ExitEventHandler.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_42"" FOLDED=""true"" TEXT=""evDataTable_Commit_PostError.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_43"" FOLDED=""true"" TEXT=""evDataTable_Commit_Pre.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_44"" TEXT=""Interfaces"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_45"" FOLDED=""true"" TEXT=""IBucket_Value.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_46"" FOLDED=""true"" TEXT=""IStateEngineTransition.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_47"" TEXT=""Lookup"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_48"" FOLDED=""true"" TEXT=""LookupWords_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_49"" FOLDED=""true"" TEXT=""LookupWords_Test.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_50"" TEXT=""Words"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_51"" FOLDED=""true"" TEXT=""Abbreviations_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_52"" FOLDED=""true"" TEXT=""Abbreviations_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_53"" FOLDED=""true"" TEXT=""Acronyms_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_54"" FOLDED=""true"" TEXT=""Acronyms_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_55"" FOLDED=""true"" TEXT=""Common_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_56"" FOLDED=""true"" TEXT=""Common_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_57"" FOLDED=""true"" TEXT=""Modifiers_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_58"" FOLDED=""true"" TEXT=""Modifiers_Objects.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_59"" FOLDED=""true"" TEXT=""Modifiers_Verb.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_60"" FOLDED=""true"" TEXT=""NewPart_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_61"" FOLDED=""true"" TEXT=""NewPart_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_62"" FOLDED=""true"" TEXT=""Nonebreaking_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_63"" FOLDED=""true"" TEXT=""Nonebreaking_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_64"" FOLDED=""true"" TEXT=""Objects_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_65"" FOLDED=""true"" TEXT=""Objects_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_66"" FOLDED=""true"" TEXT=""Prefix_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_67"" FOLDED=""true"" TEXT=""Simple_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_68"" FOLDED=""true"" TEXT=""Simple_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_69"" FOLDED=""true"" TEXT=""Verbs_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_70"" FOLDED=""true"" TEXT=""Verbs_.List.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_71"" FOLDED=""true"" TEXT=""Words_Other.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_72"" TEXT=""State"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_73"" FOLDED=""true"" TEXT=""stateCache_DataTableFileSetup.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_74"" FOLDED=""true"" TEXT=""stateCache_DataTableFileSetup_Controller.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_75"" TEXT=""Test"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_76"" FOLDED=""true"" TEXT=""Test_ClassSimple.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_77"" FOLDED=""true"" TEXT=""Test_ClassState.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_78"" FOLDED=""true"" TEXT=""Test_enum.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_79"" FOLDED=""true"" TEXT=""Test_StructSimple.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_80"" TEXT=""TypeExtensions"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_81"" FOLDED=""true"" TEXT=""Dictionary_Serializer.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
        </node>
        <node ID=""ID_82"" FOLDED=""true"" TEXT=""lib"">
          <icon BUILTIN=""folder"" />
          <node ID=""ID_83"" TEXT=""Comms"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_84"" FOLDED=""true"" TEXT=""Comms_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_85"" FOLDED=""true"" TEXT=""Comms_IPC.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_86"" FOLDED=""true"" TEXT=""Comms_Mail.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_87"" FOLDED=""true"" TEXT=""Comms_String.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_88"" FOLDED=""true"" TEXT=""Comms_Zip.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_89"" TEXT=""IO"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_90"" FOLDED=""true"" TEXT=""IO_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_91"" FOLDED=""true"" TEXT=""IO_Compress.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_92"" FOLDED=""true"" TEXT=""IO_File.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_93"" FOLDED=""true"" TEXT=""IO_Folder.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_94"" FOLDED=""true"" TEXT=""IO_Msg.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_95"" FOLDED=""true"" TEXT=""IO_Parts.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_96"" FOLDED=""true"" TEXT=""IO_RW.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_97"" FOLDED=""true"" TEXT=""IO_RW_Tests.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_98"" TEXT=""Tests"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_99"" FOLDED=""true"" TEXT=""Abstract_RemoteDataObject_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_100"" TEXT=""XML"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_101"" FOLDED=""true"" TEXT=""XML_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_102"" FOLDED=""true"" TEXT=""XML_Mindmap.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_103"" FOLDED=""true"" TEXT=""XML_xDoc.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_104"" TEXT=""DB"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_105"" TEXT=""Connection"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_106"" FOLDED=""true"" TEXT=""Connection_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_107"" FOLDED=""true"" TEXT=""Connection_DataTable.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_108"" FOLDED=""true"" TEXT=""Connection_Open.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_109"" FOLDED=""true"" TEXT=""Connection_Rules.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_110"" FOLDED=""true"" TEXT=""Connection_String.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_111"" TEXT=""SQL"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_112"" FOLDED=""true"" TEXT=""SQL_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_113"" FOLDED=""true"" TEXT=""SQL_Build.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_114"" FOLDED=""true"" TEXT=""SQL_Filter.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_115"" FOLDED=""true"" TEXT=""SQL_FilterFields.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_116"" FOLDED=""true"" TEXT=""SQL_GroupBy.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
            </node>
            <node ID=""ID_117"" FOLDED=""true"" TEXT=""DB_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_118"" FOLDED=""true"" TEXT=""DB_Search.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_119"" FOLDED=""true"" TEXT=""DB_zShortcut.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_120"" TEXT=""Setup"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_121"" TEXT=""Datacolumn"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_122"" FOLDED=""true"" TEXT=""Datacolumn_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_123"" FOLDED=""true"" TEXT=""Datacolumn_Col.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_124"" FOLDED=""true"" TEXT=""Datacolumn_Key.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
              <node ID=""ID_125"" FOLDED=""true"" TEXT=""DBSetup_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_126"" FOLDED=""true"" TEXT=""DBSetup_DataRows.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_127"" FOLDED=""true"" TEXT=""DBSetup_DataSet.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_128"" FOLDED=""true"" TEXT=""DBSetup_IO.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_129"" FOLDED=""true"" TEXT=""DBSetup_Relation.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_130"" TEXT=""Table"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_131"" TEXT=""Cache"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_132"" TEXT=""Bucket"">
                  <icon BUILTIN=""folder"" />
                  <node ID=""ID_133"" FOLDED=""true"" TEXT=""Bucket_Hits.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                  <node ID=""ID_134"" FOLDED=""true"" TEXT=""Bucket_Value.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                  <node ID=""ID_135"" FOLDED=""true"" TEXT=""Bucket_ValueDataTable.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                  <node ID=""ID_136"" FOLDED=""true"" TEXT=""BucketKeyType.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                </node>
                <node ID=""ID_137"" FOLDED=""true"" TEXT=""Cache_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_138"" FOLDED=""true"" TEXT=""Cache_Bucket.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_139"" FOLDED=""true"" TEXT=""Cache_BucketWords.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_140"" FOLDED=""true"" TEXT=""Cache_BucketWords_Setup.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_141"" FOLDED=""true"" TEXT=""Cache_BucketWords_Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_142"" FOLDED=""true"" TEXT=""Cache_DataTable.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_143"" FOLDED=""true"" TEXT=""Cache_DataTable_Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_144"" FOLDED=""true"" TEXT=""Cache_DataTableFile.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_145"" FOLDED=""true"" TEXT=""Cache_DataTableFile_Setup.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_146"" TEXT=""CacheTest"">
                  <icon BUILTIN=""folder"" />
                  <node ID=""ID_147"" FOLDED=""true"" TEXT=""Test_CacheData.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                </node>
              </node>
              <node ID=""ID_148"" TEXT=""Old"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_149"" FOLDED=""true"" TEXT=""State_Datatable_Connection.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
              <node ID=""ID_150"" FOLDED=""true"" TEXT=""Table_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_151"" FOLDED=""true"" TEXT=""Table_Datatable.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_152"" FOLDED=""true"" TEXT=""Table_Datatable_State.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_153"" FOLDED=""true"" TEXT=""lib2_.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_154"" TEXT=""Rules"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_155"" FOLDED=""true"" TEXT=""Rules_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_156"" FOLDED=""true"" TEXT=""Rules_ApplicationTools.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_157"" FOLDED=""true"" TEXT=""Rules_Environment.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_158"" FOLDED=""true"" TEXT=""Rules_Image.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_159"" TEXT=""Types"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_160"" TEXT=""List"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_161"" FOLDED=""true"" TEXT=""List_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_162"" FOLDED=""true"" TEXT=""List_Array.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_163"" FOLDED=""true"" TEXT=""List_Array_Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_164"" FOLDED=""true"" TEXT=""List_Convert.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_165"" FOLDED=""true"" TEXT=""List_Convert_Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_166"" FOLDED=""true"" TEXT=""List_Enum.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_167"" FOLDED=""true"" TEXT=""List_Enum_Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_168"" FOLDED=""true"" TEXT=""List_Generic.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_169"" FOLDED=""true"" TEXT=""List_Level.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_170"" FOLDED=""true"" TEXT=""List_Queue.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_171"" FOLDED=""true"" TEXT=""List_Shift.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_172"" FOLDED=""true"" TEXT=""List_Stack.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_173"" TEXT=""Test"">
                  <icon BUILTIN=""folder"" />
                  <node ID=""ID_174"" FOLDED=""true"" TEXT=""Test_Data.cs"">
                    <icon BUILTIN=""idea"" />
                  </node>
                </node>
              </node>
              <node ID=""ID_175"" TEXT=""String"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_176"" FOLDED=""true"" TEXT=""String_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_177"" FOLDED=""true"" TEXT=""String__Tests.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_178"" FOLDED=""true"" TEXT=""String_XML.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
              <node ID=""ID_179"" FOLDED=""true"" TEXT=""Types_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_180"" FOLDED=""true"" TEXT=""Types_Color.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_181"" FOLDED=""true"" TEXT=""Types_DateTime.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_182"" FOLDED=""true"" TEXT=""Types_Dictionary.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_183"" FOLDED=""true"" TEXT=""Types_Enum.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_184"" FOLDED=""true"" TEXT=""Types_Enum_Tests.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_185"" FOLDED=""true"" TEXT=""Types_EnumOfMan_Test.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_186"" FOLDED=""true"" TEXT=""Types_Generic.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_187"" FOLDED=""true"" TEXT=""Types_Is.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_188"" FOLDED=""true"" TEXT=""Types_Is_Tests.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_189"" FOLDED=""true"" TEXT=""Types_Money.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_190"" FOLDED=""true"" TEXT=""Types_Object.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_191"" FOLDED=""true"" TEXT=""Types_TimeSpan.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_192"" FOLDED=""true"" TEXT=""Types_To.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_193"" TEXT=""Words"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_194"" FOLDED=""true"" TEXT=""Words_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_195"" FOLDED=""true"" TEXT=""Words_Convert.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_196"" TEXT=""Spacial"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_197"" TEXT=""_2D"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_198"" FOLDED=""true"" TEXT=""g2D_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_199"" FOLDED=""true"" TEXT=""g2D_Check.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_200"" FOLDED=""true"" TEXT=""g2D_Contains.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_201"" FOLDED=""true"" TEXT=""g2D_Convert.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_202"" FOLDED=""true"" TEXT=""g2D_Distance.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_203"" FOLDED=""true"" TEXT=""g2D_Move.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_204"" FOLDED=""true"" TEXT=""g2D_Other.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_205"" FOLDED=""true"" TEXT=""g3D_Trigometry.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_206"" TEXT=""Intersects"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_207"" FOLDED=""true"" TEXT=""Intersect_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_208"" FOLDED=""true"" TEXT=""Intersect_Ellipse.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_209"" FOLDED=""true"" TEXT=""Intersect_Line.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_210"" FOLDED=""true"" TEXT=""Intersects_Arc.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_211"" FOLDED=""true"" TEXT=""Intersects_Circle.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_212"" FOLDED=""true"" TEXT=""Intersects_Polygon.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_213"" FOLDED=""true"" TEXT=""Intersects_Rectangle.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
              <node ID=""ID_214"" TEXT=""Shape"">
                <icon BUILTIN=""folder"" />
                <node ID=""ID_215"" FOLDED=""true"" TEXT=""Shape_.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_216"" FOLDED=""true"" TEXT=""Shape_Line.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_217"" FOLDED=""true"" TEXT=""Shape_Point.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_218"" FOLDED=""true"" TEXT=""Shape_Polygon.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
                <node ID=""ID_219"" FOLDED=""true"" TEXT=""Shape_Rectangle.cs"">
                  <icon BUILTIN=""idea"" />
                </node>
              </node>
            </node>
            <node ID=""ID_220"" FOLDED=""true"" TEXT=""Spatial_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_221"" TEXT=""system"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_222"" TEXT=""dotNet"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_223"" FOLDED=""true"" TEXT=""dotNet_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_224"" FOLDED=""true"" TEXT=""dotNet_Assembly.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_225"" FOLDED=""true"" TEXT=""dotNet_ClassAttribute.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_226"" FOLDED=""true"" TEXT=""dotNet_ClassMethodMessage.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_227"" FOLDED=""true"" TEXT=""dotNet_Resources.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_228"" FOLDED=""true"" TEXT=""dotNet_Stream.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_229"" FOLDED=""true"" TEXT=""dotNet_Stream_Object2Str.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_230"" FOLDED=""true"" TEXT=""dotNet_Stream_Test.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_231"" FOLDED=""true"" TEXT=""dotNet_Stream_Test2.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_232"" FOLDED=""true"" TEXT=""stateAssemblyFilters.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_233"" FOLDED=""true"" TEXT=""system_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_234"" TEXT=""Win32"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_235"" FOLDED=""true"" TEXT=""shell32.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_236"" FOLDED=""true"" TEXT=""shell32_FileInfo.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_237"" FOLDED=""true"" TEXT=""user32.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_238"" FOLDED=""true"" TEXT=""zz.IO.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_239"" FOLDED=""true"" TEXT=""zz.List.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_240"" FOLDED=""true"" TEXT=""zz.Types.cs"">
            <icon BUILTIN=""idea"" />
          </node>
        </node>
        <node ID=""ID_241"" FOLDED=""true"" TEXT=""parts"">
          <icon BUILTIN=""folder"" />
          <node ID=""ID_242"" TEXT=""AI"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_243"" TEXT=""StateEngine"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_244"" FOLDED=""true"" TEXT=""AI_StateEngine_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_245"" FOLDED=""true"" TEXT=""AI_StateEngine_Activate_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_246"" TEXT=""DBase"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_247"" FOLDED=""true"" TEXT=""DBase_.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_248"" FOLDED=""true"" TEXT=""DBase_Cache.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_249"" FOLDED=""true"" TEXT=""DBSetup_DataRows_Comparer.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_250"" FOLDED=""true"" TEXT=""DBSetup_TableShredder.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
          <node ID=""ID_251"" FOLDED=""true"" TEXT=""Dispose_BaseClass.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_252"" TEXT=""dotNet"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_253"" TEXT=""ClassState"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_254"" FOLDED=""true"" TEXT=""ClassState_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_255"" FOLDED=""true"" TEXT=""ClassState_Attribute.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_256"" FOLDED=""true"" TEXT=""ClassState_Test.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_257"" TEXT=""Controller"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_258"" FOLDED=""true"" TEXT=""Controller_.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_259"" FOLDED=""true"" TEXT=""Controller_BlueprintLogger.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_260"" FOLDED=""true"" TEXT=""Controller_BlueprintToolsLogger.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_261"" FOLDED=""true"" TEXT=""Controller_Default.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_262"" FOLDED=""true"" TEXT=""Controller_MethodCallStackLogger.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
            <node ID=""ID_263"" FOLDED=""true"" TEXT=""Injection_Config.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_264"" FOLDED=""true"" TEXT=""Injection_HackThisClass.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_265"" TEXT=""InjectionService"">
              <icon BUILTIN=""folder"" />
              <node ID=""ID_266"" FOLDED=""true"" TEXT=""InjectionService_ContextAttribute.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_267"" FOLDED=""true"" TEXT=""InjectionService_ContextBoundObject.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_268"" FOLDED=""true"" TEXT=""InjectionService_ContextProperty.cs"">
                <icon BUILTIN=""idea"" />
              </node>
              <node ID=""ID_269"" FOLDED=""true"" TEXT=""InjectionService_MessageSink.cs"">
                <icon BUILTIN=""idea"" />
              </node>
            </node>
          </node>
          <node ID=""ID_270"" FOLDED=""true"" TEXT=""Factory_.cs"">
            <icon BUILTIN=""idea"" />
          </node>
        </node>
        <node ID=""ID_271"" FOLDED=""true"" TEXT=""zz"">
          <icon BUILTIN=""folder"" />
          <node ID=""ID_272"" FOLDED=""true"" TEXT=""ArrayList_Blueprint.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_273"" FOLDED=""true"" TEXT=""Assembly_Blueprint.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_274"" FOLDED=""true"" TEXT=""bool_Shortcut.cs.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_275"" FOLDED=""true"" TEXT=""char_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_276"" FOLDED=""true"" TEXT=""DataColumn_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_277"" FOLDED=""true"" TEXT=""DataRow_Array_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_278"" FOLDED=""true"" TEXT=""DataRow_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_279"" FOLDED=""true"" TEXT=""DataRowCollection_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_280"" FOLDED=""true"" TEXT=""DataSet_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_281"" FOLDED=""true"" TEXT=""DataTable_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_282"" FOLDED=""true"" TEXT=""DateTime_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_283"" FOLDED=""true"" TEXT=""DB.DB_Search.Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_284"" FOLDED=""true"" TEXT=""Dictionary_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_285"" FOLDED=""true"" TEXT=""Enum_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_286"" FOLDED=""true"" TEXT=""Guid_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_287"" FOLDED=""true"" TEXT=""IEnumerable_string_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_288"" FOLDED=""true"" TEXT=""IEnumerable_T_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_289"" FOLDED=""true"" TEXT=""IList_object_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_290"" FOLDED=""true"" TEXT=""IList_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_291"" FOLDED=""true"" TEXT=""IList_T_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_292"" FOLDED=""true"" TEXT=""int_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_293"" FOLDED=""true"" TEXT=""List_string_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_294"" FOLDED=""true"" TEXT=""List_T_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_295"" FOLDED=""true"" TEXT=""string_Array_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_296"" FOLDED=""true"" TEXT=""string_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_297"" FOLDED=""true"" TEXT=""string_XML_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_298"" FOLDED=""true"" TEXT=""T_Array_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_299"" FOLDED=""true"" TEXT=""T_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_300"" FOLDED=""true"" TEXT=""Type_Shortcut.cs"">
            <icon BUILTIN=""idea"" />
          </node>
          <node ID=""ID_301"" TEXT=""z"">
            <icon BUILTIN=""folder"" />
            <node ID=""ID_302"" FOLDED=""true"" TEXT=""DB.DBSetup.DataColumns.DataColumns_Key_z.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_303"" FOLDED=""true"" TEXT=""string_zShortcut.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_304"" FOLDED=""true"" TEXT=""zObjects.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_305"" FOLDED=""true"" TEXT=""zObjects_Extender.cs"">
              <icon BUILTIN=""idea"" />
            </node>
            <node ID=""ID_306"" FOLDED=""true"" TEXT=""zTypes_Extender.cs"">
              <icon BUILTIN=""idea"" />
            </node>
          </node>
        </node>
      </node>
    </node>
  </node>
</map>";

            Assert.Equal(xml1Result, XML1);
        }

        [Fact]
        [Test_Method("xDoc_NodeElementAdd()")]
        public void FreemindSample_Test()
        {
            var xDoc = new XDocument();
            XElement map = xDoc.zxDoc_Element_RootSet("map");
            map.zxDoc_Attribute_Set("version", "1.0.1");

            // Create the first element
            XElement root = _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(map, "Root", 1);
            root.zxDoc_Attribute_Set("STYLE", "bubble");

            // Create all the children
            var oupa1 = _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(root, "Oupa1", 20, true);
            _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(oupa1, "Pa1", 21);
            _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(oupa1, "Pa2", 22);

            _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(root, "Oupa2", 30);
            _lamed.lib.XML.Mindmap.xDoc_NodeElementAdd(root, "Oupa3", 40);

            var xml = xDoc.ToString();
            string folder = Config_Info.Config_File_Test(_Debug, @"Text/mm/");
            _lamed.lib.IO.RW.File_Write(folder + "testSample.mm", xml, true);

        }
    }
}
