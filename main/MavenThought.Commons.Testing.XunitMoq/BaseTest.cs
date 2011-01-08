using System;
using Xunit;
using Moq;
using System.Linq.Expressions;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base test with mocking
    /// </summary>
    public abstract class BaseTest : IDisposable
    {
        /// <summary>
        /// Mocks an object of type U
        /// </summary>
        /// <param name="arguments">
        /// Arguments for the mock
        /// </param>
        /// <typeparam name="U">
        /// Type of the mock
        /// </typeparam>
        /// <returns>
        /// A mock instance of U created with arguments
        /// </returns>
        public static U MockOf<U>() where U : class
        {
            return new Moq.Mock<U>().Object;
        }

        /// <summary>
        /// Setup before all tests
        /// </summary>
        [Obsolete("Use IFixture<T> instead: http://xunit.codeplex.com/wikipage?title=Comparisons&referringTitle=HowToUse#note3", true)]
        public void FixtureSetUp()
        {
            this.BeforeAllTests();
        }

        /// <summary>
        /// Create the TSUT before each test
        /// </summary>
        public BaseTest()
        {
            this.BeforeEachTest();
        }

        /// <summary>
        /// Clean after each test
        /// </summary>
        public virtual void Dispose()
        {
            this.AfterEachTest();
        }

        /// <summary>
        /// Clean up after all tests run
        /// </summary>
        [Obsolete("Use IFixture<T> instead: http://xunit.codeplex.com/wikipage?title=Comparisons&referringTitle=HowToUse#note3", true)]
        public void FixtureTearDown()
        {
            this.AfterAllTests();
        }

        /// <summary>
        /// Placeholder to run before all tests
        /// </summary>
        protected virtual void BeforeAllTests()
        {
        }

        /// <summary>
        /// Place holder for optional initialization before each test
        /// </summary>
        protected virtual void BeforeEachTest()
        {
        }

        /// <summary>
        /// Placeholder to cleanup after each test
        /// </summary>
        protected virtual void AfterEachTest()
        {
        }

        /// <summary>
        /// Placeholder to run after all test run
        /// </summary>
        protected virtual void AfterAllTests()
        {
        }
    }
}