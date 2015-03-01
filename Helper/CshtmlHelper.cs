using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TestAuto.Models;
using TestAuto.Models.Repositories;

namespace TestAuto.Helper
{
    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    public class CshtmlHelper
    {

        /// <summary>
        /// lapon szereplő elemek száma
        /// </summary>
        public static int PAGESIZE = 10;

        /// <summary>
        /// 
        /// Évek 1990-ig a beadott évtől
        /// 
        /// </summary>
        /// <param name="from">mennyitől</param>
        /// <param name="firstempty">első üres paraméter is kell</param>
        /// <param name="selected">kiválaszottt érték</param>
        /// <returns></returns>
        public static List<SelectListItem> GetYears(int from, bool firstempty, int? selected = null)
        {
            List<SelectListItem> years = new List<SelectListItem>();

            if (firstempty)
            {
                years.Add(new SelectListItem());
            }

            for (int year = from; year >= 1900; year--)
            {
                SelectListItem year_item = new SelectListItem { Value = year.ToString(), Text = year.ToString() };

                if (selected.HasValue && selected == year)
                {
                    year_item.Selected = true;
                }

                years.Add(year_item);
            }

            return years;
        }

    }
}