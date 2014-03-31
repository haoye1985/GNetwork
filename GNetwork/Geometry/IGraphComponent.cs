using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphForWinForm
{
    public interface IGraphComponent
    {
        void Draw(PaintEventArgs e);


    }
}
