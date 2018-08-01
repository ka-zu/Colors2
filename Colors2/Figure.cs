using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 投影するオブジェクトのクラス
/// </summary>
namespace Colors2
{
    public class Figure
    {
        public String objPath;
        public Image img;//画像
        public Point p;//座標
        public int vx;//速度ベクトル
        public int vy;
        public int mode;//動きのモード
        public int speed;//動きのスピード

        public Figure(String path)//コンストラクタ
        {
            Random r = new Random(path.GetHashCode());
            objPath = path;
            img = Image.FromFile(objPath);
            p.X = r.Next(Screen.PrimaryScreen.Bounds.Width-350);
            p.Y = r.Next(Screen.PrimaryScreen.Bounds.Height-350);
        }
    }
}
