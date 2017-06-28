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
        Chara[] missile;
        int missileCount;
        Chara enemy;

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

            //ミサイルを複数作成する
            missile = new Chara[3];
            for(int i = 0; i < missile.Length; i++) {
                missile[i] = new Chara(Properties.Resources.Missile);
                charaList.Add(missile[i]);
                missile[i].Visible = false;
            }
            //敵を作成する
            enemy = new Chara(Properties.Resources.Enemy);
            enemy.x = ClientSize.Width / 2;
            enemy.y = 0;
            enemy.Rot = 180;
            charaList.Add(enemy);

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

                if (missileCount > 0)
                    --missileCount;
                if (Base.GetAsyncKeyState(Keys.Space) < 0 && missileCount == 0)
                {
                    //ミサイル発射
                    for(int j = 0; j < missile.Length; j++)
                    {
                        //使われていないミサイルを探す
                        if(missile[j].Visible == false)
                        {
                            missileCount = 50;
                            missile[j].x = ship.x;
                            missile[j].y = ship.y;
                            missile[j].Rot = ship.Rot;
                            missile[j].Visible = true;
                            break;
                        }
                    }


                }
                //戦闘機の移動処理
                ship.x += (float)Math.Sin(ship.Rot * 3.14 / 180) * speed;
                ship.y -= (float)Math.Cos(ship.Rot * 3.14 / 180) * speed;
                //敵の移動処理
                enemy.x += (float)Math.Sin(enemy.Rot * 3.14 / 180);
                enemy.y -= (float)Math.Cos(enemy.Rot * 3.14 / 180);
                //ミサイルの移動処理
                for (int j = 0; j < missile.Length; j++)
                {
                    //表示されているミサイルを探す
                    if (missile[j].Visible)
                    {
                        missile[j].x += (float)Math.Sin(missile[j].Rot * 3.14 / 180);
                        missile[j].y -= (float)Math.Cos(missile[j].Rot * 3.14 / 180);

                        //ミサイルが画面外に出たかどうか
                        if (missile[j].X1 < 0 && missile[j].X2 < 0 ||
                            missile[j].X1 > ClientSize.Width &&
                            missile[j].X2 > ClientSize.Width ||
                            missile[j].Y1 < 0 && missile[j].Y2 < 0 ||
                            missile[j].Y1 > ClientSize.Height &&
                            missile[j].Y2 > ClientSize.Height)
                            missile[j].Visible = false;
                    }
                }


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
