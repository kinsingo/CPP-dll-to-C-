using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Runtime.InteropServices; // C Lib를 쓰기 위함
namespace LGD_CS
{
    public partial class Form1 : Form
    {
        public struct typeExp
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String strTest;

            public int intTest;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byteTest;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] uintTest;
        };

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static void calltest();

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int intReturn(int n_in);

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static string strReturn(string str);

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int stReturn(ref typeExp st);

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int aryReturn(int[] intTemp);

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static IntPtr intpReturn();

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static string stringReturn();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString()); // 현재 시간
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("정말 종료하시겠습니까?", "질문",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int send = 999;
            int rtn = intReturn(send);
            MessageBox.Show(rtn + " 리턴값 수신, 호출완료");


            // 문자
            string sendstr = "sendstring C#";

            //IntPtr p  = strReturn(sendstr);
            //string c = Marshal.PtrToStringAnsi(p);

            string rtnStr = strReturn(sendstr);

            //string rtnStr = p + "///test"; // 형변환 오류

            MessageBox.Show("string test: " + rtnStr);


            typeExp testTemp = new typeExp();
            testTemp.byteTest = new byte[64];
            testTemp.uintTest = new uint[4];

            testTemp.strTest = "testtest c#";
            testTemp.byteTest[0] = byte.Parse("9");
            testTemp.uintTest[0] = uint.Parse("3");

            int rtnst = stReturn(ref testTemp);

            MessageBox.Show("struct test: " + testTemp.strTest + "," + testTemp.uintTest[0]);

            int[] intTemp = new int[10];
            int rtnary = aryReturn(intTemp);

            MessageBox.Show("arry test: " + intTemp[0] + "," + intTemp[1]);

            IntPtr pintp = intpReturn();
            string cintp = Marshal.PtrToStringAnsi(pintp);

            MessageBox.Show("intp test: " + cintp + "," + pintp);
        }
    }
}
