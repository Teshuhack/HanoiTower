using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace HanoiTower
{
    public class Disk
    {
        int x, y, width, height;
        Color fillColor;
        public Disk(Color fillColor, int x, int y, int width, int height)
        {
            this.fillColor = fillColor;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

    }
}
