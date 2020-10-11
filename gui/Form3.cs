using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// 그림판 예제입니다.
// GUI를 하다보면 Graphic 객체를 자주 사용합니다.
// 한번 분석해보시길 바랍니다.
// 여러번 보시다 보면 익숙해지는 순간이 곧 발생합니다.


namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        private const uint LBUTTONDOWN = 0x00000002;  // 왼쪽 마우스 버튼 누름
        private const uint LBUTTONUP = 0x00000004;  // 왼쪽 마우스 버튼 땜
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);

        Bitmap bmpDraw = null;
        Bitmap bmpSave = null;
        Cursor Cursor_Draw = null;
        bool bMouseEnter = false;
        bool bMouseDown = false;
        Point pntDown;
        Point pntUp;
        Pen Pen_MouseMove = new Pen(Color.Blue, 10);


        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            bMouseEnter = true;
            Set_Cursor();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            bMouseEnter = false;
            //Point pntMouse = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            //mouse_event(LBUTTONUP, (uint)pntMouse.X, (uint)pntMouse.Y, 0, 0);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;
            pntDown = new Point(e.X, e.Y);

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseDown == true)
            {
                MouseProcedure(1, e.X, e.Y);
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
            pntUp = new Point(e.X, e.Y);
            MouseProcedure(2, e.X, e.Y);

        }
        private void MouseProcedure(int iMouseAction, int iMouseX, int iMouseY) // 0 : Down, 1 : Move, 2 :Up
        {
            if (bmpDraw != null)
            {
                bmpDraw.Dispose();
                bmpDraw = null;
            }

            if(bmpSave != null)
            {
                bmpDraw = (Bitmap)bmpSave.Clone();
            }
            else
            {
                bmpDraw = new Bitmap(535, 415);
            }
            Graphics g = Graphics.FromImage(bmpDraw);
            switch (iMouseAction)
            {
                case 1:
                    g.DrawLine(Pen_MouseMove, pntDown.X, pntDown.Y, iMouseX, iMouseY);
                    g.DrawString(string.Format("X:{0}, Y:{1}", iMouseX, iMouseY), new Font("Verdana", 16), new SolidBrush(Color.Red), iMouseX, iMouseY);
                    break;

                case 2:
                    if (radioButton1.Checked == true) // 원
                    {
                        g.DrawEllipse(Pen_MouseMove, pntDown.X, pntDown.Y, Math.Abs(pntDown.X - pntUp.X), Math.Abs(pntDown.Y - pntUp.Y));
                    }
                    else if (radioButton2.Checked == true) // 사각
                    {
                        g.DrawRectangle(Pen_MouseMove, pntDown.X, pntDown.Y, Math.Abs(pntDown.X - pntUp.X), Math.Abs(pntDown.Y - pntUp.Y));
                    }
                    else if (radioButton3.Checked == true) // 선
                    {
                        g.DrawLine(Pen_MouseMove, pntDown.X, pntDown.Y, pntUp.X, pntUp.Y);
                    }
                    bmpSave = (Bitmap)bmpDraw.Clone();
                    break;
            }
            g.Dispose();
            g = null;
            pictureBox1.Image = bmpDraw;
        }

        private void Set_Cursor()
        {
            if (Cursor_Draw != null)
            {
                Cursor_Draw.Dispose();
                Cursor_Draw = null;
            }

            Bitmap Bitmap_Tmp = null;
            Graphics g = null;
            IntPtr ptr;

            int iHalfWidth = 20;
            int iHalfHeight = 20;
            Bitmap_Tmp = new Bitmap(iHalfWidth * 2 + 1, iHalfHeight * 2 + 1);
            g = Graphics.FromImage(Bitmap_Tmp);
            g.DrawEllipse(new Pen(Color.Red), 0, 0, iHalfWidth * 2, iHalfHeight * 2);
            g.DrawLine(new Pen(Color.Red), iHalfWidth, iHalfHeight - 5, iHalfWidth, iHalfHeight + 5);
            g.DrawLine(new Pen(Color.Red), iHalfWidth - 5, iHalfHeight, iHalfWidth + 5, iHalfHeight);
            ptr = Bitmap_Tmp.GetHicon();
            Cursor_Draw = new Cursor(ptr);
            pictureBox1.Cursor = Cursor_Draw;
            g.Dispose();
            g = null;
            Bitmap_Tmp.Dispose();
            Bitmap_Tmp = null;
        }
    }
}
