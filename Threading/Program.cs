namespace Threading
{
    using System;
    using System.Threading;

    internal class Program
    {
        #region Events, Anonymous Methods, Delegates
        //public class Person
        //{
        //    private string _name;

        //    public string Name
        //    {
        //        get
        //        {
        //            return _name;
        //        }

        //        set
        //        {
        //            if (_name != value && NameChanged != null)
        //            {
        //                _name = value;
        //                NameChanged(_name);
        //            }
        //            else
        //            {
        //                _name = value;
        //            }
        //        }
        //    }

        //    public Func<string, string, string> Concat = (string s1, string s2) =>
        //    {
        //        return s1 + s2;
        //    };

        //    public event Action<string> NameChanged;
        //}

        //public static void Main(string[] args)
        //{
        //    Person venkat = new Person { Name = "Venkat" };
        //    venkat.NameChanged += Person_NameChanged;
        //    venkat.Name = "Venkat";
        //    venkat.Name = venkat.Concat("Venkat", " Raghvan");
        //}

        //private static void Person_NameChanged(string name)
        //{
        //    Console.WriteLine("New name is : {0}", name);
        //} 
        #endregion

        #region Threading Basics

        //static void Main(string[] args)
        //{
        //    var thread = CreateThread();
        //    Console.WriteLine("Main Thread! {0}", Thread.CurrentThread.ManagedThreadId);
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
        //    Console.WriteLine("From worker thread: {0}", Thread.CurrentThread.ManagedThreadId);
        //    for (int i = 0; i < 500; i++)
        //    {
        //        Console.Write("X-->");
        //        Thread.Sleep(20);
        //    }
        //    Console.Write("X");
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
        //    Thread.CurrentThread.Name = "Main";
        //    Thread t = new Thread(Go);
        //    t.Name = "Test";
        //    t.Start();
        //    Go();
        //    Console.ReadKey();
        //}

        //static void Go()
        //{
        //    Console.WriteLine("Entered {0} {1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
        //    lock (Locker)
        //    {
        //        if (_done)
        //        {
        //            return;
        //        }
        //        _done = true;
        //        Console.WriteLine("Done - {0} {1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
        //    }
        //    Console.WriteLine("Exit {0} {1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
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

        //static void main(string[] args)
        //{
        //    // if isbackground is set to false, the thread is 
        //    // runs in the foreground and waits for readline().
        //    // when set to true the program exits immediately. 

        //    const bool isbackground = true;
        //    var worker = new thread(() => console.readline());
        //    if (isbackground) worker.isbackground = true;
        //    worker.start();
        //}

        #endregion
    }
}