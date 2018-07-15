using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Colors2
{
    public partial class Form5 : Form
    {
        //フルスクリーンかどうか
        private bool isFullScreenMode;
        //フルスクリーン前のウィンドウ状態を保存
        private FormWindowState prevFormState;
        //通常時のフォームの境界線スタイルを保存
        private FormBorderStyle prevFormStyle;
        //通常時のウィンドウサイズを保存
        private Size prevFormSize;
        //接続してきたクライアントのIPを保存
        private const int MAX_CLIENT_NUM = 20;
        private string[] ipString = new string[MAX_CLIENT_NUM];

        //データのバッファ
        Byte[] bytes = new Byte[1024];

        //ディスプレイサイズ
        private int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        private int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        //ウィンドウの状態
        private FormWindowState formState = FormWindowState.Normal;

        public Form5()
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

            try
            {
                System.Net.Sockets.TcpListener server = BuildServer();

            }
            catch (SocketException e) {
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            //閉じるボタンを押しても再表示できるように
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private TcpListener BuildServer()
        {
            TcpListener server = null;
            int port = 11451;

            server = new TcpListener(GetSelfIP(), port);
            server.Start();
            Console.WriteLine("Listenを開始しました({0}:{1})。",
            ((System.Net.IPEndPoint)server.LocalEndpoint).Address,
            ((System.Net.IPEndPoint)server.LocalEndpoint).Port);

            return server;
        }

        private IPAddress GetSelfIP() {
            String hostName = Dns.GetHostName();    // 自身のホスト名を取得
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress address in addresses)
            {
                // IPv4 のみを追加する
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return address;
                }
            }
            return null;
        }
        
        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
        }

    }
}
