using System;

namespace Labyrinth.Kernel
{
    //________________________________________________________________________

    public delegate void ResultEventHandler(object sender, ResultEventArgs e);
    //________________________________________________________________________

    public class ResultEventArgs
        : EventArgs
    {
        public readonly System.Collections.Generic.IEnumerable<Point> Path;
        public readonly bool IsFinish;

        public ResultEventArgs(System.Collections.Generic.IEnumerable<Point> Path, bool IsFinish)
        {
            this.Path = Path;
            this.IsFinish = IsFinish;
        }
    }
    //________________________________________________________________________
}
