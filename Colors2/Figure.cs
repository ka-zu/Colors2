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
        public Point centerPoint;//中心座標
        public int vx;//速度ベクトル
        public int vy;
        public int move;//動きのモード
        public int motionType;//設定されたモードから割り当てられる動き
        public int speed;//動きのスピード
        public int collisionTimer;//ぶつかった時のクールタイム
        public bool canCollision;//ぶつかることができるか
        public int size;

        Random r = new Random(DateTime.Now.Millisecond);

        public Figure(String path)//コンストラクタ(動きと速さがない場合)
        {
            objPath = path;
            img = Image.FromFile(objPath);

            move = 0;
            speed = returnSpeed(0);
            motionType = returnMotionType();
            collisionTimer = 10;
            canCollision = true;
            size = 150;
        }

        public Figure(String path, int randNum)//コンストラクタ(動きと速さがない場合)・乱数値も受け取る
        {
            objPath = path;
            r = new Random(randNum);

            img = Image.FromFile(objPath);
            move = 0;
            speed = returnSpeed(0);
            motionType = returnMotionType();
            collisionTimer = 10;
            canCollision = true;
            size = 150;
        }

        public Figure(String path, int mov, int spe)
        {
            objPath = path;
            img = Image.FromFile(objPath);
            move = mov;
            speed = returnSpeed(spe);
            motionType = returnMotionType();
            collisionTimer = 10;
            canCollision = true;
            size = 150;
        }

        public Figure(String path, int mov, int spe, int randNum)
        {
            objPath = path;
            r = new Random(randNum);

            img = Image.FromFile(objPath);
            move = mov;
            speed = returnSpeed(spe);
            motionType = returnMotionType();
            collisionTimer = 10;
            canCollision = true;
            size = 150;
        }

        public Figure(String path, int mov, int spe, int randNum, int s)
        {
            objPath = path;
            r = new Random(randNum);

            img = Image.FromFile(objPath);
            move = mov;
            speed = returnSpeed(spe);
            motionType = returnMotionType();
            collisionTimer = 10;
            canCollision = true;
            size = s;
        }

        private int returnMotionType()
        {
            
            switch (move)
            {
                case 0://縦横のみの時 
                    return r.Next(1000)%2 + 1;
                case 1://縦横斜め
                    return r.Next(1000)%4 + 1;
                case 2://縦・横・右斜め上・左斜め上・サイン波・円
                    return r.Next(1000)%6 + 1;
                default ://0は動かない
                    return 0;
            }
        }

        private int returnSpeed(int spe)
        {
            switch (spe)
            {
                case 0:
                    return r.Next(2)+1;
                case 1:
                    return r.Next(2,4)+1;
                case 2:
                    return r.Next(4,6)+1;
                default :
                    return 0;
            }
        }
    }
}
