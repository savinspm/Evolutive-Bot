using System;
using System.Drawing;
//
//
//       H2
// July 18, 2010
//  1389/04/27
//
//Labyrinth and Maze (for training)
//
//Direct Download:
//http://support.h02.ir/fwlink/?LinkId=1007309877
// 
///
///////////////////////////////////////////////////////////////////
/// I USED THE UI OF THIS GAME FOR MY EVOLUTIVE TANK
//  Savíns Puertas Martín
//

namespace Labyrinth
{
    internal static class Program
    {
        //________________________________________________________________________

        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        public static void Main()
        {
            //-------------- Set CurrentUICulture ...
            try
            {
                Properties.Settings.Default.Reload();
                int iLCID = Properties.Settings.Default.LCID;
                if (iLCID > 0)
                {
                    var ci = new System.Globalization.CultureInfo(iLCID);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                }
            }
            catch { }
            //-------------- Startup ...
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            using (var f = new UI.frmMain())
            {
                f.Text = "Evolutive Bot";
             
                f.Size = new Size(750, 750);
                f.MaximizeBox = false;
        
                System.Windows.Forms.Application.Run(f);
            }
        }
        //________________________________________________________________________
    }
}
