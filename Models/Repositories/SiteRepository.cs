using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAuto.Models.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SiteRepository : GenericRepository<Site>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public SiteRepository(ISession session)  : base(session)
        {
        
        }

    }

}