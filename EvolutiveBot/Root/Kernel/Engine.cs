using System;

namespace Labyrinth.Kernel
{
    public partial class Engine
        : System.IDisposable
    {
        //________________________________________________________________________

        public event ResultEventHandler Result;
        private Matrix m_Matrix;
        private long m_Version;
        private bool m_DebugMode = false;
        private System.ComponentModel.ISynchronizeInvoke m_Synchronize;
        private int m_AsyncResultCount = 0;
        //________________________________________________________________________

        public Matrix Matrix
        {
            get { return this.m_Matrix; }
            set
            {
                //-----Validation ...
                if (value == this.m_Matrix)
                    return;

                if (value == null)
                    throw new ArgumentNullException();

                if ((value.m_Size.Row <= 2) || (value.m_Size.Column <= 2))
                    throw new ArgumentOutOfRangeException();

                //-----Set ...
                this.StopProcessing();
                this.m_Matrix = value;
            }
        }
        //________________________________________________________________________

        [System.ComponentModel.DefaultValue(false)]
        public bool DebugMode
        {
            get { return this.m_DebugMode; }
            set { this.m_DebugMode = value; }
        }
        //________________________________________________________________________

        [System.ComponentModel.DefaultValue((string)null)]
        public System.ComponentModel.ISynchronizeInvoke Synchronize
        {
            get { return this.m_Synchronize; }
            set { this.m_Synchronize = value; }
        }
        //________________________________________________________________________

        public void StopProcessing()
        {
            System.Threading.Interlocked.Increment(ref this.m_Version);
        }
        //________________________________________________________________________

        public void BeginProcessing()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                this.AsyncStart,
                System.Threading.Interlocked.Increment(ref this.m_Version));
        }
        //________________________________________________________________________

        protected virtual void OnResult(ResultEventArgs e)
        {
            var handler = this.Result;
            if (handler == null) return;
            //-----
            var isync = this.m_Synchronize;
            if ((isync == null) || (!isync.InvokeRequired))
            {
                handler(this, e);
                return;
            }
            //-----
            if (e.IsFinish)
            {
                isync.BeginInvoke(handler, new object[] { this, e });
                return;
            }
            //-----
            if (this.m_AsyncResultCount <= 0) //Sync filter. [for performance]
            {
                System.Threading.Interlocked.Increment(ref this.m_AsyncResultCount); //Sync lock.
                isync.BeginInvoke(new ResultEventHandler(this.OnAsyncResult), new object[] { this, e });
            }
        }
        //________________________________________________________________________

        private void OnAsyncResult(object sender, ResultEventArgs e) //run of the app main thread.
        {
            try
            {
                var handler = this.Result;
                if (handler == null) return;
                handler(this, e);
            }
            finally
            {
                System.Threading.Interlocked.Decrement(ref this.m_AsyncResultCount); //Sync unlock.
            }
        }
        //________________________________________________________________________

        public virtual void Dispose()
        {
            this.StopProcessing();
            System.GC.SuppressFinalize(this);
        }
        //________________________________________________________________________
    }
}
