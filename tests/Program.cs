using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LamedalCore;

namespace LamedalCore.Test
{
    public sealed class Program
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        [Fact]
        public void Hello_Error_Test()
        {
            //_lamed.Error_Test();
        }
    }
}
