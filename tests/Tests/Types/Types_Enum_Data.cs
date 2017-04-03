using System;
using LamedalCore.domain.Attributes;

namespace LamedalCore.Test.Tests.Types
{
    /// <summary>
    /// This enumeral is for test purposes. It shows the capabilities that is possible with enumerales.
    /// </summary>
    [Flags]
    public enum Types_Enum_Data : uint
    {
        [BlueprintData_Description("Worship, Prayer, Drive, Mood, Gut")]
        Spirit = 1,

        [BlueprintData_Description("Mind, Heart and the Will")]
        Soul = 2,

        [BlueprintData_Description("Eyes->see, Ears->hear, Nose->smell, Mouth->taste+speak, Hands->feel+do")]
        Body = 4,

        Man = Spirit | Soul | Body,

        [BlueprintData_Description("Two persons become one in body, soul and spirit")]
        Marriage = 8,

        Family_Original = 16,

        Family_SingleParent = 32,

        Family_Rejoined = 64,

        All = Man | Marriage | Family_Original | Family_SingleParent | Family_Rejoined
    }

    [Flags]
    public enum Types_Enum_Data2
    {
        Test1,
        Test_Value
    }
}