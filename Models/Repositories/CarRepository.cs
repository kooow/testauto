using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using NHibernate.Criterion;

namespace TestAuto.Models.Repositories
{

    /// <summary>
    /// Autó entitások kezelésére
    /// </summary>
    public class CarRepository : GenericRepository<Car>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public CarRepository(ISession session) : base(session)
        {

        }

        /// <summary>
        /// 
        /// Felülírjük a lapozható nézet megjelenítését
        /// hogy az aszinkron metódusban levő sorbarendezést használjuk
        /// hogy a kezdeti nézet megegyezzen
        /// </summary>
        /// <param name="page">lapszám</param>
        /// <param name="pagesize">lapméter, lapon levő elemek</param>
        /// <returns></returns>
        public override PagedList.IPagedList<Car> PagedAndOrderedList(int page, int pagesize)
        {    
            ICriteria ic = hibernateSession.CreateCriteria<Car>("car");
            ic = ic.CreateAlias("Site", "s");

            string[] properties = new string[] { "Type", "Manufacturer", "Year", "Productiondate", "Condition", "Owners" };

            ic = ic.AddOrder(Order.Asc("s.City")).AddOrder(Order.Asc("s.Address")).AddOrder(Order.Asc("s.Postcode"));

            foreach (string p in properties)
            {
                ic = ic.AddOrder(Order.Asc(p));
            }
    
           return ic.List<Car>().ToPagedList(page, pagesize);
        }

    }

}