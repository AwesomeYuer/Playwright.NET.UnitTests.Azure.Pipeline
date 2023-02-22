namespace ConsoleApp1
{
    using System;
    public interface IHello
    {
        string Hello
                (
                    string whom
                    , string whom2
                );
    }
    /// <summary>
    /// Class1
    /// </summary>
    public class Class1
                        : IHello
    {
        // _hello
        private string _hello;
        public Class1(string hello)
        { 
            _hello = hello;
        }
        public string Hello(string whom, string whom2) 
        {
            // output 输出
            var r = $"{_hello}: {whom}, {whom2}";
            Console.WriteLine(r);
            return r;
        }  
    }
    public class Invoker
    {
        private readonly IHello _hello;
        public Invoker(IHello hello)
        {
            _hello = hello;
        }
        public string Invoke(string whom, string whom2)
        {
            return _hello.Hello(whom, whom2);
        }
    }
}
