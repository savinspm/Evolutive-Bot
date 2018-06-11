using System;

namespace Labyrinth.IO
{
    public partial class TxtMatrixSerializer
        : IMatrixSerializer
    {
        //________________________________________________________________________

        private readonly object m_Parameters = null;
        private System.IServiceProvider m_HostService;
        //________________________________________________________________________

        public virtual object Parameters
        {
            get { return this.m_Parameters; }
        }
        //________________________________________________________________________

        public virtual System.IServiceProvider HostService
        {
            get { return this.m_HostService; }
            set { this.m_HostService = value; }
        }
        //________________________________________________________________________

        public virtual string[] Extensions
        {
            get { return new string[] { ".txt", ".map" }; }
        }
        //________________________________________________________________________

        public virtual bool CanRead
        {
            get { return true; }
        }
        //________________________________________________________________________

        public virtual bool CanWrite
        {
            get { return true; }
        }
        //________________________________________________________________________
    }
}
