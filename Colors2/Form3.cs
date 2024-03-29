﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//ファイル書き込み

namespace Colors2
{
    public partial class Form3 : Form
    {

        //コンボボックスを受け取る変数
        private int spe;
        private int mov;
        private int pic;

        //ファイルピッカ用
        private OpenFileDialog open = new OpenFileDialog();

        //カラーピッカ用
        ColorDialog cd = new ColorDialog();
        //選択されたカラーを受け取る関数
        Color selectedColor = new Color();

        //リストビューのサイズ用
        int imgSize = 50;

        //選択したファイルパスの受け渡し用
        String[] selectFiles;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //コンボボックスの中身を追加
            speed.Items.Add("ゆっくり");
            speed.Items.Add("ふつう");
            speed.Items.Add("はやい");
            speed.SelectedIndex = 0;//初期値
            spe = speed.SelectedIndex;

            movement.Items.Add("まっすぐのみ");
            movement.Items.Add("斜め込み");
            movement.Items.Add("ランダム");
            movement.SelectedIndex = 0;
            mov = movement.SelectedIndex;

            kindOfPicture.Items.Add("基本画像");
            kindOfPicture.Items.Add("オリジナル画像");
            kindOfPicture.Items.Add("最新5件");//オリジナル画像から新しく追加された5個を表示
            kindOfPicture.Items.Add("最新10件");//オリジナル画像から新しく追加された10個を表示
            kindOfPicture.Items.Add("最新15件");//オリジナル画像から新しく追加された15個を表示
            kindOfPicture.SelectedIndex = 0;
            pic = kindOfPicture.SelectedIndex;

            //ファイルの複数選択を可能に
            open.Multiselect = true;
            //フィルターの設定
            open.Filter = "画像ファイル|*.png;*.jpg";
            //前回開いたファイルを覚えない
            open.RestoreDirectory = true;

            //リストビューの中で画像を表示できるように
            imageList1.ImageSize = new Size(imgSize, imgSize);
            selectedListView.SmallImageList = imageList1;
            //ビューをDetailsにする
            selectedListView.View = View.Details;
            //リストビューのヘッダー部を追加
            selectedListView.Columns.Add("画像");
            selectedListView.Columns.Add("ファイル名", 100);

            //テキストボックスの初期値
            size.Text = "150";

            //はじめに選択されている色を設定
            selectedColor = Color.FromArgb(255, 255, 255, 255);

            cd.Color = selectedColor;
            //色の作成部分を表示可能にする（デフォルトでtrue）
            //cd.AllowFullOpen = true

            //テキストから設定を読み込む
            loadSetting();
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
            pic = kindOfPicture.SelectedIndex;
            if (pic == 0 || pic == 1)//基本画像かオリジナル画像の時有効化
            {
                select.Enabled = true;
            }
            else
            {
                select.Enabled = false;
            }
        }

        //使う画像の選択
        private void select_Click(object sender, EventArgs e)
        {
            //初期表示フォルダの設定　相対パスを絶対パスに変換する必要がある
            if (pic == 0)
            {
                open.InitialDirectory = System.IO.Path.GetFullPath(@"./figureImages");
            }
            else
            {
                open.InitialDirectory = System.IO.Path.GetFullPath(@"./drawImages");
            }

            //ファイル選択でOKが押されたら
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //リストビューの初期化
                selectedListView.Items.Clear();
                //イメージリストの初期化
                imageList1.Images.Clear();

                //ファイルを送る変数に保存
                selectFiles = open.FileNames;

                int i = 0;

                try
                {
                    //ファイルに選択した画像を書き込む
                    StreamWriter writer = new StreamWriter(@"./selectLog.txt", false);

                    //選択されたファイルををテキストに表示する
                    foreach (string strFilePath in open.FileNames)
                    {

                        //書き込み
                        writer.WriteLine(strFilePath);

                        //ファイルパスからファイル名を取得
                        string strFileName = System.IO.Path.GetFileName(strFilePath);

                        //サムネイルを作成
                        Image original = Bitmap.FromFile(strFilePath);
                        Image thumbnail = createThumbnail(original, imgSize, imgSize);

                        //イメージリストに画像を入れて、リストビューへ
                        imageList1.Images.Add(thumbnail);
                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = i;
                        item.SubItems.Add(strFileName);
                        selectedListView.Items.Add(item);
                        i++;
                    }

                    writer.Close();//ファイルクローズ
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            //閉じるボタンを押しても再表示できるように
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        //戻るボタン
        private void quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        //決定ボタン
        private void apply_Click(object sender, EventArgs e)
        {

            saveSetting();
            this.Visible = false;
        }

        

        private void Form3_Shown(object sender, EventArgs e)
        {

        }

        private void selectBGColor_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                selectedColor = cd.Color;
                colorText.BackColor = selectedColor;
                colorPane.BackColor = selectedColor;
            }
        }

        //-----自作関数-----

        //選択したファイルのgetter
        private string[] getFileNames()
        {
            return open.FileNames;
        }

        //リストビューの初期化
        private void initSelectedListView()
        {
            //グリッドラインの表示
            selectedListView.GridLines = true;
            //ヘッダー部を追加
            selectedListView.Columns.Add("画像", 100);
            selectedListView.Columns.Add("ファイル名", 100);
        }

        //サムネイル画像を作成(画像、横幅、縦幅)
        Image createThumbnail(Image image, int w, int h)
        {
            Bitmap canvas = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, w, h);

            float fw = (float)w / (float)image.Width;
            float fh = (float)h / (float)image.Height;

            float scale = Math.Min(fw, fh);
            fw = image.Width * scale;
            fh = image.Height * scale;

            g.DrawImage(image, (w - fw) / 2, (h - fh) / 2, fw, fh);
            g.Dispose();

            return canvas;
        }

        //設定をを保存
        private void saveSetting()
        {
            spe = speed.SelectedIndex;
            mov = movement.SelectedIndex;
            pic = kindOfPicture.SelectedIndex;

            Console.WriteLine("spe = " + spe + " mov = " + mov + " pic = " + pic);

            try
            {
                //ファイルに選択した画像を書き込む
                StreamWriter writer = new StreamWriter(@"./settingLog.txt", false);

                writer.WriteLine(spe.ToString());
                writer.WriteLine(mov.ToString());
                writer.WriteLine(pic.ToString());
                writer.WriteLine(selectedColor.A);
                writer.WriteLine(selectedColor.R);
                writer.WriteLine(selectedColor.G);
                writer.WriteLine(selectedColor.B);
                writer.WriteLine(size.Text);

                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //前回の設定を復帰
        private void loadSetting()
        {
            int A = 255, R = 255, G=255, B=255;

            //前回の設定を読み込む（設定）
            if (File.Exists(@"./settingLog.txt"))
            {
                try
                {
                    String str;
                    StreamReader readSetting = new StreamReader(@"./settingLog.txt");

                    str = readSetting.ReadLine();
                    spe = int.Parse(str);
                    str = readSetting.ReadLine();
                    mov = int.Parse(str);
                    str = readSetting.ReadLine();
                    pic = int.Parse(str);
                    str = readSetting.ReadLine();
                    A = int.Parse(str);
                    str = readSetting.ReadLine();
                    R = int.Parse(str);
                    str = readSetting.ReadLine();
                    G = int.Parse(str);
                    str = readSetting.ReadLine();
                    B = int.Parse(str);
                    size.Text = readSetting.ReadLine();

                    Console.WriteLine("loadSetting: A=" + A + " R=" + R + " G=" + G + " B=" + B);

                    speed.SelectedIndex = spe;
                    movement.SelectedIndex = mov;
                    kindOfPicture.SelectedIndex = pic;
                    selectedColor = Color.FromArgb(A,R,G,B);
                    cd.Color = selectedColor;
                    colorText.BackColor = selectedColor;
                    colorPane.BackColor = selectedColor;

                    readSetting.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//コンソール出力
                }
            }

            //前回の設定を読み込む（ファイル）
            if (kindOfPicture.SelectedIndex == 0 || kindOfPicture.SelectedIndex == 1)//基本画像かオリジナル画像だったら
            {
                if (File.Exists(@"./selectLog.txt"))
                {
                    int i = 0;
                    try
                    {
                        //画像を割り当て
                        String str;
                        StreamReader reader = new StreamReader(@"./selectLog.txt");

                        //リストビューの初期化
                        //selectedListView.Clear();
                        //イメージリストの初期化
                        //imageList1.Images.Clear();

                        while ((str = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(str);

                            //ファイルパスからファイル名を取得
                            string strFileName = System.IO.Path.GetFileName(str);

                            //サムネイルを作成
                            Image original = Bitmap.FromFile(str);
                            Image thumbnail = createThumbnail(original, imgSize, imgSize);

                            //イメージリストに画像を入れて、リストビューへ
                            imageList1.Images.Add(thumbnail);
                            ListViewItem item = new ListViewItem();
                            item.ImageIndex = i;
                            item.SubItems.Add(strFileName);
                            selectedListView.Items.Add(item);
                            i++;
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);//コンソール出力
                    }
                }
            }
            else//最新〇件だったら
            {
                Console.WriteLine("pic num =" + pic);
                int num = (pic - 1) * 5;//持ってくる画像の数　手入力の時もここに入れる

                string directryPath = @"./drawImages";
                string[] files = Directory.GetFiles(Path.GetFullPath(directryPath));

                if (files.Length < num)//ファイル数が表示させる数より少なかったら
                {
                    Console.WriteLine("files num =" + files.Length);
                    num = files.Length;
                }

                //絵はファイル名が時間で送られてくるので降順にする
                Array.Sort(files);
                Array.Reverse(files);

                for (int i = 0; i < num; i++)//新しい物からnum件とってくる
                {
                    Console.WriteLine(files[i]);
                    //ファイルパスからファイル名を取得
                    string strFileName = System.IO.Path.GetFileName(files[i]);

                    //サムネイルを作成
                    Image original = Bitmap.FromFile(files[i]);
                    Image thumbnail = createThumbnail(original, imgSize, imgSize);

                    //イメージリストに画像を入れて、リストビューへ
                    imageList1.Images.Add(thumbnail);
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    item.SubItems.Add(strFileName);
                    selectedListView.Items.Add(item);
                }

            }
        }

        //テキストボックスに数字しか入力できないようにする
        private void size_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0~9・バックスペース以外だったら
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                //入力をキャンセル
                e.Handled = true;
            }

        }
    }
}
