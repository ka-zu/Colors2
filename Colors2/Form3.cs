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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //速度のボックス
        private void speed_SelectedIndexChanged(object sender, EventArgs e)
        {
            speed.Items.Add("ゆっくり");
            speed.Items.Add("ふつう");
            speed.Items.Add("はやい");
        }

        //動きの種類のボックス
        private void movement_SelectedIndexChanged(object sender, EventArgs e)
        {
            movement.Items.Add("まっすぐのみ");
            movement.Items.Add("斜め込み");
            movement.Items.Add("ランダム");
        }

        //画像の種類のボックス
        private void kindOfPicture_SelectedIndexChanged(object sender, EventArgs e)
        {
            kindOfPicture.Items.Add("基本画像");
            kindOfPicture.Items.Add("オリジナル画像");
        }

        /*//使う画像の選択
        private void select_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //ファイルの複数選択を可能に
            open.Multiselect = true;
            //フィルターの設定
            open.Filter = "画像ファイル|*.png;*.jpg";

            if(open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }*/
    }
}
