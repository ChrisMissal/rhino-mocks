using System;
using NUnit.Framework;

namespace Rhino.Mocks.GettingStarted
{
    [TestFixture]
    public class Stubs
    {
        [Test]
        public void Demonstrate_Stub_Implements_the_passed_type()
        {
            // Arrange
            
            // Act
            var stub = MockRepository.GenerateStub<IFoo>();

            // Assert
            Assert.That(stub.Implements<IFoo>());
        }

        /// <summary>
        /// When you need to mock a read-only property of a class.
        /// </summary>
        [Test]
        public void How_to_Stub_out_your_own_value_of_a_ReadOnlyProperty()
        {
            // Arrange
            var foo = MockRepository.GenerateStub<IFoo>();
            foo.Stub(x => x.ID).Return(123);

            // Act
            var id = foo.ID;

            // Assert
            Assert.That(id, Is.EqualTo(123));
        }

        /// <summary>
        /// Asserting that an instance inside your system under test is called with a parameter
        /// matching a set of expected criteria. This example shows that our FooService should
        /// call it's dependency's method with the Name property of the Foo object passed into
        /// the LookUpFoo method.
        /// </summary>
        [Test]
        public void How_to_Assert_that_a_method_calls_an_expected_method_with_value()
        {
            // Arrange
            var foo = new Foo {Name = "rhino-mocks"};
            var mockFooRepository = MockRepository.GenerateStub<IFooRepository>();
            var fooService = new FooService(mockFooRepository);

            // Act
            fooService.LookUpFoo(foo);

            // Assert
            mockFooRepository.AssertWasCalled(x => x.GetFooByName(Arg<string>.Matches(y => y.Equals(foo.Name))));
        }

        /// <summary>
        /// This is just a sample interface, what it is or does isn't really relevant. It could
        /// be IUser of IOrder
        /// </summary>
        public interface IFoo
        {
            int ID { get; }
            string Name { get; set; }
        }

        public class Foo : IFoo
        {
            private int id;

            public int ID
            {
                get { return id; }
            }

            public string Name { get; set; }
        }

        public interface IFooService
        {
            IFoo LookUpFoo(IFoo foo);
        }

        public class FooService : IFooService
        {
            private readonly IFooRepository fooRepository;

            public FooService(IFooRepository fooRepository)
            {
                this.fooRepository = fooRepository;
            }

            public virtual IFoo LookUpFoo(IFoo foo)
            {
                return fooRepository.GetFooByName(foo.Name);
            }
        }

        public interface IFooRepository
        {
            IFoo GetFooByName(string name);
        }
    }
}