using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
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
using TestAuto.Controllers;
using TestAuto.Helper;

namespace TestAuto
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// 
        /// </summary>
        public static IContainer container;

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
         
            NHibernateHelper helper;
            container.TryResolve(out helper);

            ISession session = helper.SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MvcApplication_EndRequest(object sender, EventArgs e)
        {
           NHibernateHelper helper;
           container.TryResolve(out helper);

           ISession session = helper.SessionFactory.GetCurrentSession();
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

            ContainerBuilder cBuilder = new ContainerBuilder();
            cBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            // NHibernateHelper osztály berakása az autofac konténerbe
            NHibernateHelper helper = new NHibernateHelper();
            cBuilder.RegisterInstance<NHibernateHelper>(helper).ExternallyOwned();
    
            MvcApplication.container = cBuilder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}