using Newtonsoft.Json;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using TestAuto.Helper;
using TestAuto.Models;
using PagedList;

namespace TestAuto.Controllers
{

    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    [SessionState(SessionStateBehavior.Disabled)]
    public class CarAsynchController : AsyncController
    {
        /// <summary>
        /// 
        /// </summary>
        private ISession session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">injektált helper</param>
        public CarAsynchController(NHibernateHelper helper)
        {
            this.session = helper.SessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// 
        /// Aszikron kérés (autó lista) visszaadása
        /// 
        /// </summary>
        /// <param name="sortDictionary">sorbarandezéshez szükséges bemenetek</param>
        /// <param name="filterDictionary">szűréshez szükséges bemenetek</param>
        /// <param name="pageNumber">oldalszám</param>
        /// <returns></returns>
        public JsonResult List(Dictionary<string, string> sortDictionary, Dictionary<string,string> filterDictionary, int pageNumber)
        {

            List<object> objs = new List<object>();

            ICriteria ic = session.CreateCriteria<Car>("car");
            ic = ic.CreateAlias("Site", "s");

            // kapcsolt tábla azonosító eq
            List<string> connected_eq_ids = new List<string>(new string[] { "SiteId" });

            // melyiknél csináluk eq Restriction-t
            List<string> eq_ids = new List<string>(new string[] {
                "Year", "Productiondate", "Owners"});

            // és melyiknél like-ot, nyílván a string-eseknél
            List<string> like_ids = new List<string>( new string[] { "Type", "Manufacturer", "Condition"  } );

            // melyiket mivé konvertáljuk, ami nincs itt az marad string bemenet
            List<string> date_properties = new List<string>(new string[] { "Productiondate" });
            List<string> int_properties = new List<string>(new string[] { "Year", "Owners", "SiteId" });


            foreach (KeyValuePair<string, string> filter in filterDictionary)
            {
                ICriterion added_criteria = null;
                object filter_value = null;

                // dátum esetén megpróbálunk konvertálni
                if (date_properties.Contains( filter.Key) && filter.Value != "")
                {
                    DateTime filter_date = DateTime.MinValue;

                    DateTime.TryParse(filter.Value, out filter_date);

                    if (!filter_date.Equals(DateTime.MinValue))
                    {
                        filter_value = DateTime.Parse(filter.Value);
                    }
                }
                // int esetén konvertálunk
                else if (int_properties.Contains(filter.Key) && filter.Value != "")
                {
                    filter_value = Int32.Parse(filter.Value);
                }
                // string esetén tartalmazást keresünk
                else if (filter.Value != "")
                {
                    filter_value = "%" + filter.Value + "%";
                }

                // ha van értékünk kliensről
                if (filter_value != null)
                {
                    //  ha kapcsolód tábla id-t keresünk
                    if (connected_eq_ids.Contains(filter.Key))
                    {
                        // feltételezzük hogy a kapcsolódó Id neve és a tábla neve kapcsolatban van
                        // pl:   ic = ic.Add(Restrictions.Eq("car.Site.SiteId", 2));
                        added_criteria = Restrictions.Eq("car." + filter.Key.Replace("Id", "") + "." +  filter.Key, filter_value);
                    }
                    // ha csak sima int egyenlőséget
                    else if (eq_ids.Contains(filter.Key))
                    {
                        added_criteria = Restrictions.Eq(filter.Key, filter_value);
                    }
                    // ha szövegrészlet egyenlőséget
                    else if (like_ids.Contains(filter.Key))
                    {
                        added_criteria = Restrictions.Like(filter.Key, filter_value);
                    }
                }

                if (added_criteria != null)
                {
                    ic = ic.Add(added_criteria);
                }
            }


            foreach (KeyValuePair<string, string> sortkv in sortDictionary)
            {
                string propname = sortkv.Key.Replace("Sort", "");

                // telehely egyedi eset
                if (propname == "Site")
                {        
                    if (sortkv.Value.ToLower() == "asc")
                    {
                        ic = ic.AddOrder(Order.Asc("s.City")).AddOrder(Order.Asc("s.Address")).AddOrder(Order.Asc("s.Postcode"));
                    }
                    else if (sortkv.Value.ToLower() == "desc")
                    {
                        ic = ic.AddOrder(Order.Desc("s.City")).AddOrder(Order.Desc("s.Address")).AddOrder(Order.Desc("s.Postcode"));
                    }
                }
                else
                {
                    if (sortkv.Value.ToLower() == "asc")
                    {
                        ic = ic.AddOrder(Order.Asc(propname));
                    }
                    else if (sortkv.Value.ToLower() == "desc")
                    {
                        ic = ic.AddOrder(Order.Desc(propname));
                    }
                }
               
            }

            // autó lista konvertálása a kliens oldalra
            IList<Car> cars = ic.List<Car>();

            IPagedList<Car> cars_pagedlist =  cars.ToPagedList<Car>(pageNumber, CshtmlHelper.PAGESIZE);
       
            var cars_converted = from c in cars_pagedlist
                                 select new  {
                                    CardId = c.CarId,
                                    Manufacturer = c.Manufacturer,
                                    Type = c.Type,
                                    Year = c.Year.ToString(),
                                    Productiondate = c.Productiondate.ToShortDateString(),
                                    Condition = c.Condition,
                                    Owners = ( c.Owners.HasValue ? c.Owners.ToString() : ""),
                                    Site = c.Site.ToString()
                                 };

            string output = JsonConvert.SerializeObject(cars_converted);
            JsonResult jsonres = new JsonResult();
            jsonres.Data = new { cars = output, pagenumber = cars_pagedlist.PageNumber, pagecount = cars_pagedlist.PageCount };

            return jsonres;
        }


    }
}
