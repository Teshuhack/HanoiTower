using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace HanoiTower
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rnd;
        Tower[] towers;
        Disk[] disks;
        List<Panel> drawAreas;
        Thread workThread;
        bool flag;
        private void btnDraw_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = true;
            panel1.Refresh();
            panel3.Refresh();
            int diskNum = (int)nudChooseDisksNumber.Value;
            int towerNum = 3;


            rnd = new Random((int)DateTime.Now.Ticks);
            towers = new Tower[towerNum];
            disks = new Disk[diskNum];  // массив дисков

            drawAreas = new List<Panel>();
            drawAreas.Add(panel1);
            drawAreas.Add(panel2);
            drawAreas.Add(panel3);

            for (int i = 0; i < towerNum; i++)
            {
                towers[i] = new Tower();
                towers[i].DrawTowers(drawAreas[i]);
            }

            // параметры дисков
            int diskX = 1;
            int diskW = panel1.Width - 5;
            int diskH = 20;
            int diskY = panel1.Height - diskH;

            // инициализация дисков
            for (int i = 0; i < diskNum; i++)
            {
                disks[i] = new Disk(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)), diskX, diskY, diskW, diskH);
                towers[0].AddDisk(disks[i]);//добавление дисков на первую башню

                diskX += 10;
                diskY -= diskH;
                diskW -= 20;
            }

            towers[0].DrawDisks(towers[0].Disks, panel1);//отрисовка дисков на первой башне

            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {




        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            btnDraw.Enabled = false;
            nudChooseDisksNumber.Enabled = false;
            btnRun.Enabled = false;
            btnStop.Enabled = true;
            CheckForIllegalCrossThreadCalls = false; //Отменяем проверку, из какого потока используется контрол
            workThread = new Thread(func);
            workThread.Start();
            flag = true;
        }

        private void Hanoi(int n, int from, int to)
        {
            if (n == 0)
                return;
            Hanoi(n - 1, from, 3 - from - to);
            Movement(towers[from], towers[to]);
            Hanoi(n - 1, 3 - from - to, to);

        }

        private void Movement(Tower from, Tower to)
        {
            to.AddDisk(from.Last());
            from.RemoveDisk();


            
            for (int i = 0; i < towers.Length; i++)
            {
                //Самое нижнее положение диска в panel
                int StartX = 382;

                List<Disk> disk = towers[i].Disks;
                foreach (var itemdisk in disk)
                {
                    itemdisk.Y = StartX;
                    StartX -= 20;
                }


                drawAreas[i].Refresh();
                towers[i].DrawTowers(drawAreas[i]);
                towers[i].DrawDisks(towers[i].Disks, drawAreas[i]);
                    
            }
            Thread.Sleep(100);

        }

        private void func()//для отдельного потока
        {
            Hanoi((int)nudChooseDisksNumber.Value, 0, 2);
            btnDraw.Enabled = true;
            nudChooseDisksNumber.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag)
                workThread.Abort();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            workThread.Abort();
            for (int i = 0; i < drawAreas.Count; i++)
            {
                drawAreas[i].Refresh();
                towers[i].DrawTowers(drawAreas[i]);
                btnDraw.Enabled = true;
                nudChooseDisksNumber.Enabled = true;
            }
            btnStop.Enabled = false;
        }

    }
}
