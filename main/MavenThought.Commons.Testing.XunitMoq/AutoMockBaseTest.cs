using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Practices.ServiceLocation;
using Xunit;
using Moq;
using Moq.Language.Flow;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base test with auto mocking of the SUT
    /// </summary>
    /// <typeparam name="TSut">Class to test</typeparam>
    /// <typeparam name="TContract">Type of the contract</typeparam>
    public abstract class AutoMockBaseTest<TSut, TContract>
        : BaseTestWithSut<TContract> where TSut : class, TContract
    {
        /// <summary>
        /// Gets or sets the auto mocking container.
        /// </summary>
        /// <value>The auto mocking container.</value>
        protected AutoMockContainer Container { get; private set; }

        protected MockFactory MockRepository { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="MockFactory.VerifyAll"/> will be used or <see cref="MockFactory.Verify"/>.
        /// </summary>
        /// <value><c>true</c> to use <see cref="MockFactory.VerifyAll"/>; otherwise, <c>false</c>.</value>
        /// <remarks><c>false</c> be default.</remarks>
        public bool VerifyAll { get; set; }

        /// <summary>
        /// Initializes a new instance of the AutoMockBaseTest class.
        /// </summary>
        public AutoMockBaseTest()
        {
            this.PartialMockSut(); // Moq's default behavior
        }

        /// <summary>
        /// Changes mocks behavior to <see cref="MockBehavior.Strict"/>.
        /// </summary>
        public void UseStrict()
        {
            this.MockRepository = new MockFactory(MockBehavior.Strict);
            this.Container = new AutoMockContainer(this.MockRepository);
        }

        /// <summary>
        /// Forces factory verification.
        /// </summary>
        /// <remarks>This is called automatically by xUnit.</remarks>
        public override void Dispose()
        {
            if (this.VerifyAll)
            {
                this.MockRepository.VerifyAll();
            }
            else
            {
                this.MockRepository.Verify();
            }

            base.Dispose();
        }

        /// <summary>
        /// Gets the concrete instance
        /// </summary>
        protected TSut ConcreteSut
        {
            get { return (TSut)this.Sut; }
        }

        /// <summary>
        /// Set the SUT as partial mock
        /// </summary>
        public void PartialMockSut()
        {
            this.MockRepository = new MockFactory(MockBehavior.Strict);
            this.Container = new AutoMockContainer(this.MockRepository);
        }

        /// <summary>
        /// Gets a dependency out of the automocking container.
        /// </summary>
        /// <typeparam name="T">Type of the dependency</typeparam>
        /// <returns>The auto mocker dependency</returns>
        public T Dep<T>() where T : class
        {
            return this.Container.Resolve<T>();
        }

        /// <summary>
        /// Helper method to get the <see cref="Mock<T>"/> of a given 
        /// object which Moq requires for setting expectations
        /// </summary>
        /// <param name="mockedObject">The mocked object</param>
        /// <typeparam name="T">Type of the mocked object</typeparam>
        /// <returns>The <see cref="Mock<T>"/> associated with 
        /// the mocked object</returns>
        public Mock<T> MockOf<T>(T mockedObject) where T : class
        {
            return Moq.Mock.Get(mockedObject);
        }

        /// <summary>
        /// Grabs the <see cref="Mock<T>"/> of a given
        /// dependency (as pulled out of the container)
        /// </summary>
        /// <typeparam name="T">The type of dependency to pull out</typeparam>
        /// <returns>The <see cref="Mock<T>"/> associated with a given type T</returns>
        public Mock<T> Configure<T>() where T : class
        {
            return MockOf(Dep<T>());
        }

        /// <summary>
        /// Create the by returning the auto mocker class under test
        /// </summary>
        /// <returns>The result of obtaining the class under test</returns>
        protected override TContract CreateSut()
        {
            return this.Container.Create<TSut>();
        }

        /// <summary>
        /// Create a stub by returning the dependency
        /// </summary>
        /// <typeparam name="TTarget">Target of the stub</typeparam>
        /// <typeparam name="TResult">Type of the dependency</typeparam>
        /// <param name="fn">Function to stub</param>
        protected ISetup<TTarget, TResult> Stub<TTarget, TResult>(Expression<Func<TTarget, TResult>> fn) where TResult : class where TTarget : class
        {
            // get the dependency from the container
            TTarget dependency = Dep<TTarget>();

            // get the Mock for which dependency is a Mock.object
            var moq = Moq.Mock.Get<TTarget>(dependency);

            // set the given expectation
            return moq.Setup(fn);
        }

        /// <summary>
        /// Create the auto mocker before each test
        /// </summary>
        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();
        }

        /// <summary>
        /// Asserts the constructor has injected the specified value
        /// </summary>
        /// <typeparam name="TResult">Type of the result of the functor</typeparam>
        /// <param name="func">Functor to use to assert the injection</param>
        protected void AssertDependencyInjection<TResult>(Func<TContract, TResult> func)
            where TResult : class
        {
            Assert.Same(Dep<TResult>(), func(this.Sut));
        }

        /// <summary>
        /// Stubs the service locator to return a resolved instance of T
        /// </summary>
        /// <typeparam name="T">Type of the service</typeparam>
        protected void StubService<T>() where T : class
        {
            Stub<IServiceLocator, T>(srv => srv.GetInstance<T>()).Returns(Container.Resolve<T>());
        }

        /// <summary>
        /// Raise a property changed for dependency T
        /// </summary>
        /// <typeparam name="T">Type of the dependency</typeparam>
        protected void RaisePropertyChanged<T>(string propertyName) where T : class, INotifyPropertyChanged
        {
            Dep<T>().RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raise a property changed for dependency T
        /// </summary>
        /// <typeparam name="TSource">Type of the source property</typeparam>
        /// <typeparam name="TResult">Type of the result</typeparam>
        protected void RaisePropertyChanged<TSource, TResult>(Expression<Func<TSource, TResult>> expression) where TSource : class, INotifyPropertyChanged
        {
            Dep<TSource>().RaisePropertyChanged(expression);
        }
    }
public static class MyClass
{
	
        public static ISetup<TTarget, TResult> Stub<TTarget, TResult>(this TTarget target, Expression<Func<TTarget, TResult>> fn) where TResult : class where TTarget : class
        {
            // get the Mock for which dependency is a Mock.object
            var moq = Moq.Mock.Get<TTarget>(target);

            // set the given expectation
            return moq.Setup(fn);
        }
}
}