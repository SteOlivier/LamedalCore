using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.lib.XML;
using LamedalCore.zz;

namespace LamedalCore.Types.String
{
    /// <summary>
    /// Strings Methods
    /// </summary>
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Link, DefaultType = typeof(string), GroupName = "Str")]
    public sealed class String_
    {

        #region Edit
        /// <summary>
        /// Gets the Edit library methods.
        /// </summary>
        public String_Edit Edit
        {
            get { return _Edit ?? (_Edit = new String_Edit()); }
        }
        private String_Edit _Edit;
        #endregion

        #region Regex
        /// <summary>
        /// Gets the Regex library methods.
        /// </summary>
        public String_Regex Regex
        {
            get { return _Regex ?? (_Regex = new String_Regex()); }
        }
        private String_Regex _Regex;
        #endregion

        #region Search
        /// <summary>
        /// Gets the Search library methods.
        /// </summary>
        public String_Search Search
        {
            get { return _Search ?? (_Search = new String_Search()); }
        }
        private String_Search _Search;
        #endregion

        #region Setup
        /// <summary>
        /// Gets the Setup library methods.
        /// </summary>
        public String_Setup Setup
        {
            get { return _Setup ?? (_Setup = new String_Setup()); }
        }
        private String_Setup _Setup;
        #endregion

        #region SpecialChar
        /// <summary>
        /// Gets the SpecialChar library methods.
        /// </summary>
        public String_SpecialChar SpecialChar
        {
            get { return _SpecialChar ?? (_SpecialChar = new String_SpecialChar()); }
        }
        private String_SpecialChar _SpecialChar;
        #endregion

        #region Test
        /// <summary>
        /// Gets the Test library methods.
        /// </summary>
        public String_Test Test
        {
            get { return _Test ?? (_Test = new String_Test()); }
        }
        private String_Test _Test;
        #endregion

        #region Word
        /// <summary>
        /// Gets the Word library methods.
        /// </summary>
        public String_Word Word
        {
            get { return _Word ?? (_Word = new String_Word()); }
        }
        private String_Word _Word;
        #endregion

    }
}
