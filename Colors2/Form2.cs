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
        //ディスプレイサイズ
        private int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        private int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        //接続されているスクリーンを取得
        private Screen[] screen = System.Windows.Forms.Screen.AllScreens;
        

        public Form2()
        {
            InitializeComponent();
        }
    }
}
