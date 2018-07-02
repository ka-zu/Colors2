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

            //コンボボックスの中身を追加
            speed.Items.Add("ゆっくり");
            speed.Items.Add("ふつう");
            speed.Items.Add("はやい");

            movement.Items.Add("まっすぐのみ");
            movement.Items.Add("斜め込み");
            movement.Items.Add("ランダム");

            kindOfPicture.Items.Add("基本画像");
            kindOfPicture.Items.Add("オリジナル画像");
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
            
        }

        //動きの種類のボックス
        private void movement_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //画像の種類のボックス
        private void kindOfPicture_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //使う画像の選択
        private void select_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //ファイルの複数選択を可能に
            open.Multiselect = true;
            //フィルターの設定
            open.Filter = "画像ファイル|*.png;*.jpg";

            //リストボックスの初期化
            pictureList.Items.Clear();
            //初期値設定
            pictureList.Items.Add("選んだ画像を表示します。");

            //ファイル選択でOKが押されたら
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //リストボックスの初期化
                pictureList.Items.Clear();

                //選択されたファイルををテキストに表示する
                foreach (string strFilePath in open.FileNames)
                {
                    //リストボックスの初期化
                    pictureList.Items.Clear();

                    //ファイルパスからファイル名を取得
                    string strFileName = System.IO.Path.GetFileName(strFilePath);

                    //リストボックスにファイル名を表示
                    pictureList.Items.Add(strFileName);
                }
            }
        }
    }
}
