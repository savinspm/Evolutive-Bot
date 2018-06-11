using System;

namespace Labyrinth.Kernel
{
    public struct Point
    {
        //________________________________________________________________________

        public byte Row;
        public byte Column;
        //________________________________________________________________________

        public Point(byte row, byte column)
        {
            this.Row = row;
            this.Column = column;
        }
        //________________________________________________________________________

        internal Point GetSidePoint(PointSide value)
        {
            Point ret = this;
            switch (value)
            {
                case PointSide.Top:
                    --ret.Row;
                    return ret;

                case PointSide.Right:
                    ++ret.Column;
                    return ret;

                case PointSide.Bottom:
                    ++ret.Row;
                    return ret;

                case PointSide.Left:
                    --ret.Column;
                    return ret;

                default:
                    return ret;
            }
        }
        //________________________________________________________________________

        public static bool operator ==(Point value1, Point value2)
        {
            return (value1.Row == value2.Row) && (value1.Column == value2.Column);
        }
        //________________________________________________________________________

        public static bool operator !=(Point value1, Point value2)
        {
            return (value1.Row != value2.Row) || (value1.Column != value2.Column);
        }
        //________________________________________________________________________

        public override bool Equals(object obj)
        {
            if (obj is Point)
                return this == (Point)obj;

            return base.Equals(obj);
        }
        //________________________________________________________________________

        public override int GetHashCode()
        {
            return (((int)this.Row) << 8) | (int)this.Column;
        }
        //________________________________________________________________________
    }
}
