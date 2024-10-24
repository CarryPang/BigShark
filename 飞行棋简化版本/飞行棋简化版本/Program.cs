using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋简化版本
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new API().TitleName();
            #region 输入玩家姓名
            Console.WriteLine("请输入玩家A的姓名");
            Model.PlayerName[0] = Console.ReadLine();
            while (Model.PlayerName[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入");
                Model.PlayerName[0] = Console.ReadLine();
            }

            Console.WriteLine("请输入玩家B的姓名");
            Model.PlayerName[1] = Console.ReadLine();
            while (Model.PlayerName[1] == "" || Model.PlayerName[1] == Model.PlayerName[0])
            {
                if (Model.PlayerName[1] == "")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入");
                    Model.PlayerName[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的姓名不能和玩家A的相同，请重新输入");
                    Model.PlayerName[1] = Console.ReadLine();
                }
            }
            #endregion 
            //玩家姓名输入正确之后
            Console.Clear();//清屏功能
            new API().TitleName();
            Console.WriteLine("{0}玩家用A表示\n{1}玩家用B表示", Model.PlayerName[0], Model.PlayerName[1]);
            //在画地图之前首先要初始化地图
            new API().InitailMap();
            new API().DrawMap();
            //当玩家A跟玩家B没有一个人在终点的时候，两个玩家就不停的去玩游戏
            while (Model.PlayerPos[0] < 99 && Model.PlayerPos[1] < 99)
            {
                if (Model.Flag[0] == false)
                {
                    API.PlayGame(0);
                }
                else
                {
                    Model.Flag[0] = false;
                }
                if (Model.PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了", Model.PlayerName[0]);
                    break;
                }
                if (Model.Flag[1] == false)
                {
                    API.PlayGame(1);
                }
                else
                {
                    Model.Flag[1] = false;
                }
                if (Model.PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了", Model.PlayerName[1]);
                    break;
                }
            }//while
            Console.ReadKey();


        }
    }
}
