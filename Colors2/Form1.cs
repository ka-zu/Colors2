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

        //設定部のフォーム

        //スタートボタン
        private void button1_Click(object sender, EventArgs e)
        {            
            prj.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //設定ボタン
        private void button4_Click(object sender, EventArgs e)
        {
            set.Show();
        }

        //終了ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //閉じるとき、ほかのフォームも一緒に閉じる
            if(prj == null || prj.IsDisposed)//投影部が開いていないなら
            {
                this.Close();//自分だけ
            }
            else//開いているなら
            {
                prj.Dispose();//投影部を閉じてから
                this.Close();
            }
        }

        //説明ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            intro.Show();
        }
    }
}
