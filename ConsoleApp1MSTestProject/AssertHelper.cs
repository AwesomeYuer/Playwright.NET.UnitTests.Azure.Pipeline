#define MsTestUnitTests
#if MsTestUnitTests
namespace Microshaoft.UnitTesting.MsTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public static class AssertHelper
    {
        private static void processExpectedExceptionMessage
                                        (
                                            Exception exception
                                            , string expectedExceptionMessage = null!
                                        )
        {
            if
                (
                    !string
                        .IsNullOrEmpty
                                (
                                    expectedExceptionMessage
                                )
                )
            {
                Assert
                    .IsTrue
                        (
                            string
                                .Compare
                                    (
                                        expectedExceptionMessage
                                        , exception
                                                .Message
                                        , true
                                    )
                            ==
                            0
                            , $@"Expected exception with a message of ""{expectedExceptionMessage}"" but exception with message of ""{exception.Message}"" was thrown instead."
                        );
            }
        }
        public static void Throws
                                <TExpectedException>
                                    (
                                        this Assert @this
                                        , Action action
                                        , string expectedExceptionMessage = null!
                                        , Action<TExpectedException> onProcessAction = null!
                                        , bool drillDownInnerExceptions = true
                                    )
                                        where TExpectedException : Exception
        {
            try
            {
                action();
            }
            catch (AggregateException aggregateException)
            {
                var innerExceptions = aggregateException.Flatten().InnerExceptions;
                foreach (var e in innerExceptions)
                {
                    if
                        (
                            e.GetType() == typeof(TExpectedException)
                            &&
                            string
                                .Compare
                                    (
                                        expectedExceptionMessage
                                        , e.Message
                                        , true
                                    ) == 0
                        )
                    {
                        Assert
                            .IsTrue
                                (
                                    e.GetType()
                                    ==
                                    typeof(TExpectedException)
                                );
                        processExpectedExceptionMessage
                                                (
                                                    e
                                                    , expectedExceptionMessage
                                                );
                        onProcessAction?.Invoke((TExpectedException)e);
                        //break;
                        return;
                    }
                    else if
                        (
                            drillDownInnerExceptions
                            &&
                            e.InnerException != null
                        )
                    {
                        var ee = e.InnerException;
                        while (ee != null)
                        {
                            if
                                (
                                    ee.GetType() == typeof(TExpectedException)
                                    &&
                                    string
                                        .Compare
                                            (
                                                expectedExceptionMessage
                                                , ee.Message
                                                , true
                                            ) == 0
                                )
                            {
                                Assert
                                    .IsTrue
                                        (
                                            ee.GetType()
                                            ==
                                            typeof(TExpectedException)
                                        );
                                processExpectedExceptionMessage
                                                        (
                                                            ee
                                                            , expectedExceptionMessage
                                                        );
                                onProcessAction?.Invoke((TExpectedException)ee);
                                return;
                            }
                            else
                            {
                                ee = ee.InnerException;
                            }
                        }
                    }
                }
            }
            catch (TExpectedException expectedException)
            {
                Assert
                    .IsTrue
                        (
                            expectedException
                                            .GetType()
                            ==
                            typeof(TExpectedException)
                        );
                processExpectedExceptionMessage
                                        (
                                            expectedException
                                            , expectedExceptionMessage
                                        );
                onProcessAction?.Invoke(expectedException);
                return;
            }
            catch (Exception exception)
            {
                Assert
                    .Fail
                        (
                            $@"Expected exception of type ""{typeof(TExpectedException)}"" but type of ""{exception.GetType()}"" was thrown instead."
                        );
                processExpectedExceptionMessage
                                        (
                                            exception
                                            , expectedExceptionMessage
                                        );

                return;
            }
            Assert
                .Fail
                    (
                        $@"Expected exception of type ""{typeof(TExpectedException)}"" but no exception was thrown."
                    );
        }

        public static void Throws
                                (
                                    this Assert @this
                                    , Type expectedExceptionType
                                    , Action action
                                    , string expectedExceptionMessage = null!
                                    , Action<Exception> onProcessAction = null!
                                    , bool drillDownInnerExceptions = true
                                )
        {
            try
            {
                action();
            }
            catch (AggregateException aggregateException)
            {
                var innerExceptions = aggregateException.Flatten().InnerExceptions;
                foreach (var e in innerExceptions)
                {
                    if
                        (
                            e
                                .GetType()
                            ==
                            expectedExceptionType
                            &&
                            string
                                .Compare
                                    (
                                        expectedExceptionMessage
                                        , e.Message
                                        , true
                                    )
                            ==
                            0
                        )
                    {
                        Assert
                            .IsTrue
                                (
                                    e.GetType()
                                    ==
                                    expectedExceptionType
                                );
                        processExpectedExceptionMessage
                                                (
                                                    e
                                                    , expectedExceptionMessage
                                                );
                        onProcessAction?.Invoke(e);
                        //break;
                        return;
                    }
                    else if
                        (
                            drillDownInnerExceptions
                            &&
                            e.InnerException != null
                        )
                    {
                        var ee = e.InnerException;
                        while (ee != null)
                        {
                            if
                                (
                                    ee.GetType() == expectedExceptionType
                                    &&
                                    string
                                        .Compare
                                            (
                                                expectedExceptionMessage
                                                , ee.Message
                                                , StringComparison.OrdinalIgnoreCase
                                            ) == 0
                                )
                            {
                                Assert
                                    .IsTrue
                                        (
                                            ee.GetType()
                                            ==
                                            expectedExceptionType
                                        );
                                processExpectedExceptionMessage
                                                        (
                                                            ee
                                                            , expectedExceptionMessage
                                                        );
                                onProcessAction?.Invoke(e);
                                return;
                            }
                            else
                            {
                                ee = ee.InnerException;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Assert
                    .IsTrue
                        (
                            exception
                                    .GetType()
                            ==
                            expectedExceptionType
                            , $@"Expected exception of type ""{expectedExceptionType}"" but type of ""{exception.GetType()}"" was thrown instead."
                        );
                processExpectedExceptionMessage
                                        (
                                            exception
                                            , expectedExceptionMessage
                                        );
                onProcessAction?.Invoke(exception);
                return;
            }
            Assert
                .Fail
                    (
                        $@"Expected exception of type ""{expectedExceptionType}"" but no exception was thrown."
                    );
        }
    }
}
#endif