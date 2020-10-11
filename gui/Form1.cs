using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



// 쉬운 GUI 사용법입니다.
// 교재와 함께 분석해보세요.

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                e.Cancel = false;  // 이벤트 취소 안함

            else
                e.Cancel = true; // 이벤트 취소

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("message", "caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //MessageBox.Show("message", "caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "텍스트 파일(*.txt)|*.txt|모든 파일(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            foreach (string strTmp in openFileDialog1.FileNames)
            {
                textBox1.Text += strTmp;
                textBox1.Text += "\r\n";
            }

        }
        

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point mousePoint = PointToClient(MousePosition);
            UpdateEventLabels("(ListBox)DoubleClick", mousePoint.X, mousePoint.Y, null);

        }

        private void UpdateEventLabels(string msg, int x, int y, MouseEventArgs e)
        {
            string message = string.Format("{0} X:{1}, Y:{2}", msg, x, y);
            string eventMsg = DateTime.Now.ToShortTimeString();
            eventMsg += " " + message;
            listBox1.Items.Insert(0, eventMsg);
            listBox1.TopIndex = 0;
            string mouseInfo;
            if (e != null)
            {
                mouseInfo = string.Format("Clicks: {0}, Delta: {1}, " + "Buttons: {2}",
                                                     e.Clicks, e.Delta, e.Button.ToString());
            }
            else { mouseInfo = string.Format("Clicks: {0}", msg); }
            label1.Text = mouseInfo;
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            Application.Exit();

        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            Point mousePoint = PointToClient(MousePosition);
            string msg = "Mouse Leave Position : " + mousePoint.X + ","
              + mousePoint.Y;
            label1.Text = msg;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = PointToClient(MousePosition);
            string msg = "Mouse Move Position : " + mousePoint.X + ","
              + mousePoint.Y;
            label1.Text = msg;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("자동으로 텍스트박스에 포커스를 지정하겠습니까?", "질문", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                textBox2.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "1234")
            {
                MessageBox.Show("로그인 완료");
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.Red;

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    MessageBox.Show("비밀번호화면에서는 방향키는 입력할 수 없습니다");
                    break;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')
            {
                e.Handled = true;
            }

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    MessageBox.Show("ESC를 누르면 자동으로 로그인버튼에 focus를 지정합니다.");
                    button1.Focus();
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int n_parsing = 0;
            if(int.TryParse(label2.Text, out n_parsing))
            {
            }
            else
            {
                label2.Text = "0";
            }
            //label2.Text = (Convert.ToInt32(label2.Text) + 1).ToString();
            label2.Text = (n_parsing + 1).ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}
