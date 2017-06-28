using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game4
{
    class Base
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int16 GetAsyncKeyState(Keys vKey);

    }
    class TimeCounter
    {
        const int TernTime = 3;
        Stopwatch sw = new System.Diagnostics.Stopwatch();
        long lastTime;
        long allCount;
        public TimeCounter()
        {
            lastTime = sw.ElapsedMilliseconds;
            sw.Start();
        }
        public long getCount()
        {
            long now = sw.ElapsedMilliseconds;
            long course = now - lastTime;
            long count = course / TernTime;
            lastTime += count * TernTime;
            allCount += count;
            return count;
        }
        public long getAllCount()
        {
            return allCount;
        }
    }
    class Chara
    {
        public float Rot;
        public float x;
        public float y;
        public Boolean Visible = true;
        Bitmap bitmap;

        public Chara(Bitmap b)
        {
            bitmap = b;
        }
        public void draw(Graphics g)
        {
            if (Visible)
            {
                g.ResetTransform();
                g.TranslateTransform(-bitmap.Width / 2, -bitmap.Height / 2);
                g.RotateTransform(Rot, MatrixOrder.Append);
                g.TranslateTransform(X, Y, MatrixOrder.Append);
                g.DrawImage(bitmap, 0, 0);
            }
        }
        public int Width
        {
            get
            {
                return bitmap.Width;
            }
        }
        public int Height
        {
            get
            {
                return bitmap.Height;
            }
        }
        public virtual float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public virtual float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public virtual float X1
        {
            get
            {
                return X - bitmap.Width / 2;
            }
        }
        public virtual float Y1
        {
            get
            {
                return Y - bitmap.Height / 2;
            }
        }
        public virtual float X2
        {
            get
            {
                return X + bitmap.Width / 2;
            }
        }
        public virtual float Y2
        {
            get
            {
                return Y + bitmap.Height / 2;
            }
        }
    }
}
