using System;

namespace Labyrinth.Globalization
{
    internal partial class Language2
    {
        //________________________________________________________________________

        private const string RESOURCEFILENAME = "Labyrinth.resources.dll";
        //________________________________________________________________________

        public static System.Drawing.Font DefaultFont
        {
            get
            {
                try
                {
                    string strfont = Language.Font;
                    if (!string.IsNullOrEmpty(strfont))
                    {
                        var converter = new System.Drawing.FontConverter();
                        var font = converter.ConvertFromString(strfont) as System.Drawing.Font;
                        if (font != null) return font;
                    }

                }
                catch { }
                return System.Drawing.SystemFonts.DialogFont;
            }
        }
        //________________________________________________________________________

        public static System.Collections.Generic.IList<System.Globalization.CultureInfo> GetExistLanguages()
        {
            var ret = new System.Collections.Generic.List<System.Globalization.CultureInfo>();

            try
            {
                foreach (string dir in System.IO.Directory.GetDirectories(System.Windows.Forms.Application.StartupPath, "*-*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        if (!System.IO.File.Exists(System.IO.Path.Combine(dir, RESOURCEFILENAME))) continue;
                        string name = System.IO.Path.GetFileName(dir);

                        var ci = new System.Globalization.CultureInfo(name);
                        if (ci == null) continue;

                        ret.Add(ci);
                    }
                    catch { }
                }
            }
            catch { }

            return ret;
        }
        //________________________________________________________________________
    }
}
