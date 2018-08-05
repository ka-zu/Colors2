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
        
        //動的配列の宣言
        List<String> strList = new List<String>();

        //図形オブジェクトの動的配列
        List<Figure> figList = new List<Figure>();

        //デフォルトで表示される図形
        Figure initFig = new Figure(@"../../logo/Colors_logo2.png");

        //サイン波動作の角度用
        double angle = 0;

        //投影する図形の最大サイズ
        private const int IMG_SIZE_X = 150;
        private const int IMG_SIZE_Y = 150;

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

            //フォームに関しての初期設定
            initForm();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

            //タイマーの初期化
            initTimer();
           
            //設定した画像から図形オブジェクトを生成
            makeFigureObj();
            //ウィンドウサイズに合わせて初期座標を変更           
            initFigurePoint( figList );      
                
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
                    //this.Visible = false;
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
                    
                    e.Graphics.DrawImage(temp.img,temp.point.X,temp.point.Y,IMG_SIZE_X,IMG_SIZE_Y);
                    //e.Graphics.DrawArc(Pens.Red,temp.point.X,temp.point.Y,10,10,0,360);座標確認用
                    //e.Graphics.DrawArc(Pens.Blue, temp.centerPoint.X, temp.centerPoint.Y, 10, 10, 0, 360);//中心座標確認用
                }
            }
            else
            {
                e.Graphics.DrawImage(initFig.img, initFig.point.X, initFig.point.Y,IMG_SIZE_X,IMG_SIZE_Y);
            }

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            //描画メソッドの呼び出し
            drawPicture(e);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //フォームの状態の保存
            saveForm();
        }

        //ウィンドウの境界を消す
        private void cahngeBorderStyle()
        {
            if (isFullScreenMode == false)//消す
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

                //境界線が消えた分を補完
                this.Height += 32;
                this.Left += 8;

                isFullScreenMode = true;
            }
            else//つける
            {
                //境界線が増える分を補完
                this.Height -= 32;
                this.Left -= 8;

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
            timer.Interval = 50;
            //時間で動く関数の呼び出し
            timer.Tick += new EventHandler(timer_Tick);

            //タイマーを有効化
            timer.Enabled = true;
        }

        //時間制御
        private void timer_Tick(object sender, EventArgs e)
        {
            //角度が一周したら
            if(angle == 360)
            {
                angle = 1;
            }
            else
            {
                angle += 5;
            }


            if (figList[0] != null)
            {
                foreach (Figure temp in figList)
                {
                    //temp.point.X += 1;
                    //temp.point.Y += 1;
                   
                    movement(temp);
                    revSpeed(temp);
                }
            }
            else
            {
                revSpeed(initFig);
                movement(initFig);
            }
            Refresh();
        }

        //ファイルを読み込んで図形オブジェクトを作成(更新)
        private void makeFigureObj()
        {
            figList.Clear();
            try
            {
                //設定読み込み部
                int spe = 0; int mov = 0; int pic = 0;
                if (File.Exists(@"./settingLog.txt"))
                {
                    String str;
                    StreamReader readSetting = new StreamReader(@"./settingLog.txt");

                    str = readSetting.ReadLine();
                    spe = int.Parse(str);
                    str = readSetting.ReadLine();
                    mov = int.Parse(str);
                    str = readSetting.ReadLine();
                    pic = int.Parse(str);
                    Console.WriteLine("spe = " + spe + " mov = " + mov + " pic = " + pic);

                    readSetting.Close();
                }

                //画像割り当て部
                if (pic == 0 || pic == 1)//選択されているのが基本・オリジナル図形だったら
                {
                    if (File.Exists(@"./selectLog.txt"))//図形が選択されていたら
                    {
                        //画像を割り当て
                        String str;
                        StreamReader reader = new StreamReader(@"./selectLog.txt");

                        strList.Clear();
                        while ((str = reader.ReadLine()) != null)
                        {
                            Figure fig = new Figure(str,mov,spe);
                            figList.Add(fig);//要素を末尾に追加
                            Console.WriteLine(str);
                        }
                        reader.Close();
                    }
                }
                else if(pic == 2)//最新５件だったら
                {
                    //後で書く
                }
                else//図形が選択されてなかったら初期図形を選択
                {
                    strList.Clear();
                    figList.Add(initFig);//要素を末尾に追加                  
                }
                                        
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//コンソール出力
            }
        }

        //動きに合わせて次の座標を与える
        private void movement(Figure fig)
        {
            //Console.WriteLine("Form2:" + fig.motionType);
            switch ( fig.motionType )
            {
                case 0://動かない(基本バグってるときのみなるはず)
                    break;
                case 1://横
                    fig.point.X += fig.speed;
                    break;
                case 2://縦
                    fig.point.Y += fig.speed;
                    break;
                case 3://右斜め上
                    fig.point.X += fig.speed;
                    fig.point.Y -= fig.speed;
                    break;
                case 4://右斜め下
                    fig.point.X += fig.speed;
                    fig.point.Y += fig.speed;
                    break;
                case 5://サイン波
                    fig.point.X += fig.speed;
                    fig.point.Y += (int)(fig.speed* 2 * Math.Sin(angle * (Math.PI / 180)));
                    break;
                case 6://円
                    fig.point.X += (int)(fig.speed * 2 * Math.Cos(angle * (Math.PI / 180)));
                    fig.point.Y += (int)(fig.speed * 2 * Math.Sin(angle * (Math.PI / 180)));
                    break;
            }
            setCenterPoint(fig);
        }

        //位置座標の初期化（Form2の形に合わせるためここで定義）
        private void initFigurePoint(List<Figure> figre)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            foreach (Figure fig in figre)
            {
                //左上座標
                fig.point.X = r.Next(this.Width-IMG_SIZE_X);
                fig.point.Y = r.Next(this.Height-IMG_SIZE_Y);
                //中心座標
                fig.centerPoint.X = r.Next(this.Width - IMG_SIZE_X) + (IMG_SIZE_X / 2);
                fig.centerPoint.Y = r.Next(this.Height - IMG_SIZE_Y) + (IMG_SIZE_Y / 2);
            }
        }

        //速度を反転
        private void revSpeed(Figure fig)
        {
            
            if(fig.centerPoint.X < 0 || fig.centerPoint.X > this.Width || fig.centerPoint.Y < 0 || fig.centerPoint.Y > this.Height)
            {
                //Console.WriteLine("reverse speed");//反転タイミング確認用
                fig.speed = (-1)*fig.speed;
            }
        }

        //図形の座標から中心座標をセット(座標を変化させたら使う)
        private void setCenterPoint(Figure fig)
        {
            fig.centerPoint.X = fig.point.X + IMG_SIZE_X / 2;
            fig.centerPoint.Y = fig.point.Y + IMG_SIZE_Y / 2;
        }

        //フォームの状態を保存
        private void saveForm()
        {
            //閉じるときにフォームの座標・サイズを保存
            try
            {
                //DesktopLocationはボーダーを含んだ座標系
                int x = this.DesktopLocation.X;
                int y = this.DesktopLocation.Y;
                int width = this.Width;
                int height = this.Height;
                StreamWriter writer = new StreamWriter("./formSize.txt", false);

                writer.WriteLine(x.ToString());
                writer.WriteLine(y.ToString());

                if (this.FormBorderStyle == FormBorderStyle.None)//ボーダーが無い状態で消されたら
                {
                    height -= 32;//ボーダー分を補完
                    writer.WriteLine(width.ToString());
                    writer.WriteLine(height.ToString());
                }
                else//ボーダーがある状態で消されたら
                {
                    writer.WriteLine(width.ToString());
                    writer.WriteLine(height.ToString());
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("saveFormLoad : x = " + this.DesktopLocation.X + " y = " + this.DesktopLocation.Y + " width = " + width + " height = " + height);
        }

        //以前のフォームサイズを復元
        private void initForm()
        {
            //フォームの位置をプログラムで決められるようにする
            this.StartPosition = FormStartPosition.Manual;

            //DesktopLocationはボーダーを含んだ座標系
            int x = this.DesktopLocation.X;
            int y = this.DesktopLocation.Y;
            int width = this.Width;
            int height = this.Height;
            try
            {
                if (File.Exists(@"./formSize.txt"))//図形が選択されていたら
                {
                    //画像を割り当て
                    String str;
                    StreamReader reader = new StreamReader(@"./formSize.txt");
                    str = reader.ReadLine();
                    x = int.Parse(str);
                    str = reader.ReadLine();
                    y = int.Parse(str);
                    str = reader.ReadLine();
                    width = int.Parse(str);
                    str = reader.ReadLine();
                    height = int.Parse(str);
                    Console.WriteLine("initForm : x = " + x + " y = " + y + " width = " + width + " height = " + height);
                    reader.Close();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
            this.DesktopLocation = new Point(x, y);
            this.Width = width;
            this.Height = height;
            Console.WriteLine("initFormLoad : x = " + this.DesktopLocation.X + " y = " + this.DesktopLocation.Y + " width = " + width + " height = " + height);
        }
    }
}
