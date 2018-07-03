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
    public partial class Form2 : Form
    {
        //フルスクリーンかどうか
        private bool isFullScreenMode;
        //フルスクリーン前のウィンドウ状態を保存
        private FormWindowState prevFormState;
        //通常時のフォームの境界線スタイルを保存
        private FormBorderStyle prevFormStyle;
        //通常時のウィンドウサイズを保存
        private Size prevFormSize;

        //ディスプレイサイズ
        private int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        private int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        //接続されているスクリーンを取得
        private Screen[] screen = System.Windows.Forms.Screen.AllScreens;

        //ウィンドウの状態
        private FormWindowState formState = FormWindowState.Normal;

        //ウィンドウ・フルスクリーンの切り替え
        private void changeWindowMode()
        {
            if (isFullScreenMode == false)
            {
                //フルスクリーンに変更

                //ウィンドウの状態を保存
                prevFormState = this.WindowState;
                //境界線スタイルを保存
                prevFormStyle = this.FormBorderStyle;

                //最大化　＝＞　フルスクリーンではタスクバーが消えないので
                //いったん通常表示
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                //フォームサイズを保存
                prevFormSize = this.ClientSize;

                //境界線スタイルをなくす
                this.FormBorderStyle = FormBorderStyle.None;

                //ウィンドウサイズを最大化
                this.WindowState = FormWindowState.Maximized;

                isFullScreenMode = true;
            }
            else
            {
                //最大化に戻すときはいったん通常表示にする
                //（フルスクリーン処理との関係のため）
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                //フォームの境界線スタイルを元に戻す
                this.FormBorderStyle = prevFormStyle;

                //フォームのウィンドウ状態をもとにもどす
                this.WindowState = prevFormState;

                isFullScreenMode = false;
            }
        }

        public Form2()
        {
            InitializeComponent();

            this.KeyPreview = true;

            //通常スクリーンモード
            isFullScreenMode = false;
            // フルスクリーン表示前のウィンドウの状態を保存する
            prevFormState = FormWindowState.Normal;
            // 通常表示時のフォームの境界線スタイルを保存する
            prevFormStyle = FormBorderStyle.Sizable;
            // 通常表示時のウィンドウのサイズを保存する
            prevFormSize = new Size(496, 219);

            //ウィンドウの状態を保存
            formState = this.WindowState;
        }

        //キー入力を判定する
        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'q'://ウィンドウ・フルスクリーンを変更
                    changeWindowMode();
                    break;

                case (char)Keys.Escape://フォームを閉じる
                    this.Close();
                    break;
            }
        }

        private void drawPicture(PaintEventArgs e)
        {
            //座標
            Point p1 = new Point();
            
            //画像を割り当て
            Image img1 = Image.FromFile("../../images/blackRectangle.png");

            e.Graphics.DrawImage(img1, p1);
            
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            //描画メソッドの呼び出し
            drawPicture(e);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //閉じるボタンを押しても再表示できるように
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
    }
}
