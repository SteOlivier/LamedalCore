using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib.XML
{
    public sealed class XML_xDoc_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Document_FixXML()")]
        public void Document_FixXML_Test()
        {
            Assert.Equal("<doc></doc>", _lamed.lib.XML.Setup.Fix_XMLErrorRootElements(null));

            var documentation = "<summary>Return the CTI network enumeral classification like CTI / CTI_Link etc.</summary>".NL()
                                + "<param name=\"classCodeStr\">The class code string.</param>".NL()
                                + "<returns>bool</returns>".NL()
                                + "<code>CTIM_Ignore;</code>";

            var documentationFix = "<doc>".NL() +
                                   "<summary>Return the CTI network enumeral classification like CTI / CTI_Link etc.</summary>".NL()
                                   + "<param name=\"classCodeStr\">The class code string.</param>".NL()
                                   + "<returns>bool</returns>".NL()
                                   + "<code>CTIM_Ignore;</code>".NL() + "</doc>";

            var result = _lamed.lib.XML.Setup.Fix_XMLErrorRootElements(documentation);
            Assert.Equal(documentationFix, result);  // Test for root node
            Assert.Equal(documentationFix, _lamed.lib.XML.Setup.Fix_XMLErrorRootElements("///" + documentation));  // If there is a comment line fix it
        }

        [Fact]
        [Test_Method("xDoc_Attribute_Set()")]
        public void xDoc_Attribute_Test()
        {
            // <remarks DefaultType="string"; IgnoreGroup="Array"></remarks>
            // ============================================
            string xml = "<Doc></Doc>";

            XDocument xDoc = xml.zxDoc_Document();
            XElement element = xDoc.zxDoc_Element_("code");
            element.zxDoc_Attribute_Set("DefaultType", "string");
            element.zxDoc_Attribute_Set("Group", "groupname");

            element.zxDoc_Element_Set(true, "IgnoreGroup", "RemoveGroupPath");

            element.zxDoc_Element_Set("CTI", false);
            element.zxDoc_Element_Set("CTI_Link", false);
            element.zxDoc_Element_Set("value set", true);

            element.zxDoc_Attribute_Set("CTI_Status", "status_value");

            #region results
            var xmlResult1 = 
@"<Doc>
  <code DefaultType=""string"" Group=""groupname"" CTI_Status=""status_value"">IgnoreGroup;RemoveGroupPath;value set;</code>
</Doc>";
            var xmlResult2 =
@"<Doc>
  <code DefaultType=""string"" Group=""groupname"" CTI_Status=""status_value"">IgnoreGroup;RemoveGroupPath;</code>
</Doc>";
            #endregion
            xml = xDoc.ToString();
            Assert.Equal(xmlResult1, xml);

            element.zxDoc_Element_Set("value set", false);
            Assert.Equal(xmlResult2, xDoc.ToString());

            element.zxDoc_Element_Set("", false);           // Do nothing
        }

        [Fact]
        [Test_Method("zxDoc_Element_()")]
        [Test_Method("zxDoc_Attribute_AsStr()")]
        [Test_Method("zxDoc_Element_AsStr()")]
        public void zxDoc_Element_Test()
        {
            // <remarks DefaultType="string"; IgnoreGroup="Array"></remarks>
            // ============================================
            string xml =
@"<Doc>
  <code DefaultType=""string"" Group=""groupname"" CTI_Status=""status_value"">IgnoreGroup;RemoveGroupPath;value set;</code>
</Doc>";

            XDocument xDoc = xml.zxDoc_Document();
            XElement element = xDoc.zxDoc_Element_("code");
            Assert.Equal("string", element.zxDoc_Attribute_AsStr("DefaultType"));
            Assert.Equal("groupname", element.zxDoc_Attribute_AsStr("Group"));

            string codeValue = element.zxDoc_Element_AsStr();
            Assert.True(codeValue.Contains("IgnoreGroup"));
            Assert.True(codeValue.Contains("RemoveGroupPath"));
            Assert.Equal("status_value", element.zxDoc_Attribute_AsStr("CTI_Status"));

            Assert.Equal("",_lamed.lib.XML.xDoc.Attribute_AsStr(null, "test"));

            element = xDoc.zxDoc_Element_("code1");  // The element is not found -> auto add it
            Assert.Equal("code1", element.Name);
            Assert.Equal("",_lamed.lib.XML.xDoc.Element_AsStr(null));
        }

        [Fact]
        [Test_Method("Element_AsStr()")]
        [Test_Method("Element_()")]
        [Test_Method("Attribute_AsStr()")]
        public void xDoc_Test()
        {
            var xml =
            @"<weather time-layout='k-p24h-n7-1'>
              <name>Weather Type, Coverage, and Intensity</name>
              <weather-conditions weather-summary='Mostly Sunny'/>
            </weather>";

            XDocument doc = _lamed.lib.XML.xDoc.Document(xml);
            // name
            XElement nameElement = _lamed.lib.XML.xDoc.Element_(doc, "name");
            Assert.Equal("Weather Type, Coverage, and Intensity", _lamed.lib.XML.xDoc.Element_AsStr(nameElement));

            // weather-conditions
            XElement conditionsElement = _lamed.lib.XML.xDoc.Element_(doc, "weather-conditions");
            Assert.Equal("", _lamed.lib.XML.xDoc.Element_AsStr(conditionsElement));
            Assert.Equal("Mostly Sunny", _lamed.lib.XML.xDoc.Attribute_AsStr(conditionsElement, "weather-summary"));
        }

        [Fact]
        [Test_Method("Document()")]
        public void Document_Test()
        {
            var doc = _lamed.lib.XML.xDoc.Document("", true);
            Assert.Equal("<doc></doc>", doc.ToString());

            // Exceptions
            Assert.Throws<XmlException>(()=> _lamed.lib.XML.xDoc.Document("<doc></dc>"));
        }

        [Fact]
        [Test_Method("Document()")]
        [Test_Method("Element_RootSet()")]
        [Test_Method("Element_Root()")]
        [Test_Method("Element_()")]
        [Test_Method("Element_AsStr()")]
        public void Element_Test()
        {
            XDocument xDoc = _lamed.lib.XML.xDoc.Document("");
            XElement root = _lamed.lib.XML.xDoc.Element_RootSet(xDoc, "root");
            Assert.Equal("doc", root.Name);

            _lamed.lib.XML.xDoc.Element_RootUpdate(xDoc, "root");
            Assert.Equal("root", root.Name);
            Assert.Equal("root", _lamed.lib.XML.xDoc.Element_Root(xDoc).Name);

            // Element_
            XElement element = _lamed.lib.XML.xDoc.Element_("", "doc", autoFix:true);
            var elementStr = _lamed.lib.XML.xDoc.Element_AsStr("", "doc", autoFix:true);
            Assert.Equal("doc",element.Name);
            Assert.Equal("", elementStr);

            // Element_Add
            _lamed.lib.XML.xDoc.Element_(element, "Heading1");
            _lamed.lib.XML.xDoc.Element_Add(element, "Heading2");
            elementStr = _lamed.lib.XML.xDoc.Element_AsStr(element);
            Assert.Equal("", elementStr);
            Assert.Equal("<doc><Heading1 /><Heading2 /></doc>", element.ToString(SaveOptions.DisableFormatting));
            
            Assert.Throws<InvalidOperationException>(()=>_lamed.lib.XML.xDoc.Element_Add(null, "Heading1"));
        }

        [Fact]
        [Test_Method("Attribute_Set()")]
        [Test_Method("Attribute_AsStr()")]
        [Test_Method("Attribute_()")]
        public void Attribute_Test()
        {
            XElement element = _lamed.lib.XML.xDoc.Element_("", "doc", autoFix: true);
            _lamed.lib.XML.xDoc.Attribute_Set(element, "Heading", "XML document");
            var xml = element.ToString(SaveOptions.DisableFormatting);
            Assert.Equal("<doc Heading=\"XML document\"></doc>", xml);
            Assert.Equal("XML document", _lamed.lib.XML.xDoc.Attribute_AsStr(element,"Heading"));
            Assert.Equal("XML document", _lamed.lib.XML.xDoc.Attribute_(element,"Heading").Value);
            Assert.Equal("XML document", _lamed.lib.XML.xDoc.Attribute_(element.Document,"doc", "Heading").Value);
            Assert.Equal("XML document", _lamed.lib.XML.xDoc.Attribute_(xml, "doc", "Heading").Value);

            List<XAttribute> attributes = _lamed.lib.XML.xDoc.List_Attributes(element);
            List<XElement> elements = _lamed.lib.XML.xDoc.List_Elements(element.Document);
            Assert.Equal("Heading", attributes[0].Name);
            Assert.Equal("doc", elements[0].Name);

            // Remove attribute
            _lamed.lib.XML.xDoc.Attribute_Set(element, "Heading", "");  // This will remove the attribute
            var xml2 = element.ToString(SaveOptions.DisableFormatting);
            Assert.Equal("<doc></doc>", xml2);

        }
    }
}
