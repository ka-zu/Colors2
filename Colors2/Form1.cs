using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colors2
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
        }

        //プロジェクション部のフォーム
        Form2 prj = new Form2();
        //設定部のフォーム
        Form3 set = new Form3();
        //説明部のフォーム
        Form4 intro = new Form4();
        //通信部のフォーム
        Form5 socket;

        //スタートボタン
        private void button1_Click(object sender, EventArgs e)
        {
            //プロジェクション部のフォーム
            prj = new Form2();
            prj.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.ImageLocation=@"./logo/Colors_logo2.png";
            tane_logo.SizeMode = PictureBoxSizeMode.Zoom;
            tane_logo.ImageLocation = @"./logo/tane_logo.png";
        }

        //設定ボタン
        private void button4_Click(object sender, EventArgs e)
        {
            set.Show();
        }

        //終了ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //説明ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            intro.Show();
        }

        //説明ボタン
        private void button5_Click(object sender, EventArgs e)
        {
            if (socket == null) {
                socket = new Form5();
            }
            //画像受信部のフォーム
            socket.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ほかのフォームを閉じる
            prj.Close();
            set.Close();
            intro.Close();
        }
    }
}
