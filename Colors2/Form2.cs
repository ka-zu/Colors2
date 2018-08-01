﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//ファイル読み取り

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
        private int height = Screen.PrimaryScreen.Bounds.Height;
        private int width = Screen.PrimaryScreen.Bounds.Width;

        //接続されているスクリーンを取得
        private Screen[] screen = System.Windows.Forms.Screen.AllScreens;

        //ウィンドウの状態
        private FormWindowState formState = FormWindowState.Normal;

        //座標
        Point p1 = new Point();

        //設定画面で選択された画像の受け取り用
        private string[] imagePath;

        //動的配列の宣言
        List<String> strList = new List<String>();

        //図形オブジェクトの動的配列
        List<Figure> figList = new List<Figure>();

        //デフォルトで表示される図形
        Figure initFig = new Figure(@"../../logo/Colors_logo2.png");

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

        private void Form2_Load(object sender, EventArgs e)
        {
            //タイマーの初期化
            initTimer();
            /*try
            {
                String str;
                StreamReader reader = new StreamReader(@"./selectLog.txt");

                strList.Clear();
                while((str = reader.ReadLine()) != null)
                {
                    strList.Add(str);//要素を末尾に追加
                }
                reader.Close();


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/
            makeFigureObj();
        }

        //キー入力を判定する
        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'q'://ウィンドウの境界を消す
                    cahngeBorderStyle();
                    break;

                case (char)Keys.Escape://フォームを閉じる
                    this.Close();
                    break;
            }
        }

        //描画管理
        private void drawPicture(PaintEventArgs e)
        {
     
            if(figList[0] != null)
            {
                foreach (Figure temp in figList ){

                    e.Graphics.DrawImage(temp.img,temp.p);
                }
            }
            else
            {
                e.Graphics.DrawImage(initFig.img, initFig.p);
            }

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

        //ウィンドウの境界を消す
        private void cahngeBorderStyle()
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
                //this.WindowState = FormWindowState.Maximized;

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

        //時間制御初期化
        private void initTimer()
        {
            //タイマーの動作時間
            timer.Interval = 100;
            //時間で動く関数の呼び出し
            timer.Tick += new EventHandler(timer_Tick);

            //タイマーを有効化
            timer.Enabled = true;
        }

        //時間制御
        private void timer_Tick(object sender, EventArgs e)
        {
           
            if (figList[0] != null)
            {
                foreach (Figure temp in figList)
                {
                    temp.p.X += 1;
                    temp.p.Y += 1;
                }
            }
            else
            {
                initFig.p.X += 1;
                initFig.p.Y += 1;
            }
            Refresh();
        }

        //ファイルを読み込んで図形オブジェクトを作成(更新)
        private void makeFigureObj()
        {
            figList.Clear();
            try
            {
                String str;
                StreamReader reader = new StreamReader(@"./selectLog.txt");

                strList.Clear();
                while ((str = reader.ReadLine()) != null)
                {
                    Figure fig = new Figure(str);
                    figList.Add(fig);//要素を末尾に追加
                    System.Console.WriteLine(str+"\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
