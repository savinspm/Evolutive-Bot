using Labyrinth.Kernel;
using System;
using System.Collections.Generic;

namespace Labyrinth.UI
{
    public class UIBoardBox
        : System.Windows.Forms.Control
    {
        //________________________________________________________________________

        private volatile Kernel.Matrix m_Matrix = new Kernel.Matrix(new Kernel.Point(26, 24));
        private System.Drawing.Size m_CellSize = new System.Drawing.Size(16, 16);

        private System.Drawing.Image m_StyleImage;
        private readonly System.Drawing.Rectangle[] m_SIRectCache = new System.Drawing.Rectangle[3];
        //________________________________________________________________________
        public List<Point> tileSolutions { get; set; }
        public UIBoardBox()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor |
                System.Windows.Forms.ControlStyles.UserPaint,
                true);

            this.m_Matrix.CreateBorder();
        }
        //________________________________________________________________________

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Size), "16,16")]
        public System.Drawing.Size CellSize
        {
            get { return this.m_CellSize; }
            set
            {
                if ((value.Width <= 0) || (value.Height <= 0) || (value.Width > 100) || (value.Height > 100))
                    throw new ArgumentOutOfRangeException();

                if (this.m_CellSize == value) return;
                this.m_CellSize = value;
                this.OnAutoSize();
                this.Invalidate();
            }
        }
        //________________________________________________________________________

        [System.ComponentModel.DefaultValue((string)null)]
        public System.Drawing.Image StyleImage
        {
            get { return this.m_StyleImage; }
            set
            {
                if (this.m_StyleImage == value) return;

                if (value == null)
                {
                    this.m_StyleImage = null;
                    this.Invalidate();
                    return;
                }

                int w = value.Width / 3;
                int h = value.Height;

                if ((w <= 0) || (h <= 0))
                    throw new ArgumentException();

                this.m_StyleImage = value;
                this.m_SIRectCache[(int)Kernel.CellType.None] = new System.Drawing.Rectangle(0, 0, w, h);
                this.m_SIRectCache[(int)Kernel.CellType.Fix] = new System.Drawing.Rectangle(w, 0, w, h);
                this.m_SIRectCache[(int)Kernel.CellType.Way] = new System.Drawing.Rectangle(w + w, 0, w, h);
                this.Invalidate();
            }
        }
        //________________________________________________________________________

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Size), "14,14")]
        public System.Drawing.Size MatrixSize
        {
            get
            {
                if (this.m_Matrix == null) return new System.Drawing.Size();
                return new System.Drawing.Size(this.m_Matrix.m_Size.Column, this.m_Matrix.m_Size.Row);
            }
            set
            {
                if ((value.Width < 3) || (value.Height < 3) || (value.Width > byte.MaxValue) || (value.Height > byte.MaxValue))
                {
                    this.m_Matrix = null;
                }
                else if ((this.m_Matrix == null) || (value.Width != this.m_Matrix.m_Size.Column) || (value.Height != this.m_Matrix.m_Size.Row))
                {
                    this.m_Matrix = new Kernel.Matrix(new Kernel.Point((byte)value.Height, (byte)value.Width));
                 //   this.m_Matrix.CreateBorder();
                }

                this.OnAutoSize();
                this.Invalidate();
            }
        }
        //________________________________________________________________________

        [System.ComponentModel.Browsable(false)]
        public Kernel.Matrix Matrix
        {
            get { return this.m_Matrix; }
            set
            {
                if (this.m_Matrix == value) return;
                this.m_Matrix = value;

                this.OnAutoSize();
                this.Invalidate();
            }
        }
        //________________________________________________________________________

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                this.OnAutoSize();
            }
        }
        //________________________________________________________________________

        private void OnAutoSize()
        {
            if (!this.AutoSize) return;

            var mxsize = this.MatrixSize;
            this.SetBounds(
                0,
                0,
                mxsize.Width * this.m_CellSize.Width,
                mxsize.Height * this.m_CellSize.Height,
                System.Windows.Forms.BoundsSpecified.Size);
        }
        //________________________________________________________________________

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            int iCell_H = this.m_CellSize.Height;
            int iCell_W = this.m_CellSize.Width;
            var graph = e.Graphics;
            var rectCell = new System.Drawing.Rectangle(0, 0, iCell_W, iCell_H);

            if ((this.m_StyleImage == null) || (this.m_Matrix.m_Size.Row <= 0) || (this.m_Matrix.m_Size.Column <= 0) || (iCell_H <= 0) || (iCell_W <= 0))
            {
                base.OnPaint(e);
                return;
            }

            for (int row = 0; row < this.m_Matrix.m_Size.Row; ++row)
            {
                rectCell.Y = row * iCell_H;
                for (int col = 0; col < this.m_Matrix.m_Size.Column; ++col)
                {
                    rectCell.X = col * iCell_W;
                    graph.DrawImage(this.m_StyleImage, rectCell, this.m_SIRectCache[(int)this.m_Matrix.m_Buffer[row, col]], System.Drawing.GraphicsUnit.Pixel);
                }
            }
            base.OnPaint(e);
        }
        //________________________________________________________________________

       /* protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            int iCell_H = this.m_CellSize.Height;
            int iCell_W = this.m_CellSize.Width;

            if ((this.m_Matrix.m_Size.Row > 0) && (this.m_Matrix.m_Size.Column > 0) && (iCell_H > 0) && (iCell_W > 0))
            {
                int row = e.Y / iCell_H;
                int col = e.X / iCell_W;

                if ((row >= 0) && (col >= 0) && (row < this.m_Matrix.m_Size.Row) && (col < this.m_Matrix.m_Size.Column))
                {
                    if (this.m_Matrix.m_Buffer[row, col] == Kernel.CellType.Fix)
                        this.m_Matrix.m_Buffer[row, col] = Kernel.CellType.None;
                    else
                        this.m_Matrix.m_Buffer[row, col] = Kernel.CellType.Fix;


                    base.OnMouseClick(e);
                    this.m_Matrix.ClearWay();
                    this.Invalidate();
                    return;
                }
            }

            base.OnMouseClick(e);
        }*/
        //________________________________________________________________________
    }
}
