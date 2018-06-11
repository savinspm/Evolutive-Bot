using System;

namespace Labyrinth.IO
{
    partial class TxtMatrixSerializer
    {
        //________________________________________________________________________

        private const string FALSECHARS = " 0-~";
        //________________________________________________________________________

        public virtual Kernel.Matrix Deserialize(System.IO.TextReader reader)
        {
            Kernel.Matrix ret = null;
            if (reader == null) return null;
            //-----
            var matrixsize = new Kernel.Point();
            using (var readflatbuffer = new System.IO.MemoryStream())
            {
                while (matrixsize.Row < byte.MaxValue)
                {
                    string line = reader.ReadLine();

                    //End of file?
                    if (line == null) break;


                    byte length;
                    if (line.Length >= byte.MaxValue)
                        length = byte.MaxValue;
                    else
                        length = (byte)line.Length;

                    //End of matrix?
                    if (length < 3)
                    {
                        if (matrixsize.Column <= byte.MinValue) continue;
                        break;
                    }

                    //Remark?
                    string trimline = line.TrimStart();
                    if ((!string.IsNullOrEmpty(trimline)) && (trimline[0] == ';')) continue;

                    //First row?
                    if (matrixsize.Column <= byte.MinValue)
                    {
                        matrixsize.Column = (byte)length;
                    }

                    //Read new line ...
                    ++matrixsize.Row;
                    for (byte i = byte.MinValue; i < matrixsize.Column; ++i)
                    {
                        if (i < length)
                        {
                            readflatbuffer.WriteByte(FALSECHARS.IndexOf(line[i]) >= 0 ? (byte)0 : (byte)1);
                        }
                        else
                        {
                            readflatbuffer.WriteByte((byte)0);
                        }
                    } //end for.
                }//end while.
                //-----
                if ((matrixsize.Column < 3) || (matrixsize.Row < 3)) return null;

                //Convert from flat to matrix ...
                readflatbuffer.Position = 0L;
                ret = new Kernel.Matrix(matrixsize);
                for (byte r = byte.MinValue; r < matrixsize.Row; ++r)
                {
                    for (byte c = byte.MinValue; c < matrixsize.Column; ++c)
                    {
                        ret.m_Buffer[r, c] = readflatbuffer.ReadByte() > 0 ? Kernel.CellType.Fix : Kernel.CellType.None;
                    }
                }
            }//end using.
            return ret;
        }
        //________________________________________________________________________

        public virtual string DeserializeConfiguration(System.IO.TextReader reader)
        {
            string solution = "";
            if (reader == null) return null;
            //-----
           
            using (var readflatbuffer = new System.IO.MemoryStream())
            {
                solution += reader.ReadLine() + "-";
                solution += reader.ReadLine() + "-";
                solution += reader.ReadLine() + "-";
                string line;
                while ((line = reader.ReadLine())!=null)
                {

                    solution += line + ";";
                
                }//end while.
               

            }//end using.
            return solution;
        }
        //________________________________________________________________________
        public Kernel.Matrix Deserialize(System.IO.Stream stream)
        {
            using (var reader = new System.IO.StreamReader(stream))
            {
                return Deserialize(reader);
            }
        }
        //________________________________________________________________________

        public Kernel.Matrix Deserialize(string file)
        {
            if ((string.IsNullOrEmpty(file)) || (!System.IO.File.Exists(file))) return null;
            //-----
            using (var reader = new System.IO.StreamReader(file))
            {
                return Deserialize(reader);
            }
        }
        //________________________________________________________________________
        public string DeserializeConfiguration(string file)
        {
            if ((string.IsNullOrEmpty(file)) || (!System.IO.File.Exists(file))) return null;
            //-----
            using (var reader = new System.IO.StreamReader(file))
            {
                return DeserializeConfiguration(reader);
            }
        }
        //________________________________________________________________________

    }
}
