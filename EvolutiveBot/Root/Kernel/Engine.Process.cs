using System;

namespace Labyrinth.Kernel
{
    partial class Engine
    {
        //________________________________________________________________________

        private const int SLEEPINDEBUGMODE = 5;
        //________________________________________________________________________

        private void AsyncStart(object state)
        {
            PointSpeedBuffer pathMin = null;

            long version = (long)state;
            try
            {
                if (this.m_Version != version) return;

                //-----Validation ...
                if ((this.m_Matrix.m_Size.Row <= 2) || (this.m_Matrix.m_Size.Column <= 2))
                    return;

                //-----Find start and end point ...
                Point ptStart, ptEnd;
                Point ptStartInside, ptEndInside; //For speed.
                if (!FindInputAndOutputPoints(this.m_Matrix, out ptStart, out ptStartInside, out ptEnd, out ptEndInside)) return;

                //-----Initialize ....
                pathMin = new PointSpeedBuffer((this.m_Matrix.m_Size.Column - 2) * (this.m_Matrix.m_Size.Row - 2));

                if ((ptStart == ptEndInside) || (ptStartInside == ptEnd))
                {
                    pathMin.m_Buffer[pathMin.m_Count++] = ptStart;
                    pathMin.m_Buffer[pathMin.m_Count++] = ptEnd;
                    return;
                }

                var matrix = new Matrix(this.m_Matrix);
                matrix.ClearWay();

                //-----Remove close points. Only for speed and performance.
                RemoveClosePoints_Pass1(matrix);
             

                if (
                    (matrix.m_Buffer[ptStartInside.Row, ptStartInside.Column] == CellType.Fix) ||
                    (matrix.m_Buffer[ptEndInside.Row, ptEndInside.Column] == CellType.Fix)
                    )
                {
                    pathMin = null;
                    return;
                }
                //-----
                var pathCur = new PointSpeedBuffer((this.m_Matrix.m_Size.Column - 2) * (this.m_Matrix.m_Size.Row - 2));
                
                pathMin.m_Count = int.MaxValue;
                pathCur.m_Buffer[pathCur.m_Count++] = ptStart;
                pathCur.m_Buffer[pathCur.m_Count++] = ptStartInside;
                
                //-----Start ....
                --pathCur.m_Count;
                int iFront = 1;
                while (true)
                {
                    if (this.m_Version != version) return;

                    if (this.m_DebugMode)
                    {
                        this.OnResult(new ResultEventArgs(pathCur, false));
                        System.Threading.Thread.Sleep(SLEEPINDEBUGMODE);
                    }

                    if (iFront > 0)
                    {
                        ++pathCur.m_Count;
                    }
                    else
                    {
                        pathCur.m_Count += iFront;
                        if (pathCur.m_Count <= 1) break;
                    }


                    iFront = FindNextPoint(matrix, pathCur, ptEndInside, pathCur.m_Count - 1, iFront > 0);
                    if (iFront > 0)
                    {
                        if (pathCur.m_Buffer[pathCur.m_Count] == ptEndInside) //Found a true path?
                        {
                            if (pathCur.m_Count < (pathCur.m_Capacity - 2))
                            {
                                ++pathCur.m_Count;
                                pathCur.m_Buffer[pathCur.m_Count++] = ptEnd;

                                if (pathCur.m_Count < pathMin.m_Count)
                                {
                                    pathMin.Fill(pathCur);
                                    this.OnResult(new ResultEventArgs(pathMin, false));
                                }

                                if (pathCur.m_Count <= 5) break;
                                iFront = -3;
                            }
                            else
                            {
                                iFront = -1;
                            }
                        }
                        else
                        {
                            iFront = +1;
                        }

                    }
                    else if (pathCur.m_Count <= 2)
                    {
                        break;
                    }
                }
            }
            catch
            {
                if (System.Threading.Interlocked.Read(ref this.m_Version) != version) return;
                pathMin = null;
#if (DEBUG)
                System.Diagnostics.Debugger.Break();
#endif
            }
            finally
            {
                if (System.Threading.Interlocked.Read(ref this.m_Version) == version)
                {
                    if ((pathMin == null) || (pathMin.m_Count <= 1) || (pathMin.m_Count > pathMin.m_Capacity))
                    {
                        //-----Not found ...
                        this.OnResult(new ResultEventArgs(null, true));
                    }
                    else
                    {
                        //-----Found ...
                        this.OnResult(new ResultEventArgs(pathMin, true));
                    }
                }
            }
        }
        //________________________________________________________________________

        private static int FindNextPoint(Matrix matrix, PointSpeedBuffer buffer, Point end, int index, bool first)
        {
            if ((index + 1) >= buffer.m_Capacity) return -1;
            PointSide side = PointSide.None;
            Point point = buffer.m_Buffer[index];
            Point back = buffer.m_Buffer[index - 1];
            Point next;
            //-----
            if (first)
            {
                //For speed.
                next = point; --next.Row; //Top
                if ((next != back) && (buffer.IndexOf(next) >= 0)) return -1;
                if (next == end)
                {
                    buffer.m_Buffer[index + 1] = next;
                    return +1;
                }
                
                next = point; ++next.Column; //Right
                if ((next != back) && (buffer.IndexOf(next) >= 0)) return -1;
                if (next == end)
                {
                    buffer.m_Buffer[index + 1] = next;
                    return +1;
                }

                next = point; ++next.Row; //Bottom
                if ((next != back) && (buffer.IndexOf(next) >= 0)) return -1;
                if (next == end)
                {
                    buffer.m_Buffer[index + 1] = next;
                    return +1;
                }

                next = point; --next.Column; //Left
                if ((next != back) && (buffer.IndexOf(next) >= 0)) return -1;
                if (next == end)
                {
                    buffer.m_Buffer[index + 1] = next;
                    return +1;
                }
            }
            else
            {
                next = buffer.m_Buffer[index + 1];

                if (point.Column == next.Column)
                {
                    if ((point.Row - 1) == next.Row)
                        side = PointSide.Top;
                    else if ((point.Row + 1) == next.Row)
                        side = PointSide.Bottom;
                }
                else if (point.Row == next.Row)
                {
                    if ((point.Column - 1) == next.Column)
                        side = PointSide.Left;
                    else if ((point.Column + 1) == next.Column)
                        side = PointSide.Right;
                }

#if (DEBUG)
                if (side == PointSide.None)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            //-----Goto next point ...
            byte maxRow = (byte)(matrix.m_Size.Row - 2);
            byte maxCol = (byte)(matrix.m_Size.Column - 2);
            while (true)
            {
                ++side;
                next = point.GetSidePoint(side);
                if (next == point) return -1; //side != Top|Right|Bottom|Left

                //-----Range test ...
                if ((next.Row <= 0) || (next.Column <= 0) || (next.Row > maxRow) || (next.Column > maxCol)) continue;

                //-----Is fix ...
                if (matrix.m_Buffer[next.Row, next.Column] == CellType.Fix) continue;

                //-----Exist test ...
                if (next == back) continue;

#if (DEBUG)
                if (buffer.IndexOf(next) >= 0)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif

                //----Next point finded ...
                buffer.m_Buffer[index + 1] = next;
                return +1;
            }
        }
        //________________________________________________________________________

        private static bool FindInputAndOutputPoints(Matrix matrix, out Point value1, out Point inside1, out Point value2, out Point inside2)
        {
            int iFindCount = 0;
            value1 = new Point();
            value2 = new Point();
            inside1 = new Point();
            inside2 = new Point();
            Point pt = new Point();
            //-----Top search ...
            pt.Row = 0;
            for (pt.Column = (byte)(matrix.m_Size.Column - 2); pt.Column > 0; --pt.Column)
            {
                if (matrix.m_Buffer[pt.Row, pt.Column] == CellType.None)
                {
                    switch (++iFindCount)
                    {
                        case 1:
                            inside1 = value1 = pt;
                            ++inside1.Row;
                            break;
                        case 2:
                            inside2 = value2 = pt;
                            ++inside2.Row;
                            break;
                        default:
                            return false;
                    }
                }
            }

            //-----Right search ...
            pt.Column = (byte)(matrix.m_Size.Column - (byte)1);
            for (pt.Row = (byte)(matrix.m_Size.Row - 2); pt.Row > 0; --pt.Row)
            {
                if (matrix.m_Buffer[pt.Row, pt.Column] == CellType.None)
                {
                    switch (++iFindCount)
                    {
                        case 1:
                            inside1 = value1 = pt;
                            --inside1.Column;
                            break;
                        case 2:
                            inside2 = value2 = pt;
                            --inside2.Column;
                            break;
                        default:
                            return false;
                    }
                }
            }

            //-----Bottom search ...
            pt.Row = (byte)(matrix.m_Size.Row - (byte)1);
            for (pt.Column = (byte)(matrix.m_Size.Column - 2); pt.Column > 0; --pt.Column)
            {
                if (matrix.m_Buffer[pt.Row, pt.Column] == CellType.None)
                {
                    switch (++iFindCount)
                    {
                        case 1:
                            inside1 = value1 = pt;
                            --inside1.Row;
                            break;
                        case 2:
                            inside2 = value2 = pt;
                            --inside2.Row;
                            break;
                        default:
                            return false;
                    }
                }
            }

            //-----Left search ...
            pt.Column = 0;
            for (pt.Row = (byte)(matrix.m_Size.Row - 2); pt.Row > 0; --pt.Row)
            {
                if (matrix.m_Buffer[pt.Row, pt.Column] == CellType.None)
                {
                    switch (++iFindCount)
                    {
                        case 1:
                            inside1 = value1 = pt;
                            ++inside1.Column;
                            break;
                        case 2:
                            inside2 = value2 = pt;
                            ++inside2.Column;
                            break;
                        default:
                            return false;
                    }
                }
            }

            //-----
            if (iFindCount != 2) return false;

            if (
                ((value1.Row == value2.Row) && (System.Math.Abs((int)value1.Column - (int)value2.Column) <= 1)) ||
                ((value1.Column == value2.Column) && (System.Math.Abs((int)value1.Row - (int)value2.Row) <= 1))
                )
            {
                inside1 = value2;
                inside2 = value1;
            }
            //-----
            return
                (matrix.m_Buffer[inside1.Row, inside1.Column] != CellType.Fix) &&
                (matrix.m_Buffer[inside2.Row, inside2.Column] != CellType.Fix);
        }
        //________________________________________________________________________

        private static void RemoveClosePoints_Pass1(Matrix matrix)
        {
            //
            // ###
            // #
            // ###
            //

            Point pt = new Point();
            Point ptMax = new Point((byte)(matrix.m_Size.Row - 2), (byte)(matrix.m_Size.Column - 2));

            PointSide side = PointSide.None;
            bool bNestedRepeat = false;
            Point ptTest = pt;

            for (pt.Row = 1; pt.Row <= ptMax.Row; ++pt.Row)
            {
                for (pt.Column = 1; pt.Column <= ptMax.Column; ++pt.Column)
                {
                    ptTest = pt;
                    do
                    {
                        bNestedRepeat = false;
                        side = PointSide.None;

                        if (matrix.m_Buffer[ptTest.Row, ptTest.Column] == CellType.Fix)
                        {
                            break;
                        }

                        if (matrix.m_Buffer[ptTest.Row - 1, ptTest.Column] != CellType.Fix) //Top test...
                        {
                            side = PointSide.Top;
                        }

                        if (matrix.m_Buffer[ptTest.Row, ptTest.Column + 1] != CellType.Fix) //Right test...
                        {
                            if (side > PointSide.None) break;
                            side = PointSide.Right;
                        }

                        if (matrix.m_Buffer[ptTest.Row + 1, ptTest.Column] != CellType.Fix) //Bottom test...
                        {
                            if (side > PointSide.None) break;
                            side = PointSide.Bottom;
                        }

                        if (matrix.m_Buffer[ptTest.Row, ptTest.Column - 1] != CellType.Fix) //Left test...
                        {
                            if (side > PointSide.None) break;
                            side = PointSide.Left;
                        }

                        matrix.m_Buffer[ptTest.Row, ptTest.Column] = CellType.Fix;
                        bNestedRepeat = side > PointSide.None;
                        if (bNestedRepeat) ptTest = ptTest.GetSidePoint(side);
                    }
                    while (bNestedRepeat);
                }
            }
        }
        //________________________________________________________________________

     
    }
}
