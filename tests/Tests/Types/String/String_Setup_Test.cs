using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LamedalCore.domain.Attributes;
using LamedalCore.zz;
using Xunit;

namespace LamedalCore.Test.Tests.Types.String
{
    public sealed class String_Setup_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("SQL_Q")]
        public void SQL_Q_Test()
        {
            object Object = "testValue";
            Assert.Equal("'testValue'", _lamed.Types.String.Quote.SQL_Q(Object));
            Assert.Equal("'testValue'", _lamed.Types.String.Quote.SQL_Q("testValue"));

            Object = "";
            Assert.Equal("''", _lamed.Types.String.Quote.SQL_Q(Object));
            Assert.Equal("''", _lamed.Types.String.Quote.SQL_Q(""));

            Object = null;
            Assert.Equal("NULL", _lamed.Types.String.Quote.SQL_Q(Object));
            Assert.Equal("NULL", _lamed.Types.String.Quote.SQL_Q(null));

            Object = new Guid("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8");
            Assert.Equal("'1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8'", _lamed.Types.String.Quote.SQL_Q(Object));

            var date = new DateTime(2010, 1, 18);
            Object = date;
            Assert.Equal("'2010-01-18 12:00:00 AM'", _lamed.Types.String.Quote.SQL_Q(date));
            Assert.Equal("'2010-01-18 12:00:00 AM'", _lamed.Types.String.Quote.SQL_Q(Object));

            // NL in SQL
            Assert.Equal("'testValue1\r\ntestValue2'",
                _lamed.Types.String.Quote.SQL_Q("testValue1".NL() + "testValue2"));

            Object = true;
            Assert.Equal("1", _lamed.Types.String.Quote.SQL_Q(true));
            Assert.Equal("1", _lamed.Types.String.Quote.SQL_Q(Object));

            Assert.Equal("747", _lamed.Types.String.Quote.SQL_Q(747));
        }

        [Fact]
        [Test_Method("Q()")]
        public void Q_Test()
        {
            // Q
            Assert.Equal("NULL", _lamed.Types.String.Quote.Q(null));
            Assert.Equal("'test'", _lamed.Types.String.Quote.Q("test"));
            Assert.Equal("''", _lamed.Types.String.Quote.Q(""));
            Assert.Equal("'1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8'", _lamed.Types.String.Quote.Q(new Guid("1eb4c570-51cb-46d3-b9ba-a76ddbc8dfe8")));
            Assert.Equal("'2010-01-18 00:00:00'", _lamed.Types.String.Quote.Q(new DateTime(2010, 1, 18)));

            // cQ
            Assert.Equal(",'test'", _lamed.Types.String.Quote.cQ("test"));

        }

        [Fact]
        [Test_Method("QQ()")]
        public void QQ_Test()
        {
            Assert.Equal("", _lamed.Types.String.Quote.QQ(null));
            Assert.Equal("\"test\"", _lamed.Types.String.Quote.QQ("test"));
            Assert.Equal("\"\"", _lamed.Types.String.Quote.QQ(""));
        }

        [Fact]
        [Test_Method("Remove_DoubleQuotes_Test")]
        public void Remove_DoubleQuotes_Test()
        {
            var line = "this \"is a\" test.";
            var line2 = _lamed.Types.String.Quote.Remove_DoubleQuotes(line);
            Assert.Equal("this is a test.", line2);
        }
    }
}
