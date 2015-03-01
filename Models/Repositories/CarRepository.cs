using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }

}