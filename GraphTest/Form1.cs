using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphForWinForm;

namespace GraphTest
{
    public partial class Form1 : Form
    {
        Point m_MouseLoc;

        public Form1()
        {
            InitializeComponent();

            m_MouseLoc = Point.Empty;
            //node1 = new GraphNode(1, "1", 100, 100, 15);
            //node2 = new GraphNode(2, "2", 100, 50, 15);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var node in this.graphPanel1.View.NodeCollection)
            {
                node.Draw(e);
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.graphPanel1.AddNode(new GraphCircle(1, "Hello", m_MouseLoc.X, m_MouseLoc.Y, 30));
        }

        private void graphPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            m_MouseLoc = e.Location;
        }

        private void graphPanel1_MouseDown(object sender, MouseEventArgs e)
        {

        }


    }
}
