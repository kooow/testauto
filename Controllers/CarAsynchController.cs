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

namespace TestAuto.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [SessionState(SessionStateBehavior.Disabled)]
    public class CarAsynchController : AsyncController
    {

        /// <summary>
        /// 
        /// Aszikron autó lista visszaadása
        /// 
        /// </summary>
        /// <param name="sortDictionary">sorbarandezéshez szükséges bemenetek</param>
        /// <param name="filterDictionary">szűréshez szükséges bementek</param>
        /// <returns></returns>
        public JsonResult List(Dictionary<string, string> sortDictionary, Dictionary<string,string> filterDictionary)
        {

            List<object> objs = new List<object>();

            ISession session =  MvcApplication.NHibernateSessionFactory.GetCurrentSession();

            ICriteria ic = session.CreateCriteria<Car>();

            // melyiknél csináluk eq Restriction-t
            List<string> eq_ids = new List<string>( new string[] { // "SiteId",
                "Year", "Productiondate", "Owners"});

            // és melyiknél like-ot, nyílván a string-eseknél
            List<string> like_ids = new List<string>( new string[] { "Type", "Manufacturer", "Condition"  } );

            // melyiket mivé konvertáljuk, ami nincs itt az marad string
            List<string> date_properties = new List<string>(new string[] { "Productiondate" });
            List<string> int_properties = new List<string>(new string[] { "Year", "Owners" });

      

            foreach (KeyValuePair<string, string> filter in filterDictionary)
            {
                ICriterion added_criteria = null;
                object filter_value = null;

                if (date_properties.Contains( filter.Key) && filter.Value != "")
                {
                    DateTime filter_date = DateTime.MinValue;

                    DateTime.TryParse(filter.Value, out filter_date);

                    if (!filter_date.Equals(DateTime.MinValue))
                    {
                        filter_value = DateTime.Parse(filter.Value);
                    }
                }
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
                    if (eq_ids.Contains(filter.Key))
                    {
                        added_criteria = Restrictions.Eq(filter.Key, filter_value);
                    }
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

                if (propname == "Site") continue;

                if ( sortkv.Value.ToLower() == "asc")
                {
                    ic = ic.AddOrder(Order.Asc(propname));
                }
                else if (sortkv.Value.ToLower() == "desc")
                {
                    ic = ic.AddOrder(Order.Desc(propname));
                }



            }



            // autó lista konvertálás a kliens oldalra
            IList<Car> cars = ic.List<Car>();

            var cars_converted =   from c in cars
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
            jsonres.Data = output;

            return jsonres;
        }

    }
}
