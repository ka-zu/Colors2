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
using System.Diagnostics;

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

        //ディスプレイサイズ
        private int height = Screen.PrimaryScreen.Bounds.Height;
        private int width = Screen.PrimaryScreen.Bounds.Width;

        //ウィンドウの状態
        private FormWindowState formState = FormWindowState.Normal;

        private List<byte> imageData = new List<byte>();

        public delegate void MyEventHandler(object sender, DataReceivedEventArgs e);
        public event MyEventHandler MyEvent = null;
        public static Process p;
        private bool flag = false;

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

            // javaでサーバ作るの
            MyEvent = new MyEventHandler(event_DataReceived);

            p = new Process();
            //string apppath = Path.GetDirectoryName(Application.ExecutablePath);
            //box.Text += apppath + @"TCPGraphicGetting_x64.exe";
            p.StartInfo.FileName = @"C:\\Users\\marii\\Desktop\\Colors2\\Colors2\\TCPGraphicGetting_x64.exe";
            box.Text += p.StartInfo.FileName;

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true; // 標準入力をリダイレクト
            p.StartInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            p.StartInfo.RedirectStandardError = true; 
            p.StartInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            p.OutputDataReceived += new DataReceivedEventHandler(p_DataReceived);

            p.Start();
            p.BeginOutputReadLine();
            /*
            try
            {
                TcpListener server = BuildServer();
                while (true)
                {
                    RunServer(server);
                }
            }
            catch (SocketException e) {
            }
            */
        }

        void event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.IndexOf("TCPサーバ") != -1) { flag = true; }
            if (e.Data.IndexOf("%") == -1) {
                if (flag) box.Text += e.Data + "\r\n";
            }
        }

        void p_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);  
            this.Invoke(MyEvent, new object[2] { sender, e });
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
        
        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
        }


        /*----------------------------一時退避--------------------------------*/
        private void RunServer(TcpListener server)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("クライアント({0}:{1})と接続しました。",
                ((IPEndPoint)client.Client.RemoteEndPoint).Address,
                ((IPEndPoint)client.Client.RemoteEndPoint).Port);
            // NetworkStreamを取得データの流れ
            NetworkStream ns = client.GetStream();

            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;

            Encoding enc = Encoding.UTF8;
            bool disconnected = false;
            byte[] resBytes = new byte[256];
            int resSize = 0;
            ulong totalResSIze = 0;
            ulong data_size = 0;
            byte[] size_buf = new byte[4];


            ns.Read(size_buf, 0, 4);
            for (int i = 0; i < 8; i++) {
                data_size += ((ulong)size_buf[i] << i*8);
            }
            while(totalResSIze<data_size)
            {
                //データの一部を受信する
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                //Readが0を返した時はクライアントが切断したと判断
                if (resSize == 0)
                {
                    disconnected = true;
                    
                    break;
                }
                //受信したデータを蓄積する

                imageData.AddRange(resBytes);
                //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                // 受信を続ける
                totalResSIze += (ulong)resSize;
            }
            System.Threading.Thread.Sleep(100);
            ns.Close();
            client.Close();
        } 

        private TcpListener BuildServer()
        {
            TcpListener server = null;
            int port = 11451;

            server = new TcpListener(GetSelfIP(), port);
            server.Start();

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
    }
}
