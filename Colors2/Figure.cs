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
        public Point point;//座標
        public int vx;//速度ベクトル
        public int vy;
        public int move;//動きのモード
        public int motionType;//設定されたモードから割り当てられる動き
        public int speed;//動きのスピード

        public Figure(String path)//コンストラクタ(動きと速さがない場合)
        {
            Random r = new Random(path.GetHashCode());
            objPath = path;
            img = Image.FromFile(objPath);
            move = 0;
            speed = 0;
            point.X = r.Next(Screen.PrimaryScreen.Bounds.Width-350);
            point.Y = r.Next(Screen.PrimaryScreen.Bounds.Height-350);
        }

        public Figure(String path, int mov, int spe)
        {
            Random r = new Random(path.GetHashCode());
            objPath = path;
            img = Image.FromFile(objPath);
            move = mov;
            speed = spe;
            point.X = r.Next(Screen.PrimaryScreen.Bounds.Width - 350);
            point.Y = r.Next(Screen.PrimaryScreen.Bounds.Height - 350);
        }

        private int returnMotionType()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            switch (move)
            {
                case 0://縦横のみの時 
                    return r.Next(1,2);
                case 1://縦横斜め
                    return r.Next(1,4);
                case 2://縦・横・右斜め上・左斜め上・サイン波・円
                    return r.Next(1,6);
                default ://0は動かない
                    return 0;
            }
        }
    }
}
