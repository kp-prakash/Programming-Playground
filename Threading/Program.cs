namespace Threading
{
    using System;
    using System.Threading;

    internal class Program
    {
        #region Threading Basics

        //static void Main(string[] args)
        //{
        //    var thread = CreateThread();
        //    thread.Start();
        //    thread.Join();
        //    Console.Write("Press any key to exit...");
        //    Console.ReadKey();
        //}

        //private static Thread CreateThread()
        //{
        //    var thread = new Thread(WriteToConsole);
        //    return thread;
        //}

        //public static void WriteToConsole()
        //{
        //    for (int i = 0; i < 500; i++)
        //    {
        //        Console.WriteLine("Writing to Console...");
        //    }
        //}  

        #endregion

        #region Local State

        //private static void Go()
        //{
        //    // Declare and use a local variable - 'cycles'
        //    for (int cycles = 0; cycles < 5; cycles++)
        //    {
        //        Console.WriteLine(cycles);
        //    }
        //}

        //private static void Main()
        //{
        //    new Thread(Go).Start(); // Call Go() on a new thread
        //    Go(); // Call Go() on the main thread
        //    Console.ReadLine();
        //}

        #endregion

        #region Shared State

        //private bool _done;

        //static void Main()
        //{
        //    var program = new Program();
        //    new Thread(program.Go).Start();
        //    program.Go();
        //    Console.ReadKey();
        //}

        //private void Go()
        //{
        //    if (_done)
        //    {
        //        return;
        //    }
        //    this._done = true;
        //    //Done gets printed only once due to shared state.
        //    Console.WriteLine("Done!");
        //}

        #endregion

        #region Shared State - Lambda Expression

        //static void Main()
        //{
        //    bool done = false;
        //    ThreadStart action = () =>
        //        {
        //            if (done)
        //            {
        //                return;
        //            }
        //            done = true;
        //            Console.WriteLine("Done");
        //        };
        //    new Thread(action).Start();
        //    action();
        //} 

        #endregion

        #region Shared State - Statics

        //static bool _done; 

        //static void Main()
        //{
        //    new Thread(Go).Start();
        //    Go();
        //}
        //static void Go()
        //{
        //    if (_done)
        //    {
        //        return;
        //    }
        //    _done = true; 
        //    Console.WriteLine("Done");
        //}

        #endregion

        #region Lock Statement

        //static bool _done;
        //static readonly object Locker = new object();

        //static void Main()
        //{
        //    new Thread(Go).Start();
        //    Go();
        //    Console.ReadKey();
        //}

        //static void Go()
        //{
        //    lock (Locker)
        //    {
        //        if (_done)
        //        {
        //            return;
        //        }
        //        Console.WriteLine("Done"); _done = true;
        //    }
        //}

        #endregion

        #region Passing data to a thread using Lambda Expression

        //static void Main()
        //{
        //    var thread = new Thread(() => Print("Hello from t!"));
        //    thread.Start();
        //    Console.ReadKey();
        //}

        //static void Print(string message)
        //{
        //    Console.WriteLine(message);
        //} 

        #endregion

        #region Passing data to a thread using Start()'s parameter

        //static void Main()
        //{
        //    var thread = new Thread(Print);
        //    thread.Start("C#");
        //}

        //private static void Print(object messageObject)
        //{
        //    if (messageObject == null)
        //    {
        //        return;
        //    }
        //    var message = messageObject.ToString();
        //    Console.WriteLine("This sample was written in : {0}",message);
        //    Console.ReadKey();
        //} 

        #endregion

        #region Lambda expressions and captured variables

        //static void Main()
        //{
        //    Console.WriteLine("Unpredictable result!");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        new Thread(() => Console.Write(i)).Start();
        //    }
        //    Console.ReadKey();
            
        //    Console.WriteLine();
            
        //    Console.WriteLine("Fixed!");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int temp = i;
        //        new Thread(() => Console.Write(temp)).Start();
        //    }
        //    Console.ReadKey();
        //}

        #endregion

        #region Exception Handling

        // See the example below. Try catch should be 
        // in the method executed in the thread.

        //public static void Main()
        //{
        //    try
        //    {
        //        new Thread(Go).Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        //The code will not reach here!
        //        Console.WriteLine("Exception!");
        //    }
        //}

        //static void Go() { throw null; } // Throws a NullReferenceException
        
        //-------------------------------------------------------------------
        //public static void Main()
        //{
        //    new Thread(Go).Start();
        //}

        //static void Go()
        //{
        //    try
        //    {
        //        throw null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception!");
        //        Console.ReadKey();
        //    }
        //}

        #endregion

        #region Foregorund vs Background Thread

        //static void Main(string[] args)
        //{
        //    // If isBackground is set to false, the thread is 
        //    // runs in the foreground and waits for ReadLine().
        //    // When set to true the program exits immediately. 

        //    const bool isBackground = true;
        //    var worker = new Thread(() => Console.ReadLine());
        //    if (isBackground) worker.IsBackground = true;
        //    worker.Start();
        //}

        #endregion


    }
}