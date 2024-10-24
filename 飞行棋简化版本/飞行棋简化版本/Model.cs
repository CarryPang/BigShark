using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋简化版本
{
    public class Model
    {
        //定义全局变量模拟全局变量，这个数组代表着地图的长度以及地图的坐标
        public static int[] Maps = new int[100];
        //声明一个静态数组用来存储玩家A和玩家B的坐标
        public static int[] PlayerPos = new int[2];
        //存储两个玩家的姓名 
        public static string[] PlayerName = new string[2];
        //存储两个bool值表示正确还是错误
        public static bool[] Flag = new bool[2];
    }
}
