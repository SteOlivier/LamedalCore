using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.XML
{
    public sealed class XML_Mindmap_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("()")]
        public void MindmapSimple_Test()
        {
            var testFolder = @"C:/test/mm/";

            #region input
            var input =
@"MindMapEdit
MindMapEdit:Heading1
MindMapEdit:Heading2
MindMapEdit:Heading3
Link->MindMapEdit:Heading1|->MindMapEdit:Heading2
Link->MindMapEdit:Heading3|->MindMapEdit:Heading1";
            #endregion

            #region result
            var result =
@"<map version=""1.0.1"">
<!-- To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net -->
<node CREATED=""1488370135477"" ID=""ID_1078424613"" MODIFIED=""1488373532710"" STYLE=""bubble"" TEXT=""MindMapEdit"">
<node CREATED=""1488370594089"" ID=""ID_1962664020"" MODIFIED=""1488373904362"" POSITION=""right"" TEXT=""Heading1"">
<arrowlink DESTINATION=""ID_1112146242"" ENDARROW=""Default"" ENDINCLINATION=""23;0;"" ID=""Arrow_ID_1162243404"" STARTARROW=""None"" STARTINCLINATION=""23;0;""/>
<linktarget COLOR=""#b0b0b0"" DESTINATION=""ID_1962664020"" ENDARROW=""Default"" ENDINCLINATION=""46;0;"" ID=""Arrow_ID_1587029454"" SOURCE=""ID_1092339353"" STARTARROW=""None"" STARTINCLINATION=""46;0;""/>
<font BOLD=""true"" NAME=""SansSerif"" SIZE=""12""/>
</node>
<node CREATED=""1488370602169"" ID=""ID_1112146242"" MODIFIED=""1488373899771"" POSITION=""right"" TEXT=""Heading2"">
<linktarget COLOR=""#b0b0b0"" DESTINATION=""ID_1112146242"" ENDARROW=""Default"" ENDINCLINATION=""23;0;"" ID=""Arrow_ID_1162243404"" SOURCE=""ID_1962664020"" STARTARROW=""None"" STARTINCLINATION=""23;0;""/>
</node>
<node CREATED=""1488373524082"" ID=""ID_1092339353"" MODIFIED=""1488373904362"" POSITION=""right"" TEXT=""Heading3"">
<arrowlink DESTINATION=""ID_1962664020"" ENDARROW=""Default"" ENDINCLINATION=""46;0;"" ID=""Arrow_ID_1587029454"" STARTARROW=""None"" STARTINCLINATION=""46;0;""/>
<font BOLD=""true"" NAME=""SansSerif"" SIZE=""12""/>
</node>
</node>
</map>";
            #endregion

            MindMap_Create(input.zConvert_Str_ToListStr("".NL()));

        }

        public string MindMap_Create(List<string> treeDefinitionList)
        {
            pcMindMap map = CreateMindmap();


            return "";
        }



        [Fact]
        [Test_Method("CreateMindmap()")]
        public void CreateMindmap_Test()
        {
            var testFolder = @"C:/test/mm/";

            // Create the map xml doc
            var map = CreateMindmap();
            var mapStr = map.mm.ToString();
            var result =
@"<map version=""1.0.1"">
  <!-- To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net -->
</map>";

            Assert.Equal(result, mapStr);

            // Create the root element
            var result2 =
@"<map version=""1.0.1"">
  <!-- To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net -->
  <node ID=""ID_1"" TEXT=""MindMapEdit"" STYLE=""bubble"">
    <font NAME=""SansSerif"" BOLD=""true"" SIZE=""20"" />
    <icon BUILTIN=""gohome"" />
  </node>
</map>";

            CreateRoot(map, "MindMapEdit");
            var mapStr2 = map.mm.ToString();
            Assert.Equal(result2, mapStr2);

            _lamed.lib.IO.RW.File_Write(testFolder + "simple1.mm", mapStr2, true);

        }

        /// <summary>Creates the root element of the mindmap.</summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public pcMindMap CreateMindmap(string version = "1.0.1")
        {
            // Create the map element
            var xDoc = new XDocument();
            XElement mm = xDoc.zxDoc_Element_RootSet("map");
            mm.zxDoc_Attribute_Set("version", version);
            Element_SetComment(mm," To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net ");
            var result = new pcMindMap(mm);
            return result;
        }


        /// <summary>Creates the root element.</summary>
        /// <param name="map">The map.</param>
        /// <param name="nodeName">Name of the node.</param>
        public void CreateRoot(pcMindMap map, string nodeName)
        {
            // Create the first element
            var id = 1;
            var nodeElement = xDoc_NodeElementAdd(map.mm, nodeName, id++);
            nodeElement.zxDoc_Attribute_Set("STYLE", "bubble");
            map.mmDictionary.Add(nodeName, nodeElement);
        }

        /// <summary>Sets the element value. The value can be added and removed.</summary>
        /// <param name="element">The element</param>
        /// <param name="newComment">The new value</param>
        public void Element_SetComment(XElement element, string newComment)
        {
            var comment = new XComment(newComment);            
            element.Add(comment);
        }

        /// <summary>Adds the node element to the parent element.</summary>
        public XElement xDoc_NodeElementAdd(XElement parentElement, string value, int id, bool right = false, bool folded = false)
        {
            if (parentElement == null) throw new ArgumentNullException(nameof(parentElement));

            // parent
            var parentStr = parentElement.zxDoc_Attribute_AsStr("TEXT");
            if (parentStr.Contains(".csproj") || value.Contains(".cs")) folded = true;   // Fold first level elements

            // node
            var element = parentElement.zxDoc_Element_Add("node");
            element.zxDoc_Attribute_Set("ID", "ID_" + id.zTo_Str().Trim());
            if (right) element.zxDoc_Attribute_Set("POSITION", "right");
            if (folded) element.zxDoc_Attribute_Set("FOLDED", "true");
            element.zxDoc_Attribute_Set("TEXT", value);

            // Icon
            var icon = enFreemindIcon.folder;
            if (id == 1) 
            {
                icon = enFreemindIcon.gohome;

                //<font BOLD="true" NAME="SansSerif" SIZE="20"/>
                XElement_AddFont(element, "SansSerif", 20, true);
            }
            else if (value.Contains(".csproj"))
            {
                icon = enFreemindIcon.launch;
                // <font BOLD="true" NAME="SansSerif" SIZE="16"/>
                XElement_AddFont(element, "SansSerif", 16, true);
            }
            else if (value.zContains_Any(".cs", ".doc", ".docx", ".xlsx", ".pptx", ".avi", ".flv", ".pdf", ".ppt", ".png",
                ".chm", ".gui", "*.jpg")) icon = enFreemindIcon.idea;
            else if (value.zContains_All("(", ")")) icon = enFreemindIcon.xmag;
            else if (value.Contains("- ")) icon = enFreemindIcon.help;

            XElement_AddIcon(element, icon);

            return element;
        }

        private static void XElement_AddFont(XElement element, string fontName, int size, bool bold = false)
        {
            // Adds font to the node
            var fontElement = element.zxDoc_Element_Add("font");
            fontElement.zxDoc_Attribute_Set("NAME", fontName);
            if (bold) fontElement.zxDoc_Attribute_Set("BOLD", "true");
            fontElement.zxDoc_Attribute_Set("SIZE", size.zTo_Str());
        }

        private void XElement_AddIcon(XElement element, enFreemindIcon icon)
        {
            var iconElement = element.zxDoc_Element_Add("icon");
            iconElement.zxDoc_Attribute_Set("BUILTIN", (icon.zTo_Description()));
        }

    }
}
