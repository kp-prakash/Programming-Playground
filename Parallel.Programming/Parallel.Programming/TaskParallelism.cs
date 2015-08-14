using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    internal class TaskParallelism
    {
        #region WaitAll

        //static void MethodA()
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine("MethodA");
        //}

        //static void MethodB() { Console.WriteLine("MethodB"); }

        //static void Main()
        //{
        //    var taskA = Task.Factory.StartNew(MethodA);
        //    var taskB = Task.Factory.StartNew(MethodB);
        //    Task.WaitAll(new[] { taskA, taskB });
        //    Console.ReadLine();
        //} 

        #endregion

        #region WaitAny

        //static void MethodA() { Thread.SpinWait(int.MaxValue / 4); }
        //static void MethodB() { Thread.SpinWait(int.MaxValue / 2); }
        //static void Main()
        //{
        //    var taskA = Task.Factory.StartNew(MethodA);
        //    var taskB = Task.Factory.StartNew(MethodB);
        //    Console.WriteLine("TaskA id = {0}", taskA.Id);
        //    Console.WriteLine("TaskB id = {0}", taskB.Id);
        //    var tasks = new[] { taskA, taskB };
        //    int whichTask = Task.WaitAny(tasks);
        //    Console.WriteLine("Task {0} is the gold medal task.",
        //    tasks[whichTask].Id);
        //    Console.WriteLine("Press enter to exit");
        //    Console.ReadLine();
        //} 

        #endregion

        #region Wait() with Timeout!!!

        //static void MethodA()
        //{
        //    Thread.Sleep(1000);
        //}
        ///// <summary>
        ///// If the Wait() method is called with waiting time less than or equal to
        ///// time taken then the method will eventually timeout.
        ///// </summary>
        //public static void Main()
        //{
        //    var taskA = Task.Factory.StartNew(MethodA);
        //    if (!taskA.Wait(1001))
        //    {
        //        Console.WriteLine("Timed out!!!");
        //        Console.ReadLine();
        //        return;
        //    }
        //    Console.WriteLine("Completed!!!");
        //    Console.ReadLine();
        //} 

        #endregion

        #region Task<T>.Factory.StartNew()

        //public static int Length(object obj)
        //{
        //    if (null == obj) return 0;
        //    return obj.ToString().Length;
        //}

        //public static void Main()
        //{
        //    #region Task<T>
        //    //var taskA = Task<int>.Factory.StartNew(() => 42);
        //    //taskA.Wait();
        //    //Console.WriteLine(taskA.Result);
        //    //Console.ReadLine(); 
        //    #endregion

        //    #region Task<T> With Return and Argument
        //    //The state object can only be object.
        //    Task<int> taskA = Task<int>.Factory.StartNew(Length, "Test String");
        //    //Task<int> taskA = Task<int>.Factory.StartNew(val => ((string)val).Length, "Test String");
        //    taskA.Wait();
        //    Console.WriteLine(taskA.Result);
        //    Console.ReadLine();
        //    #endregion
        //} 

        #endregion

        #region Aggregate Exception

        //public static void Main()
        //{
        //    Task taskA = null;
        //    try
        //    {
        //        taskA = Task.Factory.StartNew(() =>
        //                                          {
        //                                              int a = 5, b = 0;
        //                                              a /= b;
        //                                          });
        //        taskA.Wait();
        //    }
        //    catch (AggregateException ae)
        //    {
        //        Console.WriteLine("Task has " + taskA.Status.ToString());
        //        Console.WriteLine(ae.InnerException);
        //    }
        //} 

        #endregion

        #region Barrier

        //public static void MethodA()
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine("A Completed");
        //}

        //public static void MethodB()
        //{
        //    Thread.Sleep(2000);
        //    Console.WriteLine("B Completed");
        //}

        //public static void MethodC()
        //{
        //    Thread.Sleep(3000);
        //    Console.WriteLine("C Completed");
        //}

        //public static void Main()
        //{
        //    var taskBarrier = new Barrier(3);
        //    var taskA = Task.Factory.StartNew(() =>
        //                                          {
        //                                              MethodA();
        //                                              taskBarrier.SignalAndWait();
        //                                          });
        //    var taskB = Task.Factory.StartNew(() =>
        //                                          {
        //                                              MethodB();
        //                                              taskBarrier.SignalAndWait();
        //                                          });
        //    var taskC = Task.Factory.StartNew(() =>
        //                                          {
        //                                              MethodC();
        //                                              taskBarrier.SignalAndWait();
        //                                          });
        //    Task.WaitAll(new[] { taskA, taskB, taskC });
        //    Console.WriteLine("All tasks completed");
        //    Console.ReadLine();
        //} 

        #endregion

        #region Cooperative Cancellation

        //public static void MethodA(CancellationToken cancellationToken)
        //{
        //    Thread.Sleep(1000);
        //    if(cancellationToken.IsCancellationRequested)
        //        cancellationToken.ThrowIfCancellationRequested();
        //    Console.WriteLine("A Completed");
        //}

        //public static void MethodB(CancellationToken cancellationToken)
        //{
        //    Thread.Sleep(2000);
        //    if (cancellationToken.IsCancellationRequested)
        //        cancellationToken.ThrowIfCancellationRequested();
        //    Console.WriteLine("B Completed");
        //}

        //public static void MethodC(CancellationToken cancellationToken)
        //{
        //    Thread.Sleep(3000);
        //    if (cancellationToken.IsCancellationRequested)
        //        cancellationToken.ThrowIfCancellationRequested();
        //    Console.WriteLine("C Completed");
        //}

        //public static void Main()
        //{
        //    try
        //    {
        //        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //        CancellationToken cancellationToken = cancellationTokenSource.Token;
        //        // var taskBarrier = new Barrier(3);
        //        var taskA = Task.Factory.StartNew(() =>
        //                                              {
        //                                                  MethodA(cancellationToken);
        //                                                  //taskBarrier.SignalAndWait();
        //                                              }, cancellationToken);
        //        var taskB = Task.Factory.StartNew(() =>
        //                                              {
        //                                                  MethodB(cancellationToken);
        //                                                  //taskBarrier.SignalAndWait();
        //                                              }, cancellationToken);
        //        var taskC = Task.Factory.StartNew(() =>
        //                                              {
        //                                                  MethodC(cancellationToken);
        //                                                  //taskBarrier.SignalAndWait();
        //                                              }, cancellationToken);
        //        Task.WaitAny(new[] { taskA, taskB, taskC });
        //        cancellationTokenSource.Cancel();
        //        Console.WriteLine("All tasks completed");
        //        Console.ReadLine();
        //    }
        //    catch (OperationCanceledException operationCanceledException)
        //    {
        //        Console.WriteLine(operationCanceledException.Message);
        //    }
        //    catch (AggregateException aggregateException)
        //    {
        //        foreach (var innerException in aggregateException.InnerExceptions)
        //        {
        //            Console.WriteLine(innerException.Message);
        //        }
        //    }
        //}

        #endregion

        #region Task.ContinueWith()

        //public static void Main()
        //{
        //    Task<int> calculate = new Task<int>(() =>
        //                                            {
        //                                                Console.WriteLine("Calced.");
        //                                                return 42;
        //                                            });
        //    Task answer = calculate.ContinueWith((antecedent) =>
        //                                             {
        //                                                 Console.WriteLine("Answer is {0}", antecedent.Result);
        //                                             });
        //    calculate.Start();
        //    Task.WaitAll(calculate, answer);
        //    Console.ReadLine();
        //} 

        #endregion

        #region Task.Factory.ContinueWhenAll

        //public static int PerformCalculation() { return 42;}

        //public static void Main()
        //{
        //    var taskA = new Task<int>(() =>
        //                                        {
        //                                            Console.WriteLine("Task A.");
        //                                            return PerformCalculation();
        //                                        });
        //    var taskB = new Task<int>(() =>
        //                                        {
        //                                            Console.WriteLine("Task B.");
        //                                            return PerformCalculation();
        //                                        });
        //    var total = Task.Factory.ContinueWhenAll(new Task<int>[] {taskA, taskB},
        //                                              (tasks) =>
        //                                              Console.WriteLine("Total {0}", tasks[0].Result + tasks[1].Result));
        //    taskA.Start();
        //    taskB.Start();
        //    Task.WaitAll(taskA, taskB);
        //    total.Wait();
        //    Console.ReadLine();
        //} 

        #endregion

        #region Sub Task

        //public static void Main()
        //{
        //    var outer = Task.Factory.StartNew(
        //        () =>
        //            {
        //                Console.WriteLine("Outer");
        //                var inner = new Task(
        //                    () =>
        //                        {
        //                            Thread.Sleep(5000);
        //                            Console.WriteLine("Inner");
        //                        }
        //                    );
        //                inner.Start();
        //                inner.Wait();
        //            }
        //        );
        //    outer.Wait();
        //    Console.WriteLine("Both tasks completed.");
        //    Console.ReadKey();
        //} 

        #endregion

        #region Parent / Child Task
        //public static void Main()
        //{

        //    Task parent = new Task(() =>
        //                               {
        //                                   Console.WriteLine("Parent task.");
        //                                   Task.Factory.StartNew(() => { Thread.Sleep(5000); },
        //                                                         TaskCreationOptions.AttachedToParent);
        //                               });
        //    parent.Start();
        //    if ((!(parent.Wait(2000)) &&
        //         (parent.Status == TaskStatus.WaitingForChildrenToComplete)))
        //    {
        //        Console.WriteLine("Parent completed but child not finished.");
        //        parent.Wait();
        //        Console.WriteLine("Parent and Child Completed.");
        //        Console.ReadKey();
        //    }
        //}
        #endregion
    }
}