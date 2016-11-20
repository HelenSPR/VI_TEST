using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VI_Test.Models;
namespace VI_Test.Controllers
{
    public class DataController : Controller
    {
        /// <summary>
        /// возвращает список станций в форматте json
        /// </summary>
        /// <returns></returns>
        public JsonResult Index()
        {
            return Json(DBInfo.GetStation(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// возвращает кратчайший путь между станцтями, если он найден
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public string GetShort(int id1, int id2)
        {
            return DBInfo.GetShortPath(id1, id2);
        }
    }
}
