using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VI_Test.Models
{

    /// <summary>
    /// Класс, реализующий ребро
    /// </summary>
    public class Edge
    {
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public float Weight { get; private set; }

        public Edge(Point first, Point second, float valueOfWeight)
        {
            FirstPoint = first;
            SecondPoint = second;
            Weight = valueOfWeight;
        }

    }

    /// <summary>
    /// Класс, реализующий вершину графа
    /// </summary>
    public class Point
    {
        public float ValueMetka { get; set; }
        public string Name { get;  set; }
        public bool IsChecked { get; set; }
        public Point predPoint { get; set; }
        public object SomeObj { get; set; }
        public Point(int value, bool ischecked)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            predPoint = new Point();
        }
        public Point(int value, bool ischecked, string name)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            Name = name;
            predPoint = new Point();
        }
        public Point()
        {
        }


    }

    class Dekstra
    {

        public Point[] points { get; private set; }
        public Edge[] edges { get; private set; }
        public Point BeginPoint { get; private set; }

        public Dekstra(Point[] points, Edge[] edges)
        {
            this.points = points;
            this.edges = edges;
        }

        /// <summary>
        /// Запуск алгоритма расчета
        /// </summary>
        /// <param name="idstart"></param>
        public void Run(int idstart)
        {
            Point input = Get(idstart);

            if (this.points.Count() > 0 && this.edges.Count() > 0)
            {
                BeginPoint = input;
                Step(input);

                foreach (Point point in points)
                {
                    Point NextPoint = GetUncheckedPoint();
                    if (NextPoint != null)
                    {
                        Step(NextPoint);
                    }
                    else
                    {
                        break;
                    }

                }
            }

        }
        /// <summary>
        /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
        /// </summary>
        /// <param name="beginpoint"></param>
        public void Step(Point beginpoint)
        {
            foreach (Point nextp in Pred(beginpoint))
            {
                if (nextp.IsChecked == false)//не отмечена
                {
                    float newmetka = beginpoint.ValueMetka + GetEdge(nextp, beginpoint).Weight;
                    if (nextp.ValueMetka > newmetka)
                    {
                        nextp.ValueMetka = newmetka;
                        nextp.predPoint = beginpoint;
                    }
                    else
                    {

                    }
                }
            }
            beginpoint.IsChecked = true;//вычеркиваем
        }
        /// <summary>
        /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
        /// </summary>
        /// <param name="currpoint"></param>
        /// <returns></returns>
        private IEnumerable<Point> Pred(Point currpoint)
        {
            IEnumerable<Point> firstpoints = from ff in edges where ff.FirstPoint == currpoint select ff.SecondPoint;
            IEnumerable<Point> secondpoints = from sp in edges where sp.SecondPoint == currpoint select sp.FirstPoint;
            IEnumerable<Point> totalpoints = firstpoints.Concat<Point>(secondpoints);
            return totalpoints;
        }
        /// <summary>
        /// Получаем ребро, соединяющее 2 входные точки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Edge GetEdge(Point a, Point b)
        {//ищем ребро по 2 точкам
            IEnumerable<Edge> myr = from reb in edges where (reb.FirstPoint == a & reb.SecondPoint == b) || (reb.SecondPoint == a & reb.FirstPoint == b) select reb;
            if (myr.Count() > 1 || myr.Count() == 0)
            {
                return null;
            }
            else
            {
                return myr.First();
            }
        }
        /// <summary>
        /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
        /// </summary>
        /// <returns></returns>
        private Point GetUncheckedPoint()
        {
            IEnumerable<Point> pointsuncheck = from p in points where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().ValueMetka;
                Point minPoint = pointsuncheck.First();
                foreach (Point p in pointsuncheck)
                {
                    if (p.ValueMetka < minVal)
                    {
                        minVal = p.ValueMetka;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// вспомогательный метод работы с массивом
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idstart"></param>
        /// <returns></returns>
        private  Point Get(int idstart)
        {
            foreach (Point i in this.points)
                if (i.ValueMetka == idstart)
                    return i;
            return null;
        }
      
        public List<Point> MinPath1(Point end)
        {
            List<Point> listOfpoints = new List<Point>();
            Point tempp = new Point();
            tempp = end;
            while (tempp != this.BeginPoint)
            {
                listOfpoints.Add(tempp);
                tempp = tempp.predPoint;
            }

            return listOfpoints;
        }
    }

    static class PrintGrath
    {
      
        public static List<string> PrintAllMinPaths(Dekstra da)
        {
            List<string> retListOfPointsAndPaths = new List<string>();
            foreach (Point p in da.points)
            {

                if (p != da.BeginPoint)
                {
                    string s = string.Empty;
                    foreach (Point p1 in da.MinPath1(p))
                    {
                        s += string.Format("{0} ", p1.Name);
                    }
                    retListOfPointsAndPaths.Add(string.Format("Point ={0},MinPath from {1} = {2}", p.Name, da.BeginPoint.Name, s));
                }

            }
            return retListOfPointsAndPaths;

        }
    }
}