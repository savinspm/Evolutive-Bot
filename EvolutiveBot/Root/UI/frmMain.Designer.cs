namespace Labyrinth.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.miMFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miMProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.miStart = new System.Windows.Forms.ToolStripMenuItem();
            this.StopLearn = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadCreatureFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miMStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.miStyle0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.creaturesLabel = new System.Windows.Forms.Label();
            this.creaturesTextBox = new System.Windows.Forms.TextBox();
            this.evolutionsLabel = new System.Windows.Forms.Label();
            this.evolutionsTextBox = new System.Windows.Forms.TextBox();
            this.iterationLabel = new System.Windows.Forms.Label();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.randomCheckBox = new System.Windows.Forms.CheckBox();
            this.rowNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.columnNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.f_BoardBox = new Labyrinth.UI.UIBoardBox();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            panel1 = new System.Windows.Forms.Panel();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            panel1.SuspendLayout();
            this.MenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(this.f_BoardBox);
            panel1.Location = new System.Drawing.Point(32, 58);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(611, 534);
            panel1.TabIndex = 1;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(222, 6);
            // 
            // MenuBar
            // 
            this.MenuBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMFile,
            this.miMProcess,
            this.testToolStripMenuItem,
            this.miMStyle});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.MenuBar.Size = new System.Drawing.Size(978, 28);
            this.MenuBar.TabIndex = 0;
            // 
            // miMFile
            // 
            this.miMFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            toolStripMenuItem1,
            this.miExit});
            this.miMFile.Name = "miMFile";
            this.miMFile.Size = new System.Drawing.Size(75, 24);
            this.miMFile.Text = "   &Map   ";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpen.Size = new System.Drawing.Size(186, 26);
            this.miOpen.Text = "&Open ...";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.miExit.Size = new System.Drawing.Size(186, 26);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miMProcess
            // 
            this.miMProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStart,
            this.StopLearn,
            toolStripMenuItem2,
            this.miSave});
            this.miMProcess.Name = "miMProcess";
            this.miMProcess.Size = new System.Drawing.Size(94, 24);
            this.miMProcess.Text = "   &Process   ";
            // 
            // miStart
            // 
            this.miStart.Name = "miStart";
            this.miStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miStart.Size = new System.Drawing.Size(225, 26);
            this.miStart.Text = "Start learning";
            this.miStart.Click += new System.EventHandler(this.miStart_Click);
            // 
            // StopLearn
            // 
            this.StopLearn.Name = "StopLearn";
            this.StopLearn.Size = new System.Drawing.Size(225, 26);
            this.StopLearn.Text = "Stop learning";
            this.StopLearn.Click += new System.EventHandler(this.StopLearn_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(225, 26);
            this.miSave.Text = "Save creature seleted";
            this.miSave.Click += new System.EventHandler(this.miSave_Click_1);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCreatureFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.startToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.startToolStripMenuItem.Text = "Show path travelled";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // loadCreatureFileToolStripMenuItem
            // 
            this.loadCreatureFileToolStripMenuItem.Name = "loadCreatureFileToolStripMenuItem";
            this.loadCreatureFileToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.loadCreatureFileToolStripMenuItem.Text = "Load creature file";
            this.loadCreatureFileToolStripMenuItem.Click += new System.EventHandler(this.loadCreatureFileToolStripMenuItem_Click);
            // 
            // miMStyle
            // 
            this.miMStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStyle0,
            this.miStyle1,
            this.miStyle2});
            this.miMStyle.Name = "miMStyle";
            this.miMStyle.Size = new System.Drawing.Size(77, 24);
            this.miMStyle.Text = "   &Style   ";
            // 
            // miStyle0
            // 
            this.miStyle0.Name = "miStyle0";
            this.miStyle0.Size = new System.Drawing.Size(128, 26);
            this.miStyle0.Tag = "0";
            this.miStyle0.Text = "Style 1";
            this.miStyle0.Click += new System.EventHandler(this.miStyleX_Click);
            // 
            // miStyle1
            // 
            this.miStyle1.Name = "miStyle1";
            this.miStyle1.Size = new System.Drawing.Size(128, 26);
            this.miStyle1.Tag = "1";
            this.miStyle1.Text = "Style 2";
            this.miStyle1.Click += new System.EventHandler(this.miStyleX_Click);
            // 
            // miStyle2
            // 
            this.miStyle2.Checked = true;
            this.miStyle2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miStyle2.Name = "miStyle2";
            this.miStyle2.Size = new System.Drawing.Size(128, 26);
            this.miStyle2.Tag = "2";
            this.miStyle2.Text = "Style 3";
            this.miStyle2.Click += new System.EventHandler(this.miStyleX_Click);
            // 
            // creaturesLabel
            // 
            this.creaturesLabel.AutoSize = true;
            this.creaturesLabel.Location = new System.Drawing.Point(25, 32);
            this.creaturesLabel.Name = "creaturesLabel";
            this.creaturesLabel.Size = new System.Drawing.Size(87, 17);
            this.creaturesLabel.TabIndex = 2;
            this.creaturesLabel.Text = "Nº creatures";
            // 
            // creaturesTextBox
            // 
            this.creaturesTextBox.Location = new System.Drawing.Point(117, 29);
            this.creaturesTextBox.Name = "creaturesTextBox";
            this.creaturesTextBox.Size = new System.Drawing.Size(100, 22);
            this.creaturesTextBox.TabIndex = 3;
            this.creaturesTextBox.Text = "100";
            // 
            // evolutionsLabel
            // 
            this.evolutionsLabel.AutoSize = true;
            this.evolutionsLabel.Location = new System.Drawing.Point(224, 32);
            this.evolutionsLabel.Name = "evolutionsLabel";
            this.evolutionsLabel.Size = new System.Drawing.Size(91, 17);
            this.evolutionsLabel.TabIndex = 4;
            this.evolutionsLabel.Text = "Nº evolutions";
            // 
            // evolutionsTextBox
            // 
            this.evolutionsTextBox.Location = new System.Drawing.Point(322, 29);
            this.evolutionsTextBox.Name = "evolutionsTextBox";
            this.evolutionsTextBox.Size = new System.Drawing.Size(100, 22);
            this.evolutionsTextBox.TabIndex = 5;
            this.evolutionsTextBox.Text = "1000";
            // 
            // iterationLabel
            // 
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Location = new System.Drawing.Point(429, 32);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(85, 17);
            this.iterationLabel.TabIndex = 6;
            this.iterationLabel.Text = "Nº iterations";
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(521, 29);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(100, 22);
            this.iterationsTextBox.TabIndex = 7;
            this.iterationsTextBox.Text = "200";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Location = new System.Drawing.Point(32, 599);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Name = "test1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(921, 252);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(677, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Best creature of each evolution";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(680, 118);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(273, 436);
            this.listBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "0,0";
            // 
            // randomCheckBox
            // 
            this.randomCheckBox.AutoSize = true;
            this.randomCheckBox.Location = new System.Drawing.Point(680, 27);
            this.randomCheckBox.Name = "randomCheckBox";
            this.randomCheckBox.Size = new System.Drawing.Size(136, 21);
            this.randomCheckBox.TabIndex = 12;
            this.randomCheckBox.Text = "Random position";
            this.randomCheckBox.UseVisualStyleBackColor = true;
            // 
            // rowNumericUpDown
            // 
            this.rowNumericUpDown.Location = new System.Drawing.Point(729, 58);
            this.rowNumericUpDown.Name = "rowNumericUpDown";
            this.rowNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.rowNumericUpDown.TabIndex = 13;
            this.rowNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // columnNumericUpDown
            // 
            this.columnNumericUpDown.Location = new System.Drawing.Point(900, 56);
            this.columnNumericUpDown.Name = "columnNumericUpDown";
            this.columnNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.columnNumericUpDown.TabIndex = 14;
            this.columnNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(680, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Row";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(827, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Column";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(680, 565);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(273, 23);
            this.progressBar1.TabIndex = 17;
            // 
            // f_BoardBox
            // 
            this.f_BoardBox.AutoSize = true;
            this.f_BoardBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.f_BoardBox.Location = new System.Drawing.Point(4, 4);
            this.f_BoardBox.Margin = new System.Windows.Forms.Padding(4);
            this.f_BoardBox.MatrixSize = new System.Drawing.Size(26, 26);
            this.f_BoardBox.Name = "f_BoardBox";
            this.f_BoardBox.Size = new System.Drawing.Size(589, 526);
            this.f_BoardBox.StyleImage = global::Labyrinth.Properties.Resources.Style2;
            this.f_BoardBox.TabIndex = 0;
            this.f_BoardBox.tileSolutions = null;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 889);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.columnNumericUpDown);
            this.Controls.Add(this.rowNumericUpDown);
            this.Controls.Add(this.randomCheckBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.iterationsTextBox);
            this.Controls.Add(this.iterationLabel);
            this.Controls.Add(this.evolutionsTextBox);
            this.Controls.Add(this.evolutionsLabel);
            this.Controls.Add(this.creaturesTextBox);
            this.Controls.Add(this.creaturesLabel);
            this.Controls.Add(panel1);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.label2);
            this.MainMenuStrip = this.MenuBar;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(394, 358);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Evolutive Robot";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public UI.UIBoardBox f_BoardBox;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem miMFile;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miMProcess;
        private System.Windows.Forms.ToolStripMenuItem miStart;
        private System.Windows.Forms.ToolStripMenuItem miMStyle;
        private System.Windows.Forms.ToolStripMenuItem miStyle0;
        private System.Windows.Forms.ToolStripMenuItem miStyle1;
        private System.Windows.Forms.ToolStripMenuItem miStyle2;
        private System.Windows.Forms.Label creaturesLabel;
        private System.Windows.Forms.TextBox creaturesTextBox;
        private System.Windows.Forms.Label evolutionsLabel;
        private System.Windows.Forms.TextBox evolutionsTextBox;
        private System.Windows.Forms.Label iterationLabel;
        private System.Windows.Forms.TextBox iterationsTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopLearn;
        private System.Windows.Forms.ToolStripMenuItem loadCreatureFileToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox randomCheckBox;
        private System.Windows.Forms.NumericUpDown rowNumericUpDown;
        private System.Windows.Forms.NumericUpDown columnNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

