using System;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types
{
    public sealed class Types_DateTimeSpan_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance; // system library

        [Fact]
        [Test_Method("Sleep()")]
        [Test_Method("Elapsed()")]
        public void Elapsed_Test()
        {
            var now = DateTime.UtcNow;
            _lamed.lib.Command.Sleep(1000);
            var span = _lamed.Types.DateTimeSpan.Elapsed(now);
            int ticks = (int)span.TotalMilliseconds/100;
            Assert.Equal(10,ticks);
        }
    }
}