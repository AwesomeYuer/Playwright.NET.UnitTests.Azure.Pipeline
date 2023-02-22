namespace ConsoleApp1MSTests
{
    using ConsoleApp1;
    using ConsoleApp1.Fakes;
    using Microshaoft.UnitTesting.MsTest;
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass(), TestCategory(nameof(Class1Tests))]
    public class Class1Tests
    {
        [TestMethod()]
        public void Hello_Shim_Constructor_Test()
        {
            using (ShimsContext.Create())
            {
                var i = 0;
                var _hello = string.Empty;
                ShimClass1.ConstructorString = (@this, hello) =>
                {
                    i++;
                    _hello = hello;
                    var sC1 = new ShimClass1(@this)
                    {
                        HelloStringString = (whom, whom2) =>
                        {
                            return
                                $@"!{_hello}: {whom2}, {whom}";
                        }
                    };
                };
                
                var c1 = new Class1("你瞅啥");
                var r = c1.Hello("world", "world2");
                Console.WriteLine(r);
                Assert
                    .IsTrue
                        (
                            r
                                .StartsWith
                                    (
                                        $"!{_hello}"
                                        , StringComparison.OrdinalIgnoreCase
                                    )
                        );
            }
        }

        [DataRow("hello")]
        [DataRow("bye")]
        [DataRow("你愁啥")]
        [TestMethod()]
        public void Hello_Test(string hello)
        {
            var c1 = new Class1(hello);
            var r = c1.Hello("world", "world2");
            Console.WriteLine($@"Return=""{r}""");
            Assert
                .IsTrue
                    (
                        r
                            .StartsWith
                                (
                                    hello
                                    , StringComparison.OrdinalIgnoreCase
                                )
                    );
        }

        [TestMethod()]
        public void Hello_Shim_Test()
        {
            using (ShimsContext.Create())
            {
                var i = 0;
                ShimClass1
                    .AllInstances
                    .HelloStringString =
                        (sender, whom, whom2) =>
                        {
                            i++;
                            var r = $"Shim Good Bye: {whom}, {whom2}";
                            Console.WriteLine($@"Shiming return=""{r}""");
                            return r;
                        };
                var c1 = new Class1("你好");
                var r = c1.Hello("world", "world2");
                Assert
                    .IsTrue
                        (
                            r
                                .StartsWith
                                    (
                                        "shim"
                                        , StringComparison.OrdinalIgnoreCase
                                    )
                        );
            }
        }

        [TestMethod()]
        public void Hello_Stub_Test()
        {
            //Fakes stubClass1 = new Fakes.StubClass1();
            var stubIHello = new StubIHello()
            { 
                 HelloStringString =
                    (whom, whom2) =>
                    {
                        var r = $"Stub Good Bye: {whom}, {whom2}";
                        Console.WriteLine($@"Stubing return=""{r}""");
                        return r;
                    }
            };
            Invoker invoker = new (stubIHello);
            var r = invoker.Invoke("world", "world2");
            Assert
                .IsTrue
                    (
                        r
                            .StartsWith
                                (
                                    "Stub"
                                    , StringComparison.OrdinalIgnoreCase
                                )
                    );
        }

        [DataRow(typeof(DivideByZeroException), $"Message of {nameof(DivideByZeroException)}")]
        [DataRow(typeof(DivideByZeroException), "")]
        [DataRow(typeof(DivideByZeroException), null)]
        //[DataRow(typeof(DivideByZeroException))]
        //[DataRow(typeof(Exception))]
        [TestMethod()]
        public void Exception_AssertHelper_ExpectedExceptionType_Test
                            (
                                Type expectedExceptionType
                                , string expectedExceptionMessage = null!
                            )
        {
            Assert
                .That
                .Throws
                    (
                        expectedExceptionType
                        , () =>
                        {
                            throw
                                new
                                    DivideByZeroException
                                        (
                                            expectedExceptionMessage //+ "1111"
                                            , new Exception()
                                        );
                        }
                        , expectedExceptionMessage
                        , (x) =>
                        {
                            Assert.IsTrue(x is DivideByZeroException);
                            Assert.AreEqual(x.GetType(), typeof(DivideByZeroException));
                        }
                    );
        }

        [DataRow($"Message of {nameof(ArgumentNullException)}")]
        [DataRow("")]
        [DataRow(null)]
        [TestMethod()]
        public void Exception_AssertHelper_ExpectedException_Test(string expectedExceptionMessage = null!)
        {
            Assert
                .That
                .Throws
                    <ArgumentNullException>
                        (
                            () =>
                            {
                                throw
                                    new
                                        ArgumentNullException
                                            (
                                                expectedExceptionMessage
                                                , new Exception()
                                            );
                            }
                            , expectedExceptionMessage
                            , (x) =>
                            {
                                Assert.IsTrue(x is ArgumentNullException);
                                Assert.AreEqual(x.GetType(), typeof(ArgumentNullException));
                            }
                        );
        }

        //[DataRow(typeof(DivideByZeroException))]
        //[DataRow(typeof(Exception))]
        [TestMethod()]
        public void Exception_ThrowsException_Test()
        {
            // fail
            Assert
                .ThrowsException<DivideByZeroException>
                    (
                        () =>
                        { 
                            //Task
                            //    .Run
                            //      (
                            //          ()=>
                            //          {
                                            var e = new DivideByZeroException("hello");
                                            throw e;
                            //          }
                            //      )
                            //  .Wait();
                        }
                        , $@"Not {nameof(DivideByZeroException)}(""hello"")  was thrown"
                    );
        }

        [ExpectedException(typeof(NotImplementedException))]
        [TestMethod()]
        public void Exception_ExpectedExceptionAttribute_Test()
        {
            // fail
            throw
                new NotImplementedException();
                // new ArgumentNullException();
        }
    }
}
