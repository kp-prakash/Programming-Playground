using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    #region Parallel
    //public class Program
    //{
    //    private const int NUM_AES_KEYS = 800000;
    //    private const int NUM_MD5_HASHES = 100000;

    //    private static string ConvertToHexString(Byte[] byteArray)
    //    {
    //        // Convert the byte array to hexadecimal string
    //        var sb = new StringBuilder(byteArray.Length);
    //        for (int i = 0; i < byteArray.Length; i++)
    //        {
    //            sb.Append(byteArray[i].ToString("X2"));
    //        }
    //        return sb.ToString();
    //    }

    //    private static void GenerateAESKeys()
    //    {
    //        var sw = Stopwatch.StartNew();
    //        var aesM = new AesManaged();
    //        Parallel.For(1, NUM_AES_KEYS + 1, (int i) =>
    //                                              {
    //                                                  aesM.GenerateKey();
    //                                                  byte[] result = aesM.Key;
    //                                                  string hexString = ConvertToHexString(result);
    //                                                  //Console.WriteLine("AES KEY: {0} ", hexString);
    //                                              });
    //        Console.WriteLine("AES: " + sw.Elapsed.ToString());
    //    }

    //    private static void GenerateMD5Hashes()
    //    {
    //        var sw = Stopwatch.StartNew();
    //        var md5M = MD5.Create();
    //        for (int i = 1; i <= NUM_MD5_HASHES; i++)
    //        {
    //            byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
    //            byte[] result = md5M.ComputeHash(data);
    //            string hexString = ConvertToHexString(result);
    //            //Console.WriteLine("MD5 HASH: {0}", hexString);
    //        }
    //        Console.WriteLine("MD5: " + sw.Elapsed.ToString());
    //    }

    //    private static void Main(string[] args)
    //    {
    //        var sw = Stopwatch.StartNew();
    //        //GenerateAESKeys();
    //        //GenerateMD5Hashes();
    //        Parallel.Invoke(GenerateAESKeys, GenerateMD5Hashes);
    //        Console.WriteLine(sw.Elapsed.ToString());
    //        // Display the results and wait for the user to press a key
    //        Console.ReadLine();
    //    }
    //} 
    #endregion

    #region volatile
    //public class Worker
    //{
    //    // This method is called when the thread is started. 
    //    public void DoWork()
    //    {
    //        while (!_shouldStop)
    //        {
    //            Console.WriteLine("Worker thread: working...");
    //        }
    //        Console.WriteLine("Worker thread: terminating gracefully.");
    //    }
    //    public void RequestStop()
    //    {
    //        _shouldStop = true;
    //    }
    //    // Keyword volatile is used as a hint to the compiler that this data 
    //    // member is accessed by multiple threads. 
    //    private volatile bool _shouldStop;
    //}

    //public class WorkerThreadExample
    //{
    //    static void Main()
    //    {
    //        // Create the worker thread object. This does not start the thread.
    //        var workerObject = new Worker();
    //        var workerThread = new Thread(workerObject.DoWork);

    //        // Start the worker thread.
    //        workerThread.Start();
    //        Console.WriteLine("Main thread: starting worker thread...");

    //        // Loop until the worker thread activates. 
    //        while (!workerThread.IsAlive)
    //        {
    //        }

    //        // Put the main thread to sleep for 1 millisecond to 
    //        // allow the worker thread to do some work.
    //        Thread.Sleep(100);

    //        // Request that the worker thread stop itself.
    //        workerObject.RequestStop();

    //        // Use the Thread.Join method to block the current thread  
    //        // until the object's thread terminates.
    //        workerThread.Join();
    //        Console.WriteLine("Main thread: worker thread has terminated.");
    //        Console.ReadLine();
    //    }
    //    // Sample output: 
    //    // Main thread: starting worker thread... 
    //    // Worker thread: working... 
    //    // Worker thread: working... 
    //    // Worker thread: working... 
    //    // Worker thread: working... 
    //    // Worker thread: working... 
    //    // Worker thread: working... 
    //    // Worker thread: terminating gracefully. 
    //    // Main thread: worker thread has terminated.
    //} 
    #endregion


}
