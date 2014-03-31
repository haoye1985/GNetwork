using System.Drawing;
using System.Windows.Forms;

namespace GraphForWinForm
{
    public partial class GraphPanel : UserControl
    {
        #region Private Fields

        // For panel behaviour
        private int m_scrollX;
        private int m_scrollY;
        private GraphView m_view;
        private GraphEditMode m_editMode;

        private SolidBrush m_SelectionFill;
        private Pen m_selectionOutline;

        private Point m_SelectBoxOrigin;
        private Point m_SelectBoxCurrent;

        #endregion

        #region Constructor

        public GraphPanel()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // to prevent the flashing when drawing

            // Default Setting behaviour
            this.m_scrollX = 0;
            this.m_scrollY = 0;
            this.m_view = new GraphView(this);
            this.m_editMode = GraphEditMode.None;    
            this.m_SelectBoxOrigin = new Point();
            this.m_SelectBoxCurrent = new Point();

            this.m_SelectionFill = new SolidBrush(Color.FromArgb(255, 245, 244, 0));
            this.m_selectionOutline = new Pen(Color.FromArgb(255, 255, 0, 0));

        }

        #endregion

        #region Methods

        private HitType HitAll(Point pCursorLocation)
        {
            Rectangle HitTest = new Rectangle(this.ControlToView(pCursorLocation), new Size());

            foreach (var node in this.View.NodeCollection)
            {
                if (node.HitCircle.IsVisible(pCursorLocation))
                {
                    return HitType.Node;
                }
            }

           return HitType.Nothing;
        }

        private void UpdateHightlight()
        {
            Rectangle ViewRectangle = new Rectangle();

            if (this.m_SelectBoxOrigin.X > this.m_SelectBoxCurrent.X)
            {
                ViewRectangle.X = this.m_SelectBoxCurrent.X;
                ViewRectangle.Width = this.m_SelectBoxOrigin.X - this.m_SelectBoxCurrent.X;
            }
            else
            {
                ViewRectangle.X = this.m_SelectBoxOrigin.X;
                ViewRectangle.Width = this.m_SelectBoxCurrent.X - this.m_SelectBoxOrigin.X;
            }

            if (this.m_SelectBoxOrigin.Y > this.m_SelectBoxCurrent.Y)
            {
                ViewRectangle.Y = this.m_SelectBoxCurrent.Y;
                ViewRectangle.Height = this.m_SelectBoxOrigin.Y - this.m_SelectBoxCurrent.Y;
            }
            else
            {
                ViewRectangle.Y = this.m_SelectBoxOrigin.Y;
                ViewRectangle.Height = this.m_SelectBoxCurrent.Y - this.m_SelectBoxOrigin.Y;
            }

            foreach (GraphCircle i_Node in this.View.NodeCollection)
            {
                if (i_Node.HitRectangle.IntersectsWith(ViewRectangle))
                {
                    i_Node.IsHightlighted = true;
                }
                else i_Node.IsHightlighted= false;
            }
        }


        public void AddNode(GraphCircle pNode)
        {
            this.View.NodeCollection.Add(pNode);
            this.Refresh();
        }

        

        private void DrawSelectionBox(PaintEventArgs e)
        {
            if (this.m_editMode == GraphEditMode.SelectingBox)
            {
                var viewRectangle = new Rectangle();

                if (this.m_SelectBoxOrigin.X > this.m_SelectBoxCurrent.X)
                {
                    viewRectangle.X = this.m_SelectBoxCurrent.X;
                    viewRectangle.Width = this.m_SelectBoxOrigin.X - this.m_SelectBoxCurrent.X;
                }
                else
                {
                    viewRectangle.X = this.m_SelectBoxOrigin.X;
                    viewRectangle.Width = this.m_SelectBoxCurrent.X - this.m_SelectBoxOrigin.X;
                }

                if (this.m_SelectBoxOrigin.Y > this.m_SelectBoxCurrent.Y)
                {
                    viewRectangle.Y = this.m_SelectBoxCurrent.Y;
                    viewRectangle.Height = this.m_SelectBoxOrigin.Y - this.m_SelectBoxCurrent.Y;
                }
                else
                {
                    viewRectangle.Y = this.m_SelectBoxOrigin.Y;
                    viewRectangle.Height = this.m_SelectBoxCurrent.Y - this.m_SelectBoxOrigin.Y;
                }

                e.Graphics.FillRectangle(this.m_SelectionFill, this.ViewToControl(viewRectangle));
                e.Graphics.DrawRectangle(this.m_selectionOutline, this.ViewToControl(viewRectangle));
            }
        }

        public Point ViewToControl(Point pPoint)
        {
            return new Point((int)((pPoint.X + this.View.ViewX) * this.View.CurrentViewZoom) + (this.Width / 2),
                              (int)((pPoint.Y + this.View.ViewY) * this.View.CurrentViewZoom) + (this.Height / 2));
        }  

        public Rectangle ViewToControl(Rectangle pRectangle)
        {
            return new Rectangle(this.ViewToControl(new Point(pRectangle.X, pRectangle.Y)), new Size((int)(pRectangle.Width * this.View.CurrentViewZoom), (int)(pRectangle.Height * this.View.CurrentViewZoom)));

        }

        public Rectangle ControlToView(Rectangle pRectangle)
        {
            return new Rectangle(this.ControlToView(new Point(pRectangle.X, pRectangle.Y)), new Size((int)(pRectangle.Width/this.View.CurrentViewZoom), (int)(pRectangle.Height/this.View.CurrentViewZoom)));
        }

        public Point ControlToView(Point pPoint)
        {
            int pointX = (int) ((pPoint.X - (this.Width/2))/this.View.CurrentViewZoom) - (this.View.ViewX);
            int pointY = (int) ((pPoint.Y - (this.Height/2))/this.View.CurrentViewZoom) - (this.View.ViewY);
            return new Point(pointX, pointY);
        }

        #endregion

        #region Properties

        public GraphView View
        {
            get { return m_view; }
            set { m_view = value; }
        }

        #endregion

        #region Events

        private void GraphPanel_MouseDown(object sender, MouseEventArgs e)
        {
            switch (this.m_editMode)
            {
                case GraphEditMode.None:
                    switch (e.Button)
                    {
                        case MouseButtons.Middle:
                            this.m_editMode = GraphEditMode.Scrolling;// default scrolling using middle mouse
                            this.m_scrollX = e.Location.X;
                            this.m_scrollY = e.Location.Y;

                            break;
                        case MouseButtons.Left:

                            if (HitAll(e.Location) == HitType.Nothing)
                            {
                                this.m_editMode = GraphEditMode.SelectingBox;
                                this.m_SelectBoxOrigin = this.ControlToView(new Point(e.X, e.Y));
                                this.m_SelectBoxCurrent = this.ControlToView(new Point(e.X, e.Y));
                                this.UpdateHightlight();
                            }

                            break;

                        default:
                            break;
                    }

                    break;

                case GraphEditMode.SelectingBox:

                    break;

                case GraphEditMode.MovingSelection:
                    break;

                default:
                    break;
            }
        }

        private void GraphPanel_MouseMove(object sender, MouseEventArgs e)
        {
            switch (this.m_editMode)
            {
                case GraphEditMode.SelectingBox:
                    this.m_SelectBoxCurrent = this.ControlToView(new Point(e.X, e.Y));
                    this.UpdateHightlight();
                    this.Invalidate();
                    break;

                default:
                    break;
            }
        }

        private void GraphPanel_MouseUp(object sender, MouseEventArgs e)
        {
            switch (this.m_editMode)
            {
                case GraphEditMode.Selecting:
                    break;

                case GraphEditMode.SelectingBox:
                    if (e.Button == MouseButtons.Left)
                    {
                        this.m_editMode = GraphEditMode.None;
                        this.Invalidate();
                    }
                    break;

                default:
                    break;
            }
        }

        private void GraphPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawSelectionBox(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (GraphCircle node in this.View.NodeCollection)
            {
                node.Draw(e);
            }



        }

        #endregion

        public enum HitType
        {
            Nothing,
            Node,
            Connector
        }

    }
}
