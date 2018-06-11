using System;

namespace Labyrinth.Kernel
{
    partial class Matrix
    {
        //________________________________________________________________________

        private static IO.TxtMatrixSerializer m_StringHelper;
        //________________________________________________________________________

        private static IO.TxtMatrixSerializer StringHelper
        {
            get
            {
                if (Matrix.m_StringHelper == null)
                    Matrix.m_StringHelper = new IO.TxtMatrixSerializer();

                return Matrix.m_StringHelper;
            }
        }
        //________________________________________________________________________

        public static bool TryParse(string s, out Matrix result)
        {
            result = null;
            try
            {
                result = Matrix.Parse(s);
            }
            catch { }
            return (result != null);
        }
        //________________________________________________________________________

        public static Matrix Parse(string s)
        {
            if (((object)s == null) || (s.Length <= 10)) return null;

            using (var reader = new System.IO.StringReader(s))
            {
                return Matrix.StringHelper.Deserialize(reader);
            }
        }
        //________________________________________________________________________

        public override string ToString()
        {
            if ((this.m_Size.Row <= 0) || (this.m_Size.Column <= 0)) return string.Empty;
            var ret = new System.Text.StringBuilder(this.m_Size.Row * (this.m_Size.Column + 2) + 5);

            using(var writer = new System.IO.StringWriter(ret))
            {
                Matrix.StringHelper.WriteMatrix(writer, this, 0x7);
            }

            return ret.ToString();
        }
        //________________________________________________________________________
    }
}
