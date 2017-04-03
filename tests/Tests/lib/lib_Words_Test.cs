using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.Words;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.lib
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class lib_Words_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Prefixes_Remove()")]
        public void Prefixes_Remove_Test()
        {
            #region Test1: bpTooltip_AsStr -> Tooltip_AsStr

            //      ===========================================
            Assert.Equal("bp", _lamed.lib.Words.Prefixes_Remove("bp"));
            Assert.Equal("z", _lamed.lib.Words.Prefixes_Remove("z"));
            Assert.Equal("Tooltip_AsStr", _lamed.lib.Words.Prefixes_Remove("bpTooltip_AsStr"));

            Assert.Equal("ShowStatus", _lamed.lib.Words.Prefixes_Remove("zShowStatus"));
            Assert.Equal("SubStr_RemoveStrAtEnd", _lamed.lib.Words.Prefixes_Remove("zzSubStr_RemoveStrAtEnd"));
            Assert.Equal("SubStr_zzRemoveStrAtEnd", _lamed.lib.Words.Prefixes_Remove("SubStr_zzRemoveStrAtEnd"));
            Assert.Equal("ZZSubStr_RemoveStrAtEnd", _lamed.lib.Words.Prefixes_Remove("ZZSubStr_RemoveStrAtEnd"));

            #endregion
        }

        [Fact]
        [Test_Method("Word_Convert()")]
        public void Word_Abbreviation_Test()
        {
            #region Test1: word -> abbreviation

            //      ===========================================
            Assert.Equal("abbreviation", _lamed.lib.Words.Convert_Abreviation2Word("abbr"));
            Assert.Equal("database setup",_lamed.lib.Words.Convert_Abreviation2Word("dbsetup"));
            Assert.Equal("Development Tools Environment",_lamed.lib.Words.Convert_Abreviation2Word("dte"));

            #endregion

            #region Test2: abbreviation -> word

            // ===========================================
            Assert.Equal("abbr", _lamed.lib.Words.Convert_AbreviationFromWord("abbreviation"));
            Assert.Equal("dbsetup",_lamed.lib.Words.Convert_AbreviationFromWord("database setup"));
            Assert.Equal("dte",_lamed.lib.Words.Convert_AbreviationFromWord("Development Tools Environment"));
            Assert.Equal("aaa",_lamed.lib.Words.Convert_AbreviationFromWord("aaa"));  // No conversion

            #endregion
        }

        [Fact]
        [Test_Method("Convert_SimpleEnglishFromWord()")]
        [Test_Method("Convert_SimpleEnglish2Word()")]
        public void Word_simpleEnglish_Test()
        {
            #region Test1: word -> simle english
            //      ===========================================
            Assert.Equal("next to", _lamed.lib.Words.Convert_SimpleEnglishFromWord("adjacent to"));
            Assert.Equal("agree", _lamed.lib.Words.Convert_SimpleEnglishFromWord("acquiesce"));
            Assert.Equal("stress", _lamed.lib.Words.Convert_SimpleEnglishFromWord("accentuate"));
            //Assert.Equal("helpful", _lamed.lib.Words.Convert_SimpleEnglishFromWord("advantageous"));
            #endregion

            #region Test2: simle english -> word
            // ===========================================
            Assert.Equal("adjacent to", _lamed.lib.Words.Convert_SimpleEnglish2Word("next to"));
            Assert.Equal("acquiesce", _lamed.lib.Words.Convert_SimpleEnglish2Word("agree"));
            Assert.Equal("accentuate", _lamed.lib.Words.Convert_SimpleEnglish2Word("stress"));
            //Assert.Equal("advantageous", _lamed.lib.Words.Convert_SimpleEnglish2Word("helpful"));
            #endregion
        }

        [Fact]
        [Test_Method("IsAcronym()")]
        [Test_Method("IsCommonWord()")]
        [Test_Method("IsProperty()")]
        public void Is_Test1()
        {
            // IsAcronym
            Assert.True(_lamed.lib.Words.IsAcronym("ADO"));
            Assert.False(_lamed.lib.Words.IsAcronym("ado"));

            // IsCommonWord
            Assert.True(_lamed.lib.Words.IsCommonWord("were"));
            Assert.False(_lamed.lib.Words.IsCommonWord("urs"));

            // IsProperty
            Assert.True(_lamed.lib.Words.IsProperty("color"));
            Assert.True(_lamed.lib.Words.IsProperty("size"));
            Assert.False(_lamed.lib.Words.IsProperty("prop"));

        }

        [Fact]
        [Test_Method("IsTypeName()")]
        [Test_Method("IsVerb()")]
        [Test_Method("IsVerbModifier()")]
        [Test_Method("IsWordNotToUse()")]
        public void Is_Test2()
        {
            // IsTypeName
            Assert.True(_lamed.lib.Words.IsTypeName("CheckBox"));
            Assert.True(_lamed.lib.Words.IsTypeName("DataSet"));
            Assert.False(_lamed.lib.Words.IsTypeName("type"));

            // IsVerb
            Assert.True(_lamed.lib.Words.IsVerb("act"));
            Assert.True(_lamed.lib.Words.IsVerb("Assemble"));
            Assert.True(_lamed.lib.Words.IsVerb("assemble"));
            Assert.False(_lamed.lib.Words.IsVerb("type"));

            // IsVerbModifier
            Assert.True(_lamed.lib.Words.IsVerbModifier("quickly"));
            Assert.True(_lamed.lib.Words.IsVerbModifier("slowly"));
            Assert.False(_lamed.lib.Words.IsVerbModifier("modify"));

            // IsWordNotToUse
            Assert.True(_lamed.lib.Words.IsWordNotToUse("an"));
            Assert.True(_lamed.lib.Words.IsWordNotToUse("from"));
            Assert.False(_lamed.lib.Words.IsWordNotToUse("do not use"));
        }
    }
}
