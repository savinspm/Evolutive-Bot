using EvolutiveTank.Creature;
using EvolutiveTank.Management;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Labyrinth.UI
{
    public partial class frmMain
        : System.Windows.Forms.Form
    {

        //________________________________________________________________________

        private readonly Kernel.Engine m_Engine = new Kernel.Engine();
        private const string FILEDIALOGFILTER = "Map files(*.*;*.txt)|*.*;*.txt";
        private const string FILEDIALOGDIR = "Files";
        public ETGod god
        {
            get; set;
        }
        public bool mapLoaded { get; set; }
        //________________________________________________________________________

        #region " Initialize "

        public frmMain()
        {
            this.InitializeComponent();

            this.SetWorkState(false);
            this.m_Engine.Result += this.OnEngineResult;
            this.m_Engine.Synchronize = this;

            this.SetLanguage();


        }
        //________________________________________________________________________

        private void SetLanguage()
        {
            const string MAINMENUEXPAND = "  ";
            const string CHILDMENUEXPAND = "              ";

            this.RightToLeft = System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
            var font = Globalization.Language2.DefaultFont;
            this.Font = font;
            this.MenuBar.Font = font;

            this.Text = "    " + System.Windows.Forms.Application.ProductName;
            this.miMFile.Text = MAINMENUEXPAND + Globalization.Language.str_MainDialog_File + MAINMENUEXPAND;
            this.miOpen.Text = Globalization.Language.str_MainDialog_Open + CHILDMENUEXPAND;
            this.miSave.Text = Globalization.Language.str_MainDialog_Save;

            this.miExit.Text = Globalization.Language.str_MainDialog_Exit;

            this.miMProcess.Text = MAINMENUEXPAND + Globalization.Language.str_MainDialog_Process + MAINMENUEXPAND;
            this.miStart.Text = Globalization.Language.str_MainDialog_Start + CHILDMENUEXPAND;


            this.miMStyle.Text = MAINMENUEXPAND + Globalization.Language.str_MainDialog_Style + MAINMENUEXPAND;

            int i = this.miMStyle.DropDownItems.Count;
            while (--i >= 0)
            {
                ((System.Windows.Forms.ToolStripItem)this.miMStyle.DropDownItems[i]).Text = Globalization.Language.str_MainDialog_Style + " " + i.ToString() + CHILDMENUEXPAND;
            }
        }
        //________________________________________________________________________


        #endregion

        #region " Menus "

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //________________________________________________________________________

        private void miOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                string defaultdir = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, FILEDIALOGDIR);
                if (System.IO.Directory.Exists(defaultdir))
                    dialog.InitialDirectory = defaultdir;

                dialog.Filter = FILEDIALOGFILTER;
                if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

                var serializer = new IO.TxtMatrixSerializer();
                var matrix = serializer.Deserialize(dialog.FileName);
                //Calculate Matrix Solution
                this.f_BoardBox.tileSolutions = new System.Collections.Generic.List<Kernel.Point>();
                for (int i = 1; i < matrix.Size.Row - 1; i++)
                {
                    for (int j = 1; j < matrix.Size.Column - 1; j++)
                    {
                        if ((matrix.m_Buffer[i - 1, j - 1] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i - 1, j] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i - 1, j + 1] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i, j - 1] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i, j + 1] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i + 1, j - 1] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i + 1, j] == Kernel.CellType.Fix ||
                            matrix.m_Buffer[i + 1, j + 1] == Kernel.CellType.Fix) && (matrix.m_Buffer[i, j] == Kernel.CellType.None))
                        {
                            this.f_BoardBox.tileSolutions.Add(new Kernel.Point(Convert.ToByte(i), Convert.ToByte(j)));
                        }
                    }
                }
                // END

                if (matrix == null) return;

                this.f_BoardBox.Matrix = matrix;
            }
        }
        //________________________________________________________________________

        Thread thread { get; set; }
        List<ETTank> solution { get; set; }
        private void miStart_Click(object sender, EventArgs e)
        {
            if (this.f_BoardBox.Matrix.m_Buffer[0,0] == Kernel.CellType.None)
            {
                MessageBox.Show("Load map first.");
                return;
            }
            if ((Convert.ToInt32(this.creaturesTextBox.Text)/2)%2!=0)
            {
                MessageBox.Show("Nº of Creatures/2 must be an even number.");
                return;
            }
            if (!Kernel.Matrix.IsValid(this.f_BoardBox.Matrix)) return;
            this.f_BoardBox.Matrix.ClearWay();
            this.f_BoardBox.Invalidate();

            this.m_Engine.Matrix = this.f_BoardBox.Matrix;

            this.SetWorkState(true);
            this.god = new ETGod(this.f_BoardBox.Matrix);

            int creatures = Convert.ToInt32(this.creaturesTextBox.Text);
            int evolutions = Convert.ToInt32(this.evolutionsTextBox.Text);
            int iterations = Convert.ToInt32(this.iterationsTextBox.Text);
            chart1.Series["test1"].Points.Clear();
            listBox1.Items.Clear();
            frmMain obj = this;

            thread = new Thread(() =>
            {
                god.evolution(creatures, iterations, evolutions, obj, randomCheckBox.Checked, new Kernel.Point(Convert.ToByte(rowNumericUpDown.Value), Convert.ToByte(columnNumericUpDown.Value)));

            });
            thread.Start();


        }
        //________________________________________________________________________


        //________________________________________________________________________

        private void miStyleX_Click(object sender, EventArgs e)
        {
            this.SetStyle(System.Convert.ToInt32(((System.Windows.Forms.ToolStripItem)sender).Tag));
        }
        //________________________________________________________________________





        #endregion

        private void SetWorkState(bool busy)
        {
            /* this.f_BoardBox.Enabled = !busy;
             this.miOpen.Enabled = !busy;
             this.miSave.Enabled = !busy;
             this.miStart.Enabled = !busy;*/




            System.Windows.Forms.Application.DoEvents();
        }
        //________________________________________________________________________

        public void OnEngineResult(object sender, Kernel.ResultEventArgs e)
        {
            if (e == null) return;

            this.f_BoardBox.Matrix.ClearWay();
            if (e.Path != null) this.f_BoardBox.Matrix.SetAll(e.Path, Kernel.CellType.Way);



            this.f_BoardBox.Invalidate();
            System.Windows.Forms.Application.DoEvents();
        }
        //________________________________________________________________________

        public void SetStyle(int index)
        {
            int i = this.miMStyle.DropDownItems.Count;
            while (--i >= 0)
            {
                ((System.Windows.Forms.ToolStripMenuItem)this.miMStyle.DropDownItems[i]).Checked = i == index;
            }

            this.f_BoardBox.StyleImage = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("Style" + index.ToString());
        }
        //________________________________________________________________________

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null) this.components.Dispose();
                if (this.m_Engine != null) this.m_Engine.Dispose();
            }
            base.Dispose(disposing);
        }

        private void miSave_Click_1(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("First select a creature of right panel.");
                return;
            }
            if (this.f_BoardBox.Matrix == null) return;
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                string defaultdir = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, FILEDIALOGDIR);
                if (System.IO.Directory.Exists(defaultdir))
                    dialog.InitialDirectory = defaultdir;

                dialog.Filter = FILEDIALOGFILTER;
                if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

                String value = listBox1.SelectedItem.ToString();
                var serializer = new IO.TxtMatrixSerializer();
                serializer.SerializeEvolution(dialog.FileName, value);
            }
        }
        //________________________________________________________________________
        delegate void SetTextCallback(int solution);
        public void updateChart(int bestSolution)
        {
            Random rdn = new Random();

            if (this.InvokeRequired)
            {// this prevents the invoke loop

                SetTextCallback d = updateChart;
                this.BeginInvoke(d, new object[] { bestSolution });

            }
            else
            {

                chart1.Series["test1"].Points.AddXY
                           (rdn.Next(0, 1), bestSolution);

                chart1.Series["test1"].ChartType =
                                    SeriesChartType.FastLine;
                chart1.Series["test1"].Color = Color.Red;


                chart1.Update();
            }
        }
        delegate void clearListCallback();
        public void clearListBox()
        {
            if (this.InvokeRequired)
            {// this prevents the invoke loop

                clearListCallback d = clearListBox;
                this.BeginInvoke(d);

            }
            else
            {
                listBox1.Items.Clear();
            }
        }
        delegate void addEvoCallback(string evo);
        public void addEvolution(string evolution)
        {
            if (this.InvokeRequired)
            {// this prevents the invoke loop

                addEvoCallback d = addEvolution;
                this.BeginInvoke(d, new object[] { evolution });

            }
            else
            {
                listBox1.Items.Add(evolution);
            }
        }
        ETTank creature
        {
            get; set;
        }



        private void loadLearningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                string defaultdir = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, FILEDIALOGDIR);
                if (System.IO.Directory.Exists(defaultdir))
                    dialog.InitialDirectory = defaultdir;

                dialog.Filter = FILEDIALOGFILTER;
                if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

                var serializer = new IO.TxtMatrixSerializer();
                string solution = serializer.DeserializeConfiguration(dialog.FileName);
                this.listBox1.Items.Clear();
                this.listBox1.Items.Add(solution);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Select a criature of right panel.");
                return;
            }
          
            if (!selectCriatureSelected())
            {
                MessageBox.Show("Change the coordinates.");
                return;
            }
            paintPath();

        }
     
        private bool selectCriatureSelected()
        {
           
            String value = listBox1.SelectedItem.ToString();
            if (randomCheckBox.Checked)
            {
               
                creature = new ETTank(value, this.f_BoardBox.Matrix);
            }
            else
            {
                byte row = Convert.ToByte(rowNumericUpDown.Value);
                byte column = Convert.ToByte(columnNumericUpDown.Value);
                if(row>this.f_BoardBox.Matrix.m_Size.Row || column > this.f_BoardBox.Matrix.m_Size.Column||ETTank.posibleMove(new Kernel.Point(row,column),this.f_BoardBox.Matrix) == false)
                {
                   
                    return false;
                }
                Labyrinth.Kernel.Point p = new Labyrinth.Kernel.Point(row, column);
                creature = new ETTank(p, value);
            }


            return true;

        }


        public void paintPath()
        {

            this.f_BoardBox.Matrix.ClearWay();
            int iterations = Convert.ToInt32(this.iterationsTextBox.Text);
            Kernel.Point p;
            //PROGRESS BAR
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = iterations;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;

            for (int i = 0; i < iterations; i++)
            {
                string radar = creature.scanner(this.f_BoardBox.Matrix.m_Buffer);
                int direction = creature.Brain.Situations[radar];
                p = creature.move(direction, this.f_BoardBox.Matrix);
                this.f_BoardBox.Matrix.m_Buffer[p.Row, p.Column] = Kernel.CellType.Way;

                this.f_BoardBox.Invalidate();
                this.f_BoardBox.Update();
                this.f_BoardBox.Refresh();
                progressBar1.PerformStep();
                Thread.Sleep(30);
               
            }
           


        }

        private void loadCreatureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                string defaultdir = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, FILEDIALOGDIR);
                if (System.IO.Directory.Exists(defaultdir))
                    dialog.InitialDirectory = defaultdir;

                dialog.Filter = FILEDIALOGFILTER;
                if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

                var serializer = new IO.TxtMatrixSerializer();
                string solution = serializer.DeserializeConfiguration(dialog.FileName);
                this.listBox1.Items.Clear();
                this.listBox1.Items.Add(solution);
            }
        }

        private void StopLearn_Click(object sender, EventArgs e)
        {


            if (thread == null) { 
                MessageBox.Show("Start learning first.");
            return; }
            thread.Abort();
           
        }

  
        public void addBestCreatureEvolution(ETTank e, int evolution)
        {
            string bestEvolution;

            bestEvolution = "Evolucion " + (evolution+1) + "-";
            bestEvolution += e.PathTravelled.Count + "-";
            bestEvolution += e.InitialPositionMap.Row+","+e.InitialPositionMap.Column + "-";
            for (int j = 0; j < 256; j++)
            {
                bestEvolution += e.Brain.Situations.ElementAt(j) + ";";
            }
            addEvolution(bestEvolution);

        }
    }
}
