namespace GC
{
    using System;
    using System.Threading;
    using System.Runtime; // GCSettings is in this namespace

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Application is running with server GC = " + GCSettings.IsServerGC);
        }
    }

    public static class GCNotification
    {
        private static Action<Int32> _gcDone = null; // The event's field

        public static event Action<Int32> GCDone
        {
            add
            {
                // If there were no registered delegates before, start reporting notifications now
                if (_gcDone == null) { new GenObject(0); new GenObject(2); }
                _gcDone += value;
            }
            remove { _gcDone -= value; }
        }

        private sealed class GenObject
        {
            private readonly Int32 _generation;

            public GenObject(Int32 generation)
            {
                this._generation = generation;
            }

            ~GenObject()
            { 
                // This is the Finalize method
                // If this object is in the generation we want (or higher),
                // notify the delegates that a GC just completed
                if (GC.GetGeneration(this) >= this._generation)
                {
                    Action<Int32> temp = Volatile.Read(ref _gcDone);
                    if (temp != null) temp(this._generation);
                }

                // Keep reporting notifications if there is at least one delegate registered,
                // the AppDomain isn't unloading, and the process isn’t shutting down
                if ((_gcDone != null)
                && !AppDomain.CurrentDomain.IsFinalizingForUnload()
                && !Environment.HasShutdownStarted)
                {
                    // For Gen 0, create a new object; for Gen 2, resurrect the object
                    // & let the GC call Finalize again the next time Gen 2 is GC'd
                    if (this._generation == 0) new GenObject(0);
                    else GC.ReRegisterForFinalize(this);
                }
                else { /* Let the objects go away */ }
            }
        }
    }
}