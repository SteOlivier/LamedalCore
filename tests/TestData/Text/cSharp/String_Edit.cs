using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zz;

namespace LamedalCore.Types.String
{
    /// <summary>
    /// Make changes to the string
    /// </summary>
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action, DefaultType = typeof(string), GroupName = "Str")]
    public sealed class String_Edit
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        /// <summary> Change the case so that only the first letter is upper case.</summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Case_2Title(string text)
        {
            if (text.Length == 0) return text;

            string result = text.Substring(0, 1).ToUpper();
            result += text.Substring(1).ToLower();
            return result;
        }

        /// <summary>Replaces the first letter to lower case from the input string.</summary>
        /// <param name="strInput">The string input</param>
        /// <returns>string</returns>
        [Pure]
        public string Case_FirstLetter2Lower(string strInput)
        {
            if (strInput.zIsNullOrEmpty()) return "";
            strInput = char.ToLower(strInput[0]) + strInput.Substring(1);
            return strInput;
        }

        /// <summary>Replaces the first letter of the word to uppercase string.</summary>
        /// <param name="word">The word</param>
        /// <returns>string</returns>
        [Pure]
        public string Case_FirstLetter2Upper(string word)
        {
            if (word.zIsNullOrEmpty()) return "";
            word = char.ToUpper(word[0]) + word.Substring(1);
            return word;
        }

        /// <summary>Removes the extra spaces from the input string.</summary>
        /// <param name="inputStr">The input string</param>
        /// <param name="removeFirstAndLastSpace">Remove last space indicator. Default value = true.</param>
        /// <returns>string</returns>
        [Pure]
        public string Remove_ExtraSpaces(string inputStr, bool removeFirstAndLastSpace = true)
        {
            var result = inputStr.Replace("   ", " ").Replace("  ", " ").Replace("  ", " ");  // Remove extra spaces
            if (removeFirstAndLastSpace) result = result.Trim();
            return result;
        }


        /// <summary>Removes the prefix from the input string.</summary>
        /// <param name="inputStr">The input string</param>
        /// <param name="ignorePrefix">The ignore prefix array</param>
        /// <returns>string</returns>
        [Pure]
        public string Remove_Prefix(string inputStr,params string[] ignorePrefix)
        {
            var ignorePrefixSorted = ignorePrefix.zArray_SortByLength(false);
            var lenInput = inputStr.Length;
            foreach (var pre in ignorePrefixSorted)
            {
                var len = pre.Length;
                if (lenInput <= pre.Length) continue;  // Do not remove prefix if inputStr is same size or smaller
                if (inputStr.Substring(0, len) == pre)
                {
                    inputStr = inputStr.Substring(len, lenInput - len);
                    return inputStr;
                }
            }
            return inputStr;
        }

        /// <summary>
        /// Remove string at end of the input string. The default is to remove the last enter
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <param name="removeStr">The remove string.</param>
        /// <returns>
        /// string
        /// </returns>
        [Pure]
        public string Remove_StrAtEnd(string inputStr, string removeStr = "\r\n")
        {
            var len = removeStr.Length;
            if (len >= inputStr.Length) return inputStr;
            if (SubStr_Right(inputStr, len) == removeStr) inputStr = inputStr.Substring(0, inputStr.Length - len);
            return inputStr;
        }

        /// <summary>
        /// Repeat string from the string.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="total">The total setting. Default value = 1.</param>
        /// <returns>string</returns>
        public string Repeat([NotNull] string s, int total = 1)
        {
            var result = "";
            for (var ii = 0; ii < total; ii++)
            {
                result += s;
            }
            return result;
        }

        public string Replace_All(string sentence, string replaceWith, params string[] findThese)
        {
            foreach (var findValue in findThese)
            {
                sentence = sentence.Replace(findValue, replaceWith);
            }
            return sentence;
        }

        /// <summary>Replaces the input string between the start marker and end marker with the replace string.</summary>
        /// <param name="marker_start">The start marker</param>
        /// <param name="marker_end">The end marker</param>
        /// <param name="inputStr">The input string</param>
        /// <param name="replaceStr">The replace string</param>
        /// <returns>string</returns>
        [Pure]
        public string Replace_Between(string marker_start, string marker_end, string inputStr, string replaceStr)
        {
            return _lamed.Types.String.Regex.Replace_Between(marker_start, marker_end, inputStr, replaceStr);
        }

        /// <summary>
        /// Cut the specified part from the input string.
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <param name="cutPart">The cut part.</param>
        /// <returns></returns>
        [Pure]
        public string SubStr_Cut(string inputStr, string cutPart)
        {
            var position = inputStr.IndexOf(cutPart);
            var length = cutPart.Length;
            while (position != -1)
            {
                inputStr = inputStr.Remove(position, length);
                position = inputStr.IndexOf(cutPart);
            }
            return inputStr;
        }

        /// <summary>
        /// Return the specified chars from the right of the input string.
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <param name="chars">The chars.</param>
        /// <returns></returns>
        [Pure]
        public string SubStr_Right(string inputStr, int chars)
        {
            if (chars > inputStr.Length) return inputStr;
            var position = inputStr.Length - chars;
            return inputStr.Substring(position, chars);
        }

        /// <summary>Return substr of text between index start and index end</summary>
        /// <param name="text">The inputStr</param>
        /// <param name="indexStart">Index starts the</param>
        /// <param name="indexEnd">The index end</param>
        /// <returns>string</returns>
        public string SubStr_Index(string text, int indexStart, int indexEnd = -1)
        {
            if (text == "") return "";
            if (indexEnd == -1 || indexEnd > text.Length) return text.Substring(indexStart);
            return text.Substring(indexStart, indexEnd - indexStart);
        }

        /// <summary>Return substr of text that is safe</summary>
        /// <param name="text">The inputStr</param>
        /// <param name="indexStart">Index starts the</param>
        /// <param name="length">The length.</param>
        /// <returns>string</returns>
        public string SubStr_Left(string text, int indexStart, int length = -1)
        {
            if (text == "") return "";
            if (length == -1 || length > text.Length) return text.Substring(indexStart);
            return text.Substring(indexStart, length);
        }
    }
}
