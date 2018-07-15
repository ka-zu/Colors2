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

        //ディスプレイサイズ
        private int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        private int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        //ウィンドウの状態
        private FormWindowState formState = FormWindowState.Normal;

        private List<byte> imageData = new List<byte>();

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
                TcpListener server = BuildServer();
                while (true)
                {
                    RunServer(server);
                }
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

            do
            {
                //データの一部を受信する
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                //Readが0を返した時はクライアントが切断したと判断
                if (resSize == 0)
                {
                    disconnected = true;
                    Console.WriteLine("クライアント({0})が切断しました。",((IPEndPoint)client.Client.RemoteEndPoint).Address);
                    break;
                }
                //受信したデータを蓄積する
                imageData.AddRange(resBytes);
                //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                // 受信を続ける

            } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
        } 

        private TcpListener BuildServer()
        {
            TcpListener server = null;
            int port = 11451;

            server = new TcpListener(GetSelfIP(), port);
            server.Start();
            Console.WriteLine("Listenを開始しました({0}:{1})。",
            ((IPEndPoint)server.LocalEndpoint).Address,
            ((IPEndPoint)server.LocalEndpoint).Port);

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
