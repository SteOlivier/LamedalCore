using System.Diagnostics;

namespace LamedalCore.Test.Tests.Types.Class
{
    public sealed class Class_StateInfo_Data
    {
        public string Name;
        public string Description;

        /// <summary>Initializes a new instance of the <see cref="Class_StateInfo_Data"/> class default constructor</summary>
        public Class_StateInfo_Data()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Class_StateInfo_Data"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public Class_StateInfo_Data(string name, string description)
        {
            Name = name;
            Description = description;
        }

        ~Class_StateInfo_Data() { Debug.WriteLine("{0}.{1} finalized", GetType(),Name); }
    }

}
