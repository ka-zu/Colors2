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
        private String objPath;
        Image img;//画像
        private int x, y;//座標
        private int mode;//動きのモード
        private int speed;//動きのスピード

        public Figure()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
