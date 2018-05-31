using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace HanoiTower
{
    public class Tower
    {
        int height = 350, width = 6;
        int count;
        List<Disk> disks;

        public Tower()
        {
            disks = new List<Disk>();
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public List<Disk> Disks
        {
            get { return disks; }
        }

        public void DrawTowers(Panel area)
        {
            Graphics gr = area.CreateGraphics();
            gr.FillRectangle(Brushes.Pink, (area.Width / 2) - (width / 2), (area.Height - 4) - height, width, height);
            gr.DrawRectangle(Pens.Black, (area.Width / 2) - (width / 2), (area.Height - 4) - height, width, height);
        }

        public void DrawDisks(List<Disk> disksList, Panel area)
        {
            if (disks.Count != 0)
            {
                Graphics gr = area.CreateGraphics();
                Random rnd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < disksList.Count; i++)
                {
                    gr.FillRectangle(new SolidBrush(disksList[i].FillColor), disksList[i].X, disksList[i].Y, disksList[i].Width, disksList[i].Height);
                    gr.DrawRectangle(Pens.Black, disksList[i].X, disksList[i].Y, disksList[i].Width, disksList[i].Height);
                }
            }
        }

        public void AddDisk(Disk disk)
        {
            disks.Add(disk);
            count++;
        }

        public void RemoveDisk()
        {
            disks.RemoveAt(disks.Count - 1);
            count--;
        }

        public Disk Last()
        {
            
           return disks.Last();
        }
    }
}
