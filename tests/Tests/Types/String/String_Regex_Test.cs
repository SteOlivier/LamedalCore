using System;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.Types.String
{
    public sealed class String_Regex_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("IsValid_Url()")]
        public void IsValid_Url_Test()
        {
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("http://www.google.com"));
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("www.123google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_Url("-123google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_Url("http://-123.123google.com"));
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("www.google.com/help/me"));
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("ftp://psychopop.org"));
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("http://www.edsroom/ "));
            Assert.True(_lamed.Types.String.Regex.IsValid_Url("http://un/pleasant.jarrin.net/markov/index.asp"));
            //Assert.True(IsValidUrl(""));
        }

        [Fact]
        [Test_Method("IsValid_UrlHttp()")]
        public void IsValid_UrlHttp_Test()
        {
            // True tests
            Assert.True(_lamed.Types.String.Regex.IsValid_UrlHttp("http://www.google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("https://www.google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("www.123google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("www.google.com/help/me"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("ftp://psychopop.org"));
            Assert.True(_lamed.Types.String.Regex.IsValid_UrlHttp("http://www.edsroom.org/"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("http://www.edsroom"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("http://un/pleasant.jarrin.net/markov/index.asp"));
            Assert.True(_lamed.Types.String.Regex.IsValid_UrlHttp("http://unpleasant.jarrin.net/markov/index.asp"));

            // False tests
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("-123google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("http://-123.123google.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp("http://www.edsroom/"));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp(""));
            Assert.False(_lamed.Types.String.Regex.IsValid_UrlHttp(""));
        }

        [Fact]
        [Test_Method("IsValid_IP()")]
        public void IsValid_IP_Test()
        {
            Assert.True(_lamed.Types.String.Regex.IsValid_IP("127.0.0.1"));
            Assert.True(_lamed.Types.String.Regex.IsValid_IP("255.255.255.0"));
            Assert.True(_lamed.Types.String.Regex.IsValid_IP("192.168.0.1"));

            Assert.False(_lamed.Types.String.Regex.IsValid_IP("1200.5.4.3"));
            Assert.False(_lamed.Types.String.Regex.IsValid_IP("abc.def.ghi.jkl"));
            Assert.False(_lamed.Types.String.Regex.IsValid_IP("255.foo.bar.1"));

        }

        [Fact]
        [Test_Method("IsValid_eMail()")]
        public void IsValid_eMail_Test()
        {
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("foo@bar.com"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("foobar@foobar.com.au"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("test@test.com"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("nerdy.one@science.museum"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("ready&amp;set@go.com.au"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("Trais.Gray@domain.biz"));
            Assert.True(_lamed.Types.String.Regex.IsValid_eMail("\"Funny email\".notfunny@glxs.biz"));

            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("foo@bar"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("$$$@bar.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail(".test.@test.com "));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("spammer@[203.12.145.68] "));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("bla@bla"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("joe"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("@foo.com"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail("ok@[funny domain].co.za"));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail(""));
            Assert.False(_lamed.Types.String.Regex.IsValid_eMail(""));
        }

        [Fact]
        [Test_Method("IsMaliciousCode()")]
        public void IsMaliciousCode_Test()
        {
            Assert.True(_lamed.Types.String.Regex.IsMaliciousCode("http://www.domain.com/page.asp?param=&lt;/script&gt;"));
            Assert.True(_lamed.Types.String.Regex.IsMaliciousCode("https://www.domain.com/page.asp?param=;SELECT"));
            Assert.True(_lamed.Types.String.Regex.IsMaliciousCode("https://www.domain.com/page.asp?param=;select"));
            Assert.False(_lamed.Types.String.Regex.IsMaliciousCode("https://www.domain.com/page.asp?param=RealParam"));
            Assert.False(_lamed.Types.String.Regex.IsMaliciousCode(""));
        }

        [Fact]
        [Test_Method("IsAlpha()")]
        public void IsAlpha_Test()
        {
            Assert.Equal(true,_lamed.Types.String.Regex.IsAlpha("absdsdfs"));
            Assert.Equal(false,_lamed.Types.String.Regex.IsAlpha("12a"));
            Assert.Equal(false,_lamed.Types.String.Regex.IsAlpha("b2a"));
            Assert.Equal(false,_lamed.Types.Test.IsAlpha("b2a"));
        }

        [Fact]
        [Test_Method("IsLike()")]
        public void IsLike_Test()
        {
            var text = "The brown fox catch the chicken";
            Assert.True(_lamed.Types.String.Regex.IsLike(text, "The brown*"));
            Assert.True(_lamed.Types.String.Regex.IsLike(text, "The brown*fox*"));
            Assert.True(_lamed.Types.String.Regex.IsLike(text, "*brown*"));

            Assert.True(_lamed.Types.String.Regex.IsLike("abc","a*")); // true
            Assert.True(_lamed.Types.String.Regex.IsLike("Abc","[A-Z][a-z][a-z]")); // true
            Assert.True(_lamed.Types.String.Regex.IsLike("abc123","*###")); // true
            Assert.True(_lamed.Types.String.Regex.IsLike("hat","?at")); // true
            Assert.True(_lamed.Types.String.Regex.IsLike("joe","[!aeiou]*")); // true

            Assert.False(_lamed.Types.String.Regex.IsLike("joe","?at")); // false
            Assert.False(_lamed.Types.String.Regex.IsLike("joe","[A-Z][a-z][a-z]")); // false

            // Exception tests
            Assert.Equal(false, _lamed.Types.String.Regex.IsLike("", "*"));
            Assert.Equal(false, _lamed.Types.String.Regex.IsLike("Hi", ""));
            Assert.Throws<ArgumentException>(() => _lamed.Types.String.Regex.IsLike("Hi", "["));
        }

        [Fact]
        [Test_Method("IsValid_Regex()")]
        public void IsValid_Regex_Test()
        {
            string error;
            Assert.Equal(false, _lamed.Types.String.Regex.IsValid_Regex("[", out error));
            Assert.Equal("Regex Error! parsing \"[\" - Unterminated [] set.", error);
            Assert.Equal(true, _lamed.Types.String.Regex.IsValid_Regex(@"[^a-zA-Z]", out error));

        }
    }
}
