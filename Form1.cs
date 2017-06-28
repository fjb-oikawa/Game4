using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game4
{
    public partial class Form1 : Form
    {
        List<Chara> charaList;
        TimeCounter timeCount;
        Chara ship;
        Chara missile;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timeCount = new TimeCounter();
            charaList = new List<Chara>();
            ship = new Chara(Properties.Resources.ship);
            charaList.Add(ship);
            missile = new Chara(Properties.Resources.Missile);
            charaList.Add(missile);

            //初期位置の設定
            ship.X = 100;
            ship.Y = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float speed = 0;
            long count = timeCount.getCount();
            for (long i = 0; i < count; i++)
            {
                if (Base.GetAsyncKeyState(Keys.Right) < 0)
                    ship.Rot += 1;
                if (Base.GetAsyncKeyState(Keys.Left) < 0)
                    ship.Rot -= 1;
                if (Base.GetAsyncKeyState(Keys.Up) < 0)
                    speed = 1.0f;
                if (Base.GetAsyncKeyState(Keys.Down) < 0)
                    speed = -1.0f;

                if(Base.GetAsyncKeyState(Keys.Space) < 0)
                {
                    missile.x = ship.x;
                    missile.y = ship.y;
                    missile.Rot = ship.Rot;
                }

                ship.x += (float)Math.Sin(ship.Rot * 3.14 / 180) * speed;
                ship.y -= (float)Math.Cos(ship.Rot * 3.14 / 180) * speed;

            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Chara chara in charaList)
            {
                chara.draw(g);
            }
        }
    }
}
