using System;

namespace Labyrinth.IO
{
    public interface IMatrixSerializer
    {
        object Parameters { get; }
        System.IServiceProvider HostService { get; set; }

        string[] Extensions { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        Kernel.Matrix Deserialize(System.IO.Stream file);
        bool Serialize(System.IO.Stream file, Kernel.Matrix matrix);
    }
}
