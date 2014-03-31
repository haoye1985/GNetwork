using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphForWinForm
{
    public class GraphView : IDisposable
    {
        private int m_viewX;
        private int m_viewY;
        private float m_viewZoom;
        private float m_currentViewZoom;
        private List<GraphCircle> m_NodeCollection;
        private List<GraphCircle> m_SelectedNodeCollection;

        private GraphPanel m_panelControl;

        public GraphView(GraphPanel pControl)
        {
            this.m_viewX = 0;
            this.m_viewY = 0;
            this.m_viewZoom = 1.0f;
            this.m_currentViewZoom = 1.0f;
            this.m_panelControl = pControl;
            this.m_NodeCollection = new List<GraphCircle>();
            this.m_SelectedNodeCollection = new List<GraphCircle>();
        }

        public int ViewX
        {
            get { return this.m_viewX; }
            set { this.m_viewX = value; }
        }

        public int ViewY
        {
            get { return this.m_viewY; }
            set { this.m_viewY = value; }
        }

        public float CurrentViewZoom
        {
            get { return m_currentViewZoom; }
            set { m_currentViewZoom = value; }
        }

        public List<GraphCircle> NodeCollection
        {
            get { return m_NodeCollection; }
            set { m_NodeCollection = value; }
        }

        public void Dispose()
        {
            if (this.m_panelControl != null)
            {
                this.Dispose();
            }
        }





    }
}
