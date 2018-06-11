using System;

namespace Labyrinth.Kernel
{
    internal sealed class PointSpeedBuffer
        : System.Collections.Generic.IEnumerable<Point>
        , System.Collections.IEnumerable
        , System.Collections.ICollection
    {
        //________________________________________________________________________

        internal readonly Point[] m_Buffer;
        internal readonly int m_Capacity;
        internal int m_Count;
        //________________________________________________________________________

        internal PointSpeedBuffer(int capacity)
        {
            if (capacity <= 0) return;
            this.m_Capacity = capacity;
            this.m_Buffer = new Point[this.m_Capacity];
        }
        //________________________________________________________________________

        internal bool Fill(PointSpeedBuffer source)
        {
            if (source == null)
            {
                this.m_Count = 0;
                return true;
            }

            int iCount = source.Count;
            if (iCount <= 0)
            {
                this.m_Count = 0;
                return true;
            }

            if (iCount > this.m_Capacity) return false;

            this.m_Count = iCount;
            System.Array.Copy(source.m_Buffer, 0, this.m_Buffer, 0, this.m_Count);
            return true;
        }
        //________________________________________________________________________

        public Point this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.m_Count) || (index >= this.m_Capacity))
                    throw new ArgumentOutOfRangeException();

                return this.m_Buffer[index];
            }
            internal set
            {
                if ((index < 0) || (index >= this.m_Count) || (index >= this.m_Capacity))
                    throw new ArgumentOutOfRangeException();

                this.m_Buffer[index] = value;
            }
        }
        //________________________________________________________________________

        public bool Contains(Point value)
        {
            return this.IndexOf(value) >= 0;
        }
        //________________________________________________________________________

        public int IndexOf(Point value)
        {
            int index = this.Count;
            while (--index >= 0)
                if (this.m_Buffer[index] == value) return index;

            return -1;
        }
        //________________________________________________________________________

        #region " Implements "

        public System.Collections.Generic.IEnumerator<Point> GetEnumerator()
        {
            int iCount = this.Count;
            for (int i = 0; i < iCount; ++i)
                yield return this.m_Buffer[i];
        }
        //________________________________________________________________________

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        //________________________________________________________________________

        public void CopyTo(System.Array array, int index)
        {
            int iCount = this.Count;
            if (iCount <= 0) return;
            System.Array.Copy(this.m_Buffer, 0, array, index, iCount);
        }
        //________________________________________________________________________

        public int Count
        {
            get { return this.m_Count < this.m_Capacity ? this.m_Count : this.m_Capacity; }
        }
        //________________________________________________________________________

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }
        //________________________________________________________________________

        object System.Collections.ICollection.SyncRoot
        {
            get { return this; }
        }
        //________________________________________________________________________

        #endregion

    }
}
