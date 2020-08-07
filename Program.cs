using System;
using System.Threading;

namespace concurrent
{
    /*
Thread
ThreadStart
ParameterizedThreadStart
Thread.Start
ThreadInterruptedException
ThreadAbortException
Thread.Interrupt
Thread.Abort
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // 无参
            Thread thread = new Thread(Program.Method);
            thread.Start();

            Thread thread1 = new Thread(() => { Program.Method1(); });
            thread1.Start();

            // 方法参数传递
            Thread thread2 = new Thread(Program.Method2);
            thread2.Start(2);

            Thread thread3 = new Thread((seconds) => { Program.Method2(seconds); });
            thread3.Start(3);

            // 实例属性传递
            Thread thread4 = new Thread(new ClassA { Seconds = 4 }.Method);
            thread4.Start();

            // 返回值
            Thread thread5 = new Thread(new ClassA { Seconds = 5, CallBack = (milliseconds) => { Console.WriteLine($"return {milliseconds}ms"); } }.Method);
            thread5.Start();

            Console.WriteLine("all thread start");
            
            Console.WriteLine(thread5.ThreadState);
            Console.WriteLine(thread5.Join(1));
            Console.WriteLine(thread5.ThreadState);

            thread.Join();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            
            Console.WriteLine(thread5.ThreadState);
            thread5.Join();
            
            Console.WriteLine(thread5.ThreadState);

            Console.WriteLine("all thread end");
        }

        static void Method()
        {
            int seconds = 0;
            Thread.Sleep(seconds * 1000);
            Console.WriteLine($"sleep {seconds}s");
        }
        static void Method1()
        {
            int seconds = 1;
            Thread.Sleep(seconds * 1000);
            Console.WriteLine($"sleep {seconds}s");
        }

        static void Method2(object objSeconds)
        {
            int seconds = (int)objSeconds;
            Thread.Sleep(seconds * 1000);
            Console.WriteLine($"sleep {seconds}s");
        }
    }

    delegate void ThreadCallBack(int milliseconds);

    class ClassA
    {
        public ThreadCallBack CallBack { get; set; }
        public int Seconds { get; set; }
        public void Method()
        {
            Thread.Sleep(Seconds * 1000);
            Console.WriteLine($"sleep {Seconds}s");

            if (CallBack != null)
            {
                CallBack(Seconds * 1000);
            }
        }
    }
}
