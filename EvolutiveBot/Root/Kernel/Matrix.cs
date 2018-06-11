using System;
using System.Collections.Generic;

namespace Labyrinth.Kernel
{
    public partial class Matrix
    {
        //________________________________________________________________________

        internal readonly CellType[,] m_Buffer;
        internal readonly Point m_Size;
        //________________________________________________________________________

        public List<Point> tileSolutions { get; set; }
        public Matrix(Point Size)
        {
            if ((Size.Row <= 0) || (Size.Column <= 0))
                throw new ArgumentOutOfRangeException();

            this.m_Size = Size;
            this.m_Buffer = new CellType[this.m_Size.Row, this.m_Size.Column];
      
        }
        //________________________________________________________________________

        public Matrix(Matrix sourcecopy)
        {
            if ((sourcecopy == null) || (sourcecopy.m_Size.Row <= 0) || (sourcecopy.m_Size.Column <= 0))
                throw new ArgumentException();

            this.m_Size = sourcecopy.m_Size;
            this.m_Buffer = (CellType[,])sourcecopy.m_Buffer.Clone();
        }
        //________________________________________________________________________

        public Point Size
        {
            get { return this.m_Size; }
        }
        //________________________________________________________________________

        public CellType this[Point point]
        {
            get
            {
                if ((point.Row < 0) || (point.Row >= this.m_Size.Row) || (point.Column < 0) || (point.Column >= this.m_Size.Column))
                    throw new ArgumentOutOfRangeException();

                return this.m_Buffer[point.Row, point.Column];
            }
            internal set
            {
                if ((point.Row < 0) || (point.Row >= this.m_Size.Row) || (point.Column < 0) || (point.Column >= this.m_Size.Column))
                    throw new ArgumentOutOfRangeException();

                this.m_Buffer[point.Row, point.Column] = value;
            }
        }
        //________________________________________________________________________

        public void SetAll(System.Collections.Generic.IEnumerable<Point> list, CellType value)
        {
            if (list == null) return;
            foreach (Point pt in list)
            {
                if ((pt.Row >= 0) && (pt.Row < this.m_Size.Row) && (pt.Column >= 0) && (pt.Column < this.m_Size.Column))
                {
                    this.m_Buffer[pt.Row, pt.Column] = value;
                }
            }
        }
        //________________________________________________________________________

        internal void SetAll(PointSpeedBuffer list, CellType value)
        {
            if (list == null) return;
            int index = list.Count;
            while (--index >= 0)
            {
                var pt = list.m_Buffer[index];
                this.m_Buffer[pt.Row, pt.Column] = value;
            }
        }
        //________________________________________________________________________

        public void ClearWay()
        {
            for (int r = 0; r < this.m_Size.Row; ++r)
            {
                for (int c = 0; c < this.m_Size.Column; ++c)
                {
                    if (this.m_Buffer[r, c] != CellType.Fix) this.m_Buffer[r, c] = CellType.None;
                }
            }
        }
        //________________________________________________________________________

        public void CreateBorder()
        {
            Point pt = new Point();

            //-----Top border ...
            pt.Row = 0;
            for (pt.Column = 0; pt.Column < this.m_Size.Column; ++pt.Column)
            {
                this.m_Buffer[pt.Row, pt.Column] = CellType.Fix;
            }

            //-----Right border ...
            pt.Column = (byte)(this.m_Size.Column - (byte)1);
            for (pt.Row = 0; pt.Row < this.m_Size.Row; ++pt.Row)
            {
                this.m_Buffer[pt.Row, pt.Column] = CellType.Fix;
            }

            //-----Bottom border ...
            pt.Row = (byte)(this.m_Size.Row - (byte)1);
            for (pt.Column = 0; pt.Column < this.m_Size.Column; ++pt.Column)
            {
                this.m_Buffer[pt.Row, pt.Column] = CellType.Fix;
            }

            //-----Left border ...
            pt.Column = 0;
            for (pt.Row = 0; pt.Row < this.m_Size.Row; ++pt.Row)
            {
                this.m_Buffer[pt.Row, pt.Column] = CellType.Fix;
            }
        }
        //________________________________________________________________________

        public bool Contains(CellType value)
        {
            for (byte r = byte.MinValue; r < this.m_Size.Row; ++r)
            {
                for (byte c = byte.MinValue; c < this.m_Size.Column; ++c)
                {
                    if (this.m_Buffer[r, c] == value) return true;
                }
            }
            return false;
        }
        //________________________________________________________________________

        public bool IsValid()
        {
            return (this.m_Size.Row >= 3) && (this.m_Size.Column >= 3);
        }
        //________________________________________________________________________

        public static bool IsValid(Matrix matrix)
        {
            return (matrix != null) && (matrix.IsValid());
        }
        //________________________________________________________________________
    }
}
