using System;
using System.Collections.Generic;
using System.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;
using LamedalCore.zz;
using Xunit;


namespace LamedalCore.Test.Tests.domain
{
    public sealed class domain_Exceptions_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        public void Exception_Show_Test()
        {
            Assert.Throws<InvalidOperationException>(() => Exception_Show1());
            Assert.Throws<InvalidOperationException>(() => Exception_Show2());
            Assert.Throws<InvalidOperationException>(() => Exception_Show3());
        }
        #region Exception_Show
        private void Exception_Show1()
        {
            "Error message".zException_Show();
        }

        private void Exception_Show2()
        {
            "Error message".zException_Show(enExceptionAction.reThrowError);
        }

        private void Exception_Show3()
        {
            var ex = "".zException_New();
            ex.zException_Show("new error");
        }
        #endregion

        [Fact]
        public void Exception_Arguments_Test()
        {
            Assert.Throws<Exception_Argument>(() => ExceptionArguments());
            Assert.Throws<Exception_ArgumentIsNull>(() => ExceptionArgumentsIsNull());
            Assert.Throws<Exception_ArgumentIsOutOfRange>(() => ExceptionArgumentsIsOutOfRange());
        }
        #region ExceptionArguments
        private void ExceptionArguments(string parmName = null)
        {
            throw new Exception_Argument(nameof(parmName));
        }

        private void ExceptionArgumentsIsNull(string parmName = null)
        {
            throw new Exception_ArgumentIsNull(nameof(parmName));
        }
        private void ExceptionArgumentsIsOutOfRange(string parmName = null)
        {
            throw new Exception_ArgumentIsOutOfRange(nameof(parmName));
        }
        #endregion

        [Fact]
        [Test_Method("InnerExceptions()")]
        public void InnerExceptions_Test()
        {
            // Create exception
            var ex = new Exception_NotImplemented("Error1: Not implemented.");
            var ex2 = new Exception("Error2", ex);
            var ex3 = new Exception("Error3", ex2);

            
            var exList = _lamed.Exceptions.InnerExceptions(ex3).ToList();
            Assert.Equal("Error1: Not implemented.", exList[2].Message);
            Assert.Equal("Error2", exList[1].Message);
            Assert.Equal("Error3", exList[0].Message);
        }

    }
}
