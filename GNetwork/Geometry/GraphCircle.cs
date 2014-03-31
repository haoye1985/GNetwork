using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphForWinForm
{
    public class GraphCircle : GraphShape
    {
        private int m_id;
        private string m_name;
        private int m_x;
        private int m_y;
        private Point ctrlPos;
        private int m_radius;

        private SolidBrush m_NodeFill;
        private SolidBrush m_NodeHightlightedFill;
        private Pen m_NodeOutline;
        private Pen m_NodeHightlightedOutline;
        private Font m_NodeFont;
        private SolidBrush m_FontColor;
        private StringFormat m_stringFormat;
        private Rectangle viewRectangle;
        private Region m_hitCircle;

        private bool m_highlighted;

        public GraphCircle(int pX, int pY) : base(pX, pY)
        {
            this.m_x = pX;
            this.m_y = pY;
        }

        public GraphCircle(int pId, string pName, int pX, int pY, int pRadius): base(pX, pY)
        {
            this.m_id = pId;
            this.m_name = pName;
            this.m_x = pX;
            this.m_y = pY;
            this.m_radius = pRadius;
            this.ctrlPos = new Point(m_x, m_y);
            this.viewRectangle = new Rectangle(ctrlPos.X - this.m_radius, ctrlPos.Y - this.m_radius, 2 * m_radius, 2 * m_radius);

            this.m_NodeFill = new SolidBrush(Color.White);
            this.m_NodeHightlightedFill = new SolidBrush(Color.DarkOrange);
            this.m_NodeHightlightedOutline = new Pen(Color.Red, 3);
            this.m_FontColor = new SolidBrush(Color.Black);
            this.m_NodeOutline = new Pen(Color.Blue);
            this.m_NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
            this.m_stringFormat = new StringFormat();

            this.m_highlighted = false;

            GetNodeSetting();
        }

        private void GetNodeSetting()
        {
            SetStringFormat();
            UpdateHitCircleRegion();
        }

        private void UpdateHitCircleRegion()
        {
            this.m_hitCircle = new Region(this.viewRectangle);
        }

        protected virtual void SetStringFormat()
        {
            this.m_stringFormat.Alignment = StringAlignment.Center;
            this.m_stringFormat.LineAlignment = StringAlignment.Center;
        }

        public int GetX(){ return m_x;}

        public int GetY(){return m_y;}

        public void SetRadius(int pRadius){ this.m_radius = pRadius;}

        public override void Draw(PaintEventArgs e)
        {
            // Draw Rectangle
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (!this.m_highlighted)
            {
                e.Graphics.FillEllipse(this.m_NodeFill, viewRectangle);
                e.Graphics.DrawEllipse(this.m_NodeOutline, viewRectangle);
            }
            else
            {
                e.Graphics.FillEllipse(this.m_NodeHightlightedFill, viewRectangle);
                e.Graphics.DrawEllipse(this.m_NodeHightlightedOutline, viewRectangle);
            }

            // Draw Order
            if (!this.m_highlighted)
            {
                e.Graphics.DrawString(m_name, m_NodeFont, this.m_FontColor, viewRectangle, m_stringFormat);
            }
            else
            {
                e.Graphics.DrawString(m_name, m_NodeFont, this.m_FontColor, viewRectangle, m_stringFormat);
            }

        }

        public Region HitCircle
        {
            get { return m_hitCircle; }
        }

        public Rectangle HitRectangle
        {
            get { return this.viewRectangle; }
        }

        public int Radius
        {
            get { return m_radius; }
            set { m_radius = value; }
        }

        public bool IsSelected
        {
            get { return m_highlighted; }
            set { m_highlighted = value; }
        }

        public bool IsHightlighted
        {
            get { return m_highlighted; }
            set { m_highlighted = value; }
        }





    }
}
