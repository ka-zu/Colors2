using System;
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


            //前回の設定を読み込む（設定）
            if (File.Exists(@"./settingLog.txt"))
            {
                try
                {
                    String str;
                    StreamReader readSetting = new StreamReader(@"./settingLog.txt");

                    str = readSetting.ReadLine();
                    speed.SelectedIndex = int.Parse(str);
                    str = readSetting.ReadLine();
                    movement.SelectedIndex = int.Parse(str);
                    str = readSetting.ReadLine();
                    kindOfPicture.SelectedIndex = int.Parse(str);
                    readSetting.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//コンソール出力
                }
            }
            //前回の設定を読み込む（ファイル）
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
            if (pic == 2)//最新5件が選択されているときボタンを無効に
            {
                select.Enabled = false;
            }
            else
            {
                select.Enabled = true;
            }
        }

        //使う画像の選択
        private void select_Click(object sender, EventArgs e)
        {
            //初期表示フォルダの設定　相対パスを絶対パスに変換する必要がある
            if (pic == 0)
            {
                open.InitialDirectory = System.IO.Path.GetFullPath(@"../../figureImages");
            }
            else
            {
                open.InitialDirectory = System.IO.Path.GetFullPath(@"../../drawImages");
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
            spe = speed.SelectedIndex;
            mov = movement.SelectedIndex;
            pic = kindOfPicture.SelectedIndex;
            //MessageBox.Show("spe = "+spe+ " mov = " + mov+ " pic = " + pic);
            /*foreach (string strFilePath in open.FileNames)
            {
                string strFileName = System.IO.Path.GetFileName(strFilePath);
                MessageBox.Show(strFileName);
            }*/
            //MessageBox.Show(open.FileNames[0]);

            Console.WriteLine("spe = " + spe + " mov = " + mov + " pic = " + pic);

            try
            {
                //ファイルに選択した画像を書き込む
                StreamWriter writer = new StreamWriter(@"./settingLog.txt", false);
           
                writer.WriteLine(spe.ToString());
                writer.WriteLine(mov.ToString());
                writer.WriteLine(pic.ToString());

                writer.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.Visible = false;
        }

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
        Image createThumbnail(Image image, int w,int h)
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
    }
}
