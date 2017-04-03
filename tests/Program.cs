using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LamedalCore;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;

namespace LamedalCore.Test
{
    public sealed class Program
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Test_IgnoreCoverage(enTestIgnore.FrontendCode)]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        [Fact]
        public void Hello_Error_Test()
        {
            //_lamed.Error_Test();    // Uncomment line in order to produce a failed test. 
        }
    }
}
