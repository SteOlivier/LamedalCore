namespace LamedalCore.Test.Tests._Data
{
    public sealed class TestData_
    {
        // private readonly TestData_ _testData = TestData_.Instance; // Get reference to this class

        #region Singleton of TestData_
        private static readonly TestData_ _TestData = new TestData_();  // This is the only instance of this class
        private TestData_()
        {
            // Private constructor prevents creation by external clients
        }

        /// <summary>
        /// Return Instance of Singleton_Pattern
        /// </summary>
        public static TestData_ Instance
        {
            get { return _TestData; }
        }
        #endregion
    }
}
