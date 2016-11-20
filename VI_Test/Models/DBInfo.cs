using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VI_Test.Models;

namespace VI_Test.Models
{
    /// <summary>
    /// Класс, описывающий станцию
    /// </summary>
    public class Stantion
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// статичный класс-справочник для работы с БД
    /// </summary>
    public static class DBInfo
    {
        /// <summary>
        ///  список станций из БД
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Stantion> GetStation()
        {

            using (DBEntity db = new DBEntity())
            {
                var query = from b in db.Station
                            orderby b.name
                            select new Stantion() { id = b.id, name = b.name };


                return query.ToList();
            }

        }


        #region Deikstra
        /// <summary>
        /// получить массив вершин графа
        /// </summary>
        /// <returns></returns>
        public static Point[] GetPoint()
        {
            using (DBEntity db = new DBEntity())
            {
                var query = from b in db.Station
                            orderby b.name
                            select new Point() { ValueMetka = b.id, Name = "", IsChecked = false };

                return query.ToArray();
            }
            
        }

        /// <summary>
        /// получить массив ребер графа
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static VI_Test.Models.Rebro[] GetRebro(Point[] p)
        {
            List<Rebro> result = new List<Rebro>();
            List<Point> pp = p.ToList<Point>();

            using (DBEntity db = new DBEntity())
            {
                var query = from b in db.Station_Track
                            join a in db.Track on b.idtrack equals a.id

                            select new { start = b.idstation_start, stop = b.idstation_stop, weight = a.weight };

                foreach (var q in query)
                {
                    Point start = pp.Where(o => o.ValueMetka == q.start).FirstOrDefault();
                    Point stop = pp.Where(o => o.ValueMetka == q.stop).FirstOrDefault();
                    Rebro r = new Rebro(start, stop, q.weight);
                    result.Add(r);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// найти кратчайший путь между заданными точками
        /// </summary>
        /// <param name="idstart"></param>
        /// <param name="idstop"></param>
        /// <returns></returns>
        public static string GetShortPath(int idstart, int idstop)
        {
            string result = string.Empty;
            Point[] p = GetPoint();
            Rebro[] r = GetRebro(p);

            DekstraAlgorim alg = new DekstraAlgorim(p, r); // создание экземпляра для расчета

            try
            {
                alg.AlgoritmRun(idstart);           // вызов алгоритма расчета
                List<string> lr = PrintGrath.PrintAllMinPaths(alg);     // результат
            }
            catch
            {
                return "Путь не существует!";
            }

            return result;
        }

        

        #endregion Deikstra

    }
}