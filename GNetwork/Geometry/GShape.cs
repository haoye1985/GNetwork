using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphForWinForm
{
    public abstract class GraphShape : IGraphComponent
    {
        private int m_centreX;
        private int m_centreY;

        public GraphShape(int pX, int pY)
        {
            SetX(pX);
            SetX(pY);
        }

        public void SetX(int pX)
        {
            m_centreX = pX;
        }

        public void SetY(int pY)
        {
            m_centreY = pY;
        }

        public virtual void Draw(PaintEventArgs e)
        {

        }




    }
}
