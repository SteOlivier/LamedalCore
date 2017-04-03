using System;
using System.Collections.Generic;
using System.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.Test.Tests.Types.List;
using LamedalCore.Types;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_Enum_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Conversion samples")]
        public void IntConversion_Test()
        {
            // Int values
            Assert.Equal(1, (int)Types_Enum_Data.Spirit);
            Assert.Equal(2, (int)Types_Enum_Data.Soul);
            Assert.Equal(4, (int)Types_Enum_Data.Body);

            // The reverse
            Assert.Equal(Types_Enum_Data.Spirit, (Types_Enum_Data)1);
            Assert.Equal(Types_Enum_Data.Soul, (Types_Enum_Data)2);
            Assert.Equal(Types_Enum_Data.Body, (Types_Enum_Data)4);
        }

        [Fact]
        [Test_Method("enum_2IList()")]
        public void ToEnumValue_Test()
        {
            // ToString
            Assert.Equal("Spirit", Types_Enum_Data.Spirit.ToString());
            Assert.Equal("Soul", Types_Enum_Data.Soul.ToString());
            Assert.Equal("Body", Types_Enum_Data.Body.ToString());

            #region Str_2EnumValue
            Assert.Equal(Types_Enum_Data.Spirit, _lamed.Types.Enum.Str_2EnumValue<Types_Enum_Data>("Spirit"));
            Assert.Equal(Types_Enum_Data.Spirit, _lamed.Types.Enum.Str_2EnumValue<Types_Enum_Data>("mySPIRIT", true, "my"));

            // Errors
            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Enum.Str_2EnumValue<Types_Enum_Data>("mySPIRIT", true));
            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Enum.Str_2EnumValue<Types_Enum_Data>("mySPIRIT", false, "my"));
            #endregion

            // Object_2EnumValue
            var Object = 1;
            Assert.Equal(Types_Enum_Data2.Test_Value, _lamed.Types.Enum.Object_2EnumValue(Object, typeof(Types_Enum_Data2)));
        }

        [Fact]
        [Test_Method("enum_2IList()")]
        public void enum_2IList_Test()
        {
            // List values
            var enumList = new List<string>();
            _lamed.Types.Enum.enum_2IList(enumList, typeof(Types_Enum_Data), true, "my", "1", " ");
            Assert.Equal(9, enumList.Count);
            Assert.Equal("mySpirit1", enumList[0]);
            Assert.Equal("myFamily SingleParent1", enumList[6]);
        }

        [Fact]
        [Test_Method("Str_2EnumValue()")]
        public void Str_2EnumValue_Test()
        {
            Assert.Equal(enBlueprintClassNetworkType.Node_State, _lamed.Types.Enum.Str_2EnumValue<enBlueprintClassNetworkType>("Node_State"));
            Assert.Equal(enBlueprintClassNetworkType.Node_State, _lamed.Types.Enum.Str_2EnumValue<enBlueprintClassNetworkType>("enBlueprintClassNetworkType.Node_State"));
            Assert.Equal(enBlueprintClassNetworkType.Node_State, _lamed.Types.Enum.Str_2EnumValue("enBlueprintClassNetworkType.Node_State", typeof(enBlueprintClassNetworkType)));

            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Enum.Str_2EnumValue(null, typeof(enBlueprintClassNetworkType)));
            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Enum.Str_2EnumValue("Illigal value", typeof(enBlueprintClassNetworkType)));
            Assert.Throws<InvalidOperationException>(() => _lamed.Types.Enum.Str_2EnumValue< enBlueprintClassNetworkType>("Illigal value"));
        }

        [Fact]
        [Test_Method("zTo_Description()")]
        public void zTo_Description_Test()
        {
            Assert.Equal("Worship, Prayer, Drive, Mood, Gut", Types_Enum_Data.Spirit.zTo_Description());
            Assert.Equal("Worship, Prayer, Drive, Mood, Gut", _lamed.Types.Enum.enum_Description(Types_Enum_Data.Spirit));

            Assert.Equal("Mind, Heart and the Will", Types_Enum_Data.Soul.zTo_Description());
            Assert.Equal("Mind, Heart and the Will", _lamed.Types.Enum.enum_Description(Types_Enum_Data.Soul));
        }

        [Fact]
        [Test_Method("Flag_IsSet()")]
        [Test_Method("Flag_Remove()")]
        [Test_Method("Flag_Add()")]
        public void Flags_Test()
        {
            // Flags tests: Spirit & Soul
            var Spirit_Soul = (Types_Enum_Data.Spirit | Types_Enum_Data.Soul);
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul, Types_Enum_Data.Spirit));
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul, Types_Enum_Data.Soul));
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul,  true, Types_Enum_Data.Spirit, Types_Enum_Data.Soul));
            Assert.Equal(false, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul, true, Types_Enum_Data.Spirit, Types_Enum_Data.Soul, Types_Enum_Data.Body));

            // Flags tests: Spirit & Soul & Body
            Types_Enum_Data Spirit_Soul_Body = Spirit_Soul | Types_Enum_Data.Body;
            Types_Enum_Data Spirit_Soul_Body2 = _lamed.Types.Enum.Flag_Add(Spirit_Soul,Types_Enum_Data.Body);
            Spirit_Soul_Body2 = _lamed.Types.Enum.Flag_Add(Spirit_Soul_Body2, Types_Enum_Data.Body);
            Assert.Equal(Spirit_Soul_Body, Spirit_Soul_Body2);

            var enum1 = Types_Enum_Data2.Test1;
            var enum2 = _lamed.Types.Enum.Flag_Add(enum1, Types_Enum_Data2.Test_Value);
            Assert.NotEqual(enum1, enum2);
            enum2 = _lamed.Types.Enum.Flag_Remove(enum2, Types_Enum_Data2.Test_Value);
            Assert.Equal(enum1, enum2);

            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, Types_Enum_Data.Spirit));
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, Types_Enum_Data.Soul));
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, Types_Enum_Data.Body));
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, false, Types_Enum_Data.Spirit, Types_Enum_Data.Soul, Types_Enum_Data.Body));

            // Flags remove
            Spirit_Soul_Body = _lamed.Types.Enum.Flag_Remove(Spirit_Soul_Body, Types_Enum_Data.Body);
            Assert.Equal(false, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, Types_Enum_Data.Body));
            Spirit_Soul_Body = _lamed.Types.Enum.Flag_Add(Spirit_Soul_Body, Types_Enum_Data.Body);
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(Spirit_Soul_Body, Types_Enum_Data.Body));

            // Flags all test
            var allEnums = Types_Enum_Data.Spirit | Types_Enum_Data.Soul | Types_Enum_Data.Body | Types_Enum_Data.Marriage |
                            Types_Enum_Data.Family_Original | Types_Enum_Data.Family_Rejoined | Types_Enum_Data.Family_SingleParent;
            Assert.Equal(true, _lamed.Types.Enum.Flag_IsSet(allEnums, false, Types_Enum_Data.Spirit, Types_Enum_Data.Soul, Types_Enum_Data.Body, Types_Enum_Data.Marriage,
                            Types_Enum_Data.Family_Original, Types_Enum_Data.Family_Rejoined, Types_Enum_Data.Family_SingleParent));

            // Flags list
            var allEnumsList = _lamed.Types.Enum.Flags<Types_Enum_Data>(allEnums).ToList();
            Assert.Equal(9, allEnumsList.Count());
            Assert.Equal(Types_Enum_Data.Spirit, allEnumsList[0]);
            Assert.Equal(Types_Enum_Data.Soul, allEnumsList[1]);
        }

        [Fact]
        [Test_Method("()")]
        public void Enum_Test()
        {
            var man1 = Types_Enum_Data.Man;
            var man2 = Types_Enum_Data.Spirit | Types_Enum_Data.Soul | Types_Enum_Data.Body;
            Assert.Equal(man1, man2);
            Assert.Equal(Types_Enum_Data.Spirit | Types_Enum_Data.Soul | Types_Enum_Data.Body, man1);
            Assert.Equal(7, (int)man1);
            Assert.Equal(7, (int)Types_Enum_Data.Spirit + (int)Types_Enum_Data.Soul + (int)Types_Enum_Data.Body);
        }

        [Fact]
        [Test_Method("enum_2ArrayStr()")]
        public void enum_2ArrayStr_Test()
        {
            var enumResults = new[] { "Spirit", "Soul", "Body", "Man", "Marriage", "Family_Original",
                                      "Family_SingleParent", "Family_Rejoined", "All" };
            var enumValues = _lamed.Types.Enum.enum_2ArrayStr(typeof(Types_Enum_Data));
            Assert.Equal(enumResults, enumValues);
        }
    }
}
