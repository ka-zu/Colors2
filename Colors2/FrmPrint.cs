﻿using System;
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
    /// <summary>
    /// 書いた画像を印刷するフォーム
    /// </summary>
    public partial class FrmPrint : Form
    {

        //ファイル選択用
        private OpenFileDialog open = new OpenFileDialog();

        //選択されたファイル格納用
        private String selectFrame;//選んだフレーム
        private String selectImage;//選んだ画像

        //印刷前の一旦保存の名前
        String saveName;

        Bitmap drawImg;


        public FrmPrint()
        {
            InitializeComponent();
        }

        private void FrmPrint_Load(object sender, EventArgs e)
        {
            Console.WriteLine("load");
        }

        //フレームを選ぶボタン
        private void button1_Click(object sender, EventArgs e)
        {
            //初期ディレクトリ設定
            open.InitialDirectory = System.IO.Path.GetFullPath(@"./frames");

            if(open.ShowDialog() == DialogResult.OK)
            {
                selectFrame = open.FileName;//選んだファイルを格納
                label1.Text = "えらんだフレーム："+System.IO.Path.GetFileName(selectFrame);//選んだファイルを表示
                drawFrameAndImage();

            }
        }

        //画像を選ぶボタン
        private void button2_Click(object sender, EventArgs e)
        {
            //開かれる初期ディレクトリ
            open.InitialDirectory = System.IO.Path.GetFullPath(@"./drawImages");

            if (open.ShowDialog() == DialogResult.OK)
            {
                selectImage = open.FileName;//選んだファイルを格納
                label2.Text = "えらんだ画像：" + System.IO.Path.GetFileName(selectImage);//選んだファイルを表示
                drawFrameAndImage();
            }
        }

        //印刷ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //画像がない時にエラー表示
            if (selectFrame != null || selectImage != null)
            {
                DateTime dt = DateTime.Now;
                //時間をファイル名に変換
                saveName = dt.Year.ToString() + "_" + dt.Month.ToString() + "_" + dt.Day.ToString() + "_" + dt.Hour.ToString() + "_" + dt.Second.ToString();
                saveName = @"./printing/" + saveName + ".png";
                Console.WriteLine(saveName);
                Console.WriteLine("click");
                //印刷オブジェクト
                System.Drawing.Printing.PrintDocument pd =
                    new System.Drawing.Printing.PrintDocument();
                pd.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
                
                //イベントハンドラ追加
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printPage);

                Console.WriteLine("bbbb");

                /*一旦保存*/
                //画像を保存するためのImageオブジェクト作成
                Bitmap saveImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);

                //Imageオブジェクトに画像と文字列を作成する
                Graphics g = Graphics.FromImage(saveImg);
                //imageオブジェクトに画像と文字列を描画する

                //pictureBox1.Image.Save(saveName+".png",System.Drawing.Imaging.ImageFormat.Png);
                saveImg.Save(saveName, System.Drawing.Imaging.ImageFormat.Png);


                Console.WriteLine("saved");
                /*印刷開始*/
                pd.Print();
            }
            else
            {
                MessageBox.Show("フレームか画像を選択してください。", "エラー");
            }

        }

        private void FrmPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*pictureBox1.Image.Dispose();
            drawImg.Dispose();
            this.Dispose();*/
        }

        //----自作関数----
        //印刷する画像を表示
        private void drawFrameAndImage()
        {            
            //画像をブレビューするためのImageオブジェト
            drawImg = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //ImageオブジェクトのGraphicsオブジェクトを作成
            Graphics g = Graphics.FromImage(drawImg);

            //フレームが選ばれていたら表示
            if (selectFrame != null)
            {
                g.DrawImage(Image.FromFile(selectFrame), 0, 0, 350, 495);
            }

            //画像が選ばれていたら表示
            if (selectImage != null)
            {
                g.DrawImage(Image.FromFile(selectImage), 350/2-100, 495/2 - 100, 200, 200);
            }

            //Pictureboxに適用
            pictureBox1.Image = drawImg;
        }

        //印刷する関数
        private void printPage(object sender,
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            //pictureboxの中身を入れる
            Image img = Image.FromFile(saveName);
            if(img == null)
            {
                Console.WriteLine("aaaaaaa");
            }
            //画像を描画
            if (selectFrame != null)
            {
                e.Graphics.DrawImage(Image.FromFile(selectFrame), e.MarginBounds);
            }
            if (selectImage != null)
            {
                e.Graphics.DrawImage(Image.FromFile(selectImage), 700 / 2 - 200 + 50, 990 / 2 - 200 + 50, 400, 400);
            }
            //e.Graphics.DrawImage(drawImg,0,0, e.MarginBounds.Width, e.MarginBounds.Height);
            //次のページがないことを示す
            e.HasMorePages = false;
            //後始末
            img.Dispose();
        }

        
    }
}
