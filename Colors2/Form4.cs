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
    public partial class Form4 : Form
    {
        //ページ番号
        int pageNum = 1;
        const int MAX_PAGE = 6;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.ImageLocation = @"./logo/page1.png";
            pictureBox2.ImageLocation = @"./logo/page2.png";
            pictureBox3.ImageLocation = @"./logo/page3.png";
            pictureBox4.ImageLocation = @"./logo/page4.png";
            pictureBox5.ImageLocation = @"./logo/page5.png";
            pictureBox6.ImageLocation = @"./logo/page6.png";

            paging();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //閉じるボタンを押しても再表示できるように
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        //次へボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (pageNum < MAX_PAGE)
            {
                pageNum++;
                paging();
            }
        }

        //戻るボタン
        private void button2_Click(object sender, EventArgs e)
        {
            if(pageNum == 1)
            {
                this.Close();
            }
            else
            {
                pageNum--;
                paging();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //ページ遷移関数
        private void paging()
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;

            switch (pageNum)
            {
                case 1:
                    pictureBox1.Visible = true;
                    break;
                case 2:
                    pictureBox2.Visible = true;
                    break;
                case 3:
                    pictureBox3.Visible = true;
                    break;
                case 4:
                    pictureBox4.Visible = true;
                    break;
                case 5:
                    pictureBox5.Visible = true;
                    break;
                case 6:
                    pictureBox6.Visible = true;
                    break;
                default:
                    break;
            }

            label1.Text = pageNum + " / " + MAX_PAGE;
        }
       
    }
}
