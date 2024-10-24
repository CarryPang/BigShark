using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋简化版本
{
    public class API
    {
        /// <summary>
        /// 1、画游戏头
        /// </summary>
        public void TitleName()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("******************************");
        }

        /// <summary>
        /// 2、初始化地图
        /// </summary>
        public void InitailMap()
        {
            //幸运轮盘
            int[] luckyturn = { 6, 34, 23, 54, 45, 78,90};
            for(int i = 0; i<luckyturn.Length; i++)
            {
                Model.Maps[luckyturn[i]] = 1;
            }

            //地雷
            int[] landline = { 5, 22, 31, 44, 75, 87, 92 };
            for(int i = 0; i < landline.Length; i++)
            {
                Model.Maps[landline[i]] = 2;
            }

            //暂停
            int[] pause = { 7, 15, 24, 46, 56, 84 };
            for(int i = 0; i < pause.Length; i++)
            {
                Model.Maps[pause[i]] = 3;
            }

            //时空隧道
            int[] TimeTunnel = { 9, 18, 29, 37, 42, 63, 72, 88, 96 };
            for(int i = 0; i < TimeTunnel.Length;i++)
            {
                Model.Maps[TimeTunnel[i]] = 4;
            }
        }

        /// <summary>
        /// 从画地图的方法中抽象出来的一种方法
        /// </summary>
        /// <param name="i">传参进来的一个变量</param>
        /// <returns></returns>
       public string DrawStringMap(int i)
        {
            //画图
            string str = "";
            if (Model.PlayerPos[0] == Model.PlayerPos[1] && Model.PlayerPos[0] == i)
            {
                str = "<>";
            }
            else if (Model.PlayerPos[0] == i)
            {
                str = "A";
            }
            else if (Model.PlayerPos[1] == i)
            {
                str = "B";
            }
            else
            {
                switch (Model.Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "▲";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "○";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "¥";
                        break;

                }
            }
            return str;
        }

        /// <summary>
        /// 画地图
        /// </summary>
        public void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘：▲  地雷：☆  暂停：○  时空隧道：¥");

            //第一横行
            for(int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));//直接调用画地图的那个方法
            }

            //画完第一横行直接换行
            Console.WriteLine();

            //第一竖行
            for(int i = 30; i < 35; i++)
            {
                for(int j = 0; j <= 28;j++)
                {
                    Console.Write(" ");//这里画两个空格
                }
                Console.Write(DrawStringMap(i));//直接调用画地图的那个方法
                Console.WriteLine();
            }

            //第二横行
            for(int i = 64; i >= 35;i--)
            {
                Console.Write(DrawStringMap(i));
            }

            //画完第二横行直接换行
            Console.WriteLine();

            //第二竖行
            for(int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }

            //第三横行
            for(int i = 70; i <= 99;i++)
            {
                Console.Write(DrawStringMap(i));
            }

            //画完最后一行直接换行
            Console.WriteLine();
        }

        /// <summary>
        /// 玩游戏
        /// </summary>
        /// <param name="playerNumber">传递进来的一个参数来判定是谁在玩游戏</param>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();//设定随机数
            int rNumber = r.Next(1, 7);//限定随机数范围在1-7之间
            Console.WriteLine("{0}按下任意键开始掷骰子", Model.PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}掷出了{1}", Model.PlayerName[playerNumber], rNumber);//掷出了随机数1-7之间
            Model.PlayerPos[playerNumber] += rNumber;//玩家坐标加上掷出的随机数
            ChangePos();//在每一个玩家坐标变化的地方都要进行一次限制，限制玩家坐标在地图0-99之内
            Console.ReadKey(true);
            if (Model.PlayerPos[playerNumber] == Model.PlayerPos[1 - playerNumber])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}退6格", Model.PlayerName[playerNumber], Model.PlayerName[1 - playerNumber], Model.PlayerName[1 - playerNumber]);
                Model.PlayerPos[1 - playerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else
            {
                switch (Model.Maps[Model.PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，啥都没有", Model.PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择1--交换位置，2--轰炸对方", Model.PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}跟玩家{1}交换位置", Model.PlayerName[playerNumber], Model.PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                int temp = Model.PlayerPos[playerNumber];
                                Model.PlayerPos[playerNumber] = Model.PlayerPos[1 - playerNumber];
                                Model.PlayerPos[1 - playerNumber] = temp;
                                ChangePos();
                                Console.WriteLine("交换完成，按任意键继续游戏");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{2}退6格", Model.PlayerName[playerNumber], Model.PlayerName[1 - playerNumber], Model.PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                Model.PlayerPos[1 - playerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("玩家{0}退了6格，按任意键继续游戏", Model.PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("你输入的文本有误，请重新输入！");
                                input = Console.ReadLine();
                                Console.ReadKey(true);
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", Model.PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        Model.PlayerPos[playerNumber] -= 6;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", Model.PlayerName[playerNumber]);
                        Model.Flag[playerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", Model.PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        Model.PlayerPos[playerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;

                }//switch

            }//else
            ChangePos();//perfect
            Console.Clear();
        }
        /// <summary>
        /// 当玩家坐标发生改变的时候调用这个方法，用于限制玩家坐标在0-99之间
        /// </summary>
        public static void ChangePos()
        {
            if (Model.PlayerPos[0] < 0)
            {
                Model.PlayerPos[0] = 0;
            }
            if (Model.PlayerPos[0] >= 99)
            {
                Model.PlayerPos[0] = 99;
            }
            if (Model.PlayerPos[1] < 0)
            {
                Model.PlayerPos[1] = 0;
            }
            if (Model.PlayerPos[1] >= 99)
            {
                Model.PlayerPos[1] = 99;
            }
        }
    }
}
