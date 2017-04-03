using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.Words;
using LamedalCore.Test.Tests._Data;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_Dictionary_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Object_ToDictionary()")]
        public void Object_ToDictionary_Test()
        {
            // Test data
            var json = new TestData_Json();
            json.Active = true;
            json.CreatedDate = new DateTime(2016,1,1);
            json.Email = "email";
            json.Roles.Clear();
            json.Roles.Add("Mr");
            json.Roles.Add("Mev");
            json.Roles.Add("Mej");

            // Stream the object
            var dict = _lamed.Types.Dictionary.Object_ToDictionary(json);
            var lines = dict.Select(x => $"{x.Key} = {x.Value.zObject().AsStr()}").ToList();
            var str = lines.zTo_Str(", ");

            // Test
            Assert.Equal("Email = email, Active = true, CreatedDate = 2016-01-01 12:00:00 AM, Roles = [Mr,Mev,Mej], Name = json_Test_Data", str);
            Assert.Equal("Email = email, Active = true, CreatedDate = 2016-01-01 12:00:00 AM, Roles = [Mr,Mev,Mej], Name = json_Test_Data", json.zObject().AsStr());
        }

        [Fact]
        [Test_Method("Key_AddSafe()")]
        public void GetOrCreate_Test()
        {
            var myDict1 = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}, {"key3", "value3"}};

            // Key_AddSafe & ignore
            Assert.Equal("value1", myDict1["key1"]);
            _lamed.Types.Dictionary.Key_AddSafe(myDict1, "key1", "value111");
            Assert.Equal("value1", myDict1["key1"]);

            // replace
            _lamed.Types.Dictionary.Key_AddSafe(myDict1, "key1", "value111", enDuplicateError.Replace);
            Assert.Equal("value111", myDict1["key1"]);

            // error
            Assert.Throws<ArgumentException>(() => _lamed.Types.Dictionary.Key_AddSafe(myDict1, "key1", "value111", enDuplicateError.Error));
        }

        [Fact]
        [Test_Method("KeysAndValues()")]
        public void KeysAndValues_Test()
        {
            var myDict1 = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}, {"key3", "value3"}};
            List<string> keys, values;
            _lamed.Types.Dictionary.KeysAndValues(myDict1, out keys, out values);

            var resultKeys = new List<string> { "key1", "key2", "key3" };
            var resultValues = new List<string> { "value1", "value2", "value3" };
            Assert.Equal(resultKeys, keys);
            Assert.Equal(resultValues, values);
        }

        [Fact]
        [Test_Method("SortOnValue()")]
        public void SortOnValue_Test()
        {
            // Data
            var myDict = new Dictionary<string, int> {{"key1", 4}, {"key2", 3}, {"key3", 2},   {"key4", 1} };

            // Result
            var result = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("key4", 1),
                new KeyValuePair<string, int>("key3", 2),
                new KeyValuePair<string, int>("key2", 3),
                new KeyValuePair<string, int>("key1", 4)
            };

            var resultDesc = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("key1", 4),
                new KeyValuePair<string, int>("key2", 3),
                new KeyValuePair<string, int>("key3", 2),
                new KeyValuePair<string, int>("key4", 1)
            };

            // Test
            Assert.Equal(result, _lamed.Types.Dictionary.SortOnValue(myDict));
            Assert.Equal(resultDesc, _lamed.Types.Dictionary.SortOnValue(myDict,false));
        }

        [Fact]
        [Test_Method("Create_IgnoreCase()")]
        public void Create_IgnoreCase_Test()
        {
            var myDict = new Dictionary<string, int> {{"key1", 4}, {"KEY2", 3}, {"Key3", 2},   {"keY4", 1} };
            var myDictIgnoreCase = _lamed.Types.Dictionary.Create_IgnoreCase(myDict);
            var myDictIgnoreCase2 = _lamed.Types.Dictionary.Create_IgnoreCase<int>();
            myDictIgnoreCase2.Add("key1", 1);
            myDictIgnoreCase2.Add("key2", 2);

            Assert.Equal(false, myDict.ContainsKey("Key1"));
            Assert.Equal(true, myDictIgnoreCase.ContainsKey("Key1"));
            Assert.Equal(true, myDictIgnoreCase.ContainsKey("KEY1"));
            Assert.Equal(true, myDictIgnoreCase.ContainsKey("keY1"));

            Assert.Equal(true, myDictIgnoreCase2.ContainsKey("Key1"));
            Assert.Equal(true, myDictIgnoreCase2.ContainsKey("KEY1"));
            Assert.Equal(true, myDictIgnoreCase2.ContainsKey("keY1"));
        }

        [Fact]
        [Test_Method("Merge()")]
        public void Merge_Test()
        {
            var myDict1 = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}, {"key3", "value3"}};
            var myDict2 = new Dictionary<string, string> {{"key1", "value1"}, {"key4", "value4"}, {"key5", "value5"}};
            var myDict3 = new Dictionary<string, string> {{"key1", "value1"}, {"key4", "value4"}, {"key6", "value6"}};
            var myResult = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}, {"key3", "value3"}, { "key4", "value4" }, { "key5", "value5" }, { "key6", "value6" } };

            Assert.Equal(myResult, _lamed.Types.Dictionary.Merge(myDict1, myDict2, myDict3)); 
        }

        [Fact]
        [Test_Method("XML_FromDictionary()")]
        [Test_Method("XML_ToDictionary()")]
        public void XML_Test()
        {
            IDictionary<string, string> dic1 = enWord_Dictionary.Abbreviation_FromWord.zLoadDictionary();
            var XML1 = _lamed.Types.Dictionary.XML_FromDictionary(dic1, "_");
            var dic2 = _lamed.Types.Dictionary.XML_ToDictionary(XML1);
            var XML2 = _lamed.Types.Dictionary.XML_FromDictionary(dic2, "_");

            Assert.Equal(XML1, XML2);
            Assert.Equal(dic1, dic2);

            var xmlResult =
@"<root>
  <abbreviation>abbr</abbreviation>
  <aggregate>agg</aggregate>
  <alternate_key>ak</alternate_key>
  <application>app</application>
  <argument>arg</argument>
  <arguments>args</arguments>
  <asynchronous>async</asynchronous>
  <attribute>attr</attribute>
  <authentication>auth</authentication>
  <automatic>auto</automatic>
  <average>avg</average>
  <backup>bak</backup>
  <blueprint>bp</blueprint>
  <calculate>calc</calculate>
  <character>char</character>
  <color>clr</color>
  <command>cmd</command>
  <count>cnt</count>
  <communication>comms</communication>
  <configuration>config</configuration>
  <connection>conn</connection>
  <constant>const</constant>
  <control>ctl</control>
  <current>cur</current>
  <customer>cust</customer>
  <database>db</database>
  <debug>dbg</debug>
  <double>dbl</double>
  <database_setup>dbsetup</database_setup>
  <decimal>dec</decimal>
  <definition>def</definition>
  <delete>del</delete>
  <delimiter>delim</delimiter>
  <description>descr</description>
  <dictionary>dict</dictionary>
  <difference>diff</difference>
  <dialog>dlg</dialog>
  <document>doc</document>
  <Development_Tools_Environment>dte</Development_Tools_Environment>
  <enumeral>enum</enumeral>
  <error>err</error>
  <escape>esc</escape>
  <executable>exe</executable>
  <execute>exec</execute>
  <foreign_key>fk</foreign_key>
  <field>fld</field>
  <function>fn</function>
  <forward>fwd</forward>
  <global_unique_identifier>guid</global_unique_identifier>
  <hexadecimal>hex</hexadecimal>
  <icon>ico</icon>
  <identifier>id</identifier>
  <index>idx</index>
  <implementation>impl</implementation>
  <information>info</information>
  <initialize>init</initialize>
  <inter_process_communication>ipc</inter_process_communication>
  <language>lang</language>
  <label>lbl</label>
  <length>len</length>
  <library>lib</library>
  <level>lvl</level>
  <maximum>max</maximum>
  <memory>mem</memory>
  <minimum>min</minimum>
  <method_transformation_information>MTI</method_transformation_information>
  <new_line>nl</new_line>
  <number>nu</number>
  <object>obj</object>
  <original>orig</original>
  <parameter>param</parameter>
  <parameters>params</parameters>
  <physical>phys</physical>
  <primary_key>pk</primary_key>
  <position>pos</position>
  <preference>pref</preference>
  <previous>prev</previous>
  <project>prj</project>
  <product>prod</product>
  <property>prop</property>
  <password>pwd</password>
  <single_quote>q</single_quote>
  <double_quote>qq</double_quote>
  <record>rec</record>
  <reference>ref</reference>
  <relative>rel</relative>
  <resource>res</resource>
  <random>rnd</random>
  <source>src</source>
  <standard>std</standard>
  <statement>stmt</statement>
  <string>str</string>
  <structure>struct</structure>
  <sub-string>substr</sub-string>
  <synchronize>sync</synchronize>
  <system>sys</system>
  <table>tbl</table>
  <temporary>temp</temporary>
  <text>txt</text>
  <uniform_resource_identifier>uri</uniform_resource_identifier>
  <utility>util</utility>
  <value>val</value>
  <variable>var</variable>
  <xml_element>xelement</xml_element>
</root>";

            Assert.Equal(xmlResult, XML1);
        }


    }
}
