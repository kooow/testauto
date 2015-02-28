using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestAuto
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        public static ISessionFactory NHibernateSessionFactory;

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            base.Init();

            this.BeginRequest += MvcApplication_BeginRequest;
            this.EndRequest += MvcApplication_EndRequest;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(NHibernateSessionFactory);
            session.Dispose();
        }

    
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            NHibernateSessionFactory = Helper.NHibernateHelper.GetNHibernateSessionFactory();

        }
    }
}